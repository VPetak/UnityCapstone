using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int bulletDam = 15;
    public float bulletSpeed = 5f;
    public Rigidbody rb;
    private Vector3 direction;
    public bool enemyFire = true;
    public GameObject fpsCam;

    // Start is called before the first frame update
    void Start()
    {
        
        if (enemyFire)
        {
            direction = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            RaycastHit pHit;
            Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out pHit, 100);
            direction = pHit.point - transform.position;
        }
        
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && enemyFire)
        {
            PlayerController.instance.TakeDamage(bulletDam);
        }
    }
}
