using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillType
{
    Vomit,
}
public class Skill : MonoBehaviour
{
    [field: SerializeField] public SkillType skillType { get; private set; }

    public void UseSkill()
    {
        switch(skillType)
        {
            case SkillType.Vomit:
                Vomit();
                break;
        }
    }

    private void Vomit()
    {
        Debug.Log("Vomit");
    }
}
