using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
  // config params
  [SerializeField] [Range(0.1f, 10f)] float gameSpeed = 1f;
  [Header("Score Settings")]
  [SerializeField] TextMeshProUGUI scoreText;
  [SerializeField] int blockPoints;
  [SerializeField] bool autoPlay;

  // state
  [SerializeField] int score = 0;

  // getters
  public bool AutoPlayEnabled()
  {
    return autoPlay;
  }

  private void Awake()
  {
    // Gets all instances of this class
    int gameSessionCount = FindObjectsOfType<GameSession>().Length;

    // Finds out whether this is the first one created or not
    if (gameSessionCount > 1)
    {
      gameObject.SetActive(false);
      Destroy(gameObject);
    }
    else
    {
      DontDestroyOnLoad(gameObject);
    }
  }

  // Update is called once per frame
  private void Start()
  {
    scoreText.text = score.ToString();
  }

  void Update()
  {
    Time.timeScale = gameSpeed;
  }

  public void AddToScore()
  {
    score += blockPoints;
    scoreText.text = score.ToString();
  }

  public void ResetGame()
  {
    Destroy(gameObject);
  }
}
