using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTestController : MonoBehaviour
{
    public float proximityTrig = 50f; //how close the player should be before ai activates
    public Rigidbody rb2d;
    public float speed;
    public float rof = 8f;
    private float nextFire = 0f;
    private int burst = 7;
    public GameObject projectile;

    private NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.SetDestination(PlayerController.instance.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < proximityTrig)
        {
            navMeshAgent.SetDestination(PlayerController.instance.transform.position);
            ShootPlayer(PlayerController.instance.transform.position);
        }
    }

    void ShootPlayer(Vector3 dir)
    {
        if (Time.time >= nextFire) //GetButtonDown for semi, GetButton to hold it (full auto)
        {
            nextFire = Time.time + 1f / rof;  //set the next time that the weapon can fire, 
            if (burst <= 0)
            {
                nextFire += 1.5f;
                burst = 7;
            }
            GameObject bulletProj = Instantiate(projectile, this.transform.position, Quaternion.LookRotation(this.transform.position));
            Destroy(bulletProj, 4f);
            burst--;
        }
    }
}
