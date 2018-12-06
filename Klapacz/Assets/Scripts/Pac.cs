using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pac : MonoBehaviour {

    private Rigidbody2D PacBody;
    private Vector2 PacVer;
    private GameObject ColideBox;
    private GameObject Maw;
    private GameObject PacMan;    

    private bool isColideDOWN;
    private bool isColideUP;
    private bool isColideLEFT;
    private bool isColideRIGHT;

    private Transform DownColisionCheck;
    private Transform UpColisionCheck;
    private Transform LeftColisionCheck;
    private Transform RightColisionCheck;
    private Transform PacColisionCheck;
    private Transform MawColisionCheck;

    private Vector2 BoxSize;
    private Vector2 MawSize;
    private LayerMask Walls;
    private LayerMask Coins;
    private int CountCoin;

    private float PacRadiusColisionCircle;
    Collider2D ColideCoin;

    public Animator animator;

    void Start()
    {
        PacMan = GameObject.Find("Pac");
        Maw = GameObject.Find("maw");
        PacBody = GetComponent<Rigidbody2D>();
        PacVer = new Vector2(1,0);

        isColideDOWN = false;
        isColideUP = false;
        isColideLEFT = false;
        isColideRIGHT = false;

        BoxSize = new Vector2(0.03f, 0.03f);
        Walls = LayerMask.GetMask("walls");
        Coins = LayerMask.GetMask("Coins");
        ColideBox = GameObject.Find("down");
        DownColisionCheck = ColideBox.transform;
        ColideBox = GameObject.Find("up");
        UpColisionCheck = ColideBox.transform;
        ColideBox = GameObject.Find("left");
        LeftColisionCheck = ColideBox.transform;
        ColideBox = GameObject.Find("right");
        RightColisionCheck = ColideBox.transform;

        MawSize = new Vector2(0.07504337f, 0.09125525f);
        MawColisionCheck = Maw.transform;

        CountCoin = 0;

        PacColisionCheck = PacMan.transform;
        PacRadiusColisionCircle = 0.07051769f;
    }
    
    void Update ()
    {
        
        if (Input.GetKeyDown(KeyCode.A) && !isColideLEFT)
        {
            PacVer.x = -0.65f;
            PacVer.y = 0;
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
        }
        if (Input.GetKeyDown(KeyCode.D) && !isColideRIGHT)
        {
            PacVer.x = 0.65f;
            PacVer.y = 0;
            animator.SetBool("Right", true);
            animator.SetBool("Left", false);
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
        }
        if (Input.GetKeyDown(KeyCode.W) && !isColideUP)
        {
            PacVer.y = 0.65f;
            PacVer.x = 0;
            animator.SetBool("Up", true);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Down", false);
        }
        if (Input.GetKeyDown(KeyCode.S) && !isColideDOWN)
        {
            PacVer.y = -0.65f;
            PacVer.x = 0;
            animator.SetBool("Down", true);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Up", false);
        }
        if(CountCoin==168)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
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

        if ( ColideCoin=Physics2D.OverlapBox(MawColisionCheck.position, MawSize, 0, Coins))
        {
                Destroy(ColideCoin.gameObject);
                CountCoin++;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {



    }


    }
