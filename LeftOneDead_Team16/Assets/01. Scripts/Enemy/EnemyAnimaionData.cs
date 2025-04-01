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

    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string jumpParameterName = "Jump";
    [SerializeField] private string fallParameterName = "Fall";

    public int IdleParameterName {get; private set;}
    public int MoveParameterName {get; private set;}
    public int AttackParameterName {get; private set;}
    public int SkillAttackParameterName {get; private set;}
    public int DeathParameterName {get; private set;}

    public int AirParameterName {get; private set;}
    public int JumpParameterName {get; private set;}
    public int FallParameterName {get; private set;}

    public void Initialize()
    {
        IdleParameterName = Animator.StringToHash(idleParameterName);
        MoveParameterName = Animator.StringToHash(moveParameterName);
        AttackParameterName = Animator.StringToHash(attackParameterName);
        SkillAttackParameterName = Animator.StringToHash(skillAttackParameterName);
        DeathParameterName = Animator.StringToHash(deathParameterName);

        AirParameterName = Animator.StringToHash(airParameterName);
        JumpParameterName = Animator.StringToHash(jumpParameterName);
        FallParameterName = Animator.StringToHash(fallParameterName);
    }

}
