using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    float horizontalInput;
    [SerializeField] float rocketForce;
    [SerializeField] float rotateSpeed;

    Rigidbody rocketRb;

    AudioSource audioSource;
    [SerializeField] AudioClip thrustSound;

    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;


    void Start()
    {
        rocketRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        HandleThrust();
        HandleRotation();
    }

    void HandleThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }


    private void StartThrusting()
    {
        rocketRb.AddRelativeForce(rocketForce * Time.deltaTime * Vector3.up);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSound);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    void HandleRotation()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0)
        {
            leftBooster.Play();
        }
        else if (horizontalInput > 0)
        {
            rightBooster.Play();
        }

        rocketRb.freezeRotation = true;
        transform.Rotate(Vector3.right, rotateSpeed * horizontalInput * Time.deltaTime);
        rocketRb.freezeRotation = false;
    }
}
