using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerActionController playerActions;
    public static PlayerMovement Singleton;
    public Rigidbody rb;

    [SerializeField]
    private float jumpHeight;

    public float JumpHeight{
        get { return jumpHeight;}
        set { jumpHeight = value;}
    }

    private Vector3 targetDestination;

    private Transform cameraTransform;
    // private Animator animator;

    private bool isJumping = false;
    // private bool isSliding = false;
    private bool isMoving = false;

    // private static Coroutine slideCoroutine;
    private static Coroutine jumpCoroutine;
    private static Coroutine moveCoroutine;

    [SerializeField]
    // private float cameraSlideAngle = 25;

    void Awake()
    {
        Singleton = this;
        playerActions = GetComponent<PlayerActionController>();
        rb = GetComponent<Rigidbody>();
    }

    public static void PlayerMove(InputAction.CallbackContext context){
        Vector3 velocity = context.ReadValue<Vector2>();
        if(velocity.normalized == Vector3.zero)return;

        if(!PlayerMovement.Singleton.isMoving) {
            PlayerMovement.moveCoroutine = PerformCoroutine(
                MoveToPosition(
                    PlayerBehaviour.Player.transform,
                    PlayerBehaviour.Player.transform.position + (velocity * LaneBuilder.Singleton.LaneWidth),
                    1));
        }
        
        //PlayerBehaviour.Player.transform.Translate(velocity * LaneBuilder.Singleton.LaneWidth);
    }

    public static IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        PlayerMovement.Singleton.isMoving = true;
        Vector3 currentPosition = transform.position;
        float elaspedTime = 0f;
        
        while(elaspedTime <= 1f)
        {
            elaspedTime += Scroller.moveSpeed / timeToMove;
            transform.position = Vector3.Lerp(currentPosition, position, elaspedTime);
            Camera.main.transform.position =  new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
            yield return 0;
        }
        transform.position = position;
        Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        PlayerMovement.Singleton.isMoving = false;
    }

    public static void PlayerJump(InputAction.CallbackContext context)
    {
        if(PlayerMovement.Singleton.isJumping)return;
        // else if(PlayerMovement.Singleton.isSliding){
        //     PlayerMovement.Singleton.StopCoroutine(PlayerMovement.slideCoroutine);
        //     PlayerMovement.PerformSlide();
        //     return;
        // }
        // Activate State
        PlayerMovement.Singleton.isJumping = true;
        PlayerActionController.SwitchActions("Air");
        PlayerMovement.Singleton.rb.AddForce(Vector3.up * (PlayerMovement.Singleton.JumpHeight), ForceMode.Impulse);
    }

    // public static void PlayerSlide(InputAction.CallbackContext context)
    // {
    //     if (!PlayerMovement.Singleton.isSliding || !PlayerMovement.Singleton.isJumping) return;

    //     PerformSlide();

    //     slideCoroutine = PerformCoroutine(DelayedCall(1, PerformSlide));
    // }

    // private static void PerformSlide()
    // {
    //     int modifier = PlayerMovement.Singleton.isSliding ? -1 : 1;
    //     Camera.main.transform.RotateAround(PlayerBehaviour.Player.transform.position, Vector3.right, modifier * PlayerMovement.Singleton.cameraSlideAngle);
    //     PlayerMovement.Singleton.isSliding ^= true;
    // }

    public static void EndJump(InputAction.CallbackContext context)
    {
        // Apply Downwards Force until isJumping is false
        PlayerMovement.Singleton.rb.AddForce(Vector3.down * (PlayerMovement.Singleton.JumpHeight), ForceMode.Impulse);
        endJump();
    }

    public static void endJump()
    {
        PlayerMovement.Singleton.isJumping = false;
        PlayerActionController.SwitchActions("Ground");
    }

    public static IEnumerator DelayedCall(float delay, Action action){
        // Wait Delay
        yield return new WaitForSeconds(delay);
        // Perform Action
        action();
        // End Method Call
        yield break;
    }

    private void onColliderEnter(Collider other)
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        switch(other.tag){
            case "Ground":{
                endJump();
                break;
            }
            case "Obstacle":{
                Utils.PublishPlayerDeathEvent();
                break;
            }
            default:{
                Debug.Log(other.tag);
                break;
            };
        }
    }

    public static Coroutine PerformCoroutine(IEnumerator numerator)
    {
        return PlayerMovement.Singleton.StartCoroutine(numerator);
    }

    public static new Coroutine StopCoroutine(IEnumerator numerator)
    {
        return StopCoroutine(numerator);
    }
}