using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
  [SerializeField] int breakableBlocks; // Serialized for debugging

  // stored references
  SceneLoader sceneLoader;

  private void Start() {
    sceneLoader = FindObjectOfType<SceneLoader>();
  }

  public void CountBreakableBlock()
  {
    breakableBlocks++;
  }

  public void RemoveBreakableBlock()
  {
    breakableBlocks--;
    if (breakableBlocks <= 0) LoadNextLevel();
  }

  private void LoadNextLevel()
  {
    sceneLoader.LoadNextScene();
  }
}
