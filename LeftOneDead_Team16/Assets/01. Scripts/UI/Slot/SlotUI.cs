using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SlotUI : MonoBehaviour
{
    [SerializeField] private WeaponSlot weaponSlot;
    [SerializeField] private PistolSlot pistolSlot;
    [SerializeField] private ItemSlot itemSlot;

    public void Init(SlotUI slotUI) 
    {
        slotUI = this;
    }
    

    void Update()
    {
        //  마우스 휠을 내리면서(inputaction으로 지정되어 있음 scroll하면 (Swap))슬롯이 활성화된다. (아웃라인으로 알 수 있음) (이때 플레이어 쪽에서는 무기를 장착한다)

        // weaponSlot에 아이템데이터가 있을 때만 활성화, 처음은 비활성화 상태로 시작. 획득/drop시 활성/비활성

        // pistolSlot에 아이템 데이터가 있을 때만 활성화, 처음은 가진 채로 시작하기 때문에 활성화 상태로 시작. 획득/drop시 활성/비활성

        // itemSlot에 아이템 데이터가 있을 시에만 스크롤 가능함. 하지만 슬롯에 아이템데이터가 있든 없든 슬롯은 항상 활성화 상태로 존재.
    }
}
