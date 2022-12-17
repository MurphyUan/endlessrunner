using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
    private static PlayerInput playerInput;
    bool hasAwoken = false;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        EnableActions(playerInput.defaultActionMap);
        hasAwoken = true;
    }

    private void OnEnable() 
    {
        if(hasAwoken)
        EnableActions(playerInput.currentActionMap.name);
    }

    private void OnDisable() 
    {
        DisableActions(playerInput.currentActionMap.name);
    }

    private static void DisableActions(string mapContext)
    {
        switch(mapContext) {
            case "Ground":{
                playerInput.actions["Jump"].performed -= PlayerMovement.PlayerJump;
                // playerInput.actions["Slide"].performed -= PlayerMovement.PlayerSlide;
                break;
            }
            case "Air":{
                playerInput.actions["EndJump"].performed -= PlayerMovement.EndJump;
                break;
            }
            default:{
                Debug.Log($"Switch Escaped with string:{mapContext}");
                break;
            }
        }

        playerInput.actions["Move"].performed -= PlayerMovement.PlayerMove;
        playerInput.actions["ShowMenu"].performed -= SceneController.Singleton.UpdateVisibility;
    }

    private static void EnableActions(string mapContext)
    {
        switch(mapContext) {
            case "Ground":{
                Debug.Log($"{mapContext} Was Called");
                playerInput.actions["Jump"].performed += PlayerMovement.PlayerJump;
                // playerInput.actions["Slide"].performed += PlayerMovement.PlayerSlide;
                break;
            }
            case "Air":{
                Debug.Log($"{mapContext} Was Called");
                playerInput.actions["EndJump"].performed += PlayerMovement.EndJump;
                break;
            }
            default:{
                Debug.Log($"Switch Escaped with string:{mapContext}");
                break;
            }
        }

        playerInput.actions["Move"].performed += PlayerMovement.PlayerMove;
        playerInput.actions["ShowMenu"].performed += SceneController.Singleton.UpdateVisibility;
    }

    public static void SwitchActions(string mapContext)
    {
        DisableActions(playerInput.currentActionMap.name);
        playerInput.SwitchCurrentActionMap(mapContext);
        EnableActions(mapContext);
    }
}
