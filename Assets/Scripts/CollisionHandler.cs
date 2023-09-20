using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentLevelIndex;
    AudioSource audioSource;
    [SerializeField] float delay;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;

    bool isTransitioning = false;
    bool isCollidable = true;
    private void Awake()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HandleCheatKeys();
    }

    private void HandleCheatKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //toggle collision detecting
            isCollidable = !isCollidable;
            Debug.Log($"Detecting collisions: {isCollidable} ");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || !isCollidable) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //Debug.Log("You have collided with a friendly object");
                break;

            case "Finish":
                StartWinSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }
    void StartWinSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        //play win VFX

        //play win particle effect
        successParticle.Play();
        
        GetComponent<RocketController>().enabled = false;
        Invoke(nameof(LoadNextLevel), delay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        //play crash VFX
        
        //play crash particle effect
        crashParticle.Play();
        
        GetComponent<RocketController>().enabled = false;
        Invoke(nameof(ReloadLevel), 1f);
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(currentLevelIndex);
    }

    void LoadNextLevel()
    {
        int nextLevelIndex = currentLevelIndex + 1;
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }
}
