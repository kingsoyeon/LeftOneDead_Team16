using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip doorSound;
    
    [SerializeField, Range(1f, 5f)] private float movingTime;
    [SerializeField, Range(1f, 5f)] private float movingDistance;
    
    public bool IsClosed { get; private set; }
    public bool IsMoving { get; private set; }

    private void Awake()
    {
        IsClosed = true;
        IsMoving = false;
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
        if (IsMoving)
        {
            yield break;
        }

        var elapsedTime = 0f;
        var startPos = transform.position;
        var dir = IsClosed ? Vector3.up : Vector3.down;
        var endPos = startPos + dir * movingDistance;
        IsMoving = true;
        IsClosed = !IsClosed;
        SoundManager.PlayClip(doorSound);
        
        while (elapsedTime < movingTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / movingTime);
            yield return null;
        }
        
        transform.position = endPos;
        
        IsMoving = false;
    }
}