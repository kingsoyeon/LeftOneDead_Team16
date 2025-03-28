using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [field:SerializeField] private EnemySO enemySO;
    [field:SerializeField] private EnemyStateMachine stateMachine;

    public CharacterController characterController;
    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
        characterController = GetComponent<CharacterController>();
    }
}