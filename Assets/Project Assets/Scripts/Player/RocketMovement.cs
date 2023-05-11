using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{

    
    [SerializeField]
    private float _goUpForce = 3;
    [SerializeField]
    private float _rotateForce = 3;
    [SerializeField]
    private AudioClip _mainEngineSound;
    [SerializeField]
    private ParticleSystem _thrustRocketParticle;
    [SerializeField]
    private ParticleSystem _leftRocketParticle;
    [SerializeField]
    private ParticleSystem _rightRocketParticle;
    [SerializeField]
    private Light _spotlightThrust;

    private AudioSource _audioSource;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {

        if(Input.GetKey(KeyCode.Space) == true)
        {
            StartThrust();

        }
        else
        {
            StopThrust();
        }

    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateToLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateToRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StartThrust()
    {
        _rb.AddRelativeForce(Vector3.up * _goUpForce * Time.deltaTime);
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_mainEngineSound);
        }
        if (!_thrustRocketParticle.isPlaying)
        {
            _spotlightThrust.enabled = true;
            _thrustRocketParticle.Play();
        }
    }

     private void StopThrust()
     {
        _spotlightThrust.enabled = false;
        _thrustRocketParticle.Stop();
        _audioSource.Stop();
     }

    private void RotateToRight()
    {
        RotateRocket(_rotateForce);
        if (!_leftRocketParticle.isPlaying)
        {
            _leftRocketParticle.Play();
        }
    }

    private void RotateToLeft()
    {
        RotateRocket(-_rotateForce);
        if (!_rightRocketParticle.isPlaying)
        {
            _rightRocketParticle.Play();
        }
    }

    private void StopRotating()
    {
        _leftRocketParticle.Stop();
        _rightRocketParticle.Stop();
    }

    private void RotateRocket(float rotateThisFrame)
    {
        _rb.freezeRotation = true;  // freeze the rotatation so we can manualy rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateThisFrame);
        _rb.freezeRotation = false; // unfreeze rotation so the physics can take over
    }




}
