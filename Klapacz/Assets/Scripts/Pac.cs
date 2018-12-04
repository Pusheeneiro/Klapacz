using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pac : MonoBehaviour {

    private Rigidbody2D PacBody;
    private Vector2 PacVer;
    private GameObject ColideBox;

    private bool isColideDOWN;
    private bool isColideUP;
    private bool isColideLEFT;
    private bool isColideRIGHT;

    private Transform DownColisionCheck;
    private Transform UpColisionCheck;
    private Transform LeftColisionCheck;
    private Transform RightColisionCheck;

    private Vector2 BoxSize;
    private LayerMask Walls;

    void Start()
    {
        PacBody = GetComponent<Rigidbody2D>();
        PacVer = new Vector2(1,0);

        isColideDOWN = false;
        isColideUP = false;
        isColideLEFT = false;
        isColideRIGHT = false;

        BoxSize = new Vector2(0.03f, 0.03f);
        Walls = LayerMask.GetMask("walls");
        ColideBox = GameObject.Find("down");
        DownColisionCheck = ColideBox.transform;
        ColideBox = GameObject.Find("up");
        UpColisionCheck = ColideBox.transform;
        ColideBox = GameObject.Find("left");
        LeftColisionCheck = ColideBox.transform;
        ColideBox = GameObject.Find("right");
        RightColisionCheck = ColideBox.transform;
    }
    
    void Update ()
    {
        
        if (Input.GetKeyDown(KeyCode.A) && !isColideLEFT)
        {
            PacVer.x = -0.65f;
            PacVer.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.D) && !isColideRIGHT)
        {
            PacVer.x = 0.65f;
            PacVer.y = 0;
        }
        if (Input.GetKeyDown(KeyCode.W) && !isColideUP)
        {
            PacVer.y = 0.65f;
            PacVer.x = 0;
        }
        if (Input.GetKeyDown(KeyCode.S) && !isColideDOWN)
        {
            PacVer.y = -0.65f;
            PacVer.x = 0;
        }
        


    }
    void FixedUpdate()
    {
        //Move
        PacBody.MovePosition(PacBody.position + PacVer * Time.fixedDeltaTime);

        //S key block
        if (Physics2D.OverlapBox(DownColisionCheck.position, BoxSize, 0, Walls))isColideDOWN = true;
        else isColideDOWN = false;

        //W key block
        if (Physics2D.OverlapBox(UpColisionCheck.position, BoxSize, 0, Walls)) isColideUP = true;
        else isColideUP = false;

        //A key block
        if (Physics2D.OverlapBox(LeftColisionCheck.position, BoxSize, 0, Walls)) isColideLEFT = true;
        else isColideLEFT = false;

        //D key block
        if (Physics2D.OverlapBox(RightColisionCheck.position, BoxSize, 0, Walls)) isColideRIGHT = true;
        else isColideRIGHT = false;
    }

    void OnTriggerStay2D(Collider2D other)
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


    }
