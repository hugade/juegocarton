using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TocarPantalla : MonoBehaviour
{
    private Vector2 direction;

    private bool directionChanged;

    private Vector2 startPosition;
        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                    startPosition = touch.position;
                    Debug.Log("began");
                    break;

                    case TouchPhase.Moved:
                    direction = touch.position - startPosition;
                    Debug.Log("movement");
                    break;

                    case TouchPhase.Stationary:
                    break;

                    case TouchPhase.Ended:
                    directionChanged = true;
                    Debug.Log("ended");
                    break;
                }
            }
        }
    
}
