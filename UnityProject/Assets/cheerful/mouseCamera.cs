// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class mouseCamera : MonoBehaviour
// {
//     public float speedH = 2.0f;
//     public float speedV = 2.0f;

//     private float yaw = 0.0f;
//     private float pitch = 0.0f;
	
//     // Start is called before the first frame update
//     void Start()
//     {
// 		Cursor.lockState = CursorLockMode.Locked;
// 		Cursor.visible = false;
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         Camera movingCam = GameObject.Find("Camera").GetComponent<Camera>();
// 		// Debug.Log(movingcam.transform.position);

//         yaw += speedH * Input.GetAxis("Mouse X") ;
//         pitch -= speedV * Input.GetAxis("Mouse Y");
//         transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
// 		if (Input.GetKeyDown(KeyCode.Escape))
// 		{
//         	//Mistake happened here vvvv
// 			Cursor.lockState = CursorLockMode.None;
// 			Cursor.visible = true;
// 		}

// 		if (Cursor.visible && Input.GetMouseButtonDown(1))
// 		{
// 			Cursor.lockState = CursorLockMode.Locked;
// 			Cursor.visible = false;
// 		}
        
//     }
// }

using UnityEngine;
using System.Collections;
 
public class mouseCamera : MonoBehaviour {
 
    /*
    Writen by Windexglow 11-13-10.  Use it, edit it, steal it I don't care.  
    Converted to C# 27-02-13 - no credit wanted.
    Simple flycam I made, since I couldn't find any others made public.  
    Made simple to use (drag and drop, done) for regular keyboard layout  
    wasd : basic movement
    shift : Makes camera accelerate
    space : Moves camera on X and Z axis only.  So camera doesn't gain any height*/
     
     
    float mainSpeed = 10.0f; //regular speed
    float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 1000.0f; //Maximum speed when holdin gshift
    float camSens = 0.03f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun= 1.0f;
         void Start()
    {
		// Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
        
    }
    void Update () {
        
        lastMouse = Input.mousePosition - lastMouse ;
        lastMouse = new Vector3( lastMouse.y * camSens, lastMouse.x * camSens,  0 );
        lastMouse = new Vector3( transform.eulerAngles.x + lastMouse.x, 0, transform.eulerAngles.z + lastMouse.y);
        transform.eulerAngles = lastMouse;
        lastMouse =  Input.mousePosition;
        //Mouse  camera angle done.  
       
        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0){ // only move while a direction key is pressed
          if (Input.GetKey (KeyCode.LeftShift)){
              totalRun += Time.deltaTime;
              p  = p * totalRun * shiftAdd;
              p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
              p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
              p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
          } else {
              totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
              p = p * mainSpeed;
          }
         
          p = p * Time.deltaTime;
          Vector3 newPosition = transform.position;
          if (Input.GetKey(KeyCode.Space)){ //If player wants to move on X and Z axis only
              transform.Translate(p);
              newPosition.x = transform.position.x;
              newPosition.z = transform.position.z;
              transform.position = newPosition;
          } else {
              transform.Translate(p);
          }
        }
    }
     
    private Vector3 GetBaseInput() { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey (KeyCode.W)){
            p_Velocity += new Vector3(0, 0 , 1);
        }
        if (Input.GetKey (KeyCode.S)){
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey (KeyCode.A)){
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey (KeyCode.D)){
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
