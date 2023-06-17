using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void ExitGame(bool save = false)
    {
        Root.saveOnExit = save;
        Application.Quit();
    }

    public void RestartGame()
    {
        Root.SaveManager.DeleteData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause(GameObject panel)
    {
        Time.timeScale = 0f;
        panel.SetActive(true);
    }

    public void UnPause(GameObject panel)
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }
}
