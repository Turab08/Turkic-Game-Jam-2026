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
            LevelLoader.Instance.LoadNextLevel();
            //StartCoroutine(LoadScene());
        }
        else if (other.gameObject.CompareTag("QuitButton"))
        {
            Application.Quit();
        }
    }

    public void Retry()
    {
        LevelLoader.Instance.LoadThisLevel();
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void Pause()
    {
        EventManager.GamePaused();
    }
    public void Resume()
    {
        EventManager.GameResume();
    }

    IEnumerator LoadScene()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Instance.ingredientAdded);

        yield return new WaitForSeconds(0.3f);

        SceneManager.LoadScene("Game");
    }
}
