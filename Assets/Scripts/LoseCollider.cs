using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoseCollider : MonoBehaviour
{

  private void OnTriggerEnter2D(Collider2D other)
  {
    SceneManager.LoadScene("GameOver");
  }

}
