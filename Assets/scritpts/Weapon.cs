using System.Collections;
//using System.Random;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Weapon : MonoBehaviour
{

    // public int weaponSelected = 0;
    public float damage = 10f;
    public float range = 100f;
    public float rof = 15f;
    public float vRecoil = 1f;
    public float hRecoil = 0.3f;
    public float randomRclFactor = 3f;
    public float spread = 0.1f;
    public int capacity = 20; //how much ammo is used between reloads
    public float reloadSpeed = 90f;
    public int ammo = 120;
    public int ammoUsedPerShot = 1;
    private int curClip;
    public Sprite weaponSprite;
    public float impactForce = 30f;
    public AudioClip fireSound;
    private AudioSource audio;
    private float nextFire = 0f;
    public Image weapImgObj;
    public GameObject projectile;
    public bool hitscan = true;
    public Text ammoText;
    private int temp = 0; //this will be used for reloading math
    //private Random random = new Random();



    public Camera fpsCam;
    public GameObject impactEffect;

    private Vector3 inFront = new Vector3(0, 0, 4);

    void Start()
    {
        audio = GetComponent<AudioSource>();
        curClip = capacity;
    }

    void OnEnable()
    {
        weapImgObj.sprite = weaponSprite;
        ammoText.text = curClip.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        System.Random random = new System.Random();
        if (Input.GetButton("Fire1") && Time.time >= nextFire) //GetButtonDown for semi, GetButton to hold it (full auto)
        {
            nextFire = Time.time + 1f / rof;  //set the next time that the weapon can fire, 
            Shoot();
            audio.PlayOneShot(fireSound, 0.5f);
            float randomRcl = random.Next((int)(randomRclFactor * -1), (int)randomRclFactor);
            MouseLook.RecoilLook(vRecoil, hRecoil + randomRcl);
        }
    }

    void Shoot()
    {
        if (curClip <= 0) //temporary way of handling reloads
        {
            nextFire += reloadSpeed;
            ammo = ammo - (capacity - curClip); //only reload the amount mising
            if (ammo < 0)
                ammo = 0; //failsafe so player doesn't end up in ammo debt with negative numbers
            if (ammo < capacity)
                temp = capacity - ammo;
            else
                temp = 0;
            curClip = capacity - temp;
        }
        curClip = curClip - ammoUsedPerShot;
        ammoText.text = curClip.ToString();

        RaycastHit hit;
        Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range);
        Debug.DrawLine(fpsCam.transform.position, hit.point, Color.green, 2.5f);
        if (hitscan) //for things such as high-speed bullets and lasers
        {
            // Hitscan type hit detection, damages target (see target script)
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                GameObject impactEf = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(fpsCam.transform.forward));
                Destroy(impactEf, 2f); //create the impact effect, then destroy it

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
                print("hitscan: " + hit.point);
            }
        }

        else //for non-hitscan things, such as fireballs or rockets
        {
               // Target target = hit.transform.GetComponent<Target>();
                GameObject bulletProj = Instantiate(projectile, this.transform.position + inFront, Quaternion.LookRotation(fpsCam.transform.forward));
                //print("gun: " + hit.point);
                Destroy(bulletProj, 7f);
                //audio.PlayOneShot(fireSound, 0.5f);
                //burst--;
        }
    }
}
