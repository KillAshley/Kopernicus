/**
 * Kopernicus Planetary System Modifier
 * Copyright (C) 2014 Bryce C Schroeder (bryce.schroeder@gmail.com), Nathaniel R. Lewis (linux.robotdude@gmail.com)
 * 
 * http://www.ferazelhosting.net/~bryce/contact.html
 * 
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

using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Kopernicus
{
	namespace Configuration
	{
		[RequireConfigType(ConfigType.Node)]
		public class Atmosphere : IParserEventSubscriber
		{
			// Resoruces that will be edited
			private GameObject scaledVersion;
			private CelestialBody celestialBody;

			// Do we have an atmosphere?
			[PreApply]
			[ParserTarget("enabled", optional = true)]
			private NumericParser<bool> enabled 
			{
				set { celestialBody.atmosphere = value.value; }
			}

			// Does this atmosphere contain oxygen
			[ParserTarget("oxygen", optional = true)]
			private NumericParser<bool> oxygen 
			{
				set { celestialBody.atmosphereContainsOxygen = value.value; }
			}
			
			// Temperature curve (see below)
			[ParserTarget("temperatureCurve", optional = true)]
			private FloatCurveParser temperatureCurve 
			{
                set
                {
                    celestialBody.atmosphereTemperatureCurve = value.curve;
                    celestialBody.atmosphereUseTemperatureCurve = true;
                }
			}

            // Density at sea level
            [ParserTarget("staticDensityASL", optional = true)]
            private NumericParser<double> atmDensityASL
            {
                set { celestialBody.atmDensityASL = value.value; }
            }

            // atmosphereGasMassLapseRate
            [ParserTarget("gasMassLapseRate", optional = true)]
            private NumericParser<double> atmosphereGasMassLapseRate
            {
                set { celestialBody.atmosphereGasMassLapseRate = value.value; }
            }

            // atmosphereMolarMass
            [ParserTarget("atmosphereMolarMass", optional = true)]
            private NumericParser<double> atmosphereMolarMass
            {
                set { celestialBody.atmosphereMolarMass = value.value; }
            }

            // atmospherePressureCurveIsNormalized
            [ParserTarget("pressureCurveIsNormalized", optional = true)]
            private NumericParser<bool> atmospherePressureCurveIsNormalized
            {
                set { celestialBody.atmospherePressureCurveIsNormalized = value.value; }
            }

            // atmosphereTemperatureCurveIsNormalized
            [ParserTarget("temperatureCurveIsNormalized", optional = true)]
            private NumericParser<bool> atmosphereTemperatureCurveIsNormalized
            {
                set { celestialBody.atmosphereTemperatureCurveIsNormalized = value.value; }
            }

            // atmosphereTemperatureLapseRate
            [ParserTarget("temperatureLapseRate", optional = true)]
            private NumericParser<double> atmosphereTemperatureLapseRate
            {
                set { celestialBody.atmosphereTemperatureLapseRate = value.value; }
            }

            // atmosphereTemperatureSunMultCurve
            [ParserTarget("temperatureSunMultCurve", optional = true)]
            private FloatCurveParser atmosphereTemperatureSunMultCurve
            {
                set { celestialBody.atmosphereTemperatureSunMultCurve = value.curve; }
            }

			// Static pressure at sea level (all worlds are set to 1.0f?)
			[ParserTarget("staticPressureASL", optional = true)]
			private NumericParser<float> staticPressureASL 
			{
				set { celestialBody.atmospherePressureSeaLevel = value.value; }
			}

			// Pressure curve (pressure = pressure multipler * pressureCurve[altitude])
			[ParserTarget("pressureCurve", optional = true)]
			private FloatCurveParser pressureCurve 
			{
                set
                {
                    celestialBody.atmospherePressureCurve = value.curve;
                    celestialBody.atmosphereUsePressureCurve = true;
                }
			}
			
			// atmosphere cutoff altitude
			[ParserTarget("altitude", optional = true)]
			private NumericParser<float> maxAltitude 
			{
				set { celestialBody.atmosphereDepth = value.value; }
			}
			
			// ambient atmosphere color
			[ParserTarget("ambientColor", optional = true)]
			private ColorParser ambientColor 
			{
				set { celestialBody.atmosphericAmbientColor = value.value; }
			}

			// light color
			[ParserTarget("lightColor", optional = true)]
			private ColorParser lightColor 
			{
				set { scaledVersion.GetComponentsInChildren<AtmosphereFromGround> (true) [0].waveLength = value.value; }
			}

			// Parser apply event
			void IParserEventSubscriber.Apply (ConfigNode node)
			{ 
				// If we don't want an atmosphere, ignore this step
				if(!celestialBody.atmosphere)
					return;

				// If we don't already have an atmospheric shell generated
				if (scaledVersion.GetComponentsInChildren<AtmosphereFromGround> (true).Length == 0) 
				{
					// Add the material light direction behavior
					MaterialSetDirection materialLightDirection = scaledVersion.AddComponent<MaterialSetDirection>();
					materialLightDirection.valueName            = "_localLightDirection";

					// Create the atmosphere shell game object
					GameObject scaledAtmosphere       = new GameObject("atmosphere");
					scaledAtmosphere.transform.parent = scaledVersion.transform;
					scaledAtmosphere.layer            = Constants.GameLayers.ScaledSpaceAtmosphere;
					MeshRenderer renderer             = scaledAtmosphere.AddComponent<MeshRenderer>();
					renderer.material                 = new Kopernicus.MaterialWrapper.AtmosphereFromGround();
					MeshFilter meshFilter             = scaledAtmosphere.AddComponent<MeshFilter>();
					meshFilter.sharedMesh             = Utility.ReferenceGeosphere ();
					scaledAtmosphere.AddComponent<AtmosphereFromGround>();

					// Setup known defaults
					celestialBody.atmospherePressureSeaLevel = 1.0f;
					// celestialBody.atmosphereMultiplier = 1.4285f;
				}
			}

			// Parser post apply event
			void IParserEventSubscriber.PostApply (ConfigNode node) { } 

			// Store the scaled version and celestial body we are modifying internally
			public Atmosphere (CelestialBody celestialBody, GameObject scaledVersion)
			{
				this.scaledVersion = scaledVersion;
				this.celestialBody = celestialBody;
			}
		}
	}
}
