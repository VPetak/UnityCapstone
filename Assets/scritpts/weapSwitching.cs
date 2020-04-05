using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapSwitching : MonoBehaviour
{

    //public GameObject weapRef; //need this to get the ammo ct of the weapon, skip over empty ones like in Doom
    //public Weapon weapRef2;

    public int selectedWeap = 0;

    // Start is called before the first frame update
    void Start()
    {
       // weapRef2 = weapRef.GetComponent<Weapon>(); //need to do this before calling the method, or we wont have ammo referece
        SelectWeapon();
    }

    // Update is called once per frame
    void Update ()
    {
        int prevSelWeap = selectedWeap;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeap >= transform.childCount - 1) //make sure the number doesn't go over max
                selectedWeap = 0;
            else
                selectedWeap++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeap <= 0) //make sure the number doesn't go under min
                selectedWeap = transform.childCount - 1;
            else
                selectedWeap--;
        }

        if (prevSelWeap != selectedWeap)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform) //take all trfms that are children to current and loop thru each referring to current as weapon
        {
            if (i == selectedWeap /* && weapRef2.ammo > 0*/) //wanna check ammo here too
                weapon.gameObject.SetActive(true);
           // else if (weapRef2.ammo <=0)
          //  {
           //     selectedWeap++; //skip over if that weap is out of ammo, make sure melee doesn't run out of ammo
           //     weapon.gameObject.SetActive(false);
           // }
            else 
                weapon.gameObject.SetActive(false);
            i++;
        }
        //weapRef = weapon.gameObject;
    }
}
