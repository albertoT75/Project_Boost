using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

  [SerializeField] float rcsThrust = 150f;
  [SerializeField] float mainThrust = 100f;
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
      Thrust();
  		Rotate();
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
        state = State.Transcending;
        Invoke("LoadNextLevel", 1f);
        break;
      default:
        state = State.Dying;
        Invoke("LoadFirstLevel", 1f);
        break;
    }
  }

  private void LoadNextLevel()
  {
    SceneManager.LoadScene(1);
  }

  private void LoadFirstLevel()
  {
    SceneManager.LoadScene(0);
  }

  private void Thrust()
  {

    if (Input.GetKey(KeyCode.Space))
    {
      rigidBody.AddRelativeForce(Vector3.up * mainThrust);
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


