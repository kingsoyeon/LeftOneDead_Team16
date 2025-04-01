using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "GameData/Weapon")]
public class GunData : ScriptableObject
{
    public BulletBallistics BulletData;
    public float MuzzleVelocityModifier = 1f;
    public int MagazineCapacity;
    public int MaxAmmoCanHold;
    public float RoundPerMinute;
    public float ReloadingTime;
}
