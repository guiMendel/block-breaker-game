using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  // stored references
  GameSession gameSession;

  private void Start()
  {
    gameSession = FindObjectOfType<GameSession>();
  }

  public void LoadNextScene()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    SceneManager.LoadScene(currentSceneIndex + 1);
  }

  public void LoadStartScene()
  {
    SceneManager.LoadScene(0);
    gameSession.ResetGame();
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
