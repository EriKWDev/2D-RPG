# Pixel Perfect Camera
-----------------------

Support thread::
http://forum.unity3d.com/threads/released-free-pixel-perfect-camera.416141/

How to use video:
https://www.youtube.com/watch?v=OuTMcY3H2j8

Please read the documentation inside PixelPerfectCamera.cs or PixelSnap.cs for more in-depth information.

## Instructions for Pixel Perfect Camera:

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

## Instructions for PixelSnap:

1) Place the script on every object that you want to snap on the pixel grid of the assets (retroSnap mode) or on the screen's pixel grid (reduce jitter mode).

2) When the current object will be rendered by a camera, the script will look if it is a PixelPerfectCamera. If it is, it will read the retroSnap setting and render accordingly. 
So, enable or disable the retroSnap option in the camera that uses the PixelPerfectCamera script. This will affect **all** the objects that use the PixelSnap script.

3) Comment out or comment in the REDUCE_JITTER symbol on top of the script's code to disable or enable the jitter-reduction (when retroMode is disabled). By default it is disabled.

4) "Retro Snap" should be used with "Pixel Perfect" enabled, otherwie jitter artifacts will occur.

## Project set-up tips

If you want to enable the pixel-perfect mode, make sure that your project has the following set-up:

- Use an orthographic camera and throw the Pixel Perfect Camera script on it
- Leave the sprite's scale to 1.
- The textures of your sprites should use: point filtering, disable mip-mapping, Truecolor format
- All your textures should have the same Pixels Per Unit
- Make sure that the player settings of the platform(s) you are targeting don't reduce the texture size
- In your project's quality settings: set "Full res" in Texture quality and disable Anti-Aliasing
- DX9 samples from the edge of the texels instead of the center. This will result in all sprites rendered with 1 fragment offset. 
  If you use DX9 you can enable "pixel snap" in default sprite shader. This will correct the half fragment offst in the vertex shader. DX 11, OpenGL etc don't have this problem.



