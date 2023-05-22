using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    string hit;
    void OnCollisionEnter(Collision other)
    {
        hit = other.gameObject.tag;
        switch (other.gameObject.tag) 
        {
            
            case "friendly":
                Debug.Log("hit a friendly object");
                break;
            case "fuel":
                Debug.Log("add fuel");
                break;
            case "finish":
                Debug.Log("you won");
                LoadNextLevel();
                break;
            default:
                //Debug.Log("hit an obstacle");
                //SceneManager.LoadScene("SandBox");
                ReloadLevel();
                break;
        }
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
