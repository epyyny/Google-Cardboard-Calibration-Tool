using Google.XR.Cardboard;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

/// <summary>
/// Tap the screen while in VR to toggle DPI-based viewport scaling on/off.
/// Attach to any active GameObject in the scene.
/// </summary>
public class DpiScalingToggle : MonoBehaviour
{
    // Starts ON to match the modified SDK default.
    public static bool DpiScalingEnabled = true;

    // Prevents a single tap registering on multiple frames.
    private bool _touchedLastFrame = false;

    private void Update()
    {
        bool touchedThisFrame = IsTappedThisFrame();

        if (touchedThisFrame && !_touchedLastFrame)
        {
            DpiScalingEnabled = !DpiScalingEnabled;
            XRLoader.SetDpiScalingEnabled(DpiScalingEnabled);
            Debug.Log($"[DpiScalingToggle] DPI scaling: {(DpiScalingEnabled ? "ON (modified)" : "OFF (original)")}");
        }

        _touchedLastFrame = touchedThisFrame;
    }

    private static bool IsTappedThisFrame()
    {
        Touchscreen touchScreen = Touchscreen.current;
        if (touchScreen == null) return false;

        if (!touchScreen.enabled)
            InputSystem.EnableDevice(touchScreen);

        ReadOnlyArray<TouchControl> touches = touchScreen.touches;
        if (touches.Count == 0) return false;

        return touches[0].phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began;
    }
}
