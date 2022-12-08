using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody rb;

    [SerializeField]
    private float jumpHeight;
    private float laneSpeed = 5;

    public float JumpHeight{
        get { return jumpHeight;}
        set { jumpHeight = value;}
    }

    private Vector3 targetDestination;

    private Transform cameraTransform;
    // private Animator animator;

    private bool isJumping = false;
    private bool isSliding = false;
    private bool isTurning = false;

    private Coroutine slideCoroutine;
    private Coroutine jumpCoroutine;

    [SerializeField]
    private float cameraSlideAngle = 25;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        try{
            PlayerMove(playerInput.actions["Move"].ReadValue<Vector2>());
            PlayerJump(playerInput.actions["Jump"].ReadValue<bool>());
            PlayerSlide(playerInput.actions["Slide"].ReadValue<bool>());
        } catch(UnityException e) {
            Debug.Log(e.StackTrace);
        }
    }

    private void PlayerMove(Vector2 velocity)
    {
        if(velocity.normalized == Vector2.zero)return;

        this.transform.Translate(velocity);
    }

    private void PlayerJump(bool activatedkey)
    {
        if(!activatedkey || isJumping)return;
        else if(isSliding){
            StopCoroutine(slideCoroutine);
            PerformSlide();
            return;
        }
        // Activate State
        this.isJumping = true;
        playerInput.SwitchCurrentActionMap("Air");
        
        
    }

    private void PlayerSlide(bool activatedkey)
    {
        if(!activatedkey || isSliding  || isJumping) return;
       
       PerformSlide();

        //start coroutine to stop sliding after delay
        float someDelay = 5f;
        slideCoroutine = StartCoroutine(DelayedCall(someDelay, PerformSlide));
    }

    private void PerformSlide()
    {
        int modifier = isSliding ? -1 : 1;
        cameraTransform.RotateAround(this.transform.position, Vector3.right, cameraSlideAngle);
        isSliding = !isSliding;
    }

    private void EndJump()
    {

    }

    private IEnumerator DelayedCall(float delay, Action action){
        // Wait Delay
        yield return new WaitForSeconds(delay);
        // Perform Action
        action();
        // End Method Call
        yield break;
    }

    private void RotatePlayer(Vector3 rotation, Vector3 translation)
    {
        if(isTurning)
            this.transform.Rotate(rotation);
        else
            this.transform.Translate(translation);
    }

    private void onColliderEnter(Collider other)
    {
        switch(other.tag){
            case "Floor":{
                isJumping = false;
                playerInput.SwitchCurrentActionMap("Floor");
                break;
            }
            default:break;
        }
    }

}