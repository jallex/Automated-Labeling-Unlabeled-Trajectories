# Motion Capture Data Automate Labeling Unlabeled Trajectories: 

## Problem: 
During the motion capture tracking session, when the cameras lose track of a marker, even for a single frame, the system creates a whole new trajectory for the marker once it is visible again. The system cannot identify that the previous trajectory and the new one belong to the same marker, and these trajectories need to be connected to clean and label the data. 

## Solution:
Using the RGB pixel location, I cast a ray from the camera through the RGB video projected on the camera’s near plane at the pixel location of the unlabeled marker and into the camera’s frustum. This ray will pass through the specific unlabeled trajectory we want to label from the full animated marker data projected in the camera’s frustum. 

# Demo:

## Tools:
Qualisys
Blender
Unity

## Steps:
Export the fbx data containing the positions and rotations of the motion capture and RGB cameras and the c3d file from Qualisys, along with an RGB video with visible trackable markers 

### Blender set-up 1: RGB video 
Import RGB video into Blender’s Video Editing workspace. Export the video out as frames
Import the frames into Blender’s movie clip editor

Add a track to the ball / marker that you want to track, and adjust tracking settings
Set motion model to perspective or affine (handles distortion)
Enable normalize
Set correlation to .9 (90% confidence on each frame or tracking will terminate)

Let Blender autotrack forward - if it loses track, realign the track manually by dragging it to the desired location and then continue until all frames have been tracked
Go to 3D viewport, select camera and press N to bring up the property panel. Place the camera on the Z axis pointing down at 2 meters or more. Set x and y to 0 and set the rotation on all axis to 0. 
Go back to movie clip editor, and click Reconstruction > Link Empty to Track (this creates animated empty objects for each tracker)

Add mocap_jugging.py script to the project.
Now, there should be armature and mesh attached to the track. Export an fbx and in the export settings uncheck “NLA strips” and “All Actions”

For reference, we’ll call this FBX file Green_Ball_Tracked.fbx


### Blender set-up 2: Full Body Motion Capture 
- Import the c3d Qualisys export into a new Blender project
- Run mocap_jugging.py script
- Correct the frame rate by going to Output > Dimensions > Framerate > Custom
- Bake the armature Animations as pose data by selecting the armature in Object mode and doing Object > Animation > Bake Action 
- Make sure all frames are selected and it bakes as pose data.
- Export the fbx with the same export settings as the previous blender project. 
- We’ll refer to this fbx as Full_Body_Tracked.fbx 


### Unity set-up : 
- Import the Qualisys fbx with camera data into a new Unity 3D Project. 
- Add the camera models to the corresponding cameras (can be done with script) and adjust rotations 
- Fix near and far clipping planes via script: https://docs.google.com/document/d/1K2A9GwCbB07COAuDfQsE7456Ad2gutkExYVbZiecUIs/edit
- We are working with the 29 Miqus Video Camera Object in the fbx file because it took the RGB video we are working with 
- Create Canvas from cube on the RGB camera’s near plane and scale to be the exact size of the near plane:
- Download and import the cube made from quads prefab: https://drive.google.com/open?id=1owVQ_ZP9vZkgqoKcsm6jKqaOa-NjnfFE
- Attach script Move to near plane to an object, and set the camera to the RGB camera and the cube to the cube just created

- Adding video texture to cube:
  - Create new asset render texture
  - Drag it onto the Quad that makes up the front of the cube (canvas to play video)
  - This created a materials folder under assets. Go into the folder and click the new material and look at inspector
  - Change the shader to Sprites/Default
  - Create an empty and in inspector add Video Player. Set the video player to play the video and set its render mode to render texture
- From Assets folder drag new texture to the inspector of video player to target texture box
- Import both fbx files into the Unity project
- Rig each one by clicking the prefab in the asset folder and in the inspector do Rig > Avatar Definition > Copy from Other Avatar and under Source choose the corresponding avatar, click apply
- Create a new animator controller for each fbx, and in the Animator Window, create a new action that connects to the Entry node, and attach the animation from the corresponding fbx to it.
- Create an Animator Component on each fbx, and add the corresponding animator controller to it. Leave the avatar blank
- Set the position and camera of the Green Ball fbx to be that of the RGB camera
- Create an Empty GameObject at any position
- Add Attach This Camera to That script to any object in the scene
- Set Cam 1 to the RGB video camera
- Set Empty to the Empty just created
- Set FBX Camera to the Green_Ball fbx’s camera, and FBX to the Green_Ball fbx

- Projecting a line from camera to point of green ball in 3D space:
  - Add a Line Renderer Component to the RGB camera (29 Miqus)
  - Add the RenderLine script to any object
    Note: the script has to use LateUpdate or else the line will lag
  - Set the script fields:
    Set Cam as the RGB video camera 
    Set End Point as the Track from the Green_Ball Fbx 

- Add Attach Person to Correct Loc script to any Object
- Create a new Empty Game Object - I called mine Empty Parent Person
  - Set settings:
    Set the Person to be the Fbx file with the body data
    Set Person block to be the block closest to what would be (0, 0, 0) on the rigid body markers measuring the origin
    Set the Empty to the Empty Object just created

- Fix Scaling: Adjust the Calibration info of the RGB camera - sensor size, projection x and y, etc

