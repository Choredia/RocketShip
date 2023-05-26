using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } //period s�f�rsa a�a��dakilerin hi�birini yapmadan d�n.

        float cycles =Time.time / period; //hangi saniyedeyken ka� adet tam dalga olu�mu�

        const float tau = Mathf.PI * 2; // const de�i�tirilemez de�er demek. tau = 2pi
        float rawSinWave = Mathf.Sin(cycles * tau); // -1 ve 1 aras� de�erler

        movementFactor = (rawSinWave + 1f) / 2f; //0 ve 1 aras�nda de�er gelmesi i�in -1 ve 1'i i�lemle pozitif yapt�k.

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;

    }
}
