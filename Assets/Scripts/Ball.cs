using UnityEngine;

public class Ball : MonoBehaviour
{
  [Header("Configuration Parameters")]
  [SerializeField] Paddle paddle;
  [SerializeField] float launchVelocity = 13f;
  [SerializeField] AudioClip[] ballSounds;
  [SerializeField] float randomFactor = 0.2f;
  // [Header("State")]

  // state
  Vector2 paddleToBallVector;
  Boolean hasStarted = false;

  // cached component references
  AudioSource audioSource;
  Rigidbody2D rigidBody2D;

  // Start is called before the first frame update
  void Start()
  {
    paddleToBallVector = transform.position - paddle.transform.position;
    audioSource = GetComponent<AudioSource>();
    rigidBody2D = GetComponent<Rigidbody2D>();
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
    if (hasStarted)
    {
      AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
      audioSource.PlayOneShot(clip);
      TweakVelocity();
    }
  }

  private void TweakVelocity()
  {
    // Inserts a bit of randomness on the ball's direction
    float velocityTweak = Random.Range(0, randomFactor);

    // What we give we also take, to ensure the overall velocity remains unaltered
    rigidBody2D.velocity += new Vector2(velocityTweak, -velocityTweak);

    float AlterX(ref float tweak)
    {
      rigidBody2D.velocity.Set(ReduceBy(rigidBody2D.velocity.x, ref tweak), rigidBody2D.velocity.y);
    }

    float AlterY(ref float tweak)
    {
      rigidBody2D.velocity.Set(rigidBody2D.velocity.x, ReduceBy(rigidBody2D.velocity.y, ref tweak));
    }

    var tweakFunctions = { () => AlterX(ref velocityTweak), () => AlterY(ref velocityTweak) };


  }

  private float ReduceBy(float x, ref float n)
  {
    // Get the reduced absolute value
    float reduced = Mathf.Abs(x) - n;

    // See if is lower than 0
    if (reduced < 0f)
    {
      // Set n to the amount that was successfully reduced
      n = Mathf.Abs(x);
      return 0f;
    }

    // Find out the signal of the original number
    float signal = (x < 0f ? -1f : 1f);

    return signal * reduced;
  }
}