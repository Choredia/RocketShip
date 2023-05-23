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
        if (isTransitioning) { return; } //isTrantioning true ise a�a��ya ge�meden d�n dedik. b�ylece 2.kez �arp��ma yapmayacak.�arpmadaysa tekrar �arpmayacak.
        //if isTransitioning == true da yzabilirdik. ba��na ! koymak 'not' true/false anlam� ta��rd�. (!isTransitioning ---> de�er normalde true ama not truedan false olacak. o da ifi �al��t�rmaz.
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //daha temiz kod i�in sahne indexini de�i�kene atad�k.
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //daha temiz kod i�in sahne indexini de�i�kene atad�k.
        SceneManager.LoadScene(currentSceneIndex);
        //builddeki scene indexini al�r ve o sahneyi d�nd�r�r.
    }
}
