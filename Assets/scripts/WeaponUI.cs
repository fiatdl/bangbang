using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
  
    [SerializeField] private Transform itemContainer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < tankSelectManagement.tankItemByShops.Length; i++)
        {
            if (tankSelectManagement.tankItemByShops[i] != null)
            {
                itemContainer.GetChild(i).gameObject.GetComponent<Image>().sprite = tankSelectManagement.tankItemByShops[i].sprite;
            }
        }
    }
}
