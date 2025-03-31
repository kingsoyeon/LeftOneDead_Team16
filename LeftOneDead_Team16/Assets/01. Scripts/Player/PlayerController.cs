using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Inputs playerInputs { get; private set; }
    public Inputs.PlayerActions playerActions { get; private set; }

    private void Awake()
    {
        playerInputs = new Inputs();
        playerActions = playerInputs.Player;
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }
}
