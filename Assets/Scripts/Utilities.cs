using System;
using System.Collections;
using UnityEngine;

public class Utilities : MonoBehaviour {
  public static IEnumerator Delay(Action action, float secs) {
    yield return new WaitForSeconds(secs);
    action.Invoke();
  }

  public static double DegreesToRadians(double angle) {
    return (Math.PI / 180) * angle;
  }

  public static double RadiansToDegree(double rad) {
    return (180 / Math.PI) * rad;
  }

  public static float limitToAmplitudeOf360(float rotation) {
    return rotation % 360;
  }
}