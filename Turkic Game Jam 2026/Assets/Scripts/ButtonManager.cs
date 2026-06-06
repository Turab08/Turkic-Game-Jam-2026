using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayButton"))
        {
             SceneManager.LoadScene("Game");
        }
        else if (other.gameObject.CompareTag("QuitButton"))
        {
            Application.Quit();
        }
    }
}
