using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public GameObject winMenu;

    private void Start()
    {
        winMenu.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.CompareTag("Player"))
        {
            winMenu.SetActive(true);
            Time.timeScale = 0f;
            PauseMenu.isPaused = true;
        }
    }

}
