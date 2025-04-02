using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot
{
    PrimaryWeapon,  //1번 슬롯 : 주무기
    SecondaryWeapon,//2번 슬롯 : 보조무기
    Item1,          //3번 슬롯 : 아이템1
    Item2           //4번 슬롯 : 아이템2
}
public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private Transform equipPoint;
    //주무기 보조무기는 따로 넣기
    [SerializeField] private GameObject primaryWeaponPrefab;
    [SerializeField] private GameObject secondaryWeaponPrefab;
    
    [SerializeField] private GameObject item1Prefab;
    [SerializeField] private GameObject item2Prefab;

    private void Start()
    {
        equipPoint.SetParent(Camera.main.transform);
        Equipment(secondaryWeaponPrefab);
    }
    public void Equipment(GameObject prefab)
    {
        Instantiate(prefab, equipPoint.position, equipPoint.rotation, equipPoint);
    }


    public void SetEquipmentPrefab(string tag, GameObject prefab)
    {
        switch(tag)
        {
            case "PrimaryWeapon":
                primaryWeaponPrefab = prefab;
                break;
            case "SecondaryWeapon":
                secondaryWeaponPrefab = prefab;
                break;
            case "Item":
                if(item1Prefab == null)
                {
                    item1Prefab = prefab;
                }
                else
                {
                    item2Prefab = prefab;
                }
                break;
            default:
                Debug.Log("태그에 없는걸 저장시도");
                break;
        }
    }
}
