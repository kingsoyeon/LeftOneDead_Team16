using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "GameData/Weapon")]
public class GunData : ScriptableObject
{
    public BulletBallistics BulletData;
    public float MuzzleVelocityModifier = 1f;
    public int MagazineCapacity;
    public float RoundPerMinute;
    public float ReloadingTime;
}
