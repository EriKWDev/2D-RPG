Pixel Perfect Camera
-----------------------

Instructions:

1) Place the PixelPerfectCamera.cs script on a 2D orthographic camera.

2) Set the "Assets pixels per unit" to the the value used by your sprites. By default this is 100.

3) You can specify either the target width or height in units. This works as the default unity camera, where you specify half of the corresponding dimension.

4) If you enable "Pixel Perfect" mode, the script will adjust the camera size so that it is pixel perfect. The resulting size will not be the same size as the target size you specified, but it will be as close as possible (slightly bigger or smaller).

5) If you enable one or both of the "max width/height", the resulting camera width or height will never be larger that the specified values. 

6) Enable "show HUD" if you want to toggle "Pixel perfect" while the game is running.

Below the controls, you can see some numbers regarding the size calculation:

-Size: this is the resulting camera size in units: [width x height]. When a "max width/height" restricts the size, the corresponding dimension will be bold.
-Pixels Per Unit: the number of screen pixels a unit is rendered to (this is *not* the assets pixels per unit)
-Pixels: the resolution of the screen given as a multiple of 2 numbers. The first is the number of screen pixels an asset pixel will render to. The second is the camera resolution in asset pixels. For example: 4 x [100.5, 300.0] means that the screen resolution is [402, 1200] and it corresponds to a width of 100.5 pixels and a height of 300 pixels in asset pixels. A single asset pixel is rendered to 4 screen pixels. A non pixel-perfect resolution would have a non-integer in the place of "4".
-Coverage: the percentage of the resulting size to the target size. This is usually 100%, which means that the resulting size is the same as the targeted one. However, if "Pixel Perfect" is enabled, the percentage may be higher or lower than 100% if the resulting size is bigger or smaller than the target size. Also, if "max width/height" restricts the size it can reduce the coverage.

Usage tips:

- Start by disabling "pixel perfect" and adjust either the target width or height according to your scene. If needed, specify the "max width/height". Enable "pixel perfect". 

- If you set the target width, then the camera's height depends on the user's monitor/mobile device screen aspect ratio. If for example the device is quite tall, then the camera's height will be a larger number. If your world can't expand vertically forever, it's best that you set a "max height" on the script. 

- When you enable "pixel perfect" mode, the resulting size may be slightly smaller or bigger than the one you set in "target size". You may want to enable "max width/height" to restrict the size.


Project set-up tips

- If you enable PixelPerfect mode, it's best that you set all all your sprites or texture to "point (no filter)" mode. Also, in "Project settings -> Quality" disable Anti Aliasing.



