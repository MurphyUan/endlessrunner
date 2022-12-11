using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerActionController playerActions;
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

    private static bool isJumping = false;
    private static bool isSliding = false;

    private Coroutine slideCoroutine;
    private Coroutine jumpCoroutine;

    [SerializeField]
    private float cameraSlideAngle = 25;

    void Awake()
    {
        playerActions = GetComponent<PlayerActionController>();
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    public static void PlayerMove(InputAction.CallbackContext context){
        Vector2 velocity = context.ReadValue<Vector2>();
        if(velocity.normalized == Vector2.zero)return;

        PlayerBehaviour.Player.transform.Translate(velocity);
    }

    public static void PlayerJump(InputAction.CallbackContext context)
    {
        if(isJumping)return;
        else if(isSliding){
            StopCoroutine(slideCoroutine);
            PerformSlide();
            return;
        }
        // Activate State
        this.isJumping = true;
        playerActions.SwitchActions("Air");
        Debug.Log("Player Jumped, Action Map Air");
    }

    public void PlayerSlide(InputAction.CallbackContext context)
    {
        if (isSliding || isJumping) return;

        PerformSlide();
        Debug.Log("Player Sliding");

        float someDelay = 5f;
        slideCoroutine = StartCoroutine(DelayedCall(someDelay, PerformSlide));
    }

    private static void PerformSlide()
    {
        int modifier = isSliding ? -1 : 1;
        cameraTransform.RotateAround(PlayerBehaviour.Player.transform.position, Vector3.right, modifier * cameraSlideAngle);
        isSliding = !isSliding;
    }

    public void EndJump(InputAction.CallbackContext context)
    {
        // Apply Downwards Force until isJumping is false
    }

    private IEnumerator DelayedCall(float delay, Action action){
        // Wait Delay
        yield return new WaitForSeconds(delay);
        // Perform Action
        action();
        // End Method Call
        yield break;
    }

    public void RotatePlayer(Vector3 rotation, Vector3 translation)
    {
        if(PlayerBehaviour.CanTurn)
            this.transform.Rotate(rotation);
        else
            this.transform.Translate(translation);
    }

    private void onColliderEnter(Collider other)
    {
        switch(other.tag){
            case "Floor":{
                isJumping = false;
                playerActions.SwitchActions("Ground");
                break;
            }
            default:break;
        }
    }

}