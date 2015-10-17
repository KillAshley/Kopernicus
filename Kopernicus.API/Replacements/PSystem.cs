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

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Kopernicus
{
    /// <summary>
    /// Extended Planetary System Storage
    /// </summary>
    public class PSystem : global::PSystem, IEnumerable<PSystemBody>
    {
        /// <summary>
        /// Find a specific body by it's name in the PSystem
        /// </summary>
        public virtual PSystemBody Find(string name)
        {
            foreach (PSystemBody body in this)
            {
                if (body.name == name)
                    return body;
            }
            return null;
        }

        /// <summary>
        /// Returns the body with the given FlightGlobalsIndex
        /// </summary>
        public virtual PSystemBody this[int index]
        {
            get
            {
                IEnumerable<PSystemBody> bodies = GetComponentsInChildren<PSystemBody>(true).Where(b => b.flightGlobalsIndex == index);
                if (bodies.Count() != 0) return bodies.First();
                else return null;
            }
        }

        /// <summary>
        /// Returns the body with the given name
        /// </summary>
        public virtual PSystemBody this[string name]
        {
            get
            {
                IEnumerable<PSystemBody> bodies = GetComponentsInChildren<PSystemBody>(true).Where(b => b.celestialBody.bodyName == name);
                if (bodies.Count() != 0) return bodies.First();
                else return null;
            }
        } 

        /// <summary>
        /// Returns an Enumerator, that enumerates over all bodies in the PSystem
        /// </summary>
        public virtual IEnumerator<PSystemBody> GetEnumerator()
        {
            return new Enumerator(rootBody);
        }

        /// <summary>
        /// Returns an Enumerator, that enumerates over all bodies in the PSystem
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(rootBody);
        }

        /// <summary>
        /// Enumerator to enumerate over all bodies in a PSystem
        /// </summary>
        public class Enumerator : IEnumerator<PSystemBody>
        {
            public PSystemBody original;
            private int currentFGI;

            public Enumerator(PSystemBody initial)
            {
                original = initial;
                currentFGI = original.flightGlobalsIndex;
            }

            public bool MoveNext()
            {
                currentFGI++;
                return original.GetComponentInParent<PSystem>()[currentFGI] != null;
            }

            object IEnumerator.Current
            {
                get { return original.GetComponentInParent<PSystem>()[currentFGI]; }
            }

            public PSystemBody Current
            {
                get { return Current; }
            }

            public void Dispose() { }

            public void Reset()
            {
                currentFGI = original.flightGlobalsIndex;
            }
        }
    }
}
