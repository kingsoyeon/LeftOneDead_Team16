using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
public class ItemDataBomb : InteractablesSOBase
{
    public List<ExplosiveData> ExplosiveDatas = new List<ExplosiveData>();
}
*/

public enum ExplodeType
{
    InstantExplode,
    SpreadFire,
}

public class ExplosiveData
{
    public ExplodeType ExplodeType;
    public float Radius;
    public float Damage;
    public float Duration;
}