using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TocarPantalla : MonoBehaviour
{
    private Vector2 direction;

    private bool directionChanged;

    private Vector2 startPosition;

    public float speed;

    public GameObject pasillo;

    public GameObject enemigo1, enemigo2;

    private bool moverpasillo;

    private void Start()
    {
        moverpasillo = false;
    }

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
                    Object.Destroy(enemigo1);
                    moverpasillo = true;
                    break;

                    case TouchPhase.Stationary:
                    break;

                    case TouchPhase.Ended:
                    directionChanged = true;
                    Debug.Log("ended");
                    break;
                }
            }

            if (moverpasillo = true)
            {
                
                speed = 1;

               transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * speed, Space.World);
            }
            
    }
    
}
