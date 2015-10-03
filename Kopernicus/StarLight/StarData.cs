

using System;
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
using UnityEngine;

namespace Kopernicus
{
    // Class to store informations for stars
    [Serializable]
    public class StarData
    {
        // Sunlight
        public Color sunlightColor;
        public float sunlightIntensity;
        public float sunlightShadowStrength;

        // Scaled Sunlight
        public Color scaledSunlightColor;
        public float scaledSunlightIntensity;

        // IVA Sun
        public Color IVASunColor;
        public float IVASunIntensity;

        // Ambient Light
        public Color ambientLightColor;

        // LensFlare
        public Color sunLensFlareColor;
        public bool givesOffLight;
        public double AU;
        public FloatCurve brightnessCurve;

        // PowerCurve
        public FloatCurve powerCurve;

        // Return the stock values, to prevent exceptions
        public StarData()
        {
            // Fill it with default values
            sunlightColor = Color.white;
            sunlightIntensity = 0.45f;
            sunlightShadowStrength = 0.7523364f;
            scaledSunlightColor = Color.white;
            scaledSunlightIntensity = 0.45f;
            IVASunColor = new Color(1.0f, 0.977f, 0.896f, 1.0f);
            IVASunIntensity = 0.34f;
            sunLensFlareColor = Color.white;
            ambientLightColor = new Color(0.06f, 0.06f, 0.06f, 1.0f);
            AU = 13599840256;
            brightnessCurve = new FloatCurve(new Keyframe[]
            {
                new Keyframe(-0.01573471f, 0.217353f, 1.706627f, 1.706627f),
                new Keyframe(5.084181f, 3.997075f, -0.001802375f, -0.001802375f),
                new Keyframe(38.56295f, 1.82142f, 0.0001713f, 0.0001713f)
            });
            powerCurve = null;
        }
        
        /*
        private void SetActive(GameScenes scene)
        {
            GameObject sunLight = GameObject.Find("SunLight");
            GameObject scaledSunLight = GameObject.Find("Scaledspace SunLight");

            if (sunLight && scaledSunLight)
            {
                if (sunlightColor != null)
                    sunLight.light.color = sunlightColor;

                if (sunlightIntensity != float.NaN)
                    sunLight.light.intensity = sunlightIntensity;

                if (sunlightShadowStrength != float.NaN)
                    sunLight.light.shadowStrength = sunlightShadowStrength;

                if (scaledSunlightColor != null)
                    scaledSunLight.light.color = scaledSunlightColor;

                if (scaledSunlightIntensity != float.NaN)
                    scaledSunLight.light.intensity = scaledSunlightIntensity;

                if (scene == GameScenes.FLIGHT)
                {
                    GameObject IVASun = GameObject.Find("IVASun");

                    if (IVASun)
                    {
                        if (IVASunColor != null)
                            IVASun.light.color = IVASunColor;

                        if (IVASunIntensity != float.NaN)
                            IVASun.light.intensity = IVASunIntensity;
                    }
                }

                DynamicAmbientLight ambientLight = FindObjectOfType(typeof(DynamicAmbientLight)) as DynamicAmbientLight;

                if (ambientLightColor != null && ambientLight)
                    ambientLight.vacuumAmbientColor = ambientLightColor;

            }
        }
        */
    }
}

