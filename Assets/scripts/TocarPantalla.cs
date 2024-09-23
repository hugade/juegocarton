using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class TocarPantalla : MonoBehaviour
{
    private Vector2 direction;

    private bool directionChanged;

    private Vector2 startPosition;

    public GameObject pasillo;

    public GameObject enemigo1, enemigo2;

    private float limit1, limit2;

    public enum States {Move, Battle};

    public States mystate = States.Move;

    public bool eliminar;

    public bool hayenemigo1;

    public bool hayenemigo2;

    private void Start()
    {
        limit1 = pasillo.transform.position.z - 8;

        limit2 = pasillo.transform.position.z - 16;

        eliminar = false;

        hayenemigo1 = false;

        hayenemigo2 = false;

        mystate = States.Move;
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
        if (pasillo.transform.position.z <= limit1 && eliminar == false) SetState(States.Battle);
    }

    private void DestroyEnemigo()
    {
        if (hayenemigo1 == true)
        {
            Destroy(enemigo1);

            hayenemigo1 = false;
        }
        if (hayenemigo2 == true)
        {
            Destroy(enemigo2);

            hayenemigo2 = false;
        }

        eliminar = true;

        SetState(States.Move);
    }

    private void BattleFunction()
    {
        
    }
    
}
