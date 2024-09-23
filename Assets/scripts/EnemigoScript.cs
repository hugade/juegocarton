using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    private int speed;

    private float limit;

    private int movz;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3;

        limit = 2;

        movz = -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 0, movz) * speed * Time.deltaTime, Space.World);

        if (transform.position.z <= 2) movz = 0;
    }
}
