using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
  [SerializeField] AudioClip breakSound;
  [SerializeField] GameObject blockSparklesVFX;

  // cached references
  Level level;
  [Header("For Debugging")]
  [SerializeField] GameSession gameSession; // for debug

  private void Start()
  {
    gameSession = FindObjectOfType<GameSession>();
    level = FindObjectOfType<Level>();
    if (tag == "Breakable") level.CountBlock();
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (tag == "Breakable") DestroyBlock();
  }

  private void DestroyBlock()
  {

    // Tells the counter theres one less block
    level.RemoveBreakableBlock();
    // Adds to the score
    gameSession.AddToScore();
    // Takes the block away from the game
    Destroy(gameObject);

    // SFX
    AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    // VFX
    TriggerSparklesVFX();
  }

  private void TriggerSparklesVFX()
  {
    GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
    Destroy(sparkles, 2f);
  }
}
