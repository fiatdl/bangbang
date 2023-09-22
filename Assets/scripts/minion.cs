using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class minion : EnemyObject
{
    [SerializeField] public road nextRay;

    private int isAbleToClone;



    public BulletSO UntilSO;


    [SerializeField] private Transform robotParent;



    [SerializeField] protected BulletSO aoeSO;
    public static minion Instance { get; private set; }


    private Vector3 mouseDir;
    private Vector3 targetPosition;
    private Camera cam;
    [SerializeField] Transform boom;

    private bool isWalking;
    public bool isAttack;
    private float randomMoveCoundown;
    private Vector3 lastInteractionDir;
    public Vector3 moveDir;

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
        hp = 100f;
        skillCoundown = 2f;
        skillCoundownCurrent = 0f;
        CurrentHp = hp;
        Instance = this;
        damage = 10f;
        moveSpeed = 10.0f;
        rotateSpeed = 10f;
        gold.text = "+" + enemySO.coin.ToString();
        gold.gameObject.SetActive(false);
        bulletSpeed = 45f;
        speedOfBullet = 15f;
        attackSpeed = 0.5f;
        randomMoveCoundown = 2f;
        originalPos = transform.position;


        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        gun.targetPosition = target.transform.position;
        if (target != null)
        {
            skillCoundownCurrent -= Time.deltaTime;
            HandleMovement();
            HandleAttack();
        }

    }
    private void HandleAttack()
    {
        if (skillCoundownCurrent <= 0 && Vector3.Distance(transform.position, target.transform.position) <= 30f)
        {
            skillCoundownCurrent = skillCoundown;
            isAttack = true;

            PlaySound(soundSO.enemyAoe, transform.position);
            Transform bulletTransform = Instantiate(bulletSO.preFab, transform.position, Quaternion.identity, bullets);
            bullet kitchenObject = bulletTransform.GetComponent<bullet>();
            targetPosition = target.transform.position;
            targetPosition.y = 0f;
            targetPosition.x -= transform.position.x;
            targetPosition.z -= transform.position.z;
       
            kitchenObject.attackSpeed = bulletSpeed;
            kitchenObject.originalPos = transform.position;
            kitchenObject.transform.right = targetPosition;
            kitchenObject.dir = targetPosition.normalized;
            kitchenObject.shootAble = shootAble;
            kitchenObject.maxrange = maxRange;
            kitchenObject.setDamge(damage,false);
         

        }
        else
        {

        }
    }
    private void Aoe()
    {

        Transform bulletTransform = Instantiate(aoeSO.preFab, target.transform.position, Quaternion.identity, bullets);
        aoeBullet aoeBullet = bulletTransform.GetComponent<aoeBullet>();
        aoeBullet.sound = soundSO.enemyAoe;
        aoeBullet.dame = damage;
        aoeBullet.tankMask = shootAble;

    }
    private void HandleMovement()
    {

        if (Vector3.Distance(transform.position,nextRay.pos.transform.position)<=7f)
        {
           
            nextRay = nextRay.nextPos;
            moveDir = new Vector3(nextRay.pos.transform.position.x - transform.position.x, 0f, nextRay.pos.transform.position.z - transform.position.z).normalized;
        }
     
            transform.position += (Vector3)moveDir * Time.deltaTime * moveSpeed;

       

        tankBody.transform.forward = Vector3.Slerp(tankBody.transform.forward, moveDir, Time.deltaTime * rotateSpeed);







    }

 
}
