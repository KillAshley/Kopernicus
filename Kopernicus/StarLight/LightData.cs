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
    // Class to store Lightning informations for stars
    public class LightData : MonoBehaviour
    {
        public Color sunlightColor;
        public float sunlightIntensity;
        public float sunlightShadowStrength;
        public Color scaledSunlightColor;
        public float scaledSunlightIntensity;
        public Color IVASunColor;
        public float IVASunIntensity;
        public Color ambientLightColor;
        public AnimationCurve sunBrightnessCurve;
        public Color sunLensFlareColor;
        public bool givesOffLight = true;
        public double AU;
        public FloatCurve brightnessCurve;

        private static LightData prefab;

        // Return the stock values as a prefab object, to prevent exceptions
        public static LightData lightPrefab
        {
            get
            {
                if (prefab == null)
                {
                    // If the prefab is null, create it
                    GameObject prefabGOB = new GameObject("LightData");
                    prefabGOB.transform.parent = Utility.Deactivator;
                    prefab = prefabGOB.AddComponent<LightData>();

                    // Fill it with default values
                    prefab.sunlightColor = Color.white;
                    prefab.sunlightIntensity = 0.45f;
                    prefab.sunlightShadowStrength = 0.7523364f;
                    prefab.scaledSunlightColor = Color.white;
                    prefab.scaledSunlightIntensity = 0.45f;
                    prefab.IVASunColor = new Color(1.0f, 0.977f, 0.896f, 1.0f);
                    prefab.IVASunIntensity = 0.34f;
                    prefab.sunLensFlareColor = Color.white;
                    prefab.ambientLightColor = new Color(0.06f, 0.06f, 0.06f, 1.0f);
                    prefab.AU = 13599840256;
                    prefab.brightnessCurve = new FloatCurve(new Keyframe[] 
                        { 
                            new Keyframe(-0.01573471f, 0.217353f, 1.706627f, 1.706627f),
                            new Keyframe(5.084181f, 3.997075f, -0.001802375f, -0.001802375f),
                            new Keyframe(38.56295f, 1.82142f, 0.0001713f, 0.0001713f)
                        });
                }

                // Return the prefab
                return prefab;
            }
        }

        #region legacy
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
        #endregion
    }
}

