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
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Kopernicus
{
    namespace Configuration
    {
        [RequireConfigType(ConfigType.Node)]
        public class LightShifter : IParserEventSubscriber
        {
            public LightData data;

            // sunlightColor
            [ParserTarget("sunlightColor", optional = true, allowMerge = false)]
            public ColorParser sunlightColor
            {
                set { data.sunlightColor = value.value; }
            }

            // sunlightIntensity
            [ParserTarget("sunlightIntensity", optional = true, allowMerge = false)]
            public NumericParser<float> sunlightIntensity
            {
                set { data.sunlightIntensity = value.value; }
            }

            // sunlightShadowStrength
            [ParserTarget("sunlightShadowStrength", optional = true, allowMerge = false)]
            public NumericParser<float> sunlightShadowStrength
            {
                set { data.sunlightShadowStrength = value.value; }
            }

            // scaledSunlightColor
            [ParserTarget("scaledSunlightColor", optional = true, allowMerge = false)]
            public ColorParser scaledSunlightColor
            {
                set { data.scaledSunlightColor = value.value; }
            }

            // scaledSunlightIntensity
            [ParserTarget("scaledSunlightIntensity", optional = true, allowMerge = false)]
            public NumericParser<float> scaledSunlightIntensity
            {
                set { data.scaledSunlightIntensity = value.value; }
            }

            // IVASunColor
            [ParserTarget("IVASunColor", optional = true, allowMerge = false)]
            public ColorParser IVASunColor
            {
                set { data.IVASunColor = value.value; }
            }

            // IVASunIntensity
            [ParserTarget("IVASunIntensity", optional = true, allowMerge = false)]
            public NumericParser<float> IVASunIntensity
            {
                set { data.IVASunIntensity = value.value; }
            }

            // ambientLightColor
            [ParserTarget("ambientLightColor", optional = true, allowMerge = false)]
            public ColorParser ambientLightColor
            {
                set { data.ambientLightColor = value.value; }
            }

            // sunBrightnessCurve
            [ParserTarget("sunBrightnessCurve", optional = true)]
            private AnimationCurveParser sunBrightnessCurve
            {
                set { data.sunBrightnessCurve = value.curve; }
            }

            // Set the color that the star emits
            [ParserTarget("sunLensFlareColor", optional = true)]
            private ColorParser sunLensFlareColor
            {
                set { data.sunLensFlareColor = value.value; }
            }

            // givesOffLight
            [ParserTarget("givesOffLight", optional = true, allowMerge = false)]
            public NumericParser<bool> givesOffLight
            {
                set { data.givesOffLight = value.value; }
            }

            // sunAU
            [ParserTarget("sunAU", optional = true, allowMerge = false)]
            public NumericParser<double> sunAU
            {
                set { data.AU = value.value; }
            }

            // brightnessCurve
            [ParserTarget("brightnessCurve", optional = true, allowMerge = false)]
            public FloatCurveParser brightnessCurve
            {
                set { data.brightnessCurve = value.curve; }
            }

            public LightShifter()
            {
                data = LightData.Instantiate(LightData.lightPrefab) as LightData;
            }

            // Apply event
            void IParserEventSubscriber.Apply(ConfigNode node)
            {
            }

            // Post apply event
            void IParserEventSubscriber.PostApply(ConfigNode node)
            {
            }
        }
    }
}
