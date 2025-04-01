using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    [SerializeField] private GameObject explosionFx;

    private void Start()
    {
        StageManager.Instance.AddActionToEventActionDict(0, ExplosionEvent);
    }

    private void ExplosionEvent()
    {
        print("폭발 발생");
        Instantiate(explosionFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
