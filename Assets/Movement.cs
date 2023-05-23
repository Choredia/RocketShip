using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 900f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    
    [SerializeField] ParticleSystem mainBoosterEffectLoop;
    [SerializeField] ParticleSystem leftBoosterEffect;
    [SerializeField] ParticleSystem rightBoosterEffect;

    Rigidbody rb;
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
        if (Input.GetKey(KeyCode.Space)) //GETKEY: bas�l� tutuldu�u s�rece true...
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterEffectLoop.Stop();
    }

    private void StartThrusting()
    {
        ThrustSound();
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);

        if (!mainBoosterEffectLoop.isPlaying)
        {
            mainBoosterEffectLoop.Play();
        }
        //addrelative force ��nk� rotasyon de�i�ince cismin kendi y'sine g�re gitmesini istiyoruz d�nyan�n y'sine g�re de�il.
        //Vector3.up = (0,1,0) yazman�n k�sa yolu.
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotateEffect();
        }
    }

    void ApplyRotation(float rotationThisFrame) //parametreye herhangi bir isim verdik. sonra ApplyRotation(-/+ rotationThrust); �eklinde �a��rarak rotationThisFrame de�i�kenine -/+ rotationThrust atam�� olduk.
    {
        rb.freezeRotation = true; //�arp���nca rotasyonu kendi kendine d�nmesin diye. �arp�p rotasyonu de�i�ince konrol� bozuluyor.
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false; //fizi�e geri d�nd�k.
    }

    void RotateRight()
    {
        if (!leftBoosterEffect.isPlaying)
        {
            leftBoosterEffect.Play();
        }
        ApplyRotation(-rotationThrust); //parametre --> rotationThisFrame
    }

    void RotateLeft()
    {
        if (!rightBoosterEffect.isPlaying)
        {
            rightBoosterEffect.Play();
        }
        ApplyRotation(rotationThrust); //parametre --> rotationThisFrame
    }

    private void StopRotateEffect()
    {
        leftBoosterEffect.Stop();
        rightBoosterEffect.Stop();
    }

    void ThrustSound()
    {
        if (!audioSource.isPlaying) //ba��na koydu�umuz ! "not" anlam�na geliyor.yani if not playing...
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }
}


