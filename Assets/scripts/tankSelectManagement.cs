using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class tankSelectManagement : MonoBehaviour
{
    [SerializeField] private Transform[] template;
    public static BulletSO selectedTank;
    [SerializeField] private BulletSO[] tankSoList;
    [SerializeField] private Button Next;
    [SerializeField] private Button Previous;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private TextMeshProUGUI textMeshProUGUIName;
    [SerializeField] public static TankItemByShop[] tankItemByShops=new TankItemByShop[6];
    public static int roundNumber;
    public static float Gold;
    public static int index;
    public static float Exp;
    public static int numberWeapon;
    [SerializeField] private SoundSO soundSO;
    void Start()
    {   index = 0;
        roundNumber = 0;
        numberWeapon = 0;
        Gold = 1000;
        gameInput.Left += GameInput_Left;
        gameInput.Right += GameInput_Right1;
        Next.onClick.AddListener(() =>
        {
            index++;
            if (index > 1)
            {
                index = 0;
            }
            AudioSource.PlayClipAtPoint(soundSO.next, transform.position, 10f);
        });
        Previous.onClick.AddListener(() =>
        {
            index--;
            if (index < 0)
            {
                index = 1;
            }
            AudioSource.PlayClipAtPoint(soundSO.next, transform.position, 10f);
        });
        
    }

    private void GameInput_Right1(object sender, System.EventArgs e)
    {
        index++; AudioSource.PlayClipAtPoint(soundSO.next, transform.position, 10f);
        if (index > 1)
        {
            index = 0;
        }
    }

    private void GameInput_Left(object sender, System.EventArgs e)
    {
        index--; AudioSource.PlayClipAtPoint(soundSO.next, transform.position, 10f);
        if (index < 0)
        {
            index = 1;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        LoadTankSelected();
        selectedTank = tankSoList[index];
        textMeshProUGUIName.text=selectedTank.name;
       
    }

    private void GameInput_Right(object sender, System.EventArgs e)
    {
       
        index++;
        if (index > 1)
        {
            index = 0;
        }
    }

    private void LoadTankSelected()
    {
        for(int i =0; i < template.Length;i++)
        {
            if (index == i)
            {
                template[i].transform.gameObject.SetActive(true);
            }
            else
            {
                template[i].transform.gameObject.SetActive(false);
            }
        }
    }
}
