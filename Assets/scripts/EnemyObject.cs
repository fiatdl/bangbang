using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static IDisplayHP;

public class EnemyObject : MonoBehaviour, IDisplayHP
{
    public float hp;
    [SerializeField] protected EnemySO enemySO;
    [SerializeField] public Transform destroyVisual;
    [SerializeField] protected TextMeshPro gold;
    [SerializeField] public SoundSO soundSO;
    [SerializeField] protected GameObject[] hittedVisuals;
    protected float maxRange = 20f;
 
    public float moveSpeed;


    public float rotateSpeed;
    public float buttletSpeed;
    public float attackSpeed;
    public float speedOfBullet;

    protected bool isBooming;
    public static float attackRelaxTimer;

    [SerializeField] protected BulletSO bulletSO;
    [SerializeField] protected Transform bullets;
    [SerializeField] protected gun gun;
    [SerializeField] public LayerMask interactAble;
    [SerializeField] protected Canvas HpUI;
    [SerializeField] protected TextMeshPro hpStatus;
    [SerializeField] protected Transform tankBody;
    
    [SerializeField] public tank target;
    [SerializeField] public LayerMask shootAble;
    protected float bulletSpeed;
    protected float damage;

    public event EventHandler<OnDamegeTakedEventArgs> OnDamegeTaked;
    protected float CurrentHp;

    private void Awake()
    {
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void DestroySelf()
    {
        gameManagement.Instance.textMeshProUGUIAnouncement.gameObject.SetActive(true);
     
        gameManagement.Instance.textMeshProUGUIAnouncement.text += System.Environment.NewLine + enemySO.name + " has been slained ";
        Invoke("Destroymm", 0.5f);
        
        
     
    }
    private void Destroymm()
    {
         Destroy(gameObject);
    }
    protected void HideAnounce()
    {
        gameManagement.Instance.textMeshProUGUIAnouncement.gameObject.SetActive(false);
    }
    public virtual void TakedDamege(float damage,bool isCrit)
    {
        if (isCrit)
        {
            damage *= 2;
        }
        if (isBooming)
        {
            return;
        }
       
PlaySound(soundSO.hit, transform.position);
        ShowHittedVisual();
        Invoke("HideHittedVisual", 0.2f);
        if (CurrentHp >= damage)
        {
            CurrentHp -= damage;

            hpStatus.gameObject.SetActive(true);
            Invoke("SetFalse", 0.2f);


            if (isCrit)
            {
  hpStatus.text ="-" + damage+System.Environment.NewLine+ "bạo kích ";
            }
            else
            {
                hpStatus.text = "-" + damage;
            }
          

            OnDamegeTaked?.Invoke(this, new IDisplayHP.OnDamegeTakedEventArgs
        {
            hpRemainNormalized = CurrentHp / hp,
            shield=0
        });

        }
        else
        {
          
            CurrentHp = 0;
                isBooming = true;
            destroyVisual.gameObject.SetActive(true);
            tankSelectManagement.Gold += enemySO.coin;
            tankSelectManagement.Exp += enemySO.exp;
            PlaySound(soundSO.destroyed, transform.position);
            gold.gameObject.SetActive(true);
            DestroySelf();
        }
       

   
        
 


       

    }

   private void SetFalse()

    {

        hpStatus.gameObject.SetActive(false);

    }

    private void ShowHittedVisual()
    {
        foreach (var item in hittedVisuals)
        {
           if(item!=null){ item.SetActive(true); }
        }


    }
    public void HideHittedVisual()
    {

        foreach (var item in hittedVisuals)
        {
            if (item) { item.SetActive(false); }
        }
    }
    public void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 10f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * 1f);
    }
}
