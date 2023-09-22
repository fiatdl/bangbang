using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooltip : MonoBehaviour
{
    [SerializeField] private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(),Input.mousePosition,cam,out localPoint);
        transform.localPosition = localPoint;

        
    }
}
