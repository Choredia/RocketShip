using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

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
                break;
            default:
                Debug.Log("hit an obstacle");
                break;
        }
    }
}
