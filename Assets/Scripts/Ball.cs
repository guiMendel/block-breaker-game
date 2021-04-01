using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
  [Header("Configuration Parameters")]
  [SerializeField] Paddle paddle;
  [SerializeField] float launchVelocity = 13f;
  [SerializeField] AudioClip[] ballSounds;
  // [Header("State")]

  // state
  Vector2 paddleToBallVector;
  Boolean hasStarted = false;

  // cached component references
  AudioSource audioSource;

  // Start is called before the first frame update
  void Start()
  {
    paddleToBallVector = transform.position - paddle.transform.position;
    audioSource = GetComponent<AudioSource>();
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
      GetComponent<Rigidbody2D>().velocity = LaunchVectorRelativeToMouse();
      hasStarted = true;
    }
  }

  private void LockBallToPaddle()
  {
    Vector2 paddlePosition = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
    transform.position = paddlePosition + paddleToBallVector;
  }

  private Vector2 LaunchVectorRelativeToMouse()
  {
    // get x and y distances from ball to click
    float xDistance = Input.mousePosition.x / Screen.width * paddle.screenWidthInUnits - transform.position.x;
    float yDistance = Input.mousePosition.y / Screen.width * paddle.screenWidthInUnits - transform.position.y;
    // Debug.Log("xDistance: " + xDistance);
    // Debug.Log("yDistance: " + yDistance);

    // calculate distance from ball to click
    float distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));
    // Debug.Log("distance: " + distance);

    // calculate relation between launch velocity and distance from ball to click
    float distanceVelocityRelation = launchVelocity / distance;
    // Debug.Log("distanceVelocityRelation: " + distanceVelocityRelation);

    // use the relation, x and y distances to find out x and y velocities
    // Debug.Log(distanceVelocityRelation * xDistance + " and " + distanceVelocityRelation * yDistance);
    return new Vector2(distanceVelocityRelation * xDistance, distanceVelocityRelation * yDistance);
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (hasStarted) {
      AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
      audioSource.PlayOneShot(clip);
    }
  }
}
