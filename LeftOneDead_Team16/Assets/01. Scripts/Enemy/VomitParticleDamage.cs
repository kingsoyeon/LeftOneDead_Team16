using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class VomitParticleDamage : MonoBehaviour
{
    public int damage = 10;
    public float poisonDuration = 2.5f;

    Coroutine poisonCoroutine;

    private ParticleSystem ps;
    private List<ParticleSystem.Particle> enter = new();

    private int count = 0;

    private void Awake() 
    {
        ps = GetComponent<ParticleSystem>();
    }
    
    /// <summary>
    /// 포이즌 데미지 주기
    /// </summary>
    /// <returns></returns> 
    IEnumerator PoisonCoroutine()
    {
        Debug.Log("포이즌 데미지 주기");
        yield return new WaitForSeconds(poisonDuration);
        poisonCoroutine = null;
    }

    /// <summary>
    /// 파티클 충돌 시 포이즌 데미지 주기
    /// </summary>
    /// <param name="other"></param>
    private void OnParticleCollision(GameObject other)
    {
        if(poisonCoroutine == null)
        {
            poisonCoroutine = StartCoroutine(PoisonCoroutine());
        }
        if(other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            Debug.Log("포이즌 데미지 주기");
            damageable.TakeDamage(damage);
        }
    }


    /// <summary>
    /// 포이즌 트리거
    /// </summary>
    void OnParticleTrigger()
    {
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);    
        Debug.Log("numEnter: " + numEnter);

        if(numEnter != 0)
        {
            if(poisonCoroutine == null)
            {
                poisonCoroutine = StartCoroutine(PoisonCoroutine());
            }
        }
        else
        {
            Debug.Log("OnParticleTrigger: 포이즌 데미지 종료");
        }
    }

    // 나는 너무 낡고 지쳤어요...............

}
