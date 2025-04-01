using UnityEngine;

public class SafeHouse : MonoBehaviour
{
    private Player player;
    
    private Door door;
    
    private BoxCollider boxCol;

    private void Awake()
    {
        door = GetComponentInChildren<Door>();
        boxCol = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        player = StageManager.Instance.Player;
    }

    private void Update()
    {
        StageClear();
    }

    /// <summary>
    /// 플레이어가 건물 안에 있는지 검사
    /// </summary>
    /// <returns>True: 건물 내부
    /// <para>False: 건물 외부</para></returns>
    private bool IsInPlayer()
    {
        return boxCol.bounds.Contains(player.transform.position);
    }

    private void StageClear()
    {
        if (IsInPlayer() && door.IsClosed)
        {
            StageManager.Instance.ClearStage();
        }
    }
}