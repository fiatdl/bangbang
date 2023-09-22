using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class weaponIcon : MonoBehaviour, IPointerClickHandler, IPointerExitHandler,IPointerEnterHandler
{
    [SerializeField] public weaponDetail weaponDetail;
    [SerializeField] public int index;
    [SerializeField] private Transform background;


    public void OnPointerClick(PointerEventData eventData)
    {
        
        shop.Instance.detailContainer.GetChild(shop.Instance.index+1).gameObject.SetActive(false);
        shop.Instance.index = index;

       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      background.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
       
    }

    void Start()
    {
        background.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
}
