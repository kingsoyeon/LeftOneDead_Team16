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
    private Transform target;

    private int count = 0;

    private void Awake() 
    {
        ps = GetComponent<ParticleSystem>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    /// <summary>
    /// 플레이어의 콜라이더 가져온 후 파티클 시스템의 트리거 모듈을 가져와서 활성화합니다. 그 후 인덱스 0에 플레이어 콜라이더를 등록합니다.
    /// </summary>
    private void Start() {
        Collider playerCollider = target.GetComponent<Collider>();
        ParticleSystem.TriggerModule triggerModule = ps.trigger;
        triggerModule.enabled = true;
        triggerModule.SetCollider(0, playerCollider);
    }
    
    /// <summary>
    /// 포이즌 데미지 주기
    /// </summary>
    /// <returns></returns> 
    IEnumerator PoisonCoroutine()
    {
        Debug.Log("PoisonCoroutine 포이즌 데미지 주기");
        
        yield return new WaitForSeconds(poisonDuration);
        poisonCoroutine = null;
    }

    /// <summary>
    /// 파티클 충돌 시 포이즌 데미지 주기
    /// </summary>
    /// <param name="other"></param>
    private void OnParticleCollision(GameObject other)
    {
        if (poisonCoroutine == null)
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
