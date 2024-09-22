using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TocarPantalla : MonoBehaviour
{
    private Vector2 direction;

    private bool directionChanged;

    private Vector2 startPosition;

    private float speed;

    public GameObject pasillo;

    public GameObject enemigo1, enemigo2;

    private int movz;

    private float limit1, limit2;

    public enum States {Move, Battle};

    public States mystate = States.Move;

    public bool eliminar;

    public bool hayenemigo1;

    private void Start()
    {
        limit1 = transform.position.z - 10;

        limit2 = transform.position.z - 20;

        movz = -1;

        speed = 2;

        eliminar = false;

        hayenemigo1 = false;
    }

    private void Update()
    {
        switch (mystate)
        {
            case States.Move:
                MoveFunction();
                break;
            case States.Battle:
                BattleFunction();
                break;
            default:
                Debug.Log("NO HAY ESTADO");
                break;
        }

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

    private void SetState(States S)
    {
        mystate = S;
    }
    
    private void MoveFunction()
    {
        if (eliminar == false && hayenemigo1 == false) 
        {
            Instantiate(enemigo1, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);

            enemigo1.transform.SetParent(transform);

            hayenemigo1 = true;
        }

        transform.Translate(new Vector3(0, 0, movz) * Time.deltaTime * speed, Space.World);

        if (transform.position.z <= limit1 && eliminar == false) SetState(States.Battle);

        if (transform.position.z <= limit2)
        {
            Destroy(this.gameObject);
        }
    }

    private void DestroyEnemigo()
    {
        if (hayenemigo1 == true)
        {
            Destroy(enemigo1);

            hayenemigo1 = false;
        }

        Destroy(enemigo2);

        movz = -1;

        eliminar = true;

        SetState(States.Move);
        //HACiendo MÁQUINA DE ESTADOS
    }

    private void BattleFunction()
    {
        movz = 0;
    }
    
}
