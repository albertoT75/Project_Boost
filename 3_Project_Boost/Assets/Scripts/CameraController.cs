
using UnityEngine;

public class CameraController : MonoBehaviour {

  public Transform target;

  public float smoothSpeed = 0.125f;
  private Vector3 offset;


	void fixedUpdate ()
  {
    Vector3 desiredPosition = target.position + offset;
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);
		transform.position = smoothedPosition;
	}
}
