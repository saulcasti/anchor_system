# anchor_system
For the development I have considered the following steps:
1. Create a GameObject that will be in charge of the management of the anchors through a script: AnchorManager. In this script you indicate the different initial settings, such as the size of the room, the distance of the anchors and the number of anchors you want to create in the first instance. It is in charge of generating the anchors and changing the size of the floor.
This script will then check the distance of all anchors in the scene from the user and act accordingly.
2. To act on the anchors in an efficient way, a script (AnchorContext) has been created to change the colour and have the collider referenced (which will be used to show the distance later).
3. Create also a class that is in charge of launching the raycast from the user's camera. This raycast checks if it is an anchor (checking the tag) and then places the panel where it shows the distance according to the position and angle of the user. The distance is taken from the anchorManager which has a static instance of itself (Singleton).

With the current solution, I have tested performance by changing the room size, increasing the number of anchors and varying the distance. Good results have been obtained in each of them.

Considerations: Applying the Singleton pattern may seem a bit unnecessary because the scene lacks complexity and it might be simpler to reference it through the inspector. But if the project is expanded it can be useful to have this static instance, for example, if we want the colour of each anchor to vary depending on some global variable.


Another consideration is that I considered using Colliders trigger, but it introduced too much complexity, for the little benefit that would be obtained in terms of performance.

