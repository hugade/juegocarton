using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pasillo : MonoBehaviour
{
    private int speed;

    private int movz;

    public enum States { Move, Battle };

    public States mystate = States.Move;

    private float limit1, limit2;

    public bool eliminar;

    public bool hayenemigo1;

    public bool hayenemigo2;

    public GameObject enemigo1, enemigo2;

    public GameObject otropasillo;

    public bool batalla;

    public GameObject txtarriba;

    public GameObject txtabajo;

    public GameObject txtizquierda;

    public GameObject txtderecha;

    private bool dragarriba;

    //TACTIL
    private Vector2 direction;

    private bool directionChanged;

    private Vector2 startPosition;

    private float draglimit = 100f;

    // Start is called before the first frame update
    void Start()
    {
        limit1 = transform.position.z - 8;

        limit2 = transform.position.z - 16;

        movz = -1;

        speed = 2;

        eliminar = false;

        hayenemigo1 = false;

        hayenemigo2 = false;

        batalla = false;

        dragarriba = false;
    }

    // Update is called once per frame
    void Update()
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


        //TACTIL
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
                        DestroyEnemigo();

                        dragarriba = false;

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

    private void SetState(States S)
    {
        mystate = S;
    }

    private void MoveFunction()
    {
        if (eliminar == false && hayenemigo1 == false && transform.position.z > limit1)
        {
            Instantiate(enemigo1, transform.position + new Vector3(2, 0.5f, 2), transform.rotation);

            hayenemigo1 = true;
        }

        enemigo1.transform.Translate(new Vector3(0, 0, movz) * Time.deltaTime * speed, Space.World);

        transform.Translate(new Vector3(0, 0, movz) * Time.deltaTime * speed, Space.World);

        if (transform.position.z <= limit1 + 4)
        {
            txtarriba.SetActive(true);
        }

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
        if (hayenemigo2 == true)
        {
            Destroy(enemigo2);

            hayenemigo2 = false;
        }

        movz = -1;

        eliminar = true;

        SetState(States.Move);

        Instantiate(otropasillo, transform.position + new Vector3(0, 0, 8), transform.rotation);
    }

    private void BattleFunction()
    {
        movz = 0;

        batalla = true;
    }
}
