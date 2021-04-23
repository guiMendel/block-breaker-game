using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
  [SerializeField] public float screenWidthInUnits = 16f;
  [SerializeField] float minX = 1f;
  [SerializeField] float maxX = 15f;

  // stored refs
  GameSession gameSession;
  Ball theBall;

  private void Start()
  {
    gameSession = FindObjectOfType<GameSession>();
    theBall = FindObjectOfType<Ball>();
  }

  // Update is called once per frame
  void Update()
  {
    Vector2 movePaddleTo = new Vector2(transform.position.x, transform.position.y);
    movePaddleTo.x = Mathf.Clamp(GetXPos(), minX, maxX);
    transform.position = movePaddleTo;
  }

  private float GetXPos()
  {
    if (gameSession.AutoPlayEnabled())
    {
      return theBall.transform.position.x;
    }
    else
    {
      return Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
  }
}
