using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ENEMIGO1 : MonoBehaviour
{
    private int speed;

    private float limit1, limit2;

    public int movz;

    private bool eliminar, rng;

    public GameObject txtarriba, txtabajo, txtizquierda, txtderecha, enemigo1, enemigo2;

    private bool arriba, abajo, izquierda, derecha;

    //TACTIL
    private Vector2 direction;

    private bool directionChanged;

    private Vector2 startPosition;

    private float draglimit = 100f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2;

        limit1 = 2;

        limit2 = 6;

        eliminar = false;

        movz = -1;

        rng = true;

        arriba = false;

        abajo = false;

        izquierda = false;

        derecha = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, movz) * speed * Time.deltaTime, Space.World);

        if (transform.position.z > limit1)
        {
            movz = -1;
        }

        if (transform.position.z <= limit2 && rng == true)
        {
            int randomNumber = Random.Range(0, 3);

            if (randomNumber == 0)
            {
                //Instantiate(txtarriba, new Vector3( 47, 463, 0), transform.rotation);

                txtarriba.SetActive(true);

                arriba = true;

                rng = false;

                
            }

            if (randomNumber == 1)
            {
                //Instantiate(txtabajo, new Vector3(39, 463, 0), transform.rotation);

                txtabajo.SetActive(true);

                abajo = true;

                rng = false;

            }

            if (randomNumber == 2)
            {
                //Instantiate(txtizquierda, new Vector3(0, 463, 0), transform.rotation);

                txtizquierda.SetActive(true);

                izquierda = true;

                rng = false;

           
            }

            if (randomNumber == 3)
            {
                //Instantiate(txtderecha, new Vector3(0, 463, 0), transform.rotation);

                txtderecha.SetActive(true);

                derecha = true;

                rng = false;

            }
        }

        if (transform.position.z <= limit1 && eliminar == false)
        {
            movz = 0;
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
                        DestroyEnemigo();

                        arriba = false;

                        //Destroy(txtarriba);

                        txtarriba.SetActive(false);
                    }
                    if (direction.y < 0 && Mathf.Abs(direction.y) > draglimit && Mathf.Abs(direction.y) > Mathf.Abs(direction.x) && abajo == true)
                    {
                        DestroyEnemigo();

                        abajo = false;

                        //Destroy(txtabajo);

                        txtabajo.SetActive(false);
                    }
                    if (direction.x > 0 && Mathf.Abs(direction.x) > draglimit && Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && derecha == true)
                    {
                        DestroyEnemigo();

                        derecha = false;

                        //Destroy(txtizquierda);

                        txtizquierda.SetActive(false);
                    }
                    if (direction.x < 0 && Mathf.Abs(direction.x) > draglimit && Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && izquierda == true)
                    {
                        DestroyEnemigo();

                        izquierda = false;

                        //Destroy(txtderecha);

                        txtderecha.SetActive(false);
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

    private void DestroyEnemigo()
    {
        Destroy(this.gameObject);

        int enemigo = Random.Range(0, 1);

        if (enemigo == 0)
        {
            Instantiate(enemigo1, new Vector3(2, 0, 8), transform.rotation);
        }

        if (enemigo == 1)
        {
            Instantiate(enemigo2, new Vector3(2, 0, 8), transform.rotation);
        }

        rng = true; 
        movz = -1;
    }
}
