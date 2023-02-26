using Networking;
using UnityEngine;

public class Acceleration : TickedNetworkBehaviour {
  [SerializeField] private float linearAccelerationForce;
  [SerializeField] private float angularAccelerationForce;

  public float linearAcceleration = 100f;
  public float angularAcceleration = 1000f;

  private Rigidbody _body;

  // Start is called before the first frame update
  private void Awake() {
    _body = GetComponent<Rigidbody>();
  }

  protected override void OnNetworkTick() {
    if (linearAccelerationForce != 0) {
      _body.AddForce(transform.up * linearAccelerationForce, ForceMode.Force);
    }

    if (angularAccelerationForce != 0) {
      Debug.Log($"Acc Rotation: {angularAccelerationForce}");
      _body.AddTorque(Vector3.forward * angularAccelerationForce, ForceMode.Force);
    }
  }

  public void SetInput(float verticalInput, float horizontalInput) {
    float mass = _body.mass;
    linearAccelerationForce = linearAcceleration * mass * verticalInput;
    angularAccelerationForce = angularAcceleration * mass * -horizontalInput;
  }
}