using FishNet;
using TMPro;
using UnityEngine;

namespace Networking {
  public class NetworkModeStatus : MonoBehaviour {
    private string _mode;
    private TMP_Text _tmpText;

    private void Awake() {
      _tmpText = gameObject.GetComponent<TMP_Text>();
    }

    private void Update() {
      string newMode = GetNetworkMode();
      if (_mode != newMode) {
        _mode = newMode;
        _tmpText.SetText(newMode);
      }
    }

    private string GetNetworkMode() {
      if (InstanceFinder.IsHost) {
        return "Host";
      }

      if (InstanceFinder.IsServer) {
        return "Server";
      }

      if (InstanceFinder.IsClient) {
        return "Client";
      }

      return "Network mode undefined\n"
             + "[8] - Host\n"
             + "[9] - Client\n"
             + "[0] - Server";
    }
  }
}