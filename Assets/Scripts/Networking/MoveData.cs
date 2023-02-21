using FishNet.Object.Prediction;

namespace Networking {
  public struct MoveData : IReplicateData {
    public float Horizontal; // normalized, signed
    public float Vertical; // normalized, signed

    private uint _tick;

    public void Dispose() {
      // Nothing to dispose yet
    }

    public uint GetTick() => _tick;
    public void SetTick(uint value) => _tick = value;
  }
}