using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class armyTank : tank ,IDisplayHP{
    public event EventHandler OnQ;
 

    public event EventHandler OnCastESkillSound;
    public event EventHandler OnCastSpaceSkillSound;

    public static armyTank Instance { get; private set; }

    [SerializeField] private Transform QVisual;
  
  
  
    private Camera cam;
  
   
 
 
 
    [SerializeField] private BulletSO textSO;
    [SerializeField] Transform spaceVisual;
   
    [SerializeField] private BulletSO superBullet;
    [SerializeField] private BulletSO boomerangBullet;
    private Vector3 lastMousePosition;

  
   

    private void Awake()
    {
       

    
   
}
    private void Start()
    {
        Instance = this;
        shield = 0;
        isElectric=false;
        isGhost = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gameInput.skill1 += GameInput_skill1;
        gameInput.Until += GameInput_Until;
        gameInput.Sub += GameInput_Sub;
      attackRelaxTimer = attackSpeed;
        heal.gameObject.SetActive(false);
        QVisual.gameObject.SetActive(false);
        spaceVisual.gameObject.SetActive(false);
        currentBullet = bulletSO.preFab;
        hP = 500f;
        CurrentHp = hP;
        maxArange = 35f;
        updateGameItemBullet = false;
        armor = 10f;
        critPercent = 0f;
        countdown = 0f;

        damage = 20f;
        moveSpeed = 20.0f;
        rotateSpeed = 10f;

        speedOfBullet = 90f;
        attackSpeed = 0.3f;
        ESkillCurrentTimeCoundown = 0;
        ESkillTimeCoundown = 5f;
        QSkillCurrentTimeCoundown = 0;
        QSkillTimeCoundown = 5f;
        isHeal = false;

        SpaceSkillCurrentTimeCoundown = 0;
        SpaceSkillTimeCoundown = 12f;
        qInfo = "Chém gió "+System.Environment.NewLine+"gây sát thương cho kẻ định xung quanh";
        eInfo = "bắn ra 3 boomerang gây "+damage.ToString()+"*"+" 5 ";
        spaceInfo = "Dịch chuyển Cổ xưa "+System.Environment.NewLine+"dịch chuyển đến vị trí chỉ định và tăng sát thương , tốc độ di chuyển và tốc độ đánh";
        ReloadWeapon();
    }

    private void GameInput_Sub(object sender, EventArgs e)
    {
        bool isCrit = false;
        int percenCrit = Random.Range(0, 100);
        if (percenCrit < critPercent)
        {
            isCrit = true;
        }
        if (QSkillCurrentTimeCoundown<=0)
        {  
        QVisual.gameObject.SetActive(true);
        Invoke("QOff", 0.6f);
         Collider[] colliders =Physics.OverlapSphere(transform.position, 15f);
        
        foreach(Collider c in colliders)
        {
            if (c.GetComponent<EnemyObject>())
            {

                    PlaySound(soundSO.skillQ, transform.position);
                c.GetComponent<EnemyObject>().TakedDamege(damage*1.8f,isCrit);
            }
        }
            QSkillCurrentTimeCoundown = QSkillTimeCoundown * (100 - countdown) / 100;
    
        }
      
    }
    private void QOff()
    {

        QVisual.gameObject.SetActive(false);
    }

    private void GameInput_Until(object sender, EventArgs e)
    {
        if(SpaceSkillCurrentTimeCoundown<=0) {

            PlaySound(soundSO.skillSpace, transform.position);
            PlaySound(soundSO.tank1teloport, transform.position);
            moveSpeed *= (float)1.1;
            damage *= (float)1.1;
          spaceVisual.gameObject.SetActive(true);
            attackSpeed/=1.1f;
            Invoke("Nomalize", SpaceSkillTimeCoundown);
            lastMousePosition = Input.mousePosition;
            Invoke("Transport", 0.2f);
       
            currentBullet = superBullet.preFab;
   

            SpaceSkillCurrentTimeCoundown = SpaceSkillTimeCoundown * (100 - countdown) / 100;
        }
      



    }
    private void Transport()
    {
       Ray ray=cam.ScreenPointToRay(lastMousePosition);
        if(Physics.Raycast(ray,out RaycastHit raycastHit))
        {
            transform.position =new Vector3(raycastHit.point.x, 0f,raycastHit.point.z);
           
        }

    }
    private void Nomalize()
    {
        spaceVisual.gameObject.SetActive(false);

        moveSpeed /= (float)1.1;
        damage /= (float)1.1;
        currentBullet = bulletSO.preFab;
        attackSpeed *= 1.1f;
    }

    private void GameInput_skill1(object sender, EventArgs e)
    {
        if(ESkillCurrentTimeCoundown<=0)
        {
            PlaySound(soundSO.skillE, transform.position);
            targetPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
         
        targetPosition.y = 0f;
        targetPosition.x -= transform.position.x;
        targetPosition.z -= transform.position.z;
        Vector3 dir = targetPosition;
        Vector3 dir1;
        Vector3 dir2;
    dir1 = Quaternion.AngleAxis(-15, Vector3.up) * dir;
    dir2=Quaternion.AngleAxis(15,Vector3.up) * dir;
            
            SpawnBoomerangBullet(dir.normalized, dir,speedOfBullet-15f,boomerangBullet.preFab,gun.transform);
            SpawnBoomerangBullet(dir1.normalized, dir1,speedOfBullet-15f,boomerangBullet.preFab,gun.transform);
            SpawnBoomerangBullet(dir2.normalized, dir2, speedOfBullet - 15f,boomerangBullet.preFab,gun.transform);
            ESkillCurrentTimeCoundown = ESkillTimeCoundown*(100-countdown)/100;
        
        }
       
        



    }

    private void SpawnBoomerangBullet(Vector3 dir, Vector3 forward, float speed, Transform BulletTransform,Transform Owner)
    {
        UpdateGunForward();
        targetPosition.y = 0f;
        targetPosition.x -= transform.position.x;
        targetPosition.z -= transform.position.z;

        Transform bulletTransform = Instantiate(BulletTransform, this.transform.position, Quaternion.identity, bullets);
        boomerangBullet bullet = bulletTransform.GetComponent<boomerangBullet>();
        bullet.setDamge(damage);
        bullet.Oner = Owner;
        bullet.Container = TextContainer;
        bullet.originalPos = transform.position;
        bullet.transform.right = forward;
        bullet.attackSpeed = speed;
        bullet.dir = dir;
        bullet.shootAble = shootAble;
        bullet.maxrange = maxArange;

    }


 
    private void Update()
    {
       
        if (isElectric)
        {
            PlaySound(soundSO.electric,transform.position);
            isElectric = false;
        }

        if (isHeal)
        {
            HandleHeal();
            heal.gameObject.SetActive(true);
        }
        else
        {
            heal.gameObject.SetActive(false);
        }
        if (!spwaning&&!stunned) { targetPosition = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            UpdateGunForward();
        SetCooldown();
        HandleMovement();
        HandleInteractWithGameItem();
        attackRelaxTimer+=Time.deltaTime;
       
      
        if (Input.GetMouseButtonDown(1)&&!stunned)
        {
            HandleNormalAttack();
            
               
        }
       }
       

    }
    public override void TakedDamage(float damage)
    {
        base.TakedDamage(damage);

    }





}


