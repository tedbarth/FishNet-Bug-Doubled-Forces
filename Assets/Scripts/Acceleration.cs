using System;
using Networking;
using UnityEngine;

public class Acceleration : TickedNetworkBehaviour {
  [SerializeField] private float linearAccelerationForce;
  [SerializeField] private float angularAccelerationForce;

  public float maxLinearVelocity = 200; // Units per second
  public float maxAngularVelocity = 100; // Degrees per second
  public float linearAcceleration = 100f;
  public float angularAcceleration = 1000f;

  private Rigidbody2D _body;

  // Start is called before the first frame update
  private void Awake() {
    _body = GetComponent<Rigidbody2D>();
  }

  protected override void OnNetworkTick() {
    if (linearAccelerationForce != 0) {
      _body.AddForce(transform.up * linearAccelerationForce);
    }

    if (angularAccelerationForce != 0) {
      Debug.Log($"Acc Rotation: {angularAccelerationForce}");
      _body.AddTorque(angularAccelerationForce);
    }

    _body.velocity = Vector3.ClampMagnitude(_body.velocity, maxLinearVelocity);
    _body.angularVelocity = Math.Clamp(Math.Abs(_body.angularVelocity), 0, maxAngularVelocity)
                            * Math.Sign(_body.angularVelocity);
    _body.rotation = Utilities.limitToAmplitudeOf360(_body.rotation);
  }

  public void SetInput(float verticalInput, float horizontalInput) {
    float mass = _body.mass;
    linearAccelerationForce = linearAcceleration * mass * verticalInput;
    angularAccelerationForce = angularAcceleration * mass * -horizontalInput;
  }
}