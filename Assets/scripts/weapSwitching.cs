using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapSwitching : MonoBehaviour
{
    public int selectedWeap = 0;

    // Start is called before the first frame update
    void Start()
    {
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
            if (i == selectedWeap) //wanna check ammo here too
                weapon.gameObject.SetActive(true);
            else 
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
