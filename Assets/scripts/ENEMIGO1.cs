using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class ENEMIGO1 : MonoBehaviour
{
    public float speed;

    private float limit1, limit2, time, time2, time3;

    public float cd, cd2, cd3;

    private int movz, vida, score;

    private bool rng, speedup;

    public GameObject txtarriba, txtabajo, txtizquierda, txtderecha, enemigo1, enemigo2, vida1, vida2, vida3, tijeras, vacio1, vacio2, sword, menumuerte, uimenu, arribafalso, abajofalso, izquierdafalso, derechafalso;

    private Animator tijeraanim, ene1anim, ene2anim;

    private AudioSource ene1audio, ene2audio, tijerasaudio, swordaudio;

    private bool arriba, abajo, izquierda, derecha, quitarvida, hayene1, hayene2;

    public TMP_Text scoretxt, finalscore;

    public enum States { Movement, Attack, Death, NewEnemy };

    public States mystate = States.Movement;


    //TACTIL
    private Vector2 direction;

    private bool directionChanged;

    private Vector2 startPosition;

    private float draglimit = 100f;

    // Start is called before the first frame update
    void Start()
    {
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

        swordaudio = sword.GetComponent<AudioSource>();

        time = 0.4f;

        time2 = 0.8f;

        time3 = 0.7f;

        cd = time;

        cd2 = time2;

        cd3 = time3;

        speedup = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (mystate)
        {
            case States.Movement:
                Move();
                break;
            case States.Attack:
                Atacar();
                break;
            case States.Death:
                Muerte();
                break;
            case States.NewEnemy:
                Enemigos();
                break;
            default:
                Debug.Log("hola");
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
                        swordaudio.Play();

                        tijeraanim.Play("Down attack");

                        //DestroyEnemigo();

                        arriba = false;

                        //Destroy(txtarriba);

                        txtarriba.SetActive(false);
                        abajofalso.SetActive(false);

                        SetState(States.Death);
                    }
                    
                    if (direction.y < 0 && Mathf.Abs(direction.y) > draglimit && Mathf.Abs(direction.y) > Mathf.Abs(direction.x) && abajo == true)
                    {
                        swordaudio.Play();

                        tijeraanim.Play("Up attack");

                        //DestroyEnemigo();

                        abajo = false;

                        //Destroy(txtabajo);

                        txtabajo.SetActive(false);
                        arribafalso.SetActive(false);

                        SetState(States.Death);
                    }
                    
                    if (direction.x > 0 && Mathf.Abs(direction.x) > draglimit && Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && derecha == true)
                    {
                        swordaudio.Play();

                        tijeraanim.Play("Left attack");
;
                        //DestroyEnemigo();

                        derecha = false;

                        //Destroy(txtizquierda);

                        txtderecha.SetActive(false);
                        izquierdafalso.SetActive(false);

                        SetState(States.Death);
                    }
                   
                    if (direction.x < 0 && Mathf.Abs(direction.x) > draglimit && Mathf.Abs(direction.x) > Mathf.Abs(direction.y) && izquierda == true)
                    {
                        swordaudio.Play();

                        tijeraanim.Play("Right attack");

                        //DestroyEnemigo();

                        izquierda = false;

                        //Destroy(txtderecha);

                        txtizquierda.SetActive(false);
                        derechafalso.SetActive(false);

                        SetState(States.Death);
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

    private void Move()
    {
        ///ENEMIGO 1
        if (enemigo1.transform.position.x == 2)
        {
            hayene1 = true;
            ene1anim.Play("Idle");
            vacio1.transform.Translate(new Vector3(0, 0, movz) * speed * Time.deltaTime, Space.World);

            movz = -1;

            if (enemigo1.transform.position.z <= limit2 && rng == true)
            {
                int randomNumber = Random.Range(0, 4);

                if (randomNumber == 0)
                {
                    //Instantiate(txtarriba, new Vector3( 47, 463, 0), transform.rotation);
                    int queflecha = Random.Range(0, 2);

                    if (queflecha == 0)
                    {
                        txtarriba.SetActive(true);
                    }

                    if (queflecha == 1)
                    {
                        abajofalso.SetActive(true);
                    }

                    arriba = true;

                    rng = false;


                }

                if (randomNumber == 1)
                {
                    //Instantiate(txtabajo, new Vector3(39, 463, 0), transform.rotation);
                    int queflecha = Random.Range(0, 2);

                    if (queflecha == 0)
                    {
                        txtabajo.SetActive(true);
                    }

                    if (queflecha == 1)
                    {
                        arribafalso.SetActive(true);
                    }

                    abajo = true;

                    rng = false;

                }

                if (randomNumber == 2)
                {
                    //Instantiate(txtizquierda, new Vector3(0, 463, 0), transform.rotation);
                    int queflecha = Random.Range(0, 2);

                    if (queflecha == 0)
                    {
                        txtizquierda.SetActive(true);
                    }

                    if (queflecha == 1)
                    {
                        derechafalso.SetActive(true);
                    }

                    izquierda = true;

                    rng = false;


                }

                if (randomNumber == 3)
                {
                    //Instantiate(txtderecha, new Vector3(0, 463, 0), transform.rotation);
                    int queflecha = Random.Range(0, 2);

                    if (queflecha == 0)
                    {
                        txtderecha.SetActive(true);
                    }

                    if (queflecha == 1)
                    {
                        izquierdafalso.SetActive(true);
                    }

                    derecha = true;

                    rng = false;

                }
            }

            if (enemigo1.transform.position.z <= limit1)
            {
                SetState(States.Attack);
            }
        }

        ///ENEMIGO 2
        if (enemigo2.transform.position.x == 2)
        {
            hayene2 = true;
            ene2anim.Play("Idle");
            vacio2.transform.Translate(new Vector3(0, 0, movz) * speed * Time.deltaTime, Space.World);

            movz = -1;

            if (enemigo2.transform.position.z <= limit2 && rng == true)
            {
                int randomNumber = Random.Range(0, 4);

                if (randomNumber == 0)
                {
                    //Instantiate(txtarriba, new Vector3( 47, 463, 0), transform.rotation);
                    int queflecha = Random.Range(0, 2);

                    if (queflecha == 0)
                    {
                        txtarriba.SetActive(true);
                    }

                    if (queflecha == 1)
                    {
                        abajofalso.SetActive(true);
                    }

                    arriba = true;

                    rng = false;


                }

                if (randomNumber == 1)
                {
                    //Instantiate(txtabajo, new Vector3(39, 463, 0), transform.rotation);
                    int queflecha = Random.Range(0, 2);

                    if (queflecha == 0)
                    {
                        txtabajo.SetActive(true);
                    }

                    if (queflecha == 1)
                    {
                        arribafalso.SetActive(true);
                    }

                    abajo = true;

                    rng = false;

                }

                if (randomNumber == 2)
                {
                    //Instantiate(txtizquierda, new Vector3(0, 463, 0), transform.rotation);
                    int queflecha = Random.Range(0, 2);

                    if (queflecha == 0)
                    {
                        txtizquierda.SetActive(true);
                    }

                    if (queflecha == 1)
                    {
                        derechafalso.SetActive(true);
                    }

                    izquierda = true;

                    rng = false;


                }

                if (randomNumber == 3)
                {
                    //Instantiate(txtderecha, new Vector3(0, 463, 0), transform.rotation);
                    int queflecha = Random.Range(0, 2);

                    if (queflecha == 0)
                    {
                        txtderecha.SetActive(true);
                    }

                    if (queflecha == 1)
                    {
                        izquierdafalso.SetActive(true);
                    }

                    derecha = true;

                    rng = false;

                }
            }

            if (enemigo2.transform.position.z <= limit1)
            {
                SetState(States.Attack);
            }
        }
    }

    private void Atacar()
    {
        if (hayene2 = true)
        {
            ene2anim.Play("Attack");
        }
        if (hayene1 = true)
        {
            ene1anim.Play("Attack");
        }

        GestionVida();

        cd2 -= Time.deltaTime;

        if (cd2 < 0)
        {
            txtarriba.SetActive(false);
            abajofalso.SetActive(false);
            txtabajo.SetActive(false);
            arribafalso.SetActive(false);
            txtderecha.SetActive(false);
            izquierdafalso.SetActive(false);
            txtizquierda.SetActive(false);
            derechafalso.SetActive(false);

            SetState(States.NewEnemy);

            cd2 = time2;

            quitarvida = true;
        }
    }

    private void Muerte()
    {
        if (hayene1 == true)
        {
            movz = 0;
            ene1audio.Play();
            ene1anim.Play("Death");
            hayene1 = false;
        }

        if (hayene2 == true)
        {
            movz = 0;
            ene2audio.Play();
            ene2anim.Play("Death");
            hayene2 = false;
        }

        rng = true;
        
        if (speedup == true)
        {
            score = score + 10;
            scoretxt.text = score.ToString();
            limit2 = limit2 + 0.15f;
            if (speed < 20) speed = speed + 0.2f;
            speedup = false;
        }

        cd -= Time.deltaTime;

        if (cd < 0)
        {
            SetState(States.NewEnemy);

            cd = time;

            speedup = true;
        }
        
    }

    private void Enemigos()
    {
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

        SetState(States.Movement);
    }
    /*private void DestroyEnemigo()
    {
        if (hayene1 == true)
        {
            movz = 0;
            ene1audio.Play();
            ene1anim.Play("Death");
            hayene1 = false;
        }

        if (hayene2 == true)
        {
            movz = 0;
            ene2audio.Play();
            ene2anim.Play("Death");
            hayene2 = false;
        }
        score = score + 10;
        scoretxt.text = score.ToString();
        rng = true;
        limit2 = limit2 + 0.15f;
        if (speed < 20) speed = speed + 0.2f;

        /*int nuevoenemigo = Random.Range(0, 2);

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
    }*/

    private void GestionVida()
    {
        if (quitarvida == true)
        {
            tijerasaudio.Play();

            tijeraanim.Play("Hurt");

            vida = vida - 1;
        }

        quitarvida = false;

    }

    private void VIDAUI()
    {
        cd3 -= Time.deltaTime;

        if (cd3 <= 0)
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
                finalscore.text = score.ToString();

                vida1.SetActive(false);

                Time.timeScale = 0f;

                uimenu.SetActive(false);

                menumuerte.SetActive(true);
            }

            cd3 = time2;
        } 
    }

    public void SetState(States S)
    {
        mystate = S;
    }
}
