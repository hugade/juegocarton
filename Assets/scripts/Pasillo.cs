using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pasillo : MonoBehaviour
{
    public float speed;

    private float position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * speed, Space.World);
      
        
    }

    
}
