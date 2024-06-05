using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D _rb2d;
    private float _sceneReloadDelay = 0.5f;

    [SerializeField]
    private ParticleSystem _crashEffect;

    [SerializeField]
    private ParticleSystem _slidingEffect;

    private SurfaceEffector2D _surfaceEffector2D;

    private float _playerSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayerMovement();
        SpeedBoostMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            _crashEffect.Play();
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
}
