using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerscript : MonoBehaviour
{
    public CharacterController CC;
    public float speed;
    private float sprint = 0f;
    public float MouseSensitivity;

    //Place Main Camera within this
    public Transform CamTransform;
    //Set to 0 so it can be changed later
    private float camRotation = 0f;

    //Animation
    public Animator Ani;

    void Start()
    {
        //Hides cursor and locks it to edges of screen 
        Cursor.lockState = CursorLockMode.Locked;
}

    
    void Update()
    {
        //establishes Vector3 as all zeroes to begin
        Vector3 movement = Vector3.zero;

        //4-directional movement
        float forwardMove = Input.GetAxis("Vertical") * (speed + sprint) * Time.deltaTime;
        float sideMove = Input.GetAxis("Horizontal") * (speed + sprint) * Time.deltaTime;

        movement += (transform.forward * forwardMove) + (transform.right * sideMove);

        //Links movement to character controller
        CC.Move(movement);


        //Camera movement and player rotation:

        //Rotates camera up and down based on Mouse Y axis
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;
        camRotation -= mouseY;
        //Sets boundaries for the camera rotation
        camRotation = Mathf.Clamp(camRotation, -20f, 20f);
        CamTransform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0f, 0f));

        //Rotates player (and camera as well as children of camera) based on Mouse X axis
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseX, 0f));

        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            sprint = 3f;
            Ani.SetBool("Sprinting", true);
        }
        else
        {
            sprint = 0f;
            Ani.SetBool("Sprinting", false);
        }

        //Animation
        if((forwardMove != 0) || (sideMove != 0))
        {
            Ani.SetBool("Walking", true);
        }
        if(forwardMove == 0 && sideMove == 0)
        {
            Ani.SetBool("Walking", false);
        }
    }


}
