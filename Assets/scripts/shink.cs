using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class shink : MonoBehaviour, IPointerClickHandler
{
    private bool isShink;
    public void OnPointerClick(PointerEventData eventData)
    {   if (!isShink)
        {
        
            gameObject.GetComponent<RectTransform>().rect.Set(109, 870, 300, 100); }
        else
        {
            gameObject.GetComponent<RectTransform>().rect.Set(109, 693, 300, 800);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isShink = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
