using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private int eventID;
    
    private BoxCollider boxCol;

    private void Awake()
    {
        boxCol = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (boxCol.bounds.Contains(StageManager.Instance.Player.transform.position))
        {
            StageManager.Instance.InitEventAction(eventID);
            Destroy(gameObject);
        }
    }
}
