using FishNet.Object.Prediction;
using FishNet.Transporting;
using Networking;
using UnityEngine;

public class PlayerCspController : TickedNetworkBehaviour {
  private Rigidbody2D _body;
  private Acceleration _acceleration;

  private void Awake() {
    _body = GetComponent<Rigidbody2D>();
    _acceleration = GetComponent<Acceleration>();
  }

  protected override void OnNetworkTick() {
    Debug.Log($"Inertia: {_body.inertia}, Mass: {_body.mass}");

    if (IsOwner) {
      Reconcile(default, false);

      // Where input is read and values for the forces to apply are calculated (no forces are applied here)
      HandleAxesInput();
    }

    if (IsServer) {
      Move(default, true);
    }
  }

  protected override void OnNetworkPostTick() {
    if (IsServer) {
      ReconcileMoveData reconcileMoveData = new ReconcileMoveData {
        Position = transform.position,
        Rotation = Utilities.limitToAmplitudeOf360(_body.rotation),
        LinearVelocity = _body.velocity,
        AngularVelocity = _body.angularVelocity,
        LinearAcceleration = _acceleration.linearAcceleration,
        AngularAcceleration = _acceleration.angularAcceleration
      };
      Reconcile(reconcileMoveData, true);
    }
  }

  private void HandleAxesInput() {
    float currentHorizontalInput = Input.GetAxisRaw("Horizontal");
    float currentVerticalInput = Input.GetAxisRaw("Vertical");

    MoveData md = default;
    md.Horizontal = currentHorizontalInput;
    md.Vertical = currentVerticalInput;

    // Will be replicated on the server
    Move(md, false);
  }

  [Replicate]
  private void Move(
    MoveData moveData,
    bool asServer,
    Channel channel = Channel.Unreliable,
    bool replaying = false) {
    _acceleration.SetInput(moveData.Vertical, moveData.Horizontal);
  }

  [Reconcile]
  private void Reconcile(
    ReconcileMoveData recData,
    bool asServer,
    Channel channel = Channel.Unreliable) {

    // Deactivated to not resync, but to see the differences in resulting speed of client and server instead
    //transform.position = recData.Position;
    //_body.rotation = recData.Rotation;
    //_body.velocity = recData.LinearVelocity; // FIXME: To scalar value in flight direction
    //_body.angularVelocity = recData.AngularVelocity;
    //_acceleration.linearAcceleration = recData.LinearAcceleration;
    //_acceleration.angularAcceleration = recData.AngularAcceleration;
  }
}