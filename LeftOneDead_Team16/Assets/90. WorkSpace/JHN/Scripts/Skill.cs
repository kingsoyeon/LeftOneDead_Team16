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

    [Header("Vomit")]
    public ParticleSystem vomitParticle;

    void Awake()
    {
        if(vomitParticle != null)
        {
            vomitParticle.Stop();
        }
    }

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

        if(vomitParticle != null)
        {
            vomitParticle.Play();
        }
    }
}
