using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
  [SerializeField] AudioClip breakSound;

  // cached references
  Level level;
  GameStatus gameStatus;

  private void Start()
  {
    gameStatus = FindObjectOfType<GameStatus>();
    level = FindObjectOfType<Level>();
    level.CountBreakableBlock();
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    DestroyBlock();
  }

  private void DestroyBlock()
  {
    AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    level.RemoveBreakableBlock();
    gameStatus.AddToScore();
    Destroy(gameObject);
  }
}
