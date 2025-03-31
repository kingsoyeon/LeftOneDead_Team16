using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    private float jumpCooldownTimer = 0;
    [SerializeField] private float jumpCooldownTime = 0.2f;

    [SerializeField] private CharacterController controller;

    private float verticalVelocity;

    public Vector3 Movement => Vector3.up * verticalVelocity;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(jumpCooldownTimer > 0f)
        {
            jumpCooldownTimer -= Time.deltaTime;
        }

        if (controller.isGrounded && jumpCooldownTimer <= 0f)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }

    public void Jump(float jumpForce)
    {
        jumpCooldownTimer = jumpCooldownTime;
        verticalVelocity += jumpForce;
    }
}
