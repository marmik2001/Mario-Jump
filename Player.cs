using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private int count_coins;
    private Rigidbody rigidBodyComponent;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    
   

    // Start is called before the first frame update
    void Start(){
        count_coins = 0;
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    /* Update is called once per frame
    We'll take all input in this method*/
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            //Debug.Log("Space Key Was Pressed Down");
            jumpKeyWasPressed = true;
        }
        horizontalInput = Input.GetAxis("Horizontal");
    }

    /*FixedUpdate is called once every physics frame
    We apply all physics in this method*/
    private void FixedUpdate(){
        rigidBodyComponent.velocity = new Vector3(0, rigidBodyComponent.velocity.y, horizontalInput*2);

        /* 
        CheckSpere can be used as well like this:
        if(Physics.CheckSphere(groundCheckTransform.position, 0.1f, playerMask))
            return;
        OverlapSphere also gives which colliders you are colliding with and sometimes that is useful
        */
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f,playerMask).Length==0)
            return;

        if (jumpKeyWasPressed){
            rigidBodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.layer == 9){
            Destroy(other.gameObject);
            count_coins++;
        }
    }
}
