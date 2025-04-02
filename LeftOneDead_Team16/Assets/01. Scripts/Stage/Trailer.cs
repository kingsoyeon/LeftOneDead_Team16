using UnityEngine;

public class Trailer : MonoBehaviour, IExplosive
{
    [SerializeField] private AudioClip explodeSound;
    [SerializeField] private GameObject explosionFx;

    public void Explosion()
    {
        SoundManager.PlayClip(explodeSound);
        Instantiate(explosionFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
