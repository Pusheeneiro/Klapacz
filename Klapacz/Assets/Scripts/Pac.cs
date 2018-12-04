using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pac : MonoBehaviour {

    private Rigidbody2D PacBody;
    private Vector2 PacVer;


    void Start()
    {
        PacBody = GetComponent<Rigidbody2D>();
        PacVer = new Vector2(1,0);
    }
    
    void Update ()
    {
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            PacVer.x = -1;
            PacVer.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PacVer.x = 1;
            PacVer.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PacVer.y = 1;
            PacVer.x = 0;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PacVer.y = -1;
            PacVer.x = 0;
        }
        */


    }
    void FixedUpdate()
    {
        PacBody.MovePosition(PacBody.position + PacVer * Time.fixedDeltaTime);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PacVer.x = -1;
            PacVer.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PacVer.x = 1;
            PacVer.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PacVer.y = 1;
            PacVer.x = 0;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PacVer.y = -1;
            PacVer.x = 0;
        }
    }


    }
