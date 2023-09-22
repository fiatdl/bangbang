using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takedamevisual : MonoBehaviour
{
 
    [SerializeField] private EnemyObject enemyObject;
    [SerializeField] private GameObject[] visualGameObjectArray;
    void Start()
    {
      
       
        Hide();
    }



    private void Show()
    {
        foreach (var item in visualGameObjectArray)
        {
            item.SetActive(true);
        }

    }
    private void Hide()
    {
        foreach (var item in visualGameObjectArray)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
