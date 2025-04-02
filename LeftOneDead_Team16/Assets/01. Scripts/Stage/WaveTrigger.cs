using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    private BoxCollider boxCol;

    private void Awake()
    {
        boxCol = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (boxCol.bounds.Contains(StageManager.Instance.Player.transform.position))
        {
            StageManager.Instance.MakeWave();
            Destroy(gameObject);
        }
    }
}
