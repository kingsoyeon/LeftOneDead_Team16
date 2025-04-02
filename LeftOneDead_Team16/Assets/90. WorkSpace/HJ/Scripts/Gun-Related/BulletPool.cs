using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool instance;
    public static BulletPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject().AddComponent<BulletPool>();
                instance.ammoPrefab = new GameObject().AddComponent<BallisticController>().gameObject;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }






    //public BulletBallistics[] ammoDatas; //존재하는 모든 탄약 정보를 연결
    public List<BallisticController> ammoPool = new List<BallisticController>();

    public GameObject ammoPrefab;

    public BallisticController[] GetAvailableAmmo(int count, BulletBallistics ammoData)
    {
        BallisticController[] ammos = new BallisticController[count];

        //풀에서 사용 가능한 총알이 있으면 초기화 해서 배열에 넣기
        int i = 0;
        foreach (BallisticController ammo in ammoPool)
        {
            if (ammo.state == BulletState.Idle)
            {
                ammo.Initialize(ammoData);
                ammos[i++] = ammo;
            }
            //풀에 여유가 더 있다면 i 가 count 보다 커지고, outofindex 발생.
            if (i >= count) return ammos;
        }
        //foreach 를 빠져나왔다는건 부족하다는 뜻.
        //새로 만들어서 배열에 추가
        for (int j = i; j < count; j++)
        {
            BallisticController newAmmo = Instantiate(ammoPrefab, transform).GetComponent<BallisticController>();
            newAmmo.Initialize(ammoData);
            ammoPool.Add(newAmmo);
            ammos[j] = newAmmo;
        }
        return ammos;
    }
}
