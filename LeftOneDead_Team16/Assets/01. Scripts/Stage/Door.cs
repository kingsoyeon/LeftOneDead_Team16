using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField, Range(1f, 5f)] private float movingTime;
    [SerializeField, Range(1f, 5f)] private float movingDistance;

    private bool isMoving;
    
    private Material material;
    private Color originColor;
    private Color interactColor;
    
    public bool IsClosed { get; private set; }

    private void Awake()
    {
        IsClosed = true;
        isMoving = false;
        
        material = GetComponent<MeshRenderer>().material;
        originColor = material.color;
        interactColor = new Color(8f, 8f, 8f);
    }

    private void Start()
    {
        // test 코드
        StageManager.Instance.AddActionToEventActionDict(0, Interact);
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
            print("문 움직이는 중");
            yield break;
        }

        var elapsedTime = 0f;
        var startPos = transform.position;
        var dir = IsClosed ? Vector3.up : Vector3.down;
        var endPos = startPos + dir * movingDistance;
        isMoving = true;
        print(IsClosed ? "문 열기" : "문 닫기");
        while (elapsedTime < movingTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / movingTime);
            yield return null;
        }
        
        transform.position = endPos;
        IsClosed = !IsClosed;
        isMoving = false;
    }
}