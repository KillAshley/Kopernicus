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
*
* ResX sucks....
*/

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Kopernicus.Properties
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    public class Resources
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() { }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null)) resourceMan = new ResourceManager("Kopernicus.Properties.Resources", typeof(Resources).Assembly);
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        public static string DiffuseNew
        {
            get { return ResourceManager.GetString("DiffuseNew", resourceCulture); }
        }

        public static byte[] ParticleCollider
        {
            get { return (byte[])ResourceManager.GetObject("ParticleCollider", resourceCulture); }
        }

        public static string UnlitNew
        {
            get { return ResourceManager.GetString("UnlitNew", resourceCulture); }
        }
    }
}