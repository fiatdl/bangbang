using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class enemyBot : EnemyObject
{


    private int isAbleToClone;


  
    public BulletSO UntilSO;


    [SerializeField] private Transform robotParent;

 

     [SerializeField] protected BulletSO aoeSO;
    public static enemyBot Instance { get; private set; }

 
    private Vector3 mouseDir;
    private Vector3 targetPosition;
    private Camera cam;
    [SerializeField] Transform boom;

    private bool isWalking;
    public bool isAttack;
    private float randomMoveCoundown;
    private Vector3 lastInteractionDir;
    private Vector3 moveDir;

    private Vector3 originalPos;
    private float skillCoundown;
    private float skillCoundownCurrent;
    private void Awake()
    {

      
      
    }
    void Start()
    {
        isBooming = false;
        isAttack = false;
        HideHittedVisual();
        destroyVisual.gameObject.SetActive(false);
        hp = 900f;
        skillCoundown = 3f;
        skillCoundownCurrent = 0f;
        CurrentHp = hp;
        Instance = this;
        damage = 100f;
        moveSpeed = 10.0f;
        rotateSpeed = 10f;
        gold.text = "+" + enemySO.coin.ToString();
        gold.gameObject.SetActive(false);
        buttletSpeed = 15f;
        speedOfBullet = 45f;
        attackSpeed = 0.5f;
        randomMoveCoundown = 2f;  
        originalPos = transform.position;
      

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
      

        if (target != null) {  
        skillCoundownCurrent -= Time.deltaTime;
        HandleMovement();
        HandleAttack();}
       
    }
    private void HandleAttack()
    {
        if (skillCoundownCurrent <= 0&& Vector3.Distance(transform.position,target.transform.position)<=30f)
        {  skillCoundownCurrent = skillCoundown;
            isAttack = true;
     
            PlaySound(soundSO.enemyAoe, transform.position);
            Aoe();
          
        }
        else
        {
           
        }
    }
    private void Aoe()
    {
        
        Transform bulletTransform = Instantiate(aoeSO.preFab,target.transform.position,Quaternion.identity,bullets);
        aoeBullet aoeBullet =  bulletTransform.GetComponent<aoeBullet>();
        aoeBullet.sound = soundSO.enemyAoe;
        aoeBullet.dame = damage;
        aoeBullet.tankMask = shootAble;
       
    }
    private void HandleMovement()
    {
        if(randomMoveCoundown<=0)
        {
            randomMoveCoundown = 2f;
        }
    
        if (randomMoveCoundown == 2f) {
          
            moveDir  = new Vector3(UnityEngine.Random.Range(originalPos.x-10f,originalPos.x+10f)-originalPos.x, 0, UnityEngine.Random.Range(originalPos.z - 10f, originalPos.z + 10f)-originalPos.z).normalized;}
      randomMoveCoundown -= Time.deltaTime;
 
        isWalking = moveDir != Vector3.zero;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveSpeed * Time.deltaTime);
        if (canMove&&(Vector3.Distance(originalPos,transform.position)<50f))
        {
             transform.position += (Vector3)moveDir * Time.deltaTime * moveSpeed;

        }

        else
        {

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveSpeed * Time.deltaTime);
            if (canMove && (Vector3.Distance(originalPos, transform.position) < 100f))
            {
                moveDir = moveDirX;
                transform.position += (Vector3)moveDir * Time.deltaTime * moveSpeed;

            }


            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveSpeed * Time.deltaTime);
                if (canMove && (Vector3.Distance(originalPos, transform.position) < 100f))
                {
                    moveDir = moveDirZ;
                    transform.position += (Vector3)moveDir * Time.deltaTime * moveSpeed;

                }

                else
                {

                }
            }
        }


        tankBody.transform.forward = Vector3.Slerp(tankBody.transform.forward, moveDir, Time.deltaTime * rotateSpeed);







    }

    private void Extract()
    {
        
        Transform enemyBotChild = Instantiate(enemySO.preFab, transform.position, Quaternion.identity, transform);
        enemyBot enemyBot = enemyBotChild.GetComponent<enemyBot>();
        enemyBot.robotParent = robotParent;
        enemyBot.hp = hp / 2;
        enemyBot.bullets=bullets;

        enemyBot.target = target;
        enemyBot.soundSO = soundSO;
        enemyBot.shootAble=shootAble;
        enemyBot.robotParent=robotParent;
        enemyBot.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
 

    }
}
