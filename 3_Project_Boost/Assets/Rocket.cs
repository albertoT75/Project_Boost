﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

  Rigidbody rigidBody;
  AudioSource audioSource;


	// Use this for initialization
	void Start () {
    rigidBody = GetComponent<Rigidbody>();
    rigidBody.constraints = RigidbodyConstraints.FreezePositionZ & RigidbodyConstraints.FreezeRotationZ;
    audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update() {
    Thrust();
		Rotate();
	}

  private void Thrust()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      rigidBody.AddRelativeForce(Vector3.up);
      if (!audioSource.isPlaying)
      {
        audioSource.Play();
      }
    }
    else
    {
        audioSource.Stop();
    }
  }


  private void Rotate()
  {
    rigidBody.freezeRotation = true; // take manual control of rotation
    if (Input.GetKey(KeyCode.A))
    {
      transform.Rotate(-Vector3.forward);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      transform.Rotate(Vector3.forward);
    }
    rigidBody.constraints = RigidbodyConstraints.FreezeRotationX
        | RigidbodyConstraints.FreezeRotationY
        | RigidbodyConstraints.FreezePositionZ; // resume physics control of rotation
  }

}


