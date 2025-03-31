using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitParticleDamage : MonoBehaviour
{
    public int damage = 10;

    private void OnParticleCollision(GameObject other)
    {
        if(other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }
    }
}
