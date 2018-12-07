using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
    public GameObject Ghost;
    private Rigidbody2D GhostBody;

    public float speed;
    private GameObject target;
    private Vector2 GhoVer;
    private int road;
    private bool continueRoad;
    private bool onTrail;

    private bool isColideDOWN;
    private bool isColideUP;
    private bool isColideLEFT;
    private bool isColideRIGHT;

    public Transform DownColisionCheck;
    public Transform UpColisionCheck;
    public Transform LeftColisionCheck;
    public Transform RightColisionCheck;

    private LayerMask Walls;
    public Vector2 BoxSize;

    public Animator animator;


    private void Awake()
    {
        GhostBody = Ghost.GetComponent<Rigidbody2D>();

        GhoVer = new Vector2(0, 0);

        isColideDOWN = false;
        isColideUP = false;
        isColideLEFT = false;
        isColideRIGHT = false;
       

        Walls = LayerMask.GetMask("walls");
    }

    void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        continueRoad = false;
        onTrail = false;
    }

    private void Update()
    {
        if (-1.3f < (target.transform.position.x - transform.position.x)  && (target.transform.position.x - transform.position.x) < 1.3f && -1.3f < (target.transform.position.y - transform.position.y) && (target.transform.position.y - transform.position.y) < 1.3f)
        {
            onTrail = true;
        }
        else
        {
            onTrail = false;
        }         
    }

    void FixedUpdate () {
        if (!continueRoad)
        {
            //Follow run L R U D
            if (onTrail)
            {
                int turn = (int)(Random.value * 100) % 2;
                Debug.Log(turn);
                //Simple path 
                if (target.transform.position.x > transform.position.x && target.transform.position.y == transform.position.y)
                {
                    //G w prawo 
                    road = 1;

                }
                else if (target.transform.position.x < transform.position.x && target.transform.position.y == transform.position.y)
                {
                    //G w lewo
                    road = 0;
                }
                else if (target.transform.position.y > transform.position.y && target.transform.position.y == transform.position.y)
                {
                    //G w góra
                    road = 2;
                }
                else if (target.transform.position.y < transform.position.y && target.transform.position.y == transform.position.y)
                {
                    //G w dół
                    road = 3;
                }
                else if (target.transform.position.x > transform.position.x && target.transform.position.y > transform.position.y)//Adwanced path
                {
                    //G w prawo lub góra
                    if (turn == 1) road = 1;
                    else road = 2;
                }
                else if (target.transform.position.x > transform.position.x && target.transform.position.y < transform.position.y)
                {
                    //G w prawo lub dół
                    if (turn == 1) road = 1;
                    else road = 3;
                }
                else if (target.transform.position.x < transform.position.x && target.transform.position.y > transform.position.y)
                {
                    //G w lewo lub  góra
                    if (turn == 1) road = 0;
                    else road = 2;
                }
                else if (target.transform.position.x < transform.position.x && target.transform.position.y < transform.position.y)
                {
                    //G w lewo lub dół
                    if (turn == 1) road = 0;
                    else road = 3;

                }

            }
            else
            {
                road = (int)(Random.value * 100) % 4;
            }

        }

        switch (road)
        {
            case 0:
                if (!isColideLEFT)
                {
                    GhoVer.x = -speed;
                    GhoVer.y = 0;

                    continueRoad = true;

                    animator.SetBool("ghostred_Left", true);
                    animator.SetBool("ghostred_Right", false);
                    animator.SetBool("ghostred_Up", false);
                    animator.SetBool("ghostred_Down", false);
                }
                else
                {
                    continueRoad = false;
                }
                break;
            case 1:
                if (!isColideRIGHT)
                {
                    GhoVer.x = speed;
                    GhoVer.y = 0;

                    continueRoad = true;

                    animator.SetBool("ghostred_Right", true);
                    animator.SetBool("ghostred_Left", false);
                    animator.SetBool("ghostred_Up", false);
                    animator.SetBool("ghostred_Down", false);
                }
                else
                {
                    continueRoad = false;
                }
                break;
            case 2:
                if (!isColideUP)
                {
                    GhoVer.y = speed;
                    GhoVer.x = 0;

                    continueRoad = true;

                    animator.SetBool("ghostred_Up", true);
                    animator.SetBool("ghostred_Left", false);
                    animator.SetBool("ghostred_Right", false);
                    animator.SetBool("ghostred_Down", false);
                }
                else
                {
                    continueRoad = false;
                }
                break;
            case 3:
                if (!isColideDOWN)
                {
                    GhoVer.y = -speed;
                    GhoVer.x = 0;

                    continueRoad = true;

                    animator.SetBool("ghostred_Down", true);
                    animator.SetBool("ghostred_Left", false);
                    animator.SetBool("ghostred_Right", false);
                    animator.SetBool("ghostred_Up", false);
                }
                else
                {
                    continueRoad = false;
                }
                break;
        }


        //down go block
        if (Physics2D.OverlapBox(DownColisionCheck.position, BoxSize, 0, Walls)) isColideDOWN = true;
        else isColideDOWN = false;

        //up go block
        if (Physics2D.OverlapBox(UpColisionCheck.position, BoxSize, 0, Walls)) isColideUP = true;
        else isColideUP = false;

        //left go block
        if (Physics2D.OverlapBox(LeftColisionCheck.position, BoxSize, 0, Walls)) isColideLEFT = true;
        else isColideLEFT = false;

        //right go  block
        if (Physics2D.OverlapBox(RightColisionCheck.position, BoxSize, 0, Walls)) isColideRIGHT = true;
        else isColideRIGHT = false;

        //Move
        GhostBody.MovePosition(GhostBody.position + GhoVer * Time.fixedDeltaTime);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "LeftTeleport")
        {
            Ghost.transform.position = GameObject.Find("RightTeleport").transform.position + new Vector3(-0.3f, 0, 0);
        }

        if (other.gameObject.name == "RightTeleport")
        {
            Ghost.transform.position = GameObject.Find("LeftTeleport").transform.position + new Vector3(0.3f, 0, 0);
        }
    }
}
