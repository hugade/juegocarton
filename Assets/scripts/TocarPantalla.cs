using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TocarPantalla : MonoBehaviour
{
    private Vector2 direction;

    private bool directionChanged;

    private Vector2 startPosition;

    public float speed;

    public GameObject pasillo;

    public GameObject enemigo1, enemigo2;

    private int movz;

    private float limit1, limit2;

    private bool batalla;
    private void Start()
    {
        limit1 = 0;

        limit2 = -10;

        movz = -1;

        speed = 2;

        batalla = false;
    }

    private void Update()
    {
        PasilloMovimiento();

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
                    if (batalla == true)
                    {
                        DestroyEnemigo();
                    }
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

    private void PasilloMovimiento()
    {
        transform.Translate(new Vector3(0, 0, movz) * Time.deltaTime * speed, Space.World);

        if (transform.position.z <= limit1)
        {
            Batalla();
        }

       
    }

    private void DestroyEnemigo()
    {
        Destroy(enemigo1);

        Destroy(enemigo2);

        movz = -1;

        PasilloMovimiento();
        //HACER MÁQUINA DE ESTADOS
    }

    private void Batalla()
    {
        batalla = true;

        movz = 0;
    }
    
}
