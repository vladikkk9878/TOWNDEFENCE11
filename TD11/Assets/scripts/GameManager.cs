using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Панели состояний")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject losePanel; 

    private void Start()
    {

        Time.timeScale = 1f;

        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }

        if (startPanel != null)
        {
            startPanel.SetActive(true);
        }

        if (mainPanel != null)
        {
            mainPanel.SetActive(false);
        }
    }

    public void StartGame()
    {
        if (startPanel != null)
        {
            startPanel.SetActive(false);
        }

        if (mainPanel != null)
        {
            mainPanel.SetActive(true);
        }

        Debug.Log("Игра началась!");
    }

    public void TriggerGameOver()
    {
        Debug.LogError("Менеджер зафиксировал поражение!");

        if (mainPanel != null)
        {
            mainPanel.SetActive(false);
        }

        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
