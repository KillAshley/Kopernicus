﻿/**
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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Kopernicus
{
    /// <summary>
    /// The representation of a body in the PSystem - prefab
    /// </summary>
    public class PSystemBody : global::PSystemBody
    {
        /// <summary>
        /// Converts a body into it's flightGlobals Index
        /// </summary>
        public static implicit operator int(PSystemBody body)
        {
            return body.flightGlobalsIndex;
        }

        /// <summary>
        /// Converts a body into it's flightGlobals Index
        /// </summary>
        public static implicit operator string(PSystemBody body)
        {
            return body.celestialBody.bodyName;
        }

        /// <summary>
        /// Converts a flightGlobalsIndex into the PSystemBody-Representation
        /// </summary>
        public static implicit operator PSystemBody(int fgi)
        {
            IEnumerable<PSystemBody> bodies = UnityEngine.Resources.FindObjectsOfTypeAll<PSystemBody>().Where(b => b.flightGlobalsIndex == fgi);
            return bodies.Count() != 0 ? bodies.First() : null;
        }

        /// <summary>
        /// Converts a name into the PSystemBody-Representation
        /// </summary>
        public static implicit operator PSystemBody(string name)
        {
            IEnumerable<PSystemBody> bodies = UnityEngine.Resources.FindObjectsOfTypeAll<PSystemBody>().Where(b => b.celestialBody.bodyName == name);
            return bodies.Count() != 0 ? bodies.First() : null;
        }
    }
}