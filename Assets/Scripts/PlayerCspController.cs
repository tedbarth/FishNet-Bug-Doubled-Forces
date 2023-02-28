using FishNet.Object.Prediction;
using FishNet.Transporting;
using Networking;
using UnityEngine;

public class PlayerCspController : TickedNetworkBehaviour {
  private Rigidbody _body;

  public float linearAcceleration = 20f;
  public float angularAcceleration = 5f;

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
  }

  public override void OnStartNetwork() {
    base.OnStartNetwork();

    // Assign player specific color
    GetComponentInChildren<SpriteRenderer>().color = ClientColors[OwnerId % ClientColors.Length];
  }

  protected override void OnNetworkTick() {
    Debug.Log("OnTick (Start)");
    if (IsOwner) {
      Reconcile(default, false);

      // Where input is read and values for the forces to apply are calculated (no forces are applied here)
      HandleAxesInput();
    }

    if (IsServer) {
      Move(default, true);
    }
    Debug.Log("OnTick (End)");
  }

  protected override void OnNetworkPostTick() {
    Debug.Log("OnPostTick (Start)");
    if (IsServer) {
      ReconcileMoveData reconcileMoveData = new ReconcileMoveData {
        Position = transform.position,
        Rotation = _body.rotation,
        LinearVelocity = _body.velocity,
        AngularVelocity = _body.angularVelocity,
        LinearAcceleration = linearAcceleration,
        AngularAcceleration = angularAcceleration
      };
      Reconcile(reconcileMoveData, true);
    }
    Debug.Log("OnPostTick (Start)");
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

    float mass = _body.mass;
    float linearAccelerationForce = linearAcceleration * mass * moveData.Vertical;
    float angularAccelerationForce = angularAcceleration * mass * -moveData.Horizontal;

    if (linearAccelerationForce != 0) {
      _body.AddForce(transform.up * linearAccelerationForce, ForceMode.Force);
    }

    if (angularAccelerationForce != 0) {
      _body.AddTorque(Vector3.forward * angularAccelerationForce, ForceMode.Force);
    }
  }

  [Reconcile]
  private void Reconcile(
    ReconcileMoveData recData,
    bool asServer,
    Channel channel = Channel.Unreliable) {

    // Deactivated to not resync, but to see the differences in resulting speed of client and server instead
    transform.position = recData.Position;
    _body.rotation = recData.Rotation;
    _body.velocity = recData.LinearVelocity;
    _body.angularVelocity = recData.AngularVelocity;
    Debug.Log($"Reconcile() // PlayerCspController");
  }
}