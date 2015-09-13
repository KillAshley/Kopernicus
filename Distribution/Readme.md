Kopernicus Beta Release 3.3
==============================
August 18th, 2015

Created by: - Bryce C Schroeder (bryce.schroeder@gmail.com)
   		    - Nathaniel R. Lewis (linux.robotdude@gmail.com)
 
Maintained by: - Thomas P.
			   - NathanKell
 
Additional Content by: Gravitasi, aftokino, KCreator, Padishar, Kragrathea, OvenProofMars

New in this version
-------------------
- Fixed PQSMod_VertexHeightOffset
- Fixed a bug with the speed of particles
- Moved flightGlobalsIndex patching into the prefab, this fixes the bug, where the game dies before switching to MainMenu
- Added the ability to add MeshColliders to the groundscatter objects
- Added the ability to get science from groundscatter while on EVA (custom experiments)
- Added a .mu loader for meshes
- Uniform syntax for curves (in the past, the types "AnimationCurve" and "FloatCurve" didn't used the same syntax, now everything is parsed like FloatCurve (key = 0 1 0 0))
- Support for patching LandClasses (HeightColorMap, HeightColorMap2, HeightColorMapNoise, LandControl) and groundscatters
- Added a new example for patching LandClasses => RecoloredVall
- Added a way to insert custom loading code from a separate plugin
- Uniform Scale for Rings

New in Version 3.1
==================
- Hotfix for flightGlobalsIndex

New in Version 3.2
==================
- Hotfix for LandControl
- Seperate download for the examples
- Support for non spherical oceans

New in Version 3.3
==================
- Fixed Thomas Stupidity

Note - reparenting Kerbin or the Sun causes the sky to be incorrect in the space center view. It is, however, correct in the flight view and the flight map view.  Reparenting the sun causes other stars positions to not update in the tracking station for some reason.

About
-----
Kopernicus is a KSP add-on that allows for modification of stock planets and the creation of new planets via modification of the system prefab.  Why is this advantageous you might ask?  Previous planet adder mods, such as Planet Factory, modified the live planetary system and had to keep multiple hacks actively running to provide these worlds.  We strive to provide the least hacky solution by introducing planets into the game in the exact same manner Squad would.  

Kopernicus is a one step process.  It is started before the planetary system is created and rewrites a property called PSystemManager.Instance.systemPrefab.  The game itself then creates *our* planetary system as if it were blessed by Squad themselves.  The mod’s function ends here, and it terminates.  Kopernicus introduced worlds require zero maintenance by third party code, all support is driven entirely by built in functionality.  This yields an incredibly stable and incredibly flexible environment for planetary creation.

One caveat exists that prevents absolute control over a planetary system.  Due to the way Squad defines how launch control and ship recovery work, a planet called Kerbin must exist and must have a reference body id of 1.  While it is easy to provide this scenario, as a star to orbit and a single planet satisfies this, any would be universe architect needs to be aware so they don’t get mystery errors. The displayed name of the body (i.e. the celestialBody.bodyName property) can be changed after the spawn of the PSystem  

Each celestial body in KSP has a property called “flightGlobalsIndex.”  This number does not directly correspond to the reference id, but is related.  Once all of the celestial bodies have been spawned, references to them are written into the localBodies list.  This list is then sorted in increasing order of the flight globals index.  The index the body is located at after the sort becomes the reference body id.

We have yet to see if removing or rearranging the other bodies causes any sort of errors in the science or contracts system.


Instructions
------------

1) Copy the contents of the GameData/ folder to KSP’s GameData/ folder

2) Launch KSP and enjoy!

3) Please report any bugs you may find to either or both of the email addresses above.


Examples
----------
Selectively copy folders inside [KopernicusExamples/](https://github.com/Kopernicus/KopernicusExamples/) into a GameData/KopernicusExamples/ folder.  There are a number of examples of how to use Kopernicus.


Information
-----------
Coming Soon

Debugging Features
------------------

After the Kopernicus.Injector behavior rewrites the system prefab and terminates, a behavior called Kopernicus.RuntimeUtility is started to provide debugging functions.

PQS Inspector (Mod-P): Pressing Mod-P will explode your console with information about the PQS controller of the body currently being orbited

PQS Quad Visualizer (Mod-;, Mod-/): Pressing Mod-; will draw green lines on the normal vectors to the surface of the planet at the PQ nodes.  This lets the LOD structure be visualized.  Used for testing LOD levels and whether your temperamental ocean just wants to be invisible or is decidedly non present.  Pressing Mod-/ will remove the lines (and save your framerate).

Map Exporter (Mod-E-P): Pressing Mod-E-P will open the Kopernicus Map Export. The first, empty, line is for the name of the body, that you want to export. The second one is the resolution of the exported maps. The textures will be saved to GameData/Kopernicus/Cache/PluginData
