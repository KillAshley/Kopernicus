

using Kopernicus.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using UnityEngine;
using GUI = UnityEngine.GUILayout;

namespace Kopernicus
{
    public class MapExport : MonoBehaviour
    {
        // GUI-stuff
        bool window = false;
        string bodyName = "";
        int mapWidth = 2048;
        float strength = 9;
        double pixelPerFrame = 5000;

        // Dont kill us
        void Awake()
        {
            DontDestroyOnLoad(this);
        }

        // Get the Keys
        void Update()
        {
            if (GameSettings.MODIFIER_KEY.GetKey() && Input.GetKey(KeyCode.E) && Input.GetKeyDown(KeyCode.P))
                window = !window;
        }

        // Draw our export GUI
        void OnGUI()
        {
            if (window)
                GUI.Window(353647474, new Rect(30, 30, 200, 140), WindowFunction, "Export Planet Maps");
        }

        void WindowFunction(int level)
        {
            // Option
            GUILayoutOption[] gui = new GUILayoutOption[0];

            // Body-Name
            GUI.BeginHorizontal();
            GUI.Label("Body:", gui);
            bodyName = GUI.TextField(bodyName, gui);
            GUI.EndHorizontal();
            GUI.BeginHorizontal();
            GUI.Label("Size:", gui);
            mapWidth = Int32.Parse(GUI.TextField(mapWidth.ToString(), gui));
            GUI.EndHorizontal();
            GUI.BeginHorizontal();
            GUI.Label("Strength", gui);
            strength = Single.Parse(GUI.TextField(strength.ToString(), gui));
            GUI.EndHorizontal();
            GUI.BeginHorizontal();
            GUI.Label("PPF:", gui);
            pixelPerFrame = Double.Parse(GUI.TextField(pixelPerFrame.ToString(), gui));
            GUI.EndHorizontal();
            GUI.Space(15);
            if (GUI.Button("Export Maps", new GUILayoutOption[0]))
            {
                CelestialBody body = PSystemManager.Instance.localBodies.Find(b => b.name == bodyName);
                body.pqsController.mapFilesize = mapWidth;
                StartCoroutine(GeneratePQSMaps(body, strength, pixelPerFrame));
            }
        }

        public IEnumerator GeneratePQSMaps(CelestialBody body, float normalStrength, double pixelPerFrame)
        {
            // Get time
            DateTime now = DateTime.Now;

            // Get the PQS and the ScaledSpace
            PQS pqs = body.pqsController;
            GameObject scaledVersion = body.scaledBody;

            // Create the Textures
            Texture2D colorMap = new Texture2D(pqs.mapFilesize, pqs.mapFilesize / 2, TextureFormat.ARGB32, true);
            Texture2D heightMap = new Texture2D(pqs.mapFilesize, pqs.mapFilesize / 2, TextureFormat.RGB24, true);

            // Get the active mods
            IEnumerable<PQSMod> mods = pqs.GetComponentsInChildren<PQSMod>(true).Where(m => m.modEnabled);

            // Open the external Renderer
            pqs.SetupExternalRender();

            // Get the Mod-Building Methods, because I'm lazy :P
            MethodInfo modOnVertexBuildHeight = pqs.GetType().GetMethod("Mod_OnVertexBuildHeight", BindingFlags.Instance | BindingFlags.NonPublic);
            MethodInfo modOnVertexBuild = pqs.GetType().GetMethod("Mod_OnVertexBuild", BindingFlags.Instance | BindingFlags.NonPublic);

            // Stuff
            ScreenMessage message = ScreenMessages.PostScreenMessage("Generating Planet-Maps", 1f, ScreenMessageStyle.UPPER_CENTER);

            // Loop through the pixels
            for (int y = 0; y < (pqs.mapFilesize / 2); y++)
            {
                for (int x = 0; x < pqs.mapFilesize; x++)
                {
                    // Update Message
                    ScreenMessages.RemoveMessage(message);
                    double percent = ((double)((y * pqs.mapFilesize) + x) / ((pqs.mapFilesize / 2) * pqs.mapFilesize)) * 100;
                    message = ScreenMessages.PostScreenMessage("Generating Planet-Maps: " + percent.ToString("0.00") + "%", Time.deltaTime * 0.5f, ScreenMessageStyle.UPPER_CENTER);

                    // Create a VertexBuildData
                    PQS.VertexBuildData data = new PQS.VertexBuildData();

                    // Configure the VertexBuildData
                    data.directionFromCenter = (QuaternionD.AngleAxis((360d / pqs.mapFilesize) * x, Vector3d.up) * QuaternionD.AngleAxis(90d - (180d / (pqs.mapFilesize / 2)) * y, Vector3d.right)) * Vector3d.forward;
                    data.vertHeight = pqs.radius;

                    // Build from the Mods
                    modOnVertexBuildHeight.Invoke(pqs, new[] { data });
                    modOnVertexBuild.Invoke(pqs, new[] { data });

                    // Adjust the height
                    double height = (data.vertHeight - pqs.radius) * (1d / pqs.mapMaxHeight);
                    if (height < 0)
                        height = 0;
                    else if (height > 1)
                        height = 1;

                    // Adjust the Color
                    Color color = data.vertColor;
                    color.a = 1f;
                    if (pqs.mapOcean && height <= pqs.mapOceanHeight)
                        color = pqs.mapOceanColor;

                    // Set the Pixels
                    colorMap.SetPixel(x, y, color);
                    heightMap.SetPixel(x, y, new Color((float)height, (float)height, (float)height));

                    // yield return
                    if (((y * pqs.mapFilesize) + x) % pixelPerFrame == 0)
                        yield return null;
                }
            }

            // Apply the maps
            colorMap.Apply();
            heightMap.Apply();

            // Close the Renderer
            pqs.CloseExternalRender();

            // Bump to Normal Map
            Texture2D normalMap = Utility.BumpToNormalMap(heightMap, normalStrength);

            // Serialize them to disk
            string path = Body.ScaledSpaceCacheDirectory + "/PluginData/";
            Directory.CreateDirectory(path);
            File.WriteAllBytes(path + body.name + "_Color.png", colorMap.EncodeToPNG());
            File.WriteAllBytes(path + body.name + "_Height.png", heightMap.EncodeToPNG());
            File.WriteAllBytes(path + body.name + "_Normal.png", normalMap.EncodeToPNG());

            // Declare that we're done
            ScreenMessages.PostScreenMessage("Operation completed in: " + (DateTime.Now - now).TotalMilliseconds + " ms", 2f, ScreenMessageStyle.UPPER_CENTER);
        }

    }
}

