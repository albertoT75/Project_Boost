
using UnityEngine;

public class CameraController : MonoBehaviour {

  public Transform playerTransform;

  public float xoffset;
  public float yoffset;

  void start()
  {
    playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
  }

  void Update()
  {

  }

  void FixedUpdate()
  {

  }

  void LateUpdate()
  {
    // we store current camera's position in variable temp - temporary position
    Vector3 temp = transform.position;

    // we set the camera's position x to be equal to the player's position x
    temp.x = playerTransform.position.x;
    temp.y = playerTransform.position.y;

    // this will add the offset value to the temporary camera x position
    temp.x += xoffset;
    temp.y += yoffset;

    // we set back the camera's temp position to the camera's current position
    transform.position = temp;
  }
}
