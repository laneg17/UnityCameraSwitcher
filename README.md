# UnityCameraSwitcher
Efficiently incorporate Cameras into your animations / cutscenes.

**Features**
- Unity package includes animated scene that uses the script
- Tool tips help artists/animators understand what goes in each exposed array
- Enable and disable objects at the start of the script and once the final camera is finished
- Easily add and remove cameras and set the duration of each camera using the editor

**Using the tool**
1. Import your models and cameras, set up animation clips for animated objects, ensure the rig for animated objects is set to legacy in the import settings.

![Alt text](/Images/CameraSwitcher_0.png?raw=true "Cover")

3. Add all models and cameras to the scene, below is an image of what the included scene looks like

![Alt text](/Images/CameraSwitcher_1.png?raw=true "Cover")

5. Make a gameobject to hold the CameraSwitcher script and attach it

![Alt text](/Images/CameraSwitcher_2.jpg?raw=true "Cover")

7. Change the Cameras and Seconds Between array sizes to the amount of cameras in your scene. Drag the cameras in shot order into the Cameras array. The Seconds Between array determines how long each camera will be active before a camera switch (input seconds, supports decimal values)

![Alt text](/Images/CameraSwitcher_3.jpg?raw=true "Cover")

9. The enable and disable on start and at the end GameObject arrays are useful in a game or interactive media context. For example, if you want to trigger a cutscene when a player has entered a certain area, then change the Disable on Start array to size 1, and drag the player character into it. Then, take your cinematic assets, and put them in the Enable on Start array. Then, you can re-enable your gameplay assets at the end with the Enable on End array and disable your cinematic assets with the Disable on End array

![Alt text](/Images/CameraSwitcher_4.jpg?raw=true "Cover")
