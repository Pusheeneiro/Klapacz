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
    private GameObject LeftTeleport;
    private GameObject RightTeleport;
    private GameObject ScoreCount;
    private GameObject LevelIcon;
    private GameObject LevelIconBase;

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
    private LayerMask PowerUps;
    private int CountCoin;

    private float PacRadiusColisionCircle;
    Collider2D ColideCoin;
    Collider2D ColidePowerUps;

    private void Awake()
    {
        PacMan = GameObject.Find("Pac");
        Maw = GameObject.Find("maw");
        PacBody = GetComponent<Rigidbody2D>();
        PacVer = new Vector2(1, 0);

        isColideDOWN = false;
        isColideUP = false;
        isColideLEFT = false;
        isColideRIGHT = false;

        BoxSize = new Vector2(0.03f, 0.03f);
        Walls = LayerMask.GetMask("walls");
        Coins = LayerMask.GetMask("Coins");
        PowerUps = LayerMask.GetMask("PowerUps");

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
        LeftTeleport = GameObject.Find("LeftTeleport");
        RightTeleport = GameObject.Find("RightTeleport");

        LevelIconBase = GameObject.Find("1");
        ScoreCount = GameObject.Find("ScoreCount");
        ScoreCount.GetComponent<TextMesh>().text = Global.Score.ToString();


        for (int i=1; i < Global.Level;i++)
        {            
            LevelIcon=Instantiate(LevelIconBase);
            LevelIcon.transform.position = LevelIconBase.transform.position + (new Vector3(-0.14f, 0, 0)*i);
            SpriteRenderer ImgR = LevelIcon.GetComponent<SpriteRenderer>();
            string path = "Sprites/"+(i+1).ToString();
            Sprite Img = Resources.Load<Sprite>(path);
            ImgR.sprite = Img;


        }
    }

    void Start()
    {
       
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

        //Next Lvl
        if(CountCoin==168)
        {
            Global.Level = Global.Level+1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

        //Colect Coins
        if ( ColideCoin=Physics2D.OverlapBox(MawColisionCheck.position, MawSize, 0, Coins))
        {
                Destroy(ColideCoin.gameObject);
                CountCoin++;
                Global.Score= Global.Score+10;
                ScoreCount.GetComponent<TextMesh>().text = Global.Score.ToString();
        }

        //Colect PowerUps
        if (ColidePowerUps = Physics2D.OverlapBox(MawColisionCheck.position, MawSize, 0, PowerUps))
        {
            Destroy(ColidePowerUps.gameObject);
            Global.Score = Global.Score + 50;
            ScoreCount.GetComponent<TextMesh>().text = Global.Score.ToString();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "LeftTeleport")
        {
            PacMan.transform.position = RightTeleport.transform.position + new Vector3(-0.3f,0,0);
        }

        if (other.gameObject.name == "RightTeleport")
        {
            PacMan.transform.position = LeftTeleport.transform.position + new Vector3(0.3f,0,0);
        }
    }


    }
