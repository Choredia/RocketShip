using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)) //GETKEY: bas�l� tutuldu�u s�rece true...
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust); 
            //addrelative force ��nk� rotasyon de�i�ince cismin kendi y'sine g�re gitmesini istiyoruz d�nyan�n y'sine g�re de�il.
            //Vector3.up = (0,1,0) yazman�n k�sa yolu.
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }

        void ApplyRotation(float rotationThisFrame)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        }
    }
}