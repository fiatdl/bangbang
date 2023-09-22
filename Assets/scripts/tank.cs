using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static armyTank;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class tank : MonoBehaviour
{

public Canvas HpUI;
    [SerializeField] public SoundSO soundSO;
    public bool isElectric;
    protected float maxArange;
    public event EventHandler OnShootingSound;
    public float moveSpeed;
    public gun gun;
    public Transform tankBody;
    public float rotateSpeed ;
     public GameInput gameInput;
    protected float shield;
     public LayerMask EnemyMask;
   public BulletSO bulletSO;
    public Vector3 targetPosition;
    public float buttletSpeed ;
    public float attackSpeed ;
    public float speedOfBullet;
    public float hP;
    public static float attackRelaxTimer;
     public Transform bullets;
    public bool spwaning;
    public Vector3 spawnposition;
     protected bool isHeal;
    public bool stunned;
     protected float healTimer;
     protected float healCurrentTimer;
     protected float healAmount;
    public float armor;
    public float critPercent;
    public float countdown;
    public TextMeshPro hpStatus;
    public float damage;
    protected Vector3 lastInteractionDir;
    public bool dead;
    public float CurrentHp;
     public Transform TextContainer;

    public string qInfo;
    public string eInfo;
    public string spaceInfo;
    protected bool isGhost;
    private float eSkillTimeCoundown;
    private float eSkillCurrentTimeCoundown;
    private float qSkillTimeCoundown;
    private float qSkillCurrentTimeCoundown;
    private float spaceSkillTimeCoundown;
    public LayerMask shootAble;
   public LayerMask interactAble;
    private float spaceSkillCurrentTimeCoundown;
    public event EventHandler OnHealSound;
    public event EventHandler<GameItemArg> OnInteractGameItem;
    public event EventHandler OnHit;
    public event EventHandler<IDisplayHP.OnDamegeTakedEventArgs> OnDamegeTaked;
    protected Transform currentBullet;
    protected GameItem box;
    [SerializeField] protected Transform heal;
    public bool updateGameItemBullet;


    private float tempHp = 0f;
   private float tempdamage = 0f;
   private float tempMoveSpeed = 0f;
   private float tempArmor = 0f;
  private  float tempAttackSpeed = 0f;
  private  float tempCoolDown = 0f;
    private float tempcrit = 0f;
    public class GameItemArg : EventArgs
    {
        public GameItem box;
    }
    protected float ESkillTimeCoundown { get => eSkillTimeCoundown; set => eSkillTimeCoundown = value; }
    protected float ESkillCurrentTimeCoundown { get => eSkillCurrentTimeCoundown; set => eSkillCurrentTimeCoundown = value; }
    protected float QSkillTimeCoundown { get => qSkillTimeCoundown; set => qSkillTimeCoundown = value; }
    protected float QSkillCurrentTimeCoundown { get => qSkillCurrentTimeCoundown; set => qSkillCurrentTimeCoundown = value; }
    protected float SpaceSkillTimeCoundown { get => spaceSkillTimeCoundown; set => spaceSkillTimeCoundown = value; }
    protected float SpaceSkillCurrentTimeCoundown { get => spaceSkillCurrentTimeCoundown; set => spaceSkillCurrentTimeCoundown = value; }
    protected void Awake()
    {
        spwaning = false;
       stunned=false;
        dead = false;
        OnDamegeTaked?.Invoke(this, new IDisplayHP.OnDamegeTakedEventArgs
        {
            hpRemainNormalized = CurrentHp / hP,
            shield = 0
        }) ;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void HandleMovement()
    {
        if (dead)
        {
            return;
        }
        Vector2 inputVector = gameInput.GetMovementVector();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveSpeed * Time.deltaTime);
        if (canMove||isGhost)
        {
            transform.position += (Vector3)moveDir * Time.deltaTime * moveSpeed;

        }

        else
        {

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveSpeed * Time.deltaTime);
            if (canMove)
            {

                moveDir = moveDirX;
                transform.position += (Vector3)moveDir * Time.deltaTime * moveSpeed;

            }


            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveSpeed * Time.deltaTime);
                if (canMove)
                {
                    moveDir = moveDirZ;

                    transform.position += (Vector3)moveDir * Time.deltaTime * moveSpeed;

                }

                else
                {

                }
            }
        }






        tankBody.forward = Vector3.Slerp(tankBody.forward, moveDir, Time.deltaTime * rotateSpeed);


    }

    protected void UpdateGunForward()
    {

        gun.targetPosition = targetPosition;

    }

    public float GetCurrentHP()
    {
        return this.CurrentHp;
    }
    public float GetMaxHp()
    {
        return this.hP;
    }
    public float GetESkillCurrentCoundown()
    {
        return ESkillCurrentTimeCoundown;
    }
    public float GetESkillCoundown()
    {
        return ESkillTimeCoundown;
    }
    public float GetQSkillCurrentCoundown()
    {
        return QSkillCurrentTimeCoundown;
    }
    public float GetQSkillCoundown()
    {
        return QSkillTimeCoundown;
    }
    public float GetSpaceSkillCurrentCoundown()
    {
        return SpaceSkillCurrentTimeCoundown;
    }
    public float GetSpaceSkillCoundown()
    {
        return SpaceSkillTimeCoundown;
    }

    public void spawnToNewPosition(Vector3 position, float spawnTime)
    {
        spwaning = true;
        spawnposition = position;
        Invoke("Tranform", spawnTime);
    }
    private void Tranform()
    {
        transform.position=spawnposition;
        spwaning=false;
    }
    protected void DestroySelf()
    {
        dead = true;
        gameManagement.Instance.textMeshProUGUIAnouncement.gameObject.SetActive(true);
        Invoke("HideAnounce", 3f);
        gameManagement.Instance.textMeshProUGUIAnouncement.text += System.Environment.NewLine  + "You has been slained ";
        transform.position= new Vector3(transform.position.x,-20f, transform.position.z);
    }
    protected void HideAnounce()
    {
        gameManagement.Instance.textMeshProUGUIAnouncement.gameObject.SetActive(false);
    }

    public void SetHeal(float healam)
    {
        isHeal = true;
        healAmount = healam;
    }
  public void SetStunned(float timer)
    {
        stunned= true;
        Invoke("UnStun", timer);
    }
    private void UnStun()
    {
        stunned = false;
    }

    protected void HandleHeal()
    {
    isHeal=false;
        Heal();

    }
    public void Heal()
    {

        if (CurrentHp < hP)
        {

            hpStatus.text = "+" + healAmount ;
            PlaySound(soundSO.heal, transform.position);
            hpStatus.gameObject.SetActive(true);
            Invoke("SetFalse", 0.2f);
            hpStatus.color = new Color(34, 139, 34);
            OnDamegeTaked?.Invoke(this, new IDisplayHP.OnDamegeTakedEventArgs
            {
                hpRemainNormalized = CurrentHp / hP,
                shield = shield / hP
            }) ;
            float temp = (float)(CurrentHp + (healAmount) );
            if (temp > hP)
            {
                CurrentHp = hP;
            }
            else
            {
                CurrentHp += (healAmount );
            }


        }

    }
    protected void HandleInteractWithGameItem()
    {

        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractionDir = moveDir;
        }
        float interactionDistant = 7f;
        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactionDistant, interactAble))
        {
            

            if (raycastHit.transform.TryGetComponent(out firstAirGameItem firstAirGameItem))
            {
               SetHeal(hP*0.25f);
              
                Destroy(firstAirGameItem.gameObject);

              
            }
            else
            {
                if (raycastHit.transform.TryGetComponent(out SpeedUpGameItem speedUpGameItem))
                {
                 
                    UpdateSpeedGameItem();
                    Destroy(speedUpGameItem.gameObject);
                    PlaySound(soundSO.enhanceSpeed,transform.position);

                  
                }
                else
                {
                  
                    if (raycastHit.transform.TryGetComponent(out UpdateGunGameItem updateGunGameItem))
                    {
                       UpdateBulletGameItem();
                        Destroy(updateGunGameItem.gameObject);
                        PlaySound(soundSO.enhanceGun, transform.position);

                    }
                }
            }
        }
        else
        {

            box = null;

        }
    }
    private void UpdateSpeedGameItem()
    {
        moveSpeed *= 1.35f;
        Invoke("NomalizeUpdateSpeedGameItem", 15f);
    }
    private void NomalizeUpdateSpeedGameItem()
    {
        moveSpeed /= 1.35f;
    }
    private void UpdateBulletGameItem()
    {
        updateGameItemBullet=true;
        Invoke("NomalizeUpdateBulletGameItem", 8f);
    }
    private void NomalizeUpdateBulletGameItem()
    {
        updateGameItemBullet = false;
    }
    protected void SpawnBullet(Vector3 dir, Vector3 forward, float speed)
    {
        UpdateGunForward();
        bool critHit =false;
        int percenCrit = Random.Range(0, 100);
        if(percenCrit<critPercent)
        {
            critHit = true;
        }
        targetPosition.y = 0f;
        targetPosition.x -= transform.position.x;
        targetPosition.z -= transform.position.z;

        Transform bulletTransform = Instantiate(currentBullet, this.transform.position, Quaternion.identity, bullets);
        bullet bullet = bulletTransform.GetComponent<bullet>();
        bullet.setDamge(damage,critHit);

        bullet.Container = TextContainer;
        SetBulletProperty(transform.position, forward, speed, dir, shootAble, bullet, maxArange);

    }
    protected void SetBulletProperty(Vector3 position, Vector3 forward, float speed, Vector3 dir, LayerMask shootAble, bullet bulletTranform, float maxArange)
    {
        bulletTranform.originalPos = transform.position;
        bulletTranform.transform.right = forward;
        bulletTranform.attackSpeed = speed;
        bulletTranform.dir = dir;
        bulletTranform.shootAble = shootAble;
        bulletTranform.maxrange = maxArange;
    }

    protected void HandleNormalAttack()
    {
        if (attackRelaxTimer >= attackSpeed)
        {
        
            if (updateGameItemBullet)
            {
                ThreeBullet();
            }
            else
            {
   Shoot();
            }
         
            attackRelaxTimer = 0;
        }
    }
    private void ThreeBullet()
    {
        Shoot();
        Invoke("Shoot", 0.1f);
        Invoke("Shoot", 0.2f);
    }
    private void Shoot()
    {
        targetPosition.y = 0f;
        targetPosition.x -= transform.position.x;
        targetPosition.z -= transform.position.z;
        SpawnBullet(targetPosition.normalized, targetPosition, speedOfBullet);
        PlaySound(soundSO.shoot, transform.position);
    }
    protected void SetCooldown()
    {
        if (ESkillCurrentTimeCoundown > 0)
        {
            ESkillCurrentTimeCoundown -= Time.deltaTime;
        }
        if (QSkillCurrentTimeCoundown > 0)
        {
         QSkillCurrentTimeCoundown -= Time.deltaTime;

        }
        if (SpaceSkillCurrentTimeCoundown > 0)
        {
            SpaceSkillCurrentTimeCoundown -= Time.deltaTime;
        }




    }
    public virtual  void TakedDamage(float damage)
    {
        damage = damage * (500 - armor) / 500;
        if (shield>damage)
        {
            shield-=damage;
            hpStatus.gameObject.SetActive(true);
            Invoke("SetFalse", 0.2f);

            hpStatus.text = "-" + damage;

            
        }
        else
        {
            if (shield > 0)
            { damage -= shield; }
            shield = 0;
 if (CurrentHp >= damage)
        {
            CurrentHp -= damage;
            PlaySound(soundSO.hit, transform.position);

        }
        else
        {
            CurrentHp = 0;
            DestroySelf();

        }
        hpStatus.gameObject.SetActive(true);
        Invoke("SetFalse", 0.2f);



            hpStatus.color = Color.red;
        hpStatus.text = "-" + damage;

        }
       
        
        OnDamegeTaked?.Invoke(this, new IDisplayHP.OnDamegeTakedEventArgs
        {
            hpRemainNormalized = CurrentHp / hP,
            shield=shield/hP
        });
     
    }

    void SetFalse()

    {

        hpStatus.gameObject.SetActive(false);

    }
    protected void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 10f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * 2f);
    }
    public void UpdateWeapon(TankItemByShop tankItemByShop)
    {


       
       armor += tankItemByShop.armor;
        damage += tankItemByShop.damamge;
        moveSpeed += tankItemByShop.moveSpeed;
        attackSpeed =attackSpeed / (100 +tankItemByShop.attackSpeed) * 100;
        hP += tankItemByShop.hP;
        CurrentHp += tankItemByShop.hP;
        countdown += tankItemByShop.coolDown;
        critPercent += tankItemByShop.critPercent;
        

    }
    public void ReloadWeapon()
    {
   
        TankItemByShop[] temp=tankSelectManagement.tankItemByShops;
        foreach(TankItemByShop item in temp)
        {
            if (item != null) {   tempHp += item.hP;
            tempArmor+= item.armor;
            tempAttackSpeed+= item.attackSpeed;
            tempCoolDown += item.coolDown;
            tempcrit += item.critPercent;
            tempdamage += item.damamge;
            tempMoveSpeed += item.moveSpeed;}
          
        }
            
    armor +=tempArmor;
        damage += tempdamage;
        moveSpeed += tempMoveSpeed;
        attackSpeed = attackSpeed / (100 + tempAttackSpeed) * 100;
        hP += tempHp;
        CurrentHp += tempHp;
        countdown += tempCoolDown;
        critPercent += tempcrit;


    }




}
