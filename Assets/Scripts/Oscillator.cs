using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] Vector3 movementVector;
    float multiplier;

    [SerializeField] float period;
    const float TAU = Mathf.PI * 2;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        OscillateObstsacle();
    }

    private void OscillateObstsacle()
    {
        //calculate sine value
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        float rawSinValue = Mathf.Sin(TAU * cycles);
        multiplier = (rawSinValue + 1) / 2;

        //adjust position of obstacle accordingly
        Vector3 offset = movementVector * multiplier;
        transform.position = startPosition + offset;
    }
}
