using FishNet;
using UnityEngine;

namespace Networking {
  public class NetworkController : MonoBehaviour {
    private void Update() {
      if (Input.GetKeyDown(KeyCode.Alpha8)) {
        InstanceFinder.ServerManager.StartConnection();
        InstanceFinder.ClientManager.StartConnection();
        Debug.Log("Host started");
      }

      if (Input.GetKeyDown(KeyCode.Alpha9)) {
        InstanceFinder.ClientManager.StartConnection();
        Debug.Log("Client started");
      }

      if (Input.GetKeyDown(KeyCode.Alpha0)) {
        InstanceFinder.ServerManager.StartConnection();
        Debug.Log("Server started");
      }
    }
  }
}