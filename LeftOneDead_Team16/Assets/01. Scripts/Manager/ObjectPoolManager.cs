using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering.Universal;


/// <summary>
/// UnityEngine.Pool API 사용
/// </summary>

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    private Dictionary<string, ObjectPool<GameObject>> poolDictionary = new();

    [SerializeField] private bool collectionCheck = true; // 이미 pool에 있으면 반환

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    #region 수정 필요
    //public void Initialize()
    //{
    //    for (int i = 0; i < prefabs.Length; i++)
    //    {
    //        pools[i] = new Queue<GameObject>();
    //    }

    //    pools = new ObjectPool<IPoolable>(CreateObject, OnGetFromPool, OnReleaseToPool, OnDestoryPooledObject, collectionCheck, defaultCapacity, maxSize);
    //}

    /// <summary>
    /// 오브젝트풀 생성
    /// </summary>
    /// <returns></returns>
    //private void CreatePool()
    //{
    //    for (int i = 0; i < prefabs.Length; i++)
    //    {
    //        GameObject prefab = prefabs[i].gameObject;
    //    }
    //    IPoolable pooledObject = Instantiate(prefab).GetComponet<IPoolable>();
    //    pooledObject.ObjectPool = objectPool;
    //    return pooledObject;
    //}
    ///// <summary>
    ///// 오브젝트풀에서 가져오기
    ///// </summary>
    ///// <param name="pooledObject"></param>
    //private void OnGetFromPool(IPoolable pooledObject)
    //{
    //    pooledObject.gameObject.SetActive(false);
    //}

    //private void OnDestroyPooledObject(IPoolable pooledObject)
    //{
    //    Destroy(pooledObject);
    //}
    #endregion
}
