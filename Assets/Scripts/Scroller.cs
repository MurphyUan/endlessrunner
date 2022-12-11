using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] private static float minMoveSpeed = 0.01f;
    [SerializeField] private static float maxMoveSpeed = 0.05f;
    private static float moveSpeed;

    private void Awake() 
    {
        moveSpeed = minMoveSpeed;
        // Get MinimumMoveSpeed From Difficulty
        // Get MaximumMoveSpeed From Difficulty
    }

    private void FixedUpdate() 
    {
        this.transform.position += PlayerBehaviour.Player.transform.forward * moveSpeed;
    } 
}
