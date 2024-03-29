﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public CharacterController controller;
    public static PlayerController instance;

    public float speed = 12f;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    //public variables can be seen and set from the editor
    //private variables cannot but can be set from the game

    private int count = 0;     

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public int HP; //make private later
    public int maxHP = 100;
    public int armor = 100; //make private later

    public GameObject gameOverUI;

     public Text gameOverText;

    public GameObject gameWonUI;

    public Text gameWonText;
    private bool playerDead = false;

    private bool fileFlag = true; //true only if file hasnt been written to yet

    //These booleans are for opening doors with keycards, skipping 0 because doors with key0 always open by default
    public bool[] key = {true, false, false, false, false, false};

    public Text ammoText; 
    public Text HPText; 
    public Text armorText;
    public Text scoreText; //keep track of score and put it on canvas

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
        print("helo world");
        timeStartPoint = Time.time; //track when time starts to keep track of time relative to this point
        userName1 = GManager2.GetUserName();
        HPText.text = HP + "% HP";
        armorText.text = armor + "% Armor";
    }

    // Update is called once per frame
    void Update() //was FixedUpdate originally
    {
        if (!playerDead)
        {
            //time and physics, see this youtube tutorial    source: /watch?v=x-C95TuQtf0
            time = Time.time - timeStartPoint;
            timeText.text = "time: " + ((int) time / 60).ToString() + ":" + (time % 60).ToString();

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if(isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x  = Input.GetAxis("Horizontal");
            float z  = Input.GetAxis("Vertical");

            if(Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            if (Input.GetButtonDown("Jump")) //Control for game over, press SPACE to retry
            {
                //if (HP <= 0)
                    Cursor.visible = true;
                    SceneManager.LoadScene(0);
                //else
                //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
            Heal(30);
        }

        if (other.gameObject.CompareTag("armor"))
        {
            other.gameObject.SetActive(false);
            ArmorUp(100);
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            other.gameObject.SetActive(false);
            HP = 90000;
            HPText.text = "100% HP";
            playerDead = true;
            Win();
        }
    }

    public void TakeDamage(int damage)
    {
        if (armor > 0) //armor lowers damage taken but also takes damage itself
        {
            armor -= damage;
            damage = damage / 5;
            
        }
        else
            armor = 0;

        HP -= damage;
        if (!playerDead)
        {
            HPText.text = HP + "% HP";
            armorText.text = armor + "% Armor";
        }

        if (HP <=0) //death sequence
        {
            
            gameOverUI.SetActive(true);
            playerDead = true;

            if (fileFlag)
            { 
                fileFlag = false; //so we dont get a new entry for each bullet that hits the player
                File.AppendAllText(path, userName1 + " | \tScore: " + score + " | \tTime: " + time + " |  \tdead\n");
                gameOverText.text = File.ReadAllText(path);
            }
        }
    }

    public void Heal(int amount)
    {
        HP += amount;
        if (HP > maxHP)
            HP = maxHP;
        HPText.text = HP + "% HP";
    }

    public void ArmorUp(int amount)
    {
        armor += amount;
        if (armor > 150)
            armor = 150;
        armorText.text = armor + "% Armor";
    }

    public void ScoreUpdate(int amount)
    {
        score += amount;
        scoreText.text = "score: " + score;
    }

    public void Win()
    {
        gameWonUI.SetActive(true);
   
        File.AppendAllText(path, userName1 + " | \tScore: " + score + " | \tTime: " + time + " |  \tWON!\n");
        gameWonText.text = File.ReadAllText(path);
        
    }

}
