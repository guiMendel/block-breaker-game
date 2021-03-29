using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
  [SerializeField] float screenWidthInUnits = 16f;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    float cursorPosition = Input.mousePosition.x / Screen.width * screenWidthInUnits;
    Vector2 movePaddleTo = new Vector2(cursorPosition, transform.position.y);
    transform.position = movePaddleTo;
  }
}
