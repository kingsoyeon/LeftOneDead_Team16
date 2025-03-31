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
    public GameObject vomitPrefab;
    public Transform vomitPoint;

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

        if(vomitPrefab != null)
        {
            vomitPrefab.transform.position = vomitPoint.position;
            vomitPrefab.SetActive(true);
        }
    }
}
