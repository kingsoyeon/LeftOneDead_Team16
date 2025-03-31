using UnityEngine;
using System;

[Serializable]
public class EnemyAnimaionData
{
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string moveParameterName = "Move";
    [SerializeField] private string attackParameterName = "Attack";
    [SerializeField] private string skillAttackParameterName = "SkillAttack";
    [SerializeField] private string deathParameterName = "Death";

    public int IdleParameterName {get; private set;}
    public int MoveParameterName {get; private set;}
    public int AttackParameterName {get; private set;}
    public int SkillAttackParameterName {get; private set;}
    public int DeathParameterName {get; private set;}


    public void Initialize()
    {
        IdleParameterName = Animator.StringToHash(idleParameterName);
        MoveParameterName = Animator.StringToHash(moveParameterName);
        AttackParameterName = Animator.StringToHash(attackParameterName);
        SkillAttackParameterName = Animator.StringToHash(skillAttackParameterName);
        DeathParameterName = Animator.StringToHash(deathParameterName);
    }

}
