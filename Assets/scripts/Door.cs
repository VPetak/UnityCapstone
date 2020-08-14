using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public int keyID = 0; //if keyID = 0, there is no key required. Else it will require a numbered key.
    //convention for keys is as follows:    1 = yellow      2 = red     3 = blue      4 = green     5 = pink

    public GameObject playerRef;
    public PlayerController playerContrRef;
    private bool opened = false;

    // Start is called before the first frame update
    void Start()
    {
        playerContrRef = playerRef.GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            //include an if statement for if the player has the key
            if (playerContrRef.key[keyID] == true)
                OpenDoor();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
            CloseDoor();
    }

    void OpenDoor()
    {
        if (!opened)
        {
            transform.Translate(0, 250f * Time.deltaTime, 0, 0);
            opened = true;
        }
    }

    void CloseDoor()
    {
        if (opened)
        {
            transform.Translate(0, -250f * Time.deltaTime, 0, 0);
            opened = false;
        }
    }

}
