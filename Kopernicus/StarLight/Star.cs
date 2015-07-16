/**
 * Kopernicus Planetary System Modifier
 * ====================================
 * Created by: - Bryce C Schroeder (bryce.schroeder@gmail.com)
 * 			   - Nathaniel R. Lewis (linux.robotdude@gmail.com)
 * 
 * Maintained by: - Thomas P.
 * 				  - NathanKell
 * 
* Additional Content by: Gravitasi, aftokino, KCreator, Padishar, Kragrathea, OvenProofMars, zengei, MrHappyFace
 * ------------------------------------------------------------- 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301  USA
 * 
 * This library is intended to be used as a plugin for Kerbal Space Program
 * which is copyright 2011-2014 Squad. Your usage of Kerbal Space Program
 * itself is governed by the terms of its EULA, not the license above.
 * 
 * https://kerbalspaceprogram.com
 */

using System.Reflection;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Kopernicus
{
    // Class to support multiple Lensflares
    public class Star : Sun
    {
        // Name of the CelestialBody
        public string bodyName = "";

        // Static backup of Sun.Instance to use it as a prefab
        private static Sun sunInstance;

        // Light Data for the new Sun
        public LightData lightData;

        // The actual Light for the new Sun
        public Light sunLight;

        // A list of all Stars
        public static List<Star> Stars { get; private set; }

        // Instance
        public static Star Instance { get; set; }

        public void Awake()
        {
            // Create the Stars-List
            if (Stars == null)
            {
                Stars = new List<Star>();
            }

            // If we "are" Sun.Instance, overwrite it with us
            if (Sun.Instance != null)
            {
                if (Sun.Instance.sun.name == bodyName)
                {
                    // Back up Sun.Instance for cloning purposes
                    sunInstance = Sun.Instance;

                    // Get it's data
                    Utility.CopyObjectFields<Sun>(sunInstance, this, false);

                    // Overwrite Sun.Instance
                    Sun.Instance = this;

                    // Set our own Instance-member
                    Star.Instance = this;

                    // Add the light to fix PSystemSetup
                    sunLight = gameObject.AddComponent<Light>();
                    sunLight.enabled = false;
                }
                else
                {
                    // Copy the LensFlare
                    LensFlare lensFlare = Instantiate(sunInstance.sunFlare) as LensFlare;
                    DontDestroyOnLoad(lensFlare);

                    // Restore Sun.Instance and destroy useless stuff
                    Sun.Instance = Star.Instance;
                    DestroyImmediate(lensFlare.GetComponent<Sun>());
                    DestroyImmediate(lensFlare.GetComponent<Light>());

                    // Set the Lensflare
                    sunFlare = lensFlare;
                    sunFlare.color = Color.green;
                        
                    // Copy the additional data from the prefab
                    target = sunInstance.target;
                    AU = sunInstance.AU;
                    brightnessCurve = sunInstance.brightnessCurve;
                    sunDirection = sunInstance.sunDirection;
                    fadeStart = sunInstance.fadeStart;
                    fadeEnd = sunInstance.fadeEnd;
                }
            }

            // Deactivate localSpaceLight for the moment, so that we can get LensFlares running
            useLocalSpaceSunLight = false;

            // Add us to the Stars-List
            Stars.Add(this);

            // Get the LightData from the GameObject hirarchy
            lightData = gameObject.GetComponentInChildren<LightData>();

            // Set the first values
            sunFlare.color = lightData.sunLensFlareColor;
            brightnessCurve = lightData.brightnessCurve.Curve;
            AU = lightData.AU;

            // Activate the lights
            // ApplyLight(lightData, light);
        }

        public void Start()
        {
            // Reparent the LensFlare to the new Star
            sunFlare.transform.NestToParent(transform);
        }

        public void ApplyLight(LightData data, Light light)
        {
            light.color = lightData.scaledSunlightColor;
            light.intensity = lightData.scaledSunlightIntensity;
        }

        public void Update()
        {
            // If the body-list works, get the CelestialBody reference
            if (PSystemManager.Instance.localBodies != null && sun == null)
            {
                sun = PSystemManager.Instance.localBodies.Find(b => b.name == bodyName);
            }
        }

        public void SunlightEnabled(bool state)
        {
            // If we are Sun.Instance, modify everything
            if (Sun.Instance == this)
            {
                foreach (Star s in Stars)
                {
                    // Set the Star to the new state
                    s.SetLight(state);
                }
            }
            else
            {
                // Set us to the new state
                SetLight(state);
            }
        }

        public void SunlightEnabled(bool state, bool instanceOnly)
        {
            // If we should only deactivate Sun.Instance, do it
            if (instanceOnly)
            {
                (Sun.Instance as Star).SetLight(state);
            }
            else
            {
                // Do the normal stuff
                SunlightEnabled(state);
            }
        }

        // Set the light to a new State
        private void SetLight(bool state)
        {
            // Disable the Sunlight
            if (light != null)
            {
                light.enabled = state;
            }

            // Disable the Lensflare
            if (sunFlare != null)
            {
                sunFlare.enabled = state;
            }
        }
    }
}
