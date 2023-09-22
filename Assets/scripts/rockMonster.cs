using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class rockMonster : EnemyObject
{


    private int isAbleToClone;



    public BulletSO RockSkill;


    [SerializeField] private Transform robotParent;



    [SerializeField] protected BulletSO aoeSO;
    public static rockMonster Instance { get; private set; }


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
    public bool isMove;
  
    private void Awake()
    {



    }
    void Start()
    {
        isBooming = false;
        isAttack = false;
        isMove=true;
        HideHittedVisual();
        destroyVisual.gameObject.SetActive(false);
        hp = 900f;
        skillCoundown = 5f;
        skillCoundownCurrent = 0f;
        CurrentHp = hp;
        Instance = this;
        damage = 40f;
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
      
        if (target != null)
        {
            skillCoundownCurrent -= Time.deltaTime;
          
            HandleAttack();
            if(!isAttack) { isMove = true;
                HandleMovement(); }
            
         
        }

    }
    private void HandleAttack()
    {
        if (skillCoundownCurrent <= 0 && Vector3.Distance(transform.position, target.transform.position) <= 30f)
        {
            gun.targetPosition = target.transform.position;
            skillCoundownCurrent = skillCoundown;
            
            isAttack = true;
            Invoke("FinnishAtack", 1.1f);
            Invoke("Aoe", 1f);

            PlaySound(soundSO.rockSkill, transform.position);
          
  

        }
        else
        {
            
        }
    }
    private void FinnishAtack()
    {
        isAttack = false;
    }
    private void Aoe()
    {

        Transform bulletTransform = Instantiate(RockSkill.preFab, transform.position, Quaternion.identity, bullets);
        rockSkill aoeBullet = bulletTransform.GetComponent<rockSkill>();

        targetPosition.y = 0f;
        targetPosition.x =target.transform.position.x- transform.position.x;
        targetPosition.z =target.transform.position.z- transform.position.z;
        aoeBullet.damege = damage;
        aoeBullet.shootAble = shootAble;
        aoeBullet.transform.right = targetPosition;

    }
    private void HandleMovement()
    {
        isMove = true;
        if (randomMoveCoundown <= 0)
        {
            randomMoveCoundown = 2f;
        }

        if (randomMoveCoundown == 2f)
        {

            moveDir = new Vector3(UnityEngine.Random.Range(originalPos.x - 10f, originalPos.x + 10f) - originalPos.x, 0, UnityEngine.Random.Range(originalPos.z - 10f, originalPos.z + 10f) - originalPos.z).normalized;
        }
        randomMoveCoundown -= Time.deltaTime;

        isWalking = moveDir != Vector3.zero;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveSpeed * Time.deltaTime);
        if (canMove && (Vector3.Distance(originalPos, transform.position) < 50f))
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

  
}
