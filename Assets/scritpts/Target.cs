using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Target : MonoBehaviour
{

    public float health = 50f;

    PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); //need this for scorekeeping, might refactor damage taking here too
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        playerController.ScoreUpdate(100);
        Destroy(gameObject);
    }
}
