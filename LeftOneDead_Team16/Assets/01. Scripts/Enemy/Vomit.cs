using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vomit : MonoBehaviour
{
    public float damagePerSecond = 10f;
    public float duration = 5f;
    public float elapsedTime = 0f;

    
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= duration)
        {
            gameObject.SetActive(false);
            elapsedTime = 0f;
        }
    }

    /// <summary>
    /// 트리거 콜라이더에 닿은 대상에게 데미지 주기
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            int damage = Mathf.RoundToInt(damagePerSecond * Time.deltaTime);
            damageable.TakeDamage(damage);
        }
    }

    
}
