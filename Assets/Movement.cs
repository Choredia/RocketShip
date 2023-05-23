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
        if (Input.GetKey(KeyCode.Space)) //GETKEY: basýlý tutulduðu sürece true...
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
        //addrelative force çünkü rotasyon deðiþince cismin kendi y'sine göre gitmesini istiyoruz dünyanýn y'sine göre deðil.
        //Vector3.up = (0,1,0) yazmanýn kýsa yolu.
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

    void ApplyRotation(float rotationThisFrame) //parametreye herhangi bir isim verdik. sonra ApplyRotation(-/+ rotationThrust); þeklinde çaðýrarak rotationThisFrame deðiþkenine -/+ rotationThrust atamýþ olduk.
    {
        rb.freezeRotation = true; //çarpýþýnca rotasyonu kendi kendine dönmesin diye. çarpýp rotasyonu deðiþince konrolü bozuluyor.
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false; //fiziðe geri döndük.
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
        if (!audioSource.isPlaying) //baþýna koyduðumuz ! "not" anlamýna geliyor.yani if not playing...
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }
}


