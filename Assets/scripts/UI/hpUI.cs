using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessUi : MonoBehaviour
{   
    [SerializeField] private GameObject gameObjectHasProcess;
    [SerializeField] private Image image;
    [SerializeField] private Image shield;
    IDisplayHP displayHp;
    public void Start()
    {
        displayHp = gameObjectHasProcess.GetComponent<IDisplayHP>();
        if (displayHp != null)
        {
       
            displayHp.OnDamegeTaked += DisplayHp_OnDamegeTaked;
            image.fillAmount = 1;
            shield.fillAmount = 0.3f;
            Show();
        }

    }

    private void DisplayHp_OnDamegeTaked(object sender, IDisplayHP.OnDamegeTakedEventArgs e)
    {
        image.fillAmount = e.hpRemainNormalized;
        shield.fillAmount = e.shield;
        
     
        if (e.hpRemainNormalized == 0f )
        {
            Hide();
        }
        else
        {
            Show();
        }
    }




    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
