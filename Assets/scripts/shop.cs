using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class shop : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button close;
    [SerializeField] private TankItemByShop[] tankItemByShops;
    [SerializeField] private Transform ShopContainer;
    [SerializeField] public Transform detailContainer;

    [SerializeField] private Transform itemTemplate;
    [SerializeField] private Transform detailTemplate;
    public  int index;
    public static shop Instance { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        
        Instance = this;

    }
    void Start()
    {
        index = 0;
        gameObject.SetActive(false);
        if (gameObject.activeSelf)
        {
            Time.timeScale = 0f;
        }

        close.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        itemTemplate.gameObject.SetActive(false);
        detailTemplate.gameObject.SetActive(false);
        
        for(int i=0;i<tankItemByShops.Length;i++)
        {
            Transform item = Instantiate(itemTemplate, ShopContainer);
            Image im = item.GetChild(1).GetComponent<Image>();
            im.sprite = tankItemByShops[i].sprite;
            TextMeshProUGUI txt=item.GetComponentInChildren<TextMeshProUGUI>();
            txt.text = tankItemByShops[i].objectName;
            weaponIcon wi=item.GetComponent<weaponIcon>();
            wi.index=i;
            wi.gameObject.SetActive(true);
            
            Transform transform = Instantiate(detailTemplate, detailContainer);
            weaponDetail weaponDetail=transform.GetComponentInChildren<weaponDetail>();
            weaponDetail.tankItemByShop = tankItemByShops[i];
            weaponDetail.UpdateVal();
            weaponDetail.gameObject.SetActive(false);
            wi.weaponDetail=weaponDetail;
        }
    }

    // Update is called once per frame
    void Update()
    {
        detailContainer.GetChild(index+1).gameObject.SetActive(true);
    }

}
