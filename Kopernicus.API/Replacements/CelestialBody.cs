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
using Kopernicus.MaterialWrapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Kopernicus
{
    /// <summary>
    /// Replacement for CelestialBody
    /// </summary>
    public class CelestialBody : global::CelestialBody
    {
        /// <summary>
        /// Main PQS Controller
        /// </summary>
        public virtual new PQS pqsController { get; set; }

        /// <summary>
        /// Particle emitter for planets
        /// </summary>
        public virtual bool hasParticles
        {
            get { return this.HasComponentInChildren<PlanetParticleEmitter>(); }
        }

        /// <summary>
        /// Particle emitter for planets
        /// </summary>
        public virtual PlanetParticleEmitter Particles
        {
            get { return GetComponentInChildren<PlanetParticleEmitter>(); }
        }

        /// <summary>
        /// Adds a Particle emitter to the body
        /// </summary>
        public virtual PlanetParticleEmitter AddParticles()
        {
            return this.AddComponent<PlanetParticleEmitter>();
        }

        /// <summary>
        /// Adds a PQS Sphere to the body
        /// </summary>
        public virtual PQS AddSphere(TerrainTemplates template)
        {
            PQS pqsVersion = new GameObject(name).AddComponent<PQS>();
            pqsVersion.transform.parent = transform;
            if (pqsController == null) pqsController = pqsVersion;
            else
            {
                pqsController.GetComponentInChildren<PQSMod_CelestialBodyTransform>().planetFade.secondaryRenderers.Add(pqsVersion.gameObject);
                pqsVersion.parentSphere = pqsController;
            }

            /// If there's a template, clone it
            if (template != TerrainTemplates.None)
            {
                /// Get the body
                PSystemBody body = PSystemManager.Instance.systemPrefab.Find(Enum.GetName(typeof(TerrainTemplates), template));
                
                /// Reflection based copy
                foreach (FieldInfo field in (typeof(PQS)).GetFields())
                {
                    /// Only copy non static fields
                    if (!field.IsStatic)
                    {
                        field.SetValue(pqsVersion, field.GetValue(body.pqsVersion));
                    }
                }
                pqsVersion.surfaceMaterial = body.pqsVersion.surfaceMaterial;
            }

            /// Create the fallback material (always the same shader)
            pqsVersion.fallbackMaterial = new PQSProjectionFallback();

            /// Create the celestial body transform
            PQSMod_CelestialBodyTransform cbTransform = new GameObject("_CelestialBody").AddComponent<PQSMod_CelestialBodyTransform>();
            cbTransform.transform.parent = pqsVersion.transform;
            cbTransform.sphere = pqsVersion;
            cbTransform.forceActivate = false;
            cbTransform.deactivateAltitude = 115000;
            cbTransform.forceRebuildOnTargetChange = false;
            cbTransform.planetFade = new PQSMod_CelestialBodyTransform.AltitudeFade();
            cbTransform.planetFade.fadeFloatName = "_PlanetOpacity";
            cbTransform.planetFade.fadeStart = 100000.0f;
            cbTransform.planetFade.fadeEnd = 110000.0f;
            cbTransform.planetFade.valueStart = 0.0f;
            cbTransform.planetFade.valueEnd = 1.0f;
            cbTransform.planetFade.secondaryRenderers = new List<GameObject>();
            cbTransform.secondaryFades = new PQSMod_CelestialBodyTransform.AltitudeFade[0];
            cbTransform.requirements = PQS.ModiferRequirements.Default;
            cbTransform.modEnabled = true;
            cbTransform.order = 10;

            /// Create the material direction
            PQSMod_MaterialSetDirection lightDirection = new GameObject("_Material_SunLight").AddComponent<PQSMod_MaterialSetDirection>();
            lightDirection.transform.parent = pqsVersion.transform;
            lightDirection.sphere = pqsVersion;
            lightDirection.valueName = "_sunLightDirection";
            lightDirection.requirements = PQS.ModiferRequirements.Default;
            lightDirection.modEnabled = true;
            lightDirection.order = 100;

            /// Create the UV planet relative position
            PQSMod_UVPlanetRelativePosition uvs = new GameObject("_Material_SurfaceQuads").AddComponent<PQSMod_UVPlanetRelativePosition>();
            uvs.transform.parent = pqsVersion.transform;
            uvs.sphere = pqsVersion;
            uvs.requirements = PQS.ModiferRequirements.Default;
            uvs.modEnabled = true;
            uvs.order = 999999;

            /// Create the quad mesh colliders
            PQSMod_QuadMeshColliders collider = new GameObject("QuadMeshColliders").AddComponent<PQSMod_QuadMeshColliders>();
            collider.transform.parent = pqsVersion.transform;
            collider.sphere = pqsVersion;
            collider.maxLevelOffset = 0;
            collider.physicsMaterial = new PhysicMaterial();
            collider.physicsMaterial.name = "Ground";
            collider.physicsMaterial.dynamicFriction = 0.6f;
            collider.physicsMaterial.staticFriction = 0.8f;
            collider.physicsMaterial.bounciness = 0.0f;
            collider.physicsMaterial.frictionDirection2 = Vector3.zero;
            collider.physicsMaterial.dynamicFriction2 = 0.0f;
            collider.physicsMaterial.staticFriction2 = 0.0f;
            collider.physicsMaterial.frictionCombine = PhysicMaterialCombine.Maximum;
            collider.physicsMaterial.bounceCombine = PhysicMaterialCombine.Average;
            collider.requirements = PQS.ModiferRequirements.Default;
            collider.modEnabled = true;
            collider.order = 100;

            /// Our sphere is ready!
            return pqsVersion;
        }
    }
}
