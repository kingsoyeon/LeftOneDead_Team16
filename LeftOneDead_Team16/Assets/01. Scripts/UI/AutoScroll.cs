using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] private float scrollDelay = 1f; // 스크롤 시작 딜레이

    [SerializeField] private RectTransform scrollRectTransform;
    private ScrollRect scrollRect; // 스크롤 영역
    private Coroutine? scrollCoroutine;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        
    }

    private void OnEnable()
    {
        TextAutoScroll(true);
    }
    private void OnDisable()
    {
        TextAutoScroll(false);
    }

    /// <summary>
    /// 오토 스크롤 메서드
    /// </summary>
    private void TextAutoScroll(bool isEnable)
    {
        scrollRect.velocity = Vector2.zero;

        if(scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine);
           
        }
        if (isEnable)
        {
            scrollCoroutine = StartCoroutine(Scroll());
        }
    }

private IEnumerator Scroll()
    {
        yield return new WaitForSecondsRealtime(scrollDelay); // 딜레이만큼 기다렸다가 시작

        while (true)
        {
            scrollRect.verticalNormalizedPosition -= scrollSpeed * Time.deltaTime / scrollRectTransform.sizeDelta.y;

            yield return null;
        }
    }
}
