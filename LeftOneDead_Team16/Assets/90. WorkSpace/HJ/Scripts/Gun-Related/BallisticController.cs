using UnityEngine;

public class BallisticController : MonoBehaviour
{
    public BulletBallistics data;
    public BulletState state;
    public Vector3 velocity;

    private Vector3 posCurrent;
    private Vector3 posNext;

    private float lastCheckedTime;
    private float deltaTime;

    public RaycastHit HitObject;

    public void Initialize(BulletBallistics data)
    {
        this.data = data;
        velocity = Vector3.zero;
        transform.position = Vector3.zero;

        state = BulletState.Ready;
    }

    public void Fire(Transform transform, GunData weaponData)
    {
        lastCheckedTime = Time.time;

        this.transform.position = transform.position;
        velocity = transform.forward * data.MuzzleVelocity * weaponData.MuzzleVelocityModifier;

        ChangeState(BulletState.Flying);
    }

    private void Update()
    {
        deltaTime = Time.time - lastCheckedTime;

        switch (state)
        {
            case BulletState.Flying:
                if (deltaTime >= 0.02f)
                {
                    lastCheckedTime = Time.time;
                    ApplyExternalBallistics();
                }
                break;
            case BulletState.Hit:
                ApplyTerminalBallistics();
                break;

        }
    }


    private void ApplyExternalBallistics()
    {
        posCurrent = transform.position;
        float speed = velocity.magnitude;
        //항력 적용
        Vector3 dragForce = -velocity.normalized * data.DragCoefficient * 0.5f * speed * speed * data.CrossSectionArea * 1.225f;
        Vector3 acceleration = dragForce / data.Mass;
        //중력 적용
        acceleration += Physics.gravity;
        //속력 적용
        velocity += acceleration * deltaTime;
        //속력을 기반으로 위치 적용
        transform.position += velocity * deltaTime;
        posNext = transform.position;

        if (CheckCollision(out HitObject))
        {
            ChangeState(BulletState.Hit);
        }
    }

    private void ApplyTerminalBallistics()
    {
        IDamageable damageable;
        if (HitObject.transform.gameObject.TryGetComponent(out damageable))
        {
            damageable.TakeDamage((int)GetKineticEnergy() / 5);
            ChangeState(BulletState.Idle);
        }
    }

    private float GetKineticEnergy()
    {
        return data.Mass * velocity.magnitude * velocity.magnitude * 0.5f;
    }
    private bool CheckCollision(out RaycastHit hit)
    {
        Vector3 direction = posNext - posCurrent;
        Ray ray = new Ray(posCurrent, direction.normalized);
        return Physics.Raycast(ray, out hit, direction.magnitude);
    }

    public void ChangeState(BulletState state)
    {
        this.state = state;
    }
}


public enum BulletState
{
    Idle, //오브젝트 풀에서 대기 상태
    Ready, //재장전 후 대기상태
    Flying, //발사 명령 호출받고 비행중일때
    Hit //착탄했을때
}
