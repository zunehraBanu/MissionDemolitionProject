using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    // Hide the panel at start
    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    // Call this when the game ends
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    // Called when "Play Again" button is clicked
    public void PlayAgain()
	{
    Time.timeScale = 1f;  // resume normal game time
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

}


