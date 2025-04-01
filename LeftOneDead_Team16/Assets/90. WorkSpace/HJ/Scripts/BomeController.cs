using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour, IInteractable, IDamageable
{
    public List<ExplosiveData> ExplosiveDatas = new List<ExplosiveData>();
    /// <summary>
    /// 장판 효과 오브젝트 프리팹
    /// </summary>
    public GameObject SpreadPrefab;
    /// <summary>
    /// 몇 초 뒤에 터질것인지
    /// </summary>
    public float ExplodeAfterTime;
    /// <summary>
    /// 몇 번 맞으면 바로 터질것인지
    /// </summary>
    public int ImmediateExplodeCount;
    /// <summary>
    /// 몇 번 까진 맞아도 괜찮은지
    /// </summary>
    public int IgnoreExplodeCount;
    /// <summary>
    /// 지금 몇 번 맞았는지
    /// </summary>
    private int strikedCount;

    //폭발 타이머 돌아가는중인지 확인하기 위한 타이머
    private Coroutine explodeCoroutine = null;

    private List<IDamageable> damageables = new List<IDamageable>();

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        strikedCount++;
        if (explodeCoroutine == null && strikedCount > IgnoreExplodeCount)
        {
            explodeCoroutine = StartCoroutine(ExplodeCoroutine());
        }
    }

    void Explode()
    {
        foreach (var v in ExplosiveDatas)
        {
            switch (v.ExplodeType)
            {
                case ExplodeType.InstantExplode:
                    InstantExplode(v);
                    break;
                case ExplodeType.SpreadFire:
                    SpreadExplode(v);
                    break;
            }
        }
    }

    private IEnumerator ExplodeCoroutine()
    {
        float startTime = Time.time;
        while (Time.time - startTime <= ExplodeAfterTime)
        {

            if (strikedCount > ImmediateExplodeCount) break;
            yield return null;
        }
        Explode();
    }

    void InstantExplode(ExplosiveData data)
    {
        //범위 안의 데미지 입을 수 있는 오브젝트들 리스트에 넣기
        Collider[] colliders = Physics.OverlapSphere(transform.position, data.Radius);
        foreach (Collider collider in colliders)
        {
            IDamageable damageable;
            if (collider.TryGetComponent(out damageable))
            {
                damageables.Add(damageable);
            }
        }

        //리스트에 있는 오브젝트들 데미지 입히기
        foreach (IDamageable damageable in damageables)
        {
            damageable.TakeDamage((int)data.Damage);
        }
    }

    void SpreadExplode(ExplosiveData data)
    {
        Debug.Log("화염 효과 미구현");
        throw new System.NotImplementedException();
    }
}
