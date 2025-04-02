using System;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    [SerializeField] private GameObject explosionFx;

    private void ExplosionEvent()
    {
        Instantiate(explosionFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
