using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
  // config params
  [SerializeField] [Range(0.1f, 10f)] float gameSpeed = 1f;
  [Header("Score Settings")]
  [SerializeField] TextMeshProUGUI scoreText;
  [SerializeField] int blockPoints;

  // state
  [SerializeField] int score = 0;
  // Update is called once per frame
  private void Start() {
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
}
