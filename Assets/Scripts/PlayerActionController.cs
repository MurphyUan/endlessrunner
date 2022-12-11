using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
    PlayerInput playerInput;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable() 
    {
        EnableActions(playerInput.currentActionMap.ToString());
    }

    private void OnDisable() 
    {
        DisableActions(playerInput.currentActionMap.ToString());
    }

    private void DisableActions(string mapContext)
    {
        switch(mapContext) {
            case "Ground":{
                playerInput.actions["Move"].performed += PlayerMovement.PlayerMove;
                break;
            }
            case "Air":{
                break;
            }
            default:break;
        }
    }

    private void EnableActions(string mapContext)
    {
        switch(mapContext) {
            case "Ground":{
                break;
            }
            case "Air":{
                break;
            }
            default:break;
        }
    }

    public void SwitchActions(string mapContext)
    {
        DisableActions(playerInput.currentActionMap.ToString());
        playerInput.SwitchCurrentActionMap(mapContext);
        EnableActions(mapContext);
    }
}
