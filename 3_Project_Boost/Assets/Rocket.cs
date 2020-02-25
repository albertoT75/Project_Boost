﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

  [SerializeField] float rcsThrust = 150f;
  [SerializeField] float mainThrust = 100f;
  [SerializeField] AudioClip mainEngine;
  [SerializeField] AudioClip deathSound;
  [SerializeField] AudioClip levelLoad;

  Rigidbody rigidBody;
  AudioSource audioSource;

  enum State { Alive, Dying, Transcending }
  State state = State.Alive;


	// Use this for initialization
	void Start () {
    rigidBody = GetComponent<Rigidbody>();
    rigidBody.constraints = RigidbodyConstraints.FreezePositionZ & RigidbodyConstraints.FreezeRotationZ;
    audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
  {
    if (state == State.Alive)
    {
      RespondToThrustInput();
  		RespondToRotateInput();
    }
	}

  void OnCollisionEnter(Collision collision)
  {

    if (state != State.Alive) { return; } // ignore collision

    switch (collision.gameObject.tag)
    {
      case "Friendly":
        print ("OK");
        break;
      case "Finish":
        StartSuccessSequence();
        break;
      default:
        StartDeathSequence();
        break;
    }
  }

  private void StartSuccessSequence()
  {
    state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(levelLoad);
        Invoke("LoadNextLevel", 1f);
  }

  private void StartDeathSequence()
  {
    state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        Invoke("LoadFirstLevel", 3f);
  }

  private void LoadNextLevel()
  {
    SceneManager.LoadScene(1);
  }

  private void LoadFirstLevel()
  {
    SceneManager.LoadScene(0);
  }

  private void RespondToThrustInput()
  {

    if (Input.GetKey(KeyCode.Space))
    {
      ApplyThrust();
    }
    else
    {
      audioSource.Stop();
    }
  }

  private void ApplyThrust()
  {
    rigidBody.AddRelativeForce(Vector3.up * mainThrust);
      if (!audioSource.isPlaying)
      {
        audioSource.PlayOneShot(mainEngine);
      }
  }


  private void RespondToRotateInput()
  {
    rigidBody.freezeRotation = true; // take manual control of rotation
    float rotationThisFrame = rcsThrust * Time.deltaTime;

    if (Input.GetKey(KeyCode.A))
    {
      transform.Rotate(-Vector3.forward * rotationThisFrame);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      transform.Rotate(Vector3.forward * rotationThisFrame);
    }
    rigidBody.constraints = RigidbodyConstraints.FreezeRotationX
        | RigidbodyConstraints.FreezeRotationY
        | RigidbodyConstraints.FreezePositionZ; // resume physics control of rotation
  }

}


