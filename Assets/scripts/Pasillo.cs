using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pasillo : MonoBehaviour
{
    private int speed;

    private int movz;

    public enum States { Move, Battle, };

    public States mystate = States.Move;

    private float limit1, limit2;

    public bool eliminar;

    public bool hayenemigo;

    public bool hayenemigo2;

    public GameObject enemigo;

    public GameObject otropasillo;

    public bool batalla;

    public GameObject txtarriba, txtabajo, txtizquierda, txtderecha;

    private bool arriba, abajo, izquierda, derecha, rng;

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

        hayenemigo = false;

        hayenemigo2 = false;

        batalla = false;

        arriba = false;

        abajo = false;

        izquierda = false;

        derecha = false;

        rng = true;
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
                    if (direction.y > 0 && Mathf.Abs(direction.y) > draglimit && Mathf.Abs(direction.y) > Mathf.Abs(direction.x) && arriba == true)
                    {
                        //SetState(States.Destroy);

                        DestroyEnemigo();

                        arriba = false;

                        txtarriba.SetActive(false);

                        rng = true;

                        Instantiate(otropasillo, new Vector3(0, 0, transform.position.z + 8), transform.rotation);

                        Debug.Log("ARRIBA");
                    }
                    if (direction.y < 0 && Mathf.Abs(direction.y) > draglimit && Mathf.Abs(direction.y) > Mathf.Abs(direction.x) && abajo == true)
                    {
                        //SetState(States.Destroy);

                        DestroyEnemigo();

                        abajo = false;

                        txtabajo.SetActive(false);

                        rng = true;

                        Debug.Log("ABAJO");
                    }
                    if (direction.x > 0 && Mathf.Abs(direction.x) > draglimit && Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && derecha == true)
                    {
                        //SetState(States.Destroy);

                        DestroyEnemigo();

                        derecha = false;

                        txtderecha.SetActive(false);

                        rng = true;

                        Debug.Log("DERECHA");
                    }
                    if (direction.x < 0 && Mathf.Abs(direction.x) > draglimit && Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && izquierda == true)
                    {
                        //SetState(States.Destroy);

                        DestroyEnemigo();

                        izquierda = false;

                        txtizquierda.SetActive(false);

                        rng = true;

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
        if (eliminar == false && hayenemigo == false && transform.position.z > limit1)
        {
            Instantiate(enemigo, new Vector3(2, 0.5f, transform.position.z + 2), transform.rotation);

            hayenemigo = true;
        }

        transform.Translate(new Vector3(0, 0, movz) * Time.deltaTime * speed, Space.World);

        if (transform.position.z <= limit1 + 4 && rng == true)
        {
            int randomNumber = Random.Range(0, 3);

            if (randomNumber == 0)
            {
                txtarriba.SetActive(true);

                arriba = true;

                rng = false;
            }

            if (randomNumber == 1)
            {
                txtabajo.SetActive(true);

                abajo = true;

                rng = false;
            }

            if (randomNumber == 2)
            {
                txtizquierda.SetActive(true);

                izquierda = true;

                rng = false;
            }

            if (randomNumber == 3)
            {
                txtderecha.SetActive(true);

                derecha = true;

                rng = false;
            }
        }

        if (transform.position.z <= limit1 && eliminar == false && hayenemigo == true) SetState(States.Battle);

        if (transform.position.z <= limit2)
        {
            Destroy(this.gameObject);
        }
    }

    private void DestroyEnemigo()
    {
        //transform.Translate(new Vector3(0, 0, movz) * Time.deltaTime * speed, Space.World);

        Destroy(enemigo);

        eliminar = true;

        hayenemigo = false;

        Instantiate(otropasillo, new Vector3(transform.position.x, 0, transform.position.z + 8), transform.rotation);
    }

    private void BattleFunction()
    {
        movz = 0;

        batalla = true;

        if (hayenemigo == false) SetState(States.Move);
    }
}
