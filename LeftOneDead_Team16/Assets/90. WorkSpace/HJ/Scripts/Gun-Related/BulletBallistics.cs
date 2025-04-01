using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Bullet", menuName ="GameData/Bullet")]
public class BulletBallistics : ScriptableObject
{
    public float MuzzleVelocity;
    public float Mass;
    public float DragCoefficient;
    public float CrossSectionArea;
}
