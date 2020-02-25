using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

  [SerializeField] Vector3 movementVector;

  // todo remove from inpector later
  [Range(0,1)][SerializeField]float movementFactor; // o not moved, 1 fully moved

  Vector3 startingPos;

	// Use this for initialization
	void Start ()
  {
    startingPos = transform.position; // where the object is
	}

	// Update is called once per frame
	void Update ()
  {
    Vector3 offset = movementVector * movementFactor;
    transform.position = startingPos + offset;
	}
}
