using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class scrollHandler : MonoBehaviour, IScrollHandler
{
   [SerializeField]  RectTransform TransRef;
  [SerializeField]  ScrollRect ScrollRef;
  [SerializeField]  RectTransform ContentRef;

    [SerializeField] float ScrollSpeed = 100;

    float MinScroll = 0;
    float MaxScroll;

    // Use this for initialization
    void Start()
    {
        TransRef = GetComponent<RectTransform>();
        ScrollRef = GetComponent<ScrollRect>();
        ContentRef = ScrollRef.content;

        MaxScroll = ContentRef.rect.height - TransRef.rect.height;
    }

    public void OnScroll(PointerEventData eventData)
    {
        Debug.Log("scroll");
        Vector2 ScrollDelta = eventData.scrollDelta;

        ContentRef.anchoredPosition += new Vector2(0, -ScrollDelta.y * ScrollSpeed);

        if (ContentRef.anchoredPosition.y < MinScroll)
        {
            ContentRef.anchoredPosition = new Vector2(0, MinScroll);
        }
        else if (ContentRef.anchoredPosition.y > MaxScroll)
        {
            ContentRef.anchoredPosition = new Vector2(0, MaxScroll);
        }
    }
}
