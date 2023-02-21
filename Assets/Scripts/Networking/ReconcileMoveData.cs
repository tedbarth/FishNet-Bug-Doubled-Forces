using FishNet.Object.Prediction;
using UnityEngine;

namespace Networking {
  public struct ReconcileMoveData : IReconcileData {
    public Vector2 Position;
    public float Rotation;
    public Vector2 LinearVelocity; // signed
    public float AngularVelocity; // signed
    public float LinearAcceleration; // signed
    public float AngularAcceleration; // signed

    private uint _tick;

    public void Dispose() {
      // Nothing to dispose
    }

    public uint GetTick() => _tick;
    public void SetTick(uint value) => _tick = value;
  }
}