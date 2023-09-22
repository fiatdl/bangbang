using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static armyTank;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;

public class boomerangBullet : MonoBehaviour
{

    public Transform Container;
    public Transform Oner;
    public static boomerangBullet Instance { get; private set; }
    public Vector3 originalPos;
    public LayerMask shootAble;
    public float attackSpeed;
    private float damege;
    public float maxrange;
    public Vector3 dir;
    private bool backing;
    float interactionDistant = 5f;
    EnemyObject enemyObject;

    private void Awake()
    {
        Instance = this;
        backing = false;

    }
    private void Update()
    {
       
        if(Vector3.Distance(transform.position,Oner.transform.position) <=3.0f&&backing)
            {
                Destroy(gameObject);
            }

        if (Vector3.Distance(originalPos, transform.position) >= GetMaxRange()&&!backing)
        {
            backing = true;
         
        }
        else
        {
            if (backing)
            {
   transform.position += GetDirToTarget(Oner.transform.position) * Time.deltaTime * attackSpeed;
            }
            else
            {
  transform.position += dir * Time.deltaTime * attackSpeed;
            }
          
            
           
        }

        if (Physics.Raycast(transform.position, dir, out RaycastHit raycastHit, interactionDistant, shootAble))
        {

            if (raycastHit.transform.TryGetComponent(out EnemyObject Enemy))
            {


                Enemy.TakedDamege(damege, false);

                if (enemyObject != Enemy)
                {
                    enemyObject = Enemy;

                }
             
            }
            else
            {
                enemyObject = null;
            }


            if (raycastHit.transform.TryGetComponent(out tank player))
            {
                player.TakedDamage(damege);

               
            }

        }
        else
        {
            enemyObject = null;
        }

      




    }
    private Vector3 GetDirToTarget(Vector3 Target)
    {
        Target.y = 0f;
        Target.x -= transform.position.x;
        Target.z -= transform.position.z;
        return Target.normalized;

    }
    private void HideText()
    {
        foreach (Transform child in Container)
        {
            Destroy(child.gameObject);
        }
    }
    public void setDamge(float damge)
    {
        this.damege = damge;
    }
    public float GetMaxRange()
    {
        return maxrange;
    }


    public void DestroySefl()
    {
        Destroy(gameObject);
    }



}
