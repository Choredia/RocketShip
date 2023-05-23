using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2.0f;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem successEffect;
    [SerializeField] ParticleSystem crashEffect;

    AudioSource audioSource;

    bool isTransitioning;

    private void Start()
    {
        isTransitioning = false;
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; } //isTrantioning true ise aþaðýya geçmeden dön dedik. böylece 2.kez çarpýþma yapmayacak.çarpmadaysa tekrar çarpmayacak.
        //if isTransitioning == true da yzabilirdik. baþýna ! koymak 'not' true/false anlamý taþýrdý. (!isTransitioning ---> deðer normalde true ama not truedan false olacak. o da ifi çalýþtýrmaz.
        switch (other.gameObject.tag) 
        {
            
            case "friendly":
                Debug.Log("hit a friendly object");
                break;
            case "fuel":
                Debug.Log("add fuel");
                break;
            case "finish":
                StartSuccessSequence();
                break;
            default:
                //Debug.Log("hit an obstacle");
                //SceneManager.LoadScene("SandBox");
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successEffect.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //daha temiz kod için sahne indexini deðiþkene atadýk.
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //daha temiz kod için sahne indexini deðiþkene atadýk.
        SceneManager.LoadScene(currentSceneIndex);
        //builddeki scene indexini alýr ve o sahneyi döndürür.
    }
}
