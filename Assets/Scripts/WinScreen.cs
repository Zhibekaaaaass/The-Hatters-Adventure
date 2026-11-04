
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject winPanel;

    void Start()
    {
        Time.timeScale = 1f;

        if (winPanel != null)
            winPanel.SetActive(false);
        else
            Debug.LogError("Win Panel is not assigned!");
    }

    public void ShowWin()
    {
        Debug.Log("ShowWin called!");

        if (winPanel == null)
        {
            Debug.LogError("Win Panel is missing!");
            return;
        }

        winPanel.SetActive(true);
        Debug.Log("WinPanel active: " + winPanel.activeInHierarchy);

        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
