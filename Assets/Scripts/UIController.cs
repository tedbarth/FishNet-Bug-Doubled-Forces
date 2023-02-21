using UnityEngine;

[ExecuteInEditMode]
public class UIController : MonoBehaviour {
  private void Awake() {
    _enterWindowMode();
    QualitySettings.vSyncCount = 1;
  }

  private void Update() {
    // Toggle Fullscreen
    if (Input.GetKeyDown(KeyCode.F11)) {
      bool fullScreen = !Screen.fullScreen;
      if (fullScreen) {
        _enterFullscreenMode();
      } else {
        _enterWindowMode();
      }

      Debug.Log("Toggle fullscreen: " + fullScreen);
    }
  }

  private static void _enterWindowMode() {
    Screen.SetResolution(1024, 786, false);
  }

  private static void _enterFullscreenMode() {
    Screen.SetResolution(
        Screen.currentResolution.width,
        Screen.currentResolution.height,
        true);
  }
}