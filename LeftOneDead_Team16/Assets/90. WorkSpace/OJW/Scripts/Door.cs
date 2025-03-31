using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField, Range(1f, 5f)] private float movingTime;
    [SerializeField, Range(1f, 5f)] private float movingDistance;

    private bool isOpened;
    private bool isMoving;

    private void Awake()
    {
        isOpened = false;
        isMoving = false;
    }

    private void Update()
    {
        // test 코드
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ToggleDoor());
        }
    }

    public void Interact()
    {
        StartCoroutine(ToggleDoor());
    }

    /// <summary>
    /// 문 열기/닫기
    /// </summary>
    private IEnumerator ToggleDoor()
    {
        if (isMoving)
        {
            yield break;
        }

        var elapsedTime = 0f;
        var startPos = transform.position;
        var dir = isOpened ? Vector3.down : Vector3.up;
        var endPos = startPos + dir * movingDistance;
        isMoving = true;
        
        while (elapsedTime < movingTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / movingTime);
            yield return null;
        }
        
        transform.position = endPos;
        isOpened = !isOpened;
        isMoving = false;
    }
}