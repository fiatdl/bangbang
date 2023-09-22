using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selected : MonoBehaviour
{
    [SerializeField] private firstAirGameItem box;
   
    [SerializeField] private GameObject[] visualGameObjectArray;
    void Start()
    {
        

        Hide();
    }




    private void Instance_OnDamegeTaked(object sender, IDisplayHP.OnDamegeTakedEventArgs e)
    {
     
    }

    private void Instance_OnInteractGameItem(object sender, tank.GameItemArg e)
    {
        
        {
            if (e.box == box&&box.isAvaiable)
            {
               
                Show();
                Invoke("Hide", 5f);

            }
            else
            {
                Hide();
            }
        }
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
