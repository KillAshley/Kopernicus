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

using System.Collections.Generic;
using UnityEngine;
using Kopernicus.Configuration;
using System;

namespace Kopernicus
{
    // Mod runtime utilities
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class RuntimeUtility : MonoBehaviour
    {
        // Utility types
        public static readonly Dictionary<GameScenes, Type[]> types = new Dictionary<GameScenes, Type[]>()
        {
            { GameScenes.MAINMENU, new Type[]
                {
                    typeof(HiddenObjectUtils),
                    typeof(MainMenuChanger),
                    typeof(NameChangeRunner),
                    typeof(OceanUtility),
                    typeof(StarLightSwitcher),
                    typeof(MapExport)
                }
            },
            { GameScenes.SPACECENTER, new Type[]
                {
                    typeof(SpaceCenterFixer),
                    typeof(AtmosphereFixer)
                }
            },
            { GameScenes.TRACKSTATION, new Type[]
                {
                    typeof(MapViewFixer)
                }
            },
            { GameScenes.FLIGHT, new Type[]
                {
                    typeof(MapViewFixer),
                    typeof(AtmosphereFixer)
                }
            },
            { GameScenes.EDITOR, new Type[]
                {
                    typeof(SpaceCenterFixer)
                }
            }
        };
        
        // Awake() - flag this class as don't destroy on load
        public void Awake()
        {
            // Make sure the runtime utility isn't killed
            DontDestroyOnLoad (this);

            // Register a handler for Scene Switches
            GameEvents.onLevelWasLoaded.Add(onLevelWasLoaded);

            // We need to start the MainMenu Objects
            onLevelWasLoaded(GameScenes.MAINMENU);
            
            // Log
            Logger.Default.Log ("[Kopernicus]: RuntimeUtility Started");
            Logger.Default.Flush ();
        }

        // Start the RuntimeUtilities
        private void onLevelWasLoaded(GameScenes scene)
        {
            // If no array is there
            if (types[scene] == null)
                return;

            // Go through the types
            foreach (Type utility in types[scene])
            {
                // Spawn them
                new GameObject(utility.Name).AddComponent(utility);
            }
        }
    }
}

