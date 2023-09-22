using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Canvas shopCanvas;
    public void OnPointerClick(PointerEventData eventData)
    {
       if(Vector3.Distance(transform.position, gameManagement.Instance.mainTank.transform.position) < 20f)
        {
            shopCanvas.gameObject.SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    
    }

    // Start is called before the first frame update
    void Start()
    {
        shopCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
