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

using Kopernicus.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Kopernicus
{
    // Class to support multiple Lensflares
    public class Star : Sun
    {
        // Name of the CelestialBody
        public string bodyName = "";

        // Static backup of Sun.Instance to use it as a prefab
        private static Sun sunInstance;

        public void Awake()
        {
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

                    // Add the light to fix PSystemSetup
                    gameObject.AddComponent<Light>();
                }
                else
                {
                    // Copy the Lensflare
                    sunFlare = gameObject.AddComponent<LensFlare>();
                    Utility.CopyObject<LensFlare>(sunInstance.sunFlare, sunFlare, false);

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
