using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillType
{
    Vomit,
    Explosion,
}
public class Skill : MonoBehaviour
{
    [field: SerializeField] public SkillType skillType { get; private set; }

    [Header("If you use Particle System, you can use this")]
    public ParticleSystem skillParticle;



    void Awake()
    {
        if(skillParticle != null)
        {
            skillParticle.Stop();
        }
    }

    public void UseSkill()
    {
        switch(skillType)
        {
            case SkillType.Vomit:
                Vomit();
                break;
            case SkillType.Explosion:
                Explosion();
                break;
        }
    }

    private void Vomit()
    {
        Debug.Log("Vomit");

        if(skillParticle != null)
        {
            skillParticle.Play();
        }
    }

    private void Explosion()
    {
        Debug.Log("Explosion");

        if(skillParticle != null)
        {
            skillParticle.Play();
        }
        
    }
}
