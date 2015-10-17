/**
 * Kopernicus Planetary System Modifier
 * ====================================
 * Created by: BryceSchroeder and Teknoman117 (aka. Nathaniel R. Lewis)
 * Maintained by: Thomas P., NathanKell and KillAshley
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
 * which is copyright 2011-2015 Squad. Your usage of Kerbal Space Program
 * itself is governed by the terms of its EULA, not the license above.
 * 
 * https://kerbalspaceprogram.com
 */

using System.Collections.Generic;
using UnityEngine;

namespace Kopernicus
{
    /// <summary>
    /// Component to make oceans hot and dangerous
    /// </summary>
    public class HazardousOcean : MonoBehaviour
    {
        /// <summary>
        /// Heat-at-altitude Curve
        /// </summary>
        public virtual FloatCurve heatCurve { get; set; }

        /// <summary>
        /// The Ocean PQS Sphere
        /// </summary>
        protected virtual PQS ocean { get; set; }

        /// <summary>
        /// Update() runs every frame, so we use it to update our heat
        /// </summary>
        protected virtual void Update()
        {
            if (!FlightGlobals.ready)
                return;

            CelestialBody body = Part.GetComponentUpwards<CelestialBody>(ocean.gameObject);
            List<Vessel> vessels = FlightGlobals.Vessels.FindAll(v => v.mainBody == body);
            foreach (Vessel vessel in vessels)
            {
                double distanceToPlanet = Mathf.Abs(Vector3.Distance(vessel.transform.position, ocean.transformPosition)) - ocean.GetSurfaceHeight(vessel.transform.position);
                double heatingRate = heatCurve.Evaluate((float)distanceToPlanet);
                vessel.Parts.ForEach(p => p.temperature += heatingRate);
            }
        }
    }
}
