using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class concac : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cc;
    // Start is called before the first frame update
    public static concac Instance { get; private set; }
    private void Awake()
    {
        cc.gameObject.SetActive(false);

    }
    void Start()
    {

    }
   

    // Update is called once per frame
    void Update()
    {
        
      
        
    }
    private void OnMouseEnter()
    {
        cc.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        cc.gameObject.SetActive(false);
    }
}
