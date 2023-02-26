using FishNet.Object.Prediction;
using FishNet.Transporting;
using Networking;
using UnityEngine;

public class PlayerCspController : TickedNetworkBehaviour {
  private Rigidbody _body;
  private Acceleration _acceleration;

  private static readonly Color[] ClientColors = {
    Color.red,
    Color.blue,
    Color.green,
    Color.cyan,
    Color.yellow,
    Color.magenta,
    Color.white
  };

  private void Awake() {
    _body = GetComponent<Rigidbody>();
    _acceleration = GetComponent<Acceleration>();
  }

  public override void OnStartNetwork() {
    base.OnStartNetwork();

    // Assign player specific color
    GetComponentInChildren<SpriteRenderer>().color = ClientColors[OwnerId % ClientColors.Length];
  }

  protected override void OnNetworkTick() {
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
        Rotation = _body.rotation,
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