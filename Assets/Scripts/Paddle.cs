using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
  [SerializeField] public float screenWidthInUnits = 16f;
  [SerializeField] float minX = 1f;
  [SerializeField] float maxX = 15f;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    float cursorX = Input.mousePosition.x / Screen.width * screenWidthInUnits;
    Vector2 movePaddleTo = new Vector2(transform.position.x, transform.position.y);
    movePaddleTo.x = Mathf.Clamp(cursorX, minX, maxX);
    transform.position = movePaddleTo;
  }
}
