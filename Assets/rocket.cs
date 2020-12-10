
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour
{
    [SerializeField] float rcsThurst = 100f;
    [SerializeField] float mainThurst = 100f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip Succes;
    [SerializeField] AudioClip Death;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem SuccesParticle;
    [SerializeField] ParticleSystem DeathParticle;

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Trancending };
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
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
        if (state != State.Alive) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
            
                break;
            case "Finish":
                StartSuccesSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        DeathParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(Death);
        Invoke("LoadCurrentLevel", 1f);
    }

    private void StartSuccesSequence()
    {
        state = State.Trancending;
        SuccesParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(Succes);
        Invoke("LoadNextLevel", 1f);
    }

    private void LoadCurrentLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }

    private  void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticle.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThurst);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticle.Play();
    }

    void RespondToRotateInput()
    {
        float rotationThisFrame = rcsThurst * Time.deltaTime;
        rigidBody.freezeRotation=true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward* rotationThisFrame);
         
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward* rotationThisFrame);
        }
        rigidBody.freezeRotation = false;
    }
}
    