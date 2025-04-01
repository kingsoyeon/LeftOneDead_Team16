using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ExplodeType
{
    InstantExplode,
    SpreadFire,
}

[Serializable]
public class ExplosiveData
{
    public ExplodeType ExplodeType;
    public float Radius;
    public float Damage;
    public float Duration;
}