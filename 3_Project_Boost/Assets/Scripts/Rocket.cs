using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

  [SerializeField] float rcsThrust = 150f;
  [SerializeField] float mainThrust = 100f;
  [SerializeField] float levelLoadDelay = 2f;

  [SerializeField] AudioClip mainEngine;
  [SerializeField] AudioClip deathSound;
  [SerializeField] AudioClip levelLoad;
  [SerializeField] AudioClip tractorBeam;

  [SerializeField] ParticleSystem mainEngineParticles;
  [SerializeField] ParticleSystem successParticles;
  [SerializeField] ParticleSystem deathParticles;
  [SerializeField] ParticleSystem beamParticles;



  Rigidbody rigidBody;
  AudioSource audioSource;

  bool isTransictioning = false;

  bool collisionsDisabled = false;

	// Use this for initialization
	void Start () {
    rigidBody = GetComponent<Rigidbody>();
    rigidBody.constraints = RigidbodyConstraints.FreezePositionZ & RigidbodyConstraints.FreezeRotationZ;
    audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
  {

    if (!isTransictioning)
    {
      RespondToBeamImput();
      RespondToThrustInput();
  		RespondToRotateInput();
    }
    if (Debug.isDebugBuild)
    {
      RespondToDebugKeys();
    }
	}

  private void RespondToDebugKeys()
  {
    if (Input.GetKeyDown(KeyCode.L))
    {
      LoadNextLevel();
    }
    else if (Input.GetKeyDown(KeyCode.C))
    {
      collisionsDisabled = !collisionsDisabled; //toggle
    }
  }

  void OnCollisionEnter(Collision collision)
  {

    if (isTransictioning || collisionsDisabled)  { return; }

    switch (collision.gameObject.tag)
    {
      case "Friendly":
        print ("OK");
        break;
      case "Finish":
        StartSuccessSequence();
        break;
      // to do case "human"
      //   StartKidnapSequence();
        break;
      default:
        StartDeathSequence();
        break;
    }
  }

  // to do kidnap sequence

  private void StartSuccessSequence()
  {
    isTransictioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(levelLoad);
    successParticles.Play();
    Invoke("LoadNextLevel", levelLoadDelay);
  }

  private void StartDeathSequence()
  {
    isTransictioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(deathSound);
    Invoke("DeathParticles", 1f);
    mainEngineParticles.Stop();

    // destroy Ship
    Invoke("LoadFirstLevel", levelLoadDelay);
  }

  private void DeathParticles()
  {
    deathParticles.Play();
  }

  private void LoadNextLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;

    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIndex = 0; // loop back to start
    }
    SceneManager.LoadScene(nextSceneIndex);
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
      StopApplyingThrust();
    }
  }

  private void StopApplyingThrust()
  {
    audioSource.Stop();
    mainEngineParticles.Stop();
  }

  private void RespondToBeamImput()
  {
    if (Input.GetKey(KeyCode.X))
    {
      ActivateBeam();
      // to do :tract human
    }
    else
    {
      StopBeaming();
    }
  }

  private void StopBeaming()
  {

    beamParticles.Stop();
  }

  private void ActivateBeam()
  {
    if (!audioSource.isPlaying)
    {
      audioSource.PlayOneShot(tractorBeam);
    }
    beamParticles.Play();

  }

  private void ApplyThrust()
  {
    rigidBody.AddRelativeForce(Vector3.up * mainThrust);
      if (!audioSource.isPlaying)
      {
        audioSource.PlayOneShot(mainEngine);
      }
      mainEngineParticles.Play();
  }


  private void RespondToRotateInput()
  {
    rigidBody.freezeRotation = true; // take manual control of rotation
    float rotationThisFrame = rcsThrust * Time.deltaTime;

    if (Input.GetKey(KeyCode.A))
    {
      transform.Rotate(Vector3.forward * rotationThisFrame);
    }
    else if (Input.GetKey(KeyCode.D))
    {
      transform.Rotate(-Vector3.forward * rotationThisFrame);
    }
    rigidBody.constraints = RigidbodyConstraints.FreezeRotationX
        | RigidbodyConstraints.FreezeRotationY
        | RigidbodyConstraints.FreezePositionZ; // resume physics control of rotation
  }


}


