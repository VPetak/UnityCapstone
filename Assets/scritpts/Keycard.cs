using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    public int keyID = 1; //if keyID = 0, there is no key required. Else it will require a numbered key.
    //convention for keys is as follows:    1 = yellow      2 = red     3 = blue      4 = green     5 = pink

    public GameObject playerRef;
    public PlayerController playerContrRef;
    public GameObject uiKeyCard;

    // Start is called before the first frame update
    void Start()
    {
        playerContrRef = playerRef.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerContrRef.key[keyID] = true;
            uiKeyCard.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
