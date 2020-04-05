using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    private static float hRecoilLook = 0f;
    private static float vRecoilLook = 0f; 


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = hRecoilLook + Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = vRecoilLook + Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        vRecoilLook = 0;
        hRecoilLook = 0;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }

    public static void RecoilLook(float up, float right)
    {
        hRecoilLook += right;
        vRecoilLook += up;
    }
}
