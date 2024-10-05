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

    private bool rng;

    public GameObject txtarriba, txtabajo, txtizquierda, txtderecha, enemigo1, enemigo2, vida1, vida2, vida3, tijeras, vacio1, vacio2;

    private Animator tijeraanim, ene1anim, ene2anim;

    private AudioSource ene1audio, ene2audio, tijerasaudio;

    private bool arriba, abajo, izquierda, derecha, quitarvida, hayene1, hayene2;

    public TMP_Text scoretxt;

    //TACTIL
    private Vector2 direction;

    private bool directionChanged;

    private Vector2 startPosition;

    private float draglimit = 100f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        speed = 2;

        limit1 = 0.5f;

        limit2 = 6;

        movz = -1;

        rng = true;

        arriba = false;

        abajo = false;

        izquierda = false;

        derecha = false;

        quitarvida = true;

        vida = 3;

        score = 0;

        tijeraanim = tijeras.GetComponent<Animator>();

        ene1anim = enemigo1.GetComponent<Animator>();

        ene2anim = enemigo2.GetComponent<Animator>();

        hayene1 = false;

        hayene2 = false;

        ene1audio = enemigo1.GetComponent<AudioSource>();

        ene2audio = enemigo2.GetComponent<AudioSource>();

        tijerasaudio = tijeras.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ///ENEMIGO 1
        if (enemigo1.transform.position.x == 2)
        {
            hayene1 = true;

            ene1anim.Play("Idle");
            vacio1.transform.Translate(new Vector3(0, 0, movz) * speed * Time.deltaTime, Space.World);

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

            if (enemigo1.transform.position.z <= limit1)
            {
                movz = 0;

                ene1anim.Play("Attack");

                GestionVida();
            }
        }

        ///ENEMIGO 2
        if (enemigo2.transform.position.x == 2)
        {
            ene2anim.Play("Idle");
            vacio2.transform.Translate(new Vector3(0, 0, movz) * speed * Time.deltaTime, Space.World);

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

            if (enemigo2.transform.position.z <= limit1)
            {
                movz = 0;

                ene2anim.Play("Attack");

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
                        tijerasaudio.Play(1);

                        tijeraanim.Play("Down attack");

                        DestroyEnemigo();

                        arriba = false;

                        //Destroy(txtarriba);

                        txtarriba.SetActive(false);
                    }
                    
                    if (direction.y < 0 && Mathf.Abs(direction.y) > draglimit && Mathf.Abs(direction.y) > Mathf.Abs(direction.x) && abajo == true)
                    {
                        tijerasaudio.Play(1);

                        tijeraanim.Play("Up attack");

                        DestroyEnemigo();

                        abajo = false;

                        //Destroy(txtabajo);

                        txtabajo.SetActive(false);
                    }
                    
                    if (direction.x > 0 && Mathf.Abs(direction.x) > draglimit && Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && derecha == true)
                    {
                        tijerasaudio.Play(1);

                        tijeraanim.Play("Left attack");

                        DestroyEnemigo();

                        derecha = false;

                        //Destroy(txtizquierda);

                        txtderecha.SetActive(false);
                    }
                   
                    if (direction.x < 0 && Mathf.Abs(direction.x) > draglimit && Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && izquierda == true)
                    {
                        tijerasaudio.Play(1);

                        tijeraanim.Play("Right attack");

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
        if (hayene1 == true)
        {
            ene1audio.Play();
            ene1anim.Play("Death");
            hayene1 = false;
        }

        if (hayene2 == true)
        {
            ene2audio.Play();
            ene2anim.Play("Death");
            hayene2 = false;
        }
        score = score + 10;
        scoretxt.text = score.ToString();
        rng = true;
        movz = -1;
        limit2 = limit2 + 0.15f;
        if (speed < 20) speed = speed + 0.2f;

        int nuevoenemigo = Random.Range(0, 2);

        if (nuevoenemigo == 0)
        {
            vacio2.transform.position = new Vector3(2, 0, 12);

            vacio1.transform.position = new Vector3(-2, 0, 12);

            hayene2 = true;
        }

        if (nuevoenemigo == 1)
        {
            vacio1.transform.position = new Vector3(2, 0, 12);

            vacio2.transform.position = new Vector3(-2, 0, 12);

            hayene1 = true;
        }
    }

    private void GestionVida()
    {
        if (quitarvida == true)
        {
            tijerasaudio.Play(2);

            tijeraanim.Play("Hurt");

            vida = vida - 1;
        }

        quitarvida = false;
    }

    private void VIDAUI()
    {
        if (vida == 2)
        {
            vida3.SetActive(false);
        }

        if (vida == 1)
        {
            vida2.SetActive(false);
        }

        if (vida == 0)
        {
            vida1.SetActive(false);
        }
    }
}
