using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private int _levelLoadDelay;
    [SerializeField]
    private AudioClip _crashSound;
    [SerializeField]
    private AudioClip _winSound;
    [SerializeField]
    private ParticleSystem _winParticle;
    [SerializeField]
    private ParticleSystem _crashParticle;

    private AudioSource _audioSource;

    private bool _isTransitioning = false;
    private bool _collisionDisabled = false;

    #endregion

    #region Lifecycle
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    #endregion

    #region OnCollisionEnter
    private void OnCollisionEnter(Collision collision)
    {
        if(_isTransitioning || _collisionDisabled) { return; }

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly Tag");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("Fuel Tag");
                break;
            default:
                StartCrachSequence();
                break;
        }
    }

    #endregion

    #region Private
    private void StartCrachSequence()
    {
        
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_crashSound);
        _crashParticle.Play();
        GetComponent<RocketMovement>().enabled = false;
        Invoke("ReloadLevel", _levelLoadDelay);
        
    }

    private void StartSuccessSequence()
    {
      
        _isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_winSound);
        _winParticle.Play();
        GetComponent<RocketMovement>().enabled = false;
        Invoke("LoadNextLevel", _levelLoadDelay);
       
    }
    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneindex = currentSceneIndex + 1;
        if(nextSceneindex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneindex = 1;
        }
        SceneManager.LoadScene(nextSceneindex);
    }

    #endregion

    #region Debug

    private void RespondToDebugKeys()
    {
        DEBUGLoadNextLevel();
        DEBUGDisableCollision();
    }
    private void DEBUGLoadNextLevel()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    private void DEBUGDisableCollision()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _collisionDisabled = !_collisionDisabled; // Toggle Collision
            
        }
    }
    #endregion
}