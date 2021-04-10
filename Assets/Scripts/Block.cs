using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
  // config params
  [SerializeField] AudioClip breakSound;
  [SerializeField] GameObject blockSparklesVFX;
  [SerializeField] Sprite[] damageLevelSprites;

  // cached references
  Level level;
  [SerializeField] GameSession gameSession; // for debug

  // state
  [SerializeField] int timesHit; // serialized for debug

  private void Start()
  {
    gameSession = FindObjectOfType<GameSession>();
    level = FindObjectOfType<Level>();
    if (tag == "Breakable") level.CountBlock();
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (tag == "Breakable")
    {
      HandleHit();
    }
  }

  private void HandleHit()
  {
    int maxHits = damageLevelSprites.Length + 1;
    if (++timesHit >= maxHits) DestroyBlock();
    else ShowNextDamageSprite();
  }

  private void ShowNextDamageSprite()
  {
    int spriteIndex = timesHit - 1;
    if (damageLevelSprites[spriteIndex] != null)
      GetComponent<SpriteRenderer>().sprite = damageLevelSprites[spriteIndex];
    else Debug.LogError("Block sprite is missing from array. At " + gameObject.name);
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
