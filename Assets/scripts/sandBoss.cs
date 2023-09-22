using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sandBoss : EnemyObject
{

    private int isAbleToClone;



    public BulletSO UntilSO;


    [SerializeField] private Transform robotParent;



    [SerializeField] protected BulletSO aoeSO;
    public static sandBoss Instance { get; private set; }


    private Vector3 mouseDir;
    private Vector3 targetPosition;
    private Camera cam;
    [SerializeField] Transform boom;

    private bool isWalking;

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

        destroyVisual.gameObject.SetActive(false);
        hp = 2000f;
        skillCoundown = 3f;
        skillCoundownCurrent = 0f;
        CurrentHp = hp;
        Instance = this;
        damage = 8f;
        moveSpeed = 10.0f;
        rotateSpeed = 10f;
        HideHittedVisual();
        buttletSpeed = 15f;
        speedOfBullet = 50f;
        attackSpeed = 1.0f;
        randomMoveCoundown = 2f;
        originalPos = transform.position;


        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {


        if (target != null)
        {
            gun.targetPosition = target.transform.position;
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
            PlaySound(soundSO.enemyAoe, transform.position);
            Aoe();
            Invoke("Aoe", 0.2f);
            Invoke("Aoe", 0.4f);

        }
    }
    private void Aoe()
    {

        targetPosition = target.transform.position;

        targetPosition.y = 0f;
        targetPosition.x -= transform.position.x;
        targetPosition.z -= transform.position.z;
        Vector3 dir = targetPosition;



        Transform bulletTransform = Instantiate(bulletSO.preFab, this.transform.position, Quaternion.identity, bullets);
        bullet bl = bulletTransform.GetComponent<bullet>();
        bl.setDamge(damage, false);

        bl.Container = bullets;
        bl.originalPos = transform.position;
        bl.transform.right = dir;
        bl.attackSpeed = speedOfBullet;
        bl.dir = dir.normalized;
        bl.shootAble = shootAble;
        bl.maxrange = maxRange;


    }
    private void HandleMovement()
    {
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
