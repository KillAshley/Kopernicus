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
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Kopernicus
{
    /// <summary>
    /// A Delegate that requires nothing and returns nothing
    /// </summary>
    public delegate void KopernicusVoidDelegate();

    /// <summary>
    /// Enumeration of MonoBehavior functions
    /// </summary>
    public enum MonoFunction
    {
        Awake,
        Start,
        Update,
        FixedUpdate,
        LateUpdate,
        OnGUI,
        OnDestroy
    }

    /// <summary>
    /// Enumeration of Call-Places in a MonoBehaviour
    /// </summary>
    public enum MonoPlace
    {
        Start,
        Main,
        Stop
    }

    /// <summary>
    /// Handler for various Events
    /// </summary>
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class Kopernicus : MonoBehaviour
    {
        /// <summary>
        /// First position in Awake()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> AwakeStart { get; set; }

        /// <summary>
        /// Second position in Awake()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> AwakeMain { get; set; }

        /// <summary>
        /// Third position in Awake()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> AwakeStop { get; set; }

        /// <summary>
        /// Awake() is the first function that is called on a MonoBehaviour
        /// </summary>
        public virtual void Awake()
        {
            /// Call Start
            AwakeStart[HighLogic.LoadedScene]();

            /// Call Main
            AwakeMain[HighLogic.LoadedScene]();

            /// Call Stop
            AwakeStop[HighLogic.LoadedScene]();
        }

        /// <summary>
        /// First position in Start()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> StartStart { get; set; }

        /// <summary>
        /// Second position in Start()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> StartMain { get; set; }

        /// <summary>
        /// Third position in Start()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> StartStop { get; set; }

        /// <summary>
        /// Start() is the second function that is called on a MonoBehaviour
        /// </summary>
        public virtual void Start()
        {
            /// Call Start
            StartStart[HighLogic.LoadedScene]();

            /// Call Main
            StartMain[HighLogic.LoadedScene]();

            /// Call Stop
            StartStop[HighLogic.LoadedScene]();
        }

        /// <summary>
        /// First position in Update()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> UpdateStart { get; set; }

        /// <summary>
        /// Second position in Update()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> UpdateMain { get; set; }

        /// <summary>
        /// Third position in Update()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> UpdateStop { get; set; }

        /// <summary>
        /// Update() is called every frame on a MonoBehaviour
        /// </summary>
        public virtual void Update()
        {
            /// Call Start
            UpdateStart[HighLogic.LoadedScene]();

            /// Call Main
            UpdateMain[HighLogic.LoadedScene]();

            /// Call Stop
            UpdateStop[HighLogic.LoadedScene]();
        }

        /// <summary>
        /// First position in FixedUpdate()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> FixedUpdateStart { get; set; }

        /// <summary>
        /// Second position in FixedUpdate()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> FixedUpdateMain { get; set; }

        /// <summary>
        /// Third position in FixedUpdate()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> FixedUpdateStop { get; set; }

        /// <summary>
        /// FixedUpdate() is called every x seconds, where x is a constant value
        /// </summary>
        public virtual void FixedUpdate()
        {
            /// Call Start
            FixedUpdateStart[HighLogic.LoadedScene]();

            /// Call Main
            FixedUpdateMain[HighLogic.LoadedScene]();

            /// Call Stop
            FixedUpdateStop[HighLogic.LoadedScene]();
        }

        /// <summary>
        /// First position in LateUpdate()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> LateUpdateStart { get; set; }

        /// <summary>
        /// Second position in LateUpdate()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> LateUpdateMain { get; set; }

        /// <summary>
        /// Third position in LateUpdate()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> LateUpdateStop { get; set; }

        /// <summary>
        /// LateUpdate() is called every frame after Update() in a MonoBehaviour
        /// </summary>
        public virtual void LateUpdate()
        {
            /// Call Start
            LateUpdateStart[HighLogic.LoadedScene]();

            /// Call Main
            LateUpdateMain[HighLogic.LoadedScene]();

            /// Call Stop
            LateUpdateStop[HighLogic.LoadedScene]();
        }

        /// <summary>
        /// First position in OnGUI()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> OnGUIStart { get; set; }

        /// <summary>
        /// Second position in OnGUI()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> OnGUIMain { get; set; }

        /// <summary>
        /// Third position in OnGUI()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> OnGUIStop { get; set; }

        /// <summary>
        /// OnGUI() is Unitys GUI System
        /// </summary>
        public virtual void OnGUI()
        {
            /// Call Start
            OnGUIStart[HighLogic.LoadedScene]();

            /// Call Main
            OnGUIMain[HighLogic.LoadedScene]();

            /// Call Stop
            OnGUIStop[HighLogic.LoadedScene]();
        }

        /// <summary>
        /// First position in OnDestroy()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> OnDestroyStart { get; set; }

        /// <summary>
        /// Second position in OnDestroy()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> OnDestroyMain { get; set; }

        /// <summary>
        /// Third position in OnDestroy()
        /// </summary>
        private static Dictionary<GameScenes, KopernicusVoidDelegate> OnDestroyStop { get; set; }

        /// <summary>
        /// OnDestroy() is called, when a MonoBehaviour is destroyed
        /// </summary>
        public virtual void OnDestroy()
        {
            /// Call Start
            OnDestroyStart[HighLogic.LoadedScene]();

            /// Call Main
            OnDestroyMain[HighLogic.LoadedScene]();

            /// Call Stop
            OnDestroyStop[HighLogic.LoadedScene]();
        }

        /// <summary>
        /// Register a function for the Kopernicus handling
        /// </summary>
        public static void Register(KopernicusVoidDelegate kopernicusVoid, GameScenes scene, MonoFunction function, MonoPlace position)
        {
            string Function = Enum.GetName(typeof(MonoFunction), function);
            string Position = Enum.GetName(typeof(MonoPlace), position);
            PropertyInfo dlg = typeof(Kopernicus).GetProperty(Function + Position, BindingFlags.Static | BindingFlags.NonPublic);
            Dictionary<GameScenes, KopernicusVoidDelegate> dictionary = dlg.GetValue(null, null) as Dictionary<GameScenes, KopernicusVoidDelegate>;
            if (dictionary.ContainsKey(scene))
                dictionary[scene] += kopernicusVoid;
            else
                dictionary.Add(scene, kopernicusVoid);
            dlg.SetValue(null, dictionary, null);
        }

        /// <summary>
        /// Register a function for the Kopernicus handling
        /// </summary>
        public static void Register(KopernicusVoidDelegate kopernicusVoid, GameScenes[] scenes, MonoFunction function, MonoPlace position)
        {
            foreach (GameScenes scene in scenes)
                Register(kopernicusVoid, scene, function, position);
        }

        /// <summary>
        /// Register a function for the Kopernicus handling
        /// </summary>
        public static void Register(KopernicusVoidDelegate kopernicusVoid, GameScenes scene, MonoFunction[] functions, MonoPlace position)
        {
            foreach (MonoFunction function in functions)
                Register(kopernicusVoid, scene, function, position);
        }

        /// <summary>
        /// Register a function for the Kopernicus handling
        /// </summary>
        public static void Register(KopernicusVoidDelegate kopernicusVoid, GameScenes scene, MonoFunction function, MonoPlace[] positions)
        {
            foreach (MonoPlace position in positions)
                Register(kopernicusVoid, scene, function, position);
        }

        /// <summary>
        /// Register a function for the Kopernicus handling
        /// </summary>
        public static void Register(KopernicusVoidDelegate kopernicusVoid, GameScenes[] scenes, MonoFunction[] functions, MonoPlace position)
        {
            foreach (GameScenes scene in scenes)
                foreach (MonoFunction function in functions)
                    Register(kopernicusVoid, scene, function, position);
        }

        /// <summary>
        /// Register a function for the Kopernicus handling
        /// </summary>
        public static void Register(KopernicusVoidDelegate kopernicusVoid, GameScenes[] scenes, MonoFunction function, MonoPlace[] positions)
        {
            foreach (GameScenes scene in scenes)
                foreach (MonoPlace position in positions)
                    Register(kopernicusVoid, scene, function, position);
        }

        /// <summary>
        /// Register a function for the Kopernicus handling
        /// </summary>
        public static void Register(KopernicusVoidDelegate kopernicusVoid, GameScenes scene, MonoFunction[] functions, MonoPlace[] positions)
        {
            foreach (MonoPlace position in positions)
                foreach (MonoFunction function in functions)
                    Register(kopernicusVoid, scene, function, position);
        }
    }
}
