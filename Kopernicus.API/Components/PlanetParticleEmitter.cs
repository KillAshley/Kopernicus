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
using System;

namespace Kopernicus
{
    namespace Components
    {
        /// <summary>
        /// Component to emit particles on planets
        /// </summary>
        public class PlanetParticleEmitter : MonoBehaviour
        {
            /// <summary>
            /// The internal particle emitter
            /// </summary>
            protected virtual ParticleEmitter emitter { get; set; }

            /// <summary>
            /// The internal animator component
            /// </summary>
            protected virtual ParticleAnimator animator { get; set; }

            /// <summary>
            /// The internal rendering component
            /// </summary>
            protected virtual ParticleRenderer Renderer { get; set; }

            /// <summary>
            /// The point where the particles are pointing at
            /// </summary>
            public virtual Transform target { get; set; }

            /// <summary>
            /// Angular Velocity of the particles
            /// </summary>
            public virtual float angularVelocity
            {
                get { return emitter.angularVelocity; }
                set { emitter.angularVelocity = value; }
            }

            /// <summary>
            /// If the ParticleEmitter should emit Particles
            /// </summary>
            public virtual bool emit
            {
                get { return emitter.emit; }
                set { emitter.emit = value; }
            }

            /// <summary>
            /// Scale the velocity up / down
            /// </summary>
            public virtual float emitterVelocityScale
            {
                get { return emitter.emitterVelocityScale; }
                set { emitter.emitterVelocityScale = value; }
            }

            /// <summary>
            /// Enable / disable the emitter
            /// </summary>
            public virtual new bool enabled
            {
                get { return enabled && emitter.enabled && Renderer.enabled; }
                set { enabled = emitter.enabled = Renderer.enabled = value; }
            }

            /// <summary>
            /// Velocity, relative to the emitter
            /// </summary>
            public virtual Vector3 localVelocity
            {
                get { return emitter.localVelocity; }
                set { emitter.localVelocity = value; }
            }

            /// <summary>
            /// Maximum amount of emitted particles
            /// </summary>
            public virtual float maxEmission
            {
                get { return emitter.maxEmission; }
                set { emitter.maxEmission = value; }
            }

            /// <summary>
            /// Maximum lifespan of emitted particles
            /// </summary>
            public virtual float maxEnergy
            {
                get { return emitter.maxEnergy; }
                set { emitter.maxEnergy = value; }
            }

            /// <summary>
            /// Maximum size of emitted particles
            /// </summary>
            public virtual float maxSize
            {
                get { return emitter.maxSize; }
                set { emitter.maxSize = value; }
            }

            /// <summary>
            /// Minimum amount of emitted particles
            /// </summary>
            public virtual float minEmission
            {
                get { return emitter.minEmission; }
                set { emitter.minEmission = value; }
            }

            /// <summary>
            /// Minimum lifespan of emitted particles
            /// </summary>
            public virtual float minEnergy
            {
                get { return emitter.minEnergy; }
                set { emitter.minEnergy = value; }
            }

            /// <summary>
            /// Minimum size of emitted particles
            /// </summary>
            public virtual float minSize
            {
                get { return emitter.minSize; }
                set { emitter.minSize = value; }
            }

            /// <summary>
            /// Name of the Component
            /// </summary>
            public virtual new string name
            {
                get { return name; }
                set { name = emitter.name = animator.name = Renderer.name = value; }
            }

            /// <summary>
            /// The current amount of particles
            /// </summary>
            public virtual int particleCount
            {
                get { return emitter.particleCount; }
            }

            /// <summary>
            /// The current particle objects
            /// </summary>
            public virtual Particle[] particles
            {
                get { return emitter.particles; }
                set { emitter.particles = value; }
            }

            /// <summary>
            /// A randomized addition to the angularVelocity
            /// </summary>
            public virtual float rndAngularVelocity
            {
                get { return emitter.rndAngularVelocity; }
                set { emitter.rndAngularVelocity = value; }
            }

            /// <summary>
            /// If this is true, the particles will be rotated randomly
            /// </summary>
            public virtual bool rndRotation
            {
                get { return emitter.rndRotation; }
                set { emitter.rndRotation = value; }
            }

            /// <summary>
            /// Applies a randomized velocity to the particles. 
            /// Values (x, y, z) are the maximum amount of speed.
            /// </summary>
            public virtual Vector3 rndVelocity
            {
                get { return emitter.rndVelocity; }
                set { emitter.rndVelocity = value; }
            }

            /// <summary>
            /// If this is set to true, the particles will be emitted 
            /// relative to the camera, and not to the body.
            /// </summary>
            public virtual bool useWorldSpace
            {
                get { return emitter.useWorldSpace; }
                set { emitter.useWorldSpace = value; }
            }

            /// <summary>
            /// Velocity, relative to the world center
            /// </summary>
            public virtual Vector3 worldVelocity
            {
                get { return emitter.worldVelocity; }
                set { emitter.worldVelocity = value; }
            }

            /// <summary>
            /// Should the particles automatically get destroyed?
            /// </summary>
            public virtual bool autodestruct
            {
                get { return animator.autodestruct; }
                set { animator.autodestruct = value; }
            }

            /// <summary>
            /// The animated colors of the particles
            /// </summary>
            public virtual Color[] colorAnimation
            {
                get { return animator.colorAnimation; }
                set { animator.colorAnimation = value; }
            }

            /// <summary>
            /// Values between 0 and 1 reduce the speed of the particles
            /// </summary>
            public virtual float damping
            {
                get { return animator.damping; }
                set { animator.damping = value; }
            }

            /// <summary>
            /// If false, colorAnimation isn't used
            /// </summary>
            public virtual bool doesAnimateColor
            {
                get { return animator.doesAnimateColor; }
                set { animator.doesAnimateColor = value; }
            }

            /// <summary>
            /// Constant force, applied to every particle
            /// </summary>
            public virtual Vector3 force
            {
                get { return animator.force; }
                set { animator.force = value; }
            }

            /// <summary>
            /// Rotation relative to the emitter
            /// </summary>
            public virtual Vector3 localRotationAxis
            {
                get { return animator.localRotationAxis; }
                set { animator.localRotationAxis = value; }
            }

            /// <summary>
            /// Randomized force, applied to the particles
            /// </summary>
            public virtual Vector3 rndForce
            {
                get { return animator.rndForce; }
                set { animator.rndForce = value; }
            }

            /// <summary>
            /// Defines, how fast a particle grows
            /// </summary>
            public virtual float sizeGrow
            {
                get { return animator.sizeGrow; }
                set { animator.sizeGrow = value; }
            }

            /// <summary>
            /// Rotation, relative to the center of the world
            /// </summary>
            public virtual Vector3 worldRotationAxis
            {
                get { return animator.worldRotationAxis; }
                set { animator.worldRotationAxis = value; }
            }

            /// <summary>
            /// Amount of animated textures
            /// </summary>
            [Obsolete("animatedTextureCount has been replaced by uvAnimationXTile and uvAnimationYTile")]
            public virtual int animatedTextureCount
            {
                get { return Renderer.animatedTextureCount; }
                set { Renderer.animatedTextureCount = value; }
            }

            /// <summary>
            /// Defines, that the velocity gets scaled in the camera too
            /// </summary>
            public virtual float cameraVelocityScale
            {
                get { return Renderer.cameraVelocityScale; }
                set { Renderer.cameraVelocityScale = value; }
            }

            /// <summary>
            /// Defines, whether the particles should cast shadows on planets.
            /// </summary>
            public virtual bool castShadows
            {
                get { return Renderer.castShadows; }
                set { Renderer.castShadows = value; }
            }

            /// <summary>
            /// How much should the particles get scaled in length
            /// </summary>
            public virtual float lengthScale
            {
                get { return Renderer.lengthScale; }
                set { Renderer.lengthScale = value; }
            }

            /// <summary>
            /// The local material of the renderer. Will only change this instance of the material.
            /// </summary>
            public virtual Material material
            {
                get { return Renderer.material; }
                set { Renderer.material = value; }
            }

            /// <summary>
            /// This allows the use of multiple materials / shaders.
            /// Changing this will only affect this instance of the materials
            /// </summary>
            public virtual Material[] materials
            {
                get { return Renderer.materials; }
                set { Renderer.materials = value; }
            }

            /// <summary>
            /// Maxmimal size of a particle
            /// </summary>
            public virtual float maxParticleSize
            {
                get { return Renderer.maxParticleSize; }
                set { Renderer.maxParticleSize = value; }
            }

            /// <summary>
            /// Maxmimal size of a partille
            /// </summary>
            public virtual float maxPartileSize
            {
                get { return Renderer.maxPartileSize; }
                set { Renderer.maxPartileSize = value; }
            }

            /// <summary>
            /// Defines the way how particles get rendered
            /// </summary>
            public virtual ParticleRenderMode particleRenderMode
            {
                get { return Renderer.particleRenderMode; }
                set { Renderer.particleRenderMode = value; }
            }

            /// <summary>
            /// Whether the particles should recieve shadows from planets.
            /// </summary>
            public virtual bool receiveShadows
            {
                get { return Renderer.receiveShadows; }
                set { Renderer.receiveShadows = value; }
            }

            /// <summary>
            /// The shared material of the renderer. Will change every instance of the material.
            /// </summary>
            public virtual Material sharedMaterial
            {
                get { return Renderer.sharedMaterial; }
                set { Renderer.sharedMaterial = value; }
            }

            /// <summary>
            /// This allows the use of multiple materials / shaders.
            /// Changing this will affect every instance of the materials
            /// </summary>
            public virtual Material[] sharedMaterials
            {
                get { return Renderer.sharedMaterials; }
                set { Renderer.sharedMaterials = value; }
            }

            /// <summary>
            /// The cycles of the UV-Animation (animated Texture)
            /// </summary>
            public virtual float uvAnimationCycles
            {
                get { return Renderer.uvAnimationCycles; }
                set { Renderer.uvAnimationCycles = value; }
            }

            /// <summary>
            /// The X-Tile of the UV-Animation (animated Texture)
            /// </summary>
            public virtual int uvAnimationXTile
            {
                get { return Renderer.uvAnimationXTile; }
                set { Renderer.uvAnimationXTile = value; }
            }

            /// <summary>
            /// The Y-Tile of the UV-Animation (animated Texture)
            /// </summary>
            public virtual int uvAnimationYTile
            {
                get { return Renderer.uvAnimationYTile; }
                set { Renderer.uvAnimationYTile = value; }
            }

            /// <summary>
            /// Splits the texture into multiple segments, to support UV-Animation
            /// </summary>
            public virtual Rect[] uvTiles
            {
                get { return Renderer.uvTiles; }
                set { Renderer.uvTiles = value; }
            }

            /// <summary>
            /// Defines, how much the velocity of the particles is scaled
            /// </summary>
            public virtual float velocityScale
            {
                get { return Renderer.velocityScale; }
                set { Renderer.velocityScale = value; }
            }

            /// <summary>
            /// The texture of the particles. Will be colored later.
            /// </summary>
            public virtual Texture2D texture
            {
                get { return material.mainTexture as Texture2D; }
                set { material.mainTexture = value; }
            }

            /// <summary>
            /// The main initialisation. Here we create the subcomponents.
            /// </summary>
            protected virtual void Awake()
            {
                if (!this.HasComponent<ParticleEmitter>())
                {
                    emitter = (ParticleEmitter)this.AddComponent("MeshParticleEmitter");
                    useWorldSpace = false;
                    emit = true;
                }

                if (!this.HasComponent<ParticleAnimator>())
                {
                    animator = this.AddComponent<ParticleAnimator>();
                    doesAnimateColor = true;
                }

                if (!this.HasComponent<ParticleRenderer>())
                {
                    Renderer = this.AddComponent<ParticleRenderer>();
                    material = new Material(Shader.Find("Particles/Alpha Blended"));
                }
            }

            /// <summary>
            /// Timewarp rate from last frame, used to fix problems with 1x TimeWarp
            /// </summary>
            private float LastRate { get; set; }

            /// <summary>
            /// Update() runs every frame, so that we can update the particles here.
            /// </summary>
            protected virtual void Update()
            {
                if (target == null)
                    return;

                worldVelocity = ((target.position - transform.position) * velocityScale) * TimeWarp.CurrentRate;
                minEnergy = (minEnergy * LastRate) / TimeWarp.CurrentRate;
                maxEnergy = (maxEnergy * LastRate) / TimeWarp.CurrentRate;
                maxEmission = (maxEmission / LastRate) * TimeWarp.CurrentRate;
                minEmission = (minEmission / LastRate) * TimeWarp.CurrentRate;
                rndVelocity = (rndVelocity / LastRate) * TimeWarp.CurrentRate;
            }

            /// <summary>
            /// Removes every particle that is alive currently.
            /// </summary>
            public virtual void ClearParticles()
            {
                emitter.ClearParticles();
            }

            /// <summary>
            /// Emit particles based on the stored information
            /// </summary>
            public virtual void Emit()
            {
                emitter.Emit();
            }

            /// <summary>
            /// Emit particles based on given information
            /// </summary>
            public virtual void Emit(int count)
            {
                emitter.Emit(count);
            }

            /// <summary>
            /// Emit particles based on given information
            /// </summary>
            public virtual void Emit(Vector3 pos, Vector3 velocity, float size, float energy, Color color)
            {
                emitter.Emit(pos, velocity, size, energy, color);
            }

            /// <summary>
            /// Emit particles based on given information
            /// </summary>
            public virtual void Emit(Vector3 pos, Vector3 velocity, float size, float energy, Color color, float rotation, float angularVelocity)
            {
                emitter.Emit(pos, velocity, size, energy, color, rotation, angularVelocity);
            }

            /// <summary>
            /// Emit particles for x frames
            /// </summary>
            public virtual void Simulate(float deltaTime)
            {
                emitter.Simulate(deltaTime);
            }

            /// <summary>
            /// Convert a PlanetParticleEmitter into a normal ParticleEmitter
            /// </summary>
            public static implicit operator ParticleEmitter(PlanetParticleEmitter ppe)
            {
                return ppe.emitter;
            }

            /// <summary>
            /// Convert a PlanetParticleEmitter into a normal ParticleAnimator
            /// </summary>
            public static implicit operator ParticleAnimator(PlanetParticleEmitter ppe)
            {
                return ppe.animator;
            }

            /// <summary>
            /// Convert a PlanetParticleEmitter into a normal ParticleRenderer
            /// </summary>
            public static implicit operator ParticleRenderer(PlanetParticleEmitter ppe)
            {
                return ppe.Renderer;
            }
        }
    }
}