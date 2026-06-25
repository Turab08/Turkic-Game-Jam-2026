using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;
    public Animator animator;

    void Awake()
    {
        Instance = this;
    }

    public void LoadPreviousLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void LoadThisLevel()
    {
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelIndex);
    }
}
