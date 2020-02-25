using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

  [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
  [SerializeField] float period = 2f;

  // todo remove from inpector later
  float movementFactor; // o not moved, 1 fully moved
  Vector3 startingPos;

	// Use this for initialization
	void Start ()
  {
    startingPos = transform.position; // where the object is
	}

	// Update is called once per frame
	void Update ()
  {
    if (period <= Mathf.Epsilon) { return; }// protect against period is zero
    float cycles = Time.time / period; // grows continually from 0

    const float tau = Mathf.PI * 2f; // about 6.28, 1 cycle of a circle
    float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

    movementFactor = rawSinWave / 2f + 0.5f;
    Vector3 offset = movementVector * movementFactor;
    transform.position = startingPos + offset;
	}
}
