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

using Kopernicus.Components;
using System;
using System.Reflection;
using UnityEngine;

namespace Kopernicus
{
    /// <summary>
    /// Extensions for the MonoBehaviour Object (i.e. every code that is executable on it's own)
    /// </summary>
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// Checks if the MonoBehavior / it's GameObject has a component T attached
        /// </summary>
        public static bool HasComponent<T>(this MonoBehaviour mono) where T : Component
        {
            if (mono.GetComponents<T>().Length == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Checks if the MonoBehavior / it's GameObject has a component T attached
        /// </summary>
        public static bool HasComponent(this MonoBehaviour mono, Type type)
        {
            if (mono.GetComponents(type).Length == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Checks if the MonoBehavior / it's GameObject has a component T attached
        /// </summary>
        public static bool HasComponentInChildren<T>(this MonoBehaviour mono) where T : Component
        {
            if (mono.GetComponentsInChildren<T>().Length == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Checks if the MonoBehavior / it's GameObject has a component T attached
        /// </summary>
        public static bool HasComponentInChildren<T>(this MonoBehaviour mono, bool includeInactive) where T : Component
        {
            if (mono.GetComponentsInChildren<T>(includeInactive).Length == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Checks if the MonoBehavior / it's GameObject has a component T attached
        /// </summary>
        public static bool HasComponentInChildren(this MonoBehaviour mono, Type type)
        {
            if (mono.GetComponentsInChildren(type).Length == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Checks if the MonoBehavior / it's GameObject has a component T attached
        /// </summary>
        public static bool HasComponentInChildren(this MonoBehaviour mono, Type type, bool includeInactive)
        {
            if (mono.GetComponentsInChildren(type, includeInactive).Length == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Adds a new component with type T to the MonoBehaviour / it's GameObject
        /// </summary>
        public static T AddComponent<T>(this MonoBehaviour mono) where T : Component
        {
            return mono.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// Adds a new component with type T to the MonoBehaviour / it's GameObject
        /// </summary>
        public static Component AddComponent(this MonoBehaviour mono, Type type)
        {
            return mono.gameObject.AddComponent(type);
        }

        /// <summary>
        /// Adds a new component with type T to the MonoBehaviour / it's GameObject
        /// </summary>
        public static Component AddComponent(this MonoBehaviour mono, string className)
        {
            return mono.gameObject.AddComponent(className);
        }

        /// <summary>
        /// Changes the Type of a MonoBehaviour
        /// </summary>
        public static T ChangeType<T>(this T mono) where T : MonoBehaviour
        {
            T changed = mono.AddComponent<T>();
            foreach (FieldInfo info in mono.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                typeof(T).GetField(info.Name).SetValue(changed, info.GetValue(mono));
            }
            foreach (PropertyInfo info in mono.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                typeof(T).GetProperty(info.Name).SetValue(changed, info.GetValue(mono, null), null);
            }
            UnityEngine.Object.Destroy(mono);
            return changed;
        }
    }
}
