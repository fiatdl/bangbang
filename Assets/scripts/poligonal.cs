using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDisplayHP;

public class poligonal : EnemyObject,IDisplayHP
{

  

 


    [SerializeField] private Transform robotParent;


    private event EventHandler<OnDamegeTakedEventArgs> OnDamegeTake;

    public static poligonal Instance { get; private set; }


    private Vector3 mouseDir;
    private Vector3 targetPosition;
    private Camera cam;
    [SerializeField] Transform boom;

    private bool isWalking;
    public bool isMoving;
    public bool isAttack;
    public bool isDefen;
    private float randomMoveCoundown;
    private Vector3 lastInteractionDir;
    private Vector3 moveDir;

    private Vector3 originalPos;
    private float skillCoundown;
    private float skillCoundownCurrent;

    private float surfingCoolDown;
    private float surfingCoolDownCurrent;
    void Start()
    {
        isDefen = false;
        isMoving= false;
        isAttack= false;
        isBooming = false;
        surfingCoolDown = 6f;
        surfingCoolDownCurrent = 6f;
        HideHittedVisual();
        destroyVisual.gameObject.SetActive(false);
        hp = 300f;
        skillCoundown = 3f;
        skillCoundownCurrent = 0f;
        CurrentHp = hp;
        Instance = this;
        damage = 100f;
        moveSpeed = 5.0f;
        rotateSpeed = 10f;
        gold.text ="+"+ enemySO.coin.ToString();
        gold.gameObject.SetActive(false);
        buttletSpeed = 15f;
        speedOfBullet = 45f;
        attackSpeed = 0.5f;
        randomMoveCoundown = 2f;
        originalPos = transform.position;
        skillCoundown = 3f;
        skillCoundownCurrent = 0f;

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        surfingCoolDownCurrent -= Time.deltaTime;
        if(surfingCoolDownCurrent < 0 )
        {
            Surfing();
            surfingCoolDownCurrent = surfingCoolDown;
            Invoke("UnSurfing", 0.5f);
        }
        
 skillCoundownCurrent -= Time.deltaTime;
        if (CurrentHp <= hp * 0.3f)
        {
            isDefen=true;
            isMoving = false;
            isAttack=false;
        }
        if(target!=null) {
       


                if (Vector3.Distance(transform.position, target.transform.position) <= 10f)
                {
                    HandleAttack();   isAttack = true;
                    isMoving=false;
                    isDefen = false;
                }
                else
                {isMoving = true;
                    isAttack = false;
                    isDefen = false;
                    HandleMovement();

                }

        
         }
       
    }
    private void Surfing()
    {
        moveSpeed +=50;

    
    }
    private void UnSurfing() {
    moveSpeed -=50;
    }

    private void HandleAttack()
    {
        if (skillCoundownCurrent <= 0 )
        {
            skillCoundownCurrent = skillCoundown;
            PlaySound(soundSO.bite, transform.position);
            target.TakedDamage(damage);

        }
        else
        {
            
        }
    }

    private void HandleMovement()
    {
        

            moveDir = (target.transform.position - transform.position).normalized; isWalking = moveDir != Vector3.zero;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveSpeed * Time.deltaTime);
        if (canMove&&Vector3.Distance(transform.position,target.transform.position)>5f )
        {
            transform.position += (Vector3)moveDir * Time.deltaTime * moveSpeed;

        }

        else
        {

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveSpeed * Time.deltaTime);
            if (canMove && Vector3.Distance(transform.position, target.transform.position) > 5f)
            {
                moveDir = moveDirX;
                transform.position += (Vector3)moveDir * Time.deltaTime * moveSpeed;

            }


            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveSpeed * Time.deltaTime);
                if (canMove && Vector3.Distance(transform.position, target.transform.position) > 5f)
                {
                    moveDir = moveDirZ;
                    transform.position += (Vector3)moveDir * Time.deltaTime * moveSpeed;

                }

                else
                {
                    isMoving = false;
                }
            }
        }


        tankBody.transform.forward = Vector3.Slerp(tankBody.transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }







    public override void TakedDamege(float damage,bool isCrit)
    {
        if (isDefen)
        {
            damage /= 3;
        }
base.TakedDamege(damage,false);




    }


    private void ShowHittedVisual()
    {
        foreach (var item in hittedVisuals)
        {
            item.SetActive(true);
        }


    }


}
