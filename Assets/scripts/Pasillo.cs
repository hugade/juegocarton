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

    // Start is called before the first frame update
    void Start()
    {
        limit1 = transform.position.z - 8;

        limit2 = transform.position.z - 16;

        movz = -1;

        speed = 3;

        eliminar = false;

        hayenemigo1 = false;

        hayenemigo2 = false;
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
    }

    private void SetState(States S)
    {
        mystate = S;
    }

    private void MoveFunction()
    {
        if (eliminar == false && hayenemigo1 == false && transform.position.z > limit1)
        {
            Instantiate(enemigo1, transform.position + new Vector3(2, 0.5f, 4), transform.rotation);

            hayenemigo1 = true;
        }

        enemigo1.transform.Translate(new Vector3(0, 0, movz) * Time.deltaTime * speed, Space.World);

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
        if (hayenemigo2 == true)
        {
            Destroy(enemigo2);

            hayenemigo2 = false;
        }

        movz = -1;

        eliminar = true;

        SetState(States.Move);
    }

    private void BattleFunction()
    {
        movz = 0;
    }
}
