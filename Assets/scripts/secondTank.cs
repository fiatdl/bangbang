using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using static UnityEngine.UI.GridLayoutGroup;
using Random = UnityEngine.Random;

public class secondTank : tank, IDisplayHP
{
    public event EventHandler OnQ;
 
    [SerializeField] protected BulletSO aoeSO;
    [SerializeField] private Transform shiedVisual;
    public event EventHandler OnCastESkillSound;
    public event EventHandler OnCastSpaceSkillSound;

    public static secondTank Instance { get; private set; }

    [SerializeField] private Transform QVisual;



    private Camera cam;

    public bool isFly;



    [SerializeField] private BulletSO textSO;
    [SerializeField] Transform spaceVisual;
 
    [SerializeField] private BulletSO songam;
    [SerializeField] private BulletSO boomerangBullet;
    private Vector3 lastMousePosition;
    private float flySpeed;
    public bool isSpacing;
    public bool EHit;
    public bool ECastAgain;
    public Transform ETranformHitted;
    public float timeValidToCastEAgain;
    public float timeValidToAttackEAgainCurrent;

    public float spacingTime;
    public float spacingTimeCurrent;
    private float scanAble;

    public float qTime;
    public float qTimeCurrent;
    private bool isShieldByQ;


    private void Awake()
    {
     


    }
    private void Start()
    {
        isElectric=false;
        Instance = this;
        isGhost = false;
        spaceVisual.gameObject.SetActive(false);
        shiedVisual.gameObject.SetActive(false);
        ECastAgain = false;
        shield = 0;
        scanAble = 0;
        EHit = false;
        spacingTime = 7f;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gameInput.skill1 += GameInput_skill1;
        gameInput.Until += GameInput_Until;
        gameInput.Sub += GameInput_Sub;
        attackRelaxTimer = attackSpeed;
        isFly = false;
        currentBullet = bulletSO.preFab;
        hP = 1000f;
        armor = 20f;
        countdown = 0f;
        critPercent = 0f;
        updateGameItemBullet = false;
        CurrentHp = hP;
        qTime = 4f;
        maxArange = 35f;
        timeValidToCastEAgain = 3f;
        damage = 70f;
        moveSpeed = 20.0f;
        flySpeed = 70.0f;
        rotateSpeed = 10f;
        isSpacing = false;
        speedOfBullet = 80f;
        attackSpeed = 0.5f;
        ESkillCurrentTimeCoundown = 0;
        ESkillTimeCoundown = 10f;
        QSkillCurrentTimeCoundown = 0;
        QSkillTimeCoundown = 10f;
        isHeal = false;
        isShieldByQ = false;
        SpaceSkillCurrentTimeCoundown = 0;
        SpaceSkillTimeCoundown = 12f;
        qInfo = "Tạo lá chắn tương đương 20% hp  ("+(hP/5).ToString()+") trong 4 s";
        eInfo = "Bắn nam châm tới mục tiêu gây "+(damage)*1.5+ " , nếu có kẻ địch bị hút bởi nam châm , bạn có thể tái kích hoạt để bay tới kẻ địch ";
        spaceInfo = "xoay nam châm xung quanh bản thân gây sát thương và hút máu";


    }

    private void GameInput_Sub(object sender, EventArgs e)
    {
        if (QSkillCurrentTimeCoundown <= 0)
        {
       
            QSkillCurrentTimeCoundown = QSkillTimeCoundown * (100 - countdown) / 100;

          qTimeCurrent = qTime;
            PlaySound(soundSO.armor, transform.position);
            shield = hP/5;
            isShieldByQ = true;




        }

    }
    private void QOff()
    {

        QVisual.gameObject.SetActive(false);
    }
    private void HandleSpacingAttack()
    {
        bool isCrit = false;
        int percenCrit = Random.Range(0, 100);
        if (percenCrit < critPercent)
        {
            isCrit = true;
        }
        if (scanAble<=0)
        {
            scanAble = 0.2f;

            PlaySound(soundSO.wind, transform.position);
            Collider[] colliders = Physics.OverlapSphere(transform.position, 7f, shootAble);
            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.transform.TryGetComponent(out EnemyObject enemyObject))

                    {
                        enemyObject.TakedDamege(damage, isCrit);
                        SetHeal(damage/2.5f);


                    }

                }
            }
        }
    }
    private void GameInput_Until(object sender, EventArgs e)
    {
        if (SpaceSkillCurrentTimeCoundown <= 0)
        {
            isSpacing = true;
                SpaceSkillCurrentTimeCoundown = SpaceSkillTimeCoundown * (100 - countdown) / 100;

            spacingTimeCurrent = spacingTime;
            moveSpeed +=5;
            Invoke("Nomalize", spacingTime);

            

        }


    }
    private void Nomalize()
    {
        moveSpeed -=5;
    }
    private void HandleFly()
    {
        
        if (dead)
        {
            return;
        }


        Vector3 moveDir = new Vector3(ETranformHitted.transform.position.x - transform.position.x, 0f, ETranformHitted.transform.position.z - transform.position.z).normalized;


        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveSpeed * Time.deltaTime);
        if (canMove)
        {
            transform.position += (Vector3)moveDir * Time.deltaTime * flySpeed;

        }

        else
        {

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveSpeed * Time.deltaTime);
            if (canMove)
            {

                moveDir = moveDirX;
                transform.position += (Vector3)moveDir * Time.deltaTime * flySpeed;

            }


            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveSpeed * Time.deltaTime);
                if (canMove)
                {
                    moveDir = moveDirZ;

                    transform.position += (Vector3)moveDir * Time.deltaTime * flySpeed;

                }

                else
                {

                }
            }
        }






        tankBody.forward = Vector3.Slerp(tankBody.forward, moveDir, Time.deltaTime * rotateSpeed);
        if (Vector3.Distance(transform.position, ETranformHitted.position) <= 5f)
        {
            isFly= false;
            EHit = false;
            ECastAgain= false;
        }
        
    }

    private void GameInput_skill1(object sender, EventArgs e)
    {

       if(ESkillCurrentTimeCoundown<=0) {
            ESkillCurrentTimeCoundown = ESkillTimeCoundown * (100 - countdown) / 100;
            targetPosition.y = 0f;
        targetPosition.x -= transform.position.x;
        targetPosition.z -= transform.position.z;
        Vector3 dir = targetPosition.normalized;
        Transform bl = Instantiate(songam.preFab, transform.position, Quaternion.identity, bullets);
        songam songanBl = bl.GetComponent<songam>();
      songanBl.setDamge(4*damage);
     
        songanBl.Container = bullets;
        songanBl.originalPos = transform.position;
        songanBl.transform.right = dir;
        songanBl.attackSpeed = speedOfBullet;
        songanBl.shootAble = shootAble;
        songanBl.maxrange = maxArange;
        songanBl.dir = dir; }
        if (EHit==true&&ECastAgain==false)
        {
            isFly = true;
            EHit = false;
            PlaySound(soundSO.flye, transform.position);
            ECastAgain = true;
           

        }
        





    }




    private void Update()
    {
        if (isElectric)
        {
            PlaySound(soundSO.electric, transform.position);
            isElectric= false;
        }
        HandleInteractWithGameItem();
        targetPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        if (isSpacing)
        {
            
            spaceVisual.gameObject.SetActive(true);
            spacingTimeCurrent -= Time.deltaTime;
            scanAble -= Time.deltaTime;
            HandleSpacingAttack();
            isGhost = true;

        }
        else
        {
            spaceVisual.gameObject.SetActive(false);
        }
        if (spacingTimeCurrent <= 0)
        {
            isSpacing = false;
          isGhost = false;

        }
        if (isShieldByQ)
        {
            shiedVisual.gameObject.SetActive(true);
            qTimeCurrent -= Time.deltaTime;
        }   
        if(qTimeCurrent <= 0||shield==0)
        {
            isShieldByQ = false;
            shield = 0;
            shiedVisual.gameObject.SetActive(false);
      
        }
        SetCooldown();
        if (isHeal)
        {
            HandleHeal();
            heal.gameObject.SetActive(true);
        }
        else
        {
            heal.gameObject.SetActive(false);
        }

     
        if(Vector2.Distance(new Vector2(lastMousePosition.x,lastMousePosition.z),new Vector2(transform.position.x,transform.position.z))<3f&&isFly) { isFly = false;
           
        }
        if (!isFly)
        {
            
            UpdateGunForward();
       
            HandleMovement();
       
            attackRelaxTimer += Time.deltaTime;


            if (Input.GetMouseButtonDown(1))
            {
                HandleNormalAttack();

            }
        }
        if (isFly)
        {
            if (ETranformHitted == null)
            {
                isFly = false;
                EHit = false;
                ECastAgain = false;
            }
            else
            {
 HandleFly();
            }
           
        }


    }


    private void Aoe()
    {

        Transform bulletTransform = Instantiate(aoeSO.preFab, targetPosition, Quaternion.identity, bullets);
        aoeBullet aoeBullet = bulletTransform.GetComponent<aoeBullet>();
        aoeBullet.sound = soundSO.enemyAoe;
        aoeBullet.tankMask = shootAble;

    }


}


