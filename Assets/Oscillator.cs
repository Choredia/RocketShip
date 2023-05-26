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
        if (period <= Mathf.Epsilon) { return; } //period sýfýrsa aþaðýdakilerin hiçbirini yapmadan dön.

        float cycles =Time.time / period; //hangi saniyedeyken kaç adet tam dalga oluþmuþ

        const float tau = Mathf.PI * 2; // const deðiþtirilemez deðer demek. tau = 2pi
        float rawSinWave = Mathf.Sin(cycles * tau); // -1 ve 1 arasý deðerler

        movementFactor = (rawSinWave + 1f) / 2f; //0 ve 1 arasýnda deðer gelmesi için -1 ve 1'i iþlemle pozitif yaptýk.

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;

    }
}
