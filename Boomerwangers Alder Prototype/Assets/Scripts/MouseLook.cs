using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100;
    public Transform PlayerBody;

    private float xRot = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            mouseSensitivity = mouseSensitivity + 50;
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            mouseSensitivity = mouseSensitivity - 50;
            if(mouseSensitivity < 1)
            {
                mouseSensitivity = 1;
            }
        }

    }
}
