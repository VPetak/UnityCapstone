using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlayerController : MonoBehaviour
{

    public CharacterController controller;
    public static PlayerController instance;

    public float speed = 12f;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    //public variables can be seen and set from the editor
    //private variables cannot but can be set from the game
    //private Rigidbody rb;
    private int count = 0;
    //public float speed;
 

//    https://www.youtube.com/watch?v=_QajrabyTJc        

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public int HP; //make private later
    public int maxHP = 100;
    public int armor = 100; //make private later

    public GameObject gameOverUI;
    private bool playerDead = false;

    private bool fileFlag = true; //true only if file hasnt been written to yet

    //These booleans are for opening doors with keycards, skipping 0 because doors with key0 always open by default

    public bool[] key = {true, false, false, false, false, false};

   public Text ammoText; 
    public Text scoreText; //keep track of score and put it on canvas

    public Text gameOverText;
    public int score = 0;
    public Text timeText;
    private float time;
    private float timeStartPoint;
    public string userName1;
    public GameObject GManager1;
    private GameManager GManager2;

    private string path = @"saves\\testIO.txt";  //https://docs.microsoft.com/en-us/dotnet/api/system.io.file?view=netcore-3.1
    //set current file path to ./saves/(filename)

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GManager2 = GManager1.GetComponent<GameManager>();
        //GManager = GetComponent<GameManager>();
        print("helo world");
        timeStartPoint = Time.time; //track when time starts to keep track of time relative to this point
        userName1 = GManager2.GetUserName();
    }

    // Update is called once per frame
    void Update() //was FixedUpdate originally
    {

        if (!playerDead)
        {

            //time stuff    source: /watch?v=x-C95TuQtf0
            time = Time.time - timeStartPoint;
            timeText.text = "time: " + ((int) time / 60).ToString() + ":" + (time % 60).ToString();

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if(isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x  = Input.GetAxis("Horizontal");
            float z  = Input.GetAxis("Vertical");
        // float jmp = Input.GetAxis("Jump");

            if(Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

        // jmp = jmp * (float)0.4;

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;
            

            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            if (Input.GetButtonDown("Jump")) //Control for game over, press SPACE to retry
            {
                scoreText.text = "TEST: NORMALLY THE GAME WILL RESTART WHEN THAT BEHAVIOUR IS ADDED";
                
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print(other);
        if (other.gameObject.CompareTag("pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            scoreText.text = "Coins: " + count.ToString();
        }

        if (other.gameObject.CompareTag("health"))
        {
            other.gameObject.SetActive(false);
            HP += 30;
            if (HP > maxHP)
                HP = maxHP;
        }

        if (other.gameObject.CompareTag("armor"))
        {
            other.gameObject.SetActive(false);
            armor = 100;
        }

     /*   if (other.gameObject.CompareTag("door"))
        {
            other.gameObject.SetActive(false);
            armor = 100;
        }*/
    }

    public void TakeDamage(int damage)
    {
        if (armor > 0) //armor lowers damage taken but also takes damage itself
        {
            damage = damage / 3;
            armor -= damage;
        }

        HP -= damage;

        if (HP <=0) //death sequence
        {
            
            gameOverUI.SetActive(true);
            playerDead = true;

            if (fileFlag)
            { 
                fileFlag = false; //so we dont get a new entry for each bullet that hits the player
                //File.AppendAllText(path, "testuser" + (int)(time / 1.2) + " | Score: " + score + " | Time: " + time + " |  dead\n");
                File.AppendAllText(path, userName1 + " | Score: " + score + " | Time: " + time + " |  dead\n");
                //File.AppendText("testuser" + (int)(time / 8) + " | Score: " + score + " | Time: " + time + " |  dead");
              /*  using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("test!!!!");
                    sw.WriteLine("Score: " + score);
                    sw.WriteLine("Time: " + time);
                } */
                gameOverText.text = File.ReadAllText(path);
            }
        }
    }

    public void ScoreUpdate(int amount)
    {
        score += amount;
        scoreText.text = "score: " + score;
    }

}
