 using UnityEngine;
 using System.Collections;
 
 public class FixCamera : MonoBehaviour
 {
     public Vector3 offsetWorldPosition;
     private Vector3 fixedRotation;
     private float x;
     private float z;
 
     private void Awake()
     {
         fixedRotation = transform.position;
        //  x = transform.position.x;
        //     z = transform.position.z;

         //offsetWorldPosition = transform.localPosition; // Add this if you don't want to set the value in the inspector
     }
 
     private void LateUpdate()
     {
         transform.position = fixedRotation;
        //  x = fixedRotation.x;
        // z = fixedRotation.z;

     }
 }