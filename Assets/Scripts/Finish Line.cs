using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private float _sceneReloadDelay = 2f;
    [SerializeField]
    private ParticleSystem _finishEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            _finishEffect.Play();
            GetComponent<AudioSource>().Play();

            Player player = GameObject.Find("Player").GetComponent<Player>();
            player.DisableControl();

            Invoke("ReloadScene", _sceneReloadDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
