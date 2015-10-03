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

using System.Linq;
using UnityEngine;

namespace Kopernicus
{
    // Replacement class for SolarPanels
    public class KopernicusSolarPanel : ModuleDeployableSolarPanel
    {
        // Current star
        public CelestialBody star;

        // We need to manipulate the sun transform of the solar panels
        public override void OnStart(StartState state)
        {
            // Backup home
            CelestialBody sun = Planetarium.fetch.Sun;
            Planetarium.fetch.Sun = star == null ? sun : star;

            // Get the powerCurve, if it exists
            Star component = Planetarium.fetch.Sun.GetComponentInChildren<Star>();
            if (component != null)
            {
                // If we have data
                if (component.data != null)
                {
                    // If there's a powerCurve
                    if (component.data.powerCurve != null)
                    {
                        useCurve = true;
                        powerCurve = component.data.powerCurve;
                    }
                    else
                    {
                        useCurve = false;
                        powerCurve = null;
                    }
                }
            }

            // Call base
            base.OnStart(state);

            // Reset home
            Planetarium.fetch.Sun = sun;
        }

        // Update - get the current star
        public override void OnUpdate()
        {
            // Call base
            base.OnUpdate();

            // Get the current position of the active vessel
            if (FlightGlobals.ActiveVessel != null)
            {
                Vector3 position = FlightGlobals.ActiveVessel.GetTransform().position;
                CelestialBody newStar = Star.Stars.OrderBy(star => FlightGlobals.getAltitudeAtPos(position, star.sun)).First().sun;

                // If we didn't found anything
                if (newStar == null)
                    return;

                // Patch
                if (newStar != star)
                {
                    star = newStar;
                    OnStart(StartState.Orbital);
                }
            }
        }
    }
}
