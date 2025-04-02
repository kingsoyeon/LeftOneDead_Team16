using System;
using UnityEngine;

public class Trailer : MonoBehaviour, IExplosive
{
    [SerializeField] private AudioClip explodeSound;
    [SerializeField] private GameObject explosionFx;

    private void Start()
    {
        StageManager.Instance.AddActionToEventActionDict(1, Explosion);
    }

    public void Explosion()
    {
        SoundManager.PlayClip(explodeSound);
        Instantiate(explosionFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
