using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    StateChange stateChange;

    // Start is called before the first frame update
    void Start()
    {
        stateChange = StateChange.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(stateChange.gameIsActive)
        {
            // Set roation input

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Setting for looking up and down

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Setting for looking sideways

            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
