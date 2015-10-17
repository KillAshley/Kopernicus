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

using UnityEngine;

namespace Kopernicus
{
    /// <summary>
    /// Class for detailled Ocean-Support
    /// </summary>
    public class Ocean : PQS
    {
        /// <summary>
        /// Status for the Hazardous Ocean Module
        /// </summary>
        public virtual bool isHazardous
        {
            get { return this.HasComponentInChildren<HazardousOcean>(); }
        }

        /// <summary>
        /// The Hazardous Ocean controller for the Ocean.
        /// </summary>
        public virtual HazardousOcean hazadousOcean
        {
            get { return GetComponentInChildren<HazardousOcean>(); }
        }

        /// <summary>
        /// Make the Ocean Hazardous
        /// </summary>
        public virtual HazardousOcean MakeHazardousOcean(FloatCurve curve)
        {
            HazardousOcean ocean = this.AddComponent<HazardousOcean>();
            ocean.heatCurve = curve;
            return ocean;
        }

        /// <summary>
        /// Make the Ocean Hazardous
        /// </summary>
        public virtual HazardousOcean MakeHazardousOcean(ConfigNode node)
        {
            HazardousOcean ocean = this.AddComponent<HazardousOcean>();
            ocean.heatCurve = new FloatCurve();
            ocean.heatCurve.Load(node);
            return ocean;
        }

        /// <summary>
        /// Make the Ocean Hazardous
        /// </summary>
        public virtual HazardousOcean MakeHazardousOcean(Keyframe[] frames)
        {
            HazardousOcean ocean = this.AddComponent<HazardousOcean>();
            ocean.heatCurve = new FloatCurve(frames);
            return ocean;
        }
    }
}
