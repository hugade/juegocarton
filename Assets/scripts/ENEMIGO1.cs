using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class ENEMIGO1 : MonoBehaviour
{
    public float speed;

    private float limit1, limit2;

    private int movz, vida, score;

    private bool eliminar, rng;

    public GameObject txtarriba, txtabajo, txtizquierda, txtderecha, enemigo1, enemigo2, vida1, vida2, vida3;

    private bool arriba, abajo, izquierda, derecha, quitarvida;

    public TMP_Text scoretxt;

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

        quitarvida = true;

        vida = 3;

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ///ENEMIGO 1
        if (enemigo1.transform.position.x == 2)
        {
            enemigo1.transform.Translate(new Vector3(0, 0, movz) * speed * Time.deltaTime, Space.World);

            if (enemigo1.transform.position.z > limit1)
            {
                movz = -1;
            }

            if (enemigo1.transform.position.z <= limit2 && rng == true)
            {
                int randomNumber = Random.Range(0, 4);

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

            if (enemigo1.transform.position.z <= limit1 && eliminar == false)
            {
                movz = 0;

                GestionVida();
            }
        }

        ///ENEMIGO 2
        if (enemigo2.transform.position.x == 2)
        {
            enemigo2.transform.Translate(new Vector3(0, 0, movz) * speed * Time.deltaTime, Space.World);

            if (enemigo2.transform.position.z > limit1)
            {
                movz = -1;
            }

            if (enemigo2.transform.position.z <= limit2 && rng == true)
            {
                int randomNumber = Random.Range(0, 4);

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

            if (enemigo2.transform.position.z <= limit1 && eliminar == false)
            {
                movz = 0;

                GestionVida();
            }
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

                        txtderecha.SetActive(false);
                    }
                   
                    if (direction.x < 0 && Mathf.Abs(direction.x) > draglimit && Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && izquierda == true)
                    {
                        DestroyEnemigo();

                        izquierda = false;

                        //Destroy(txtderecha);

                        txtizquierda.SetActive(false);
                    }
                    break;

                case TouchPhase.Stationary:
                    break;

                case TouchPhase.Ended:
                    directionChanged = true;
                    quitarvida = true;
                    break;
            }
        }

        VIDAUI();
    }

    private void DestroyEnemigo()
    {
        score = score + 10;
        scoretxt.text = score.ToString();
        rng = true;
        movz = -1;
        limit2 = limit2 + 0.1f;
        if (speed < 20) speed = speed + 0.2f;

        int nuevoenemigo = Random.Range(0, 2);

        if (nuevoenemigo == 0)
        {
            enemigo2.transform.position = new Vector3(2, 0, 12);

            enemigo1.transform.position = new Vector3(-2, 0, 12);
        }

        if (nuevoenemigo == 1)
        {
            enemigo1.transform.position = new Vector3(2, 0, 12);

            enemigo2.transform.position = new Vector3(-2, 0, 12);
        }
    }

    private void GestionVida()
    {
        if (quitarvida == true)
        {
            vida = vida - 1;
        }

        quitarvida = false;
    }

    private void VIDAUI()
    {
        if (vida == 2)
        {
            vida3.SetActive(false);
            vida2.SetActive(true);
        }

        if (vida == 1)
        {
            vida2.SetActive(false);
            vida1.SetActive(true);
        }

        if (vida == 0)
        {
            vida1.SetActive(false);
        }
    }
}
