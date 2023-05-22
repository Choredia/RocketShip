using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 900f;
    [SerializeField] float rotationThrust = 100f;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (Input.GetKey(KeyCode.Space)) //GETKEY: basýlý tutulduðu sürece true...
        {
            ThrustSound();
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust); 
            //addrelative force çünkü rotasyon deðiþince cismin kendi y'sine göre gitmesini istiyoruz dünyanýn y'sine göre deðil.
            //Vector3.up = (0,1,0) yazmanýn kýsa yolu.
        }
        else
        {
            audioSource.Stop();
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
            rb.freezeRotation = true; //çarpýþýnca rotasyonu kendi kendine dönmesin diye. çarpýp rotasyonu deðiþince konrolü bozuluyor.
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
            rb.freezeRotation = false; //fiziðe geri döndük.
        }
    }

    void ThrustSound()
    {
        if (!audioSource.isPlaying) //baþýna koyduðumuz ! "not" anlamýna geliyor.yani if not playing...
        {
            audioSource.Play();
        }
    }
}
