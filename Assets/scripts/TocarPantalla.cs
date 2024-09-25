using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TocarPantalla : MonoBehaviour
{
    private Vector2 direction;

    private bool directionChanged;

    private Vector2 startPosition;

    public GameObject pasillovent, pasillopta;

    private float draglimit = 100f;

    private void Start()
    {

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
                    break;

                    case TouchPhase.Moved:
                    direction = touch.position - startPosition;
                    if (direction.y > 0 && Mathf.Abs(direction.y) > draglimit && Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
                    {
                        pasillovent.GetComponent<Pasillo>();
                       
                        Debug.Log("ARRIBA");

                    }
                    if (direction.y < 0 && Mathf.Abs(direction.y) > draglimit && Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
                    {
                        Debug.Log("ABAJO");
                    }
                    if (direction.x > 0 && Mathf.Abs(direction.x) > draglimit && Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                    {
                        Debug.Log("DERECHA");
                    }
                    if (direction.x < 0 && Mathf.Abs(direction.x) > draglimit && Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                    {
                        Debug.Log("IZQUIERDA");
                    }
                    break;

                    case TouchPhase.Stationary:
                    break;

                    case TouchPhase.Ended:
                    directionChanged = true;
                    break;
                }
        }   
    }
}
