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
    //GameObject bulletProj = Instantiate(projectile, this.transform.position, Quaternion.LookRotation(this.transform.position));

    // Start is called before the first frame update
    void Start()
    {
        
        if (enemyFire)
        {
            direction = PlayerController.instance.transform.position - transform.position;
        }
        else
        {
            //direction = fpsCam.transform.forward - transform.position;
            RaycastHit pHit;
            Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out pHit, 100);
            direction = pHit.point - transform.position;
            //print("bullet: " + pHit.point);
        }
        
        direction.Normalize();
        //direction = direction * bulletSpeed;
        //print(direction);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * bulletSpeed;
        //print(rb.velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && enemyFire)
        {
            PlayerController.instance.TakeDamage(bulletDam);
            //Destroy(gameObject);
        }
       // Destroy(bulletProj);
    }

    private void OnCollision(Collision other)
    {
       /// Destroy(this.gameObject);
      //  if (other.tag == "Player")
      //  {
      //      PlayerController.instance.TakeDamage(bulletDam);
            //Destroy(gameObject);
      //  }
    }
}
