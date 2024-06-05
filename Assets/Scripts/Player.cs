using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D _rb2d;
    private float _sceneReloadDelay = 2f;

    [SerializeField]
    private ParticleSystem _crashEffect;

    [SerializeField]
    private ParticleSystem _slidingEffect;

    private SurfaceEffector2D _surfaceEffector2D;

    [SerializeField]
    private AudioClip _crashSFX;

    private float _playerSpeed = 10;

    private bool _canMove = true;
    private bool _playCrashSFX = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            RotatePlayerMovement();
            SpeedBoostMovement();
        }
    }

    // Crash Detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            DisableControl();
            _crashEffect.Play();
            if (!_playCrashSFX)
            {
                GetComponent<AudioSource>().PlayOneShot(_crashSFX);
                _playCrashSFX = true;
            }
            Invoke("ReloadScene", _sceneReloadDelay);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            _slidingEffect.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            _slidingEffect.Stop();
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    private void RotatePlayerMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        _rb2d.AddTorque(horizontalInput * 10);
    }

    private void SpeedBoostMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        _surfaceEffector2D.speed = _playerSpeed + verticalInput * 10;
    }

    public void DisableControl()
    {
        _canMove = false;
    }
}
