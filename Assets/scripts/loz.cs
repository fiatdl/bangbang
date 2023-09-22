using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class loz : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Detect if the Cursor starts to pass over the GameObject
    [SerializeField] private Transform cc;
    private void Awake()
    {
        cc.gameObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        cc.gameObject.SetActive(true);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        cc.gameObject.SetActive(false); 
     
    }
}