using FishNet.Object;

namespace Networking {
  public abstract class TickedNetworkBehaviour : NetworkBehaviour {
    public override void OnStartNetwork() {
      base.OnStartNetwork();
      if (IsServer || IsClient) {
        TimeManager.OnTick += OnNetworkTick;
        TimeManager.OnPostTick += OnNetworkPostTick;
      }
    }

    public override void OnStopNetwork() {
      base.OnStopNetwork();
      if (TimeManager) {
        TimeManager.OnTick -= OnNetworkTick;
        TimeManager.OnPostTick -= OnNetworkPostTick;
      }
    }

    protected virtual void OnNetworkTick() {
      // Nothing to do yet
    }

    protected virtual void OnNetworkPostTick() {
      // Nothing to do yet
    }
  }
}