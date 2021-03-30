using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
  [Header("Configuration Parameters")]
  [SerializeField] Paddle paddle;
  [SerializeField] float xLaunchVelocity = 2f;
  [SerializeField] float yLaunchVelocity = 15f;
  // [Header("State")]

  // state
  Vector2 paddleToBallVector;
  Boolean hasStarted = false;

  // Start is called before the first frame update
  void Start()
  {
    paddleToBallVector = transform.position - paddle.transform.position;
  }

  // Update is called once per frame
  void Update()
  {
    if (hasStarted) return;
    LockBallToPaddle();
    LaunchOnMouseClick();
  }

  private void LaunchOnMouseClick()
  {
    // On left click
    if (Input.GetMouseButtonDown(0))
    {
      GetComponent<Rigidbody2D>().velocity = new Vector2(xLaunchVelocity, yLaunchVelocity);
      hasStarted = true;
    }
  }

  private void LockBallToPaddle()
  {
    Vector2 paddlePosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
    transform.position = paddlePosition + paddleToBallVector;
  }
}
