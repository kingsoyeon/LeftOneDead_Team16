using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GunController : MonoBehaviour,IInteractable
{
    public GunActions GunAction;
    public AudioClip fireSound;
    public AudioClip reloadingSound;


    public GunController()
    {
        GunAction = new GunActions(this);
    }
    public GunData Data;
    public GunState State;

    public GameObject MuzzlePositon;


    public GunFireMode[] FireModes;
    public GunFireMode CurrentFireMode;

    public int AmmoCount;
    public int CurrentAmmoHolding;

    private float fireInterval;
    private float timeLastFired;

    private BallisticController[] ammos = new BallisticController[0];
    private BulletPool pool;

    private bool isTriggerPulled;
    private bool awaitTriggerRelease;


    private bool init = false;

    private void Start()
    {
        pool = BulletPool.Instance;
        Initialize();
    }
    void Initialize()
    {
        fireInterval = 60f / Data.RoundPerMinute;
        CurrentAmmoHolding = Data.MaxAmmoCanHold;
        Reload();
        init = true;

    }
    private void Update()
    {
        switch (State)
        {
            case GunState.Idle:
                if (isTriggerPulled)
                {
                    ChangeGunState(GunState.Firing);
                }
                break;
            case GunState.Firing:
                if (AmmoCount > 0)
                {
                    Firing();
                }
                else
                {
                    ChangeGunState(GunState.Empty);
                }
                if (!isTriggerPulled)
                {
                    ChangeGunState(GunState.Idle);
                }
                break;
            case GunState.Reload:

                break;
            case GunState.Jam:

                break;
            case GunState.Empty:

                break;
        }
    }


    private Coroutine reloadCoroutine = null;
    void Reload()
    {
        if (reloadCoroutine == null)
        {
            if(init)
                SoundManager.PlayClip(reloadingSound);
            reloadCoroutine = StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        //먼저 들어있는 탄약 폐기(?)
        if (CurrentAmmoHolding != -1) //-1이면 무한
        {
            foreach (var ammo in ammos)
            {
                if (ammo.state == BulletState.Ready)
                {
                    ammo.ChangeState(BulletState.Idle);
                    CurrentAmmoHolding++;
                }
            }
        }
        yield return new WaitForSeconds(Data.ReloadingTime); //재장전에 걸리는 시간

        int poolRequestAmount = Data.MagazineCapacity;
        if (CurrentAmmoHolding != -1 && CurrentAmmoHolding < Data.MagazineCapacity)
        {
            poolRequestAmount = CurrentAmmoHolding;
        }
        ammos = pool.GetAvailableAmmo(poolRequestAmount, Data.BulletData);
        CurrentAmmoHolding -= (CurrentAmmoHolding != -1) ? ammos.Length : 0;
        AmmoCount = ammos.Length;
        reloadCoroutine = null;
        ChangeGunState(GunState.Idle);
    }



    private int shotCounter;
    void Firing()
    {
        switch (CurrentFireMode)
        {
            case GunFireMode.Full:
                Fire();
                break;
            case GunFireMode.Burst:
                if (!awaitTriggerRelease)
                {
                    shotCounter++;
                    Fire();
                    if (shotCounter > 2)
                    {
                        shotCounter = 0;
                        awaitTriggerRelease = true;
                    }
                }
                break;
            case GunFireMode.Semi:
                if (!awaitTriggerRelease)
                {
                    Fire();
                    awaitTriggerRelease = true;
                }
                break;
        }
    }


    void Fire()
    {
        float deltaTime = Time.time - timeLastFired;
        if (deltaTime >= fireInterval)
        {
            timeLastFired = Time.time;
            //fire
            AmmoCount--;
            ammos[AmmoCount].Fire(MuzzlePositon.transform, Data);
            SoundManager.PlayClip(fireSound);
            
        }
    }




    public void ChangeGunState(GunState state)
    {
        State = state;
    }

    /// <summary>
    /// 발사 모드를 바꿈
    /// </summary>
    /// <param name="mode">바꿀 발사 모드</param>
    private void ChangeFireMode(GunFireMode mode)
    {
        if (State == GunState.Idle)
        {
            //없는 발사 모드로 바꿀 수 없게.
            if (FireModes.Contains(mode)) CurrentFireMode = mode;
        }
    }

    public void Interact()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 총 조작하는 메서드가 담긴 클래스
    /// </summary>
    public class GunActions
    {
        private GunController parentGun;
        public GunActions(GunController parent)
        {
            parentGun = parent;
        }
        /// <summary>
        /// 재장전
        /// </summary>
        public void Reload()
        {
            parentGun.ChangeGunState(GunState.Reload);
            parentGun.Reload();
        }
        /// <summary>
        /// 방아쇠 당기기
        /// </summary>
        public void TriggerPull()
        {
            parentGun.isTriggerPulled = true;
        }
        /// <summary>
        /// 방아쇠 놓기
        /// </summary>
        public void TriggerRelease()
        {
            parentGun.isTriggerPulled = false;
            parentGun.awaitTriggerRelease = false;
        }
        /// <summary>
        /// 발사 모드를 한 단계씩 바꿈
        /// </summary>
        public void ChangeFireMode()
        {
            if (parentGun.State == GunState.Idle)
            {
                int index = Array.IndexOf(parentGun.FireModes, parentGun.CurrentFireMode);
                //전위 연산으로 index에 1 더하기, 전체 배열 길이로 나머지 연산하여 순환.
                //반대로 1씩 빼는 경우 로직 수정이 필요할지도..
                parentGun.ChangeFireMode(parentGun.FireModes[++index % parentGun.FireModes.Length]);
            }
        }
    }
}


public enum GunState
{
    Idle,
    Firing,
    Jam,
    Reload,
    Empty
}

public enum GunFireMode
{
    Safe,
    Semi,
    Burst,
    Full
}
public interface IFirearm
{
    public GunData GunData { get; set; }
    public GunState GunState { get; set; }

    public void Fire();
    public void TriggerPull();
    public void TriggerRelease();
}