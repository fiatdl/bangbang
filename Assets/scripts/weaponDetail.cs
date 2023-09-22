using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class weaponDetail : MonoBehaviour
{
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI txtNameAndPrice;
    [SerializeField] public TextMeshProUGUI txtDamage;
    [SerializeField] public TextMeshProUGUI txtArmor;
    [SerializeField] public TextMeshProUGUI txtCoolDown;
    [SerializeField] public TextMeshProUGUI crit;
    [SerializeField] public TextMeshProUGUI hP;
    [SerializeField] public TextMeshProUGUI moveSpeed;
    [SerializeField] public TextMeshProUGUI attackSpeed;
    [SerializeField] public Button buy;
    public TankItemByShop tankItemByShop;
    [SerializeField] public SoundSO soundSO;
    [SerializeField] private Transform shopPos;
    private void Awake()
    {
       
    }
    void Start()
    {
        buy.interactable = false;
        buy.onClick.AddListener(() =>
        {
            if (tankSelectManagement.Gold >= tankItemByShop.price && tankSelectManagement.numberWeapon <= 5 && Vector3.Distance(shopPos.position, gameManagement.Instance.mainTank.transform.position) <= 20f)
            {
                tankSelectManagement.Gold-=tankItemByShop.price;
                PlaySound(soundSO.buy,gameManagement.Instance.mainTank.transform.position);
                tankSelectManagement.tankItemByShops[tankSelectManagement.numberWeapon] = tankItemByShop;
                tankSelectManagement.numberWeapon++;
                gameManagement.Instance.mainTank.UpdateWeapon(tankItemByShop);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
      
        if (tankItemByShop.price <= tankSelectManagement.Gold)
        {
            buy.interactable = true;
        }
    }
    public void UpdateVal()
    {
          image.sprite=tankItemByShop.sprite;
        txtNameAndPrice.text=tankItemByShop.objectName +"    "+"Giá :"+tankItemByShop.price.ToString();
        txtDamage.text=tankItemByShop.damamge.ToString()+ " sát thương";
        txtArmor.text =  tankItemByShop.armor.ToString() + " giáp";
        txtCoolDown.text = tankItemByShop.coolDown.ToString() + "% thời gian hồi chiêu";
        hP.text =  tankItemByShop.hP.ToString() + " hP";
        attackSpeed.text =   tankItemByShop.attackSpeed.ToString() + " tốc độ đánh";
        moveSpeed.text = tankItemByShop.moveSpeed.ToString() + " tốc độ di chuyển ";
        crit.text = tankItemByShop.critPercent.ToString() + " tỉ lệ bạo kích ";
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 10f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * 2f);
    }
}
