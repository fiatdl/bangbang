using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static armyTank;
using UnityEngine.UIElements;
using TMPro;

public class bullet : MonoBehaviour
{

     public Transform Container;
    public static bullet Instance { get; private set; }
    public Vector3 originalPos;
    public LayerMask shootAble;
    public float attackSpeed ;
    public float damege;
    public float maxrange ;
    public Vector3 dir;
        float interactionDistant = 5f;
    EnemyObject enemyObject;
    private bool isCrit;

    private void Awake()
    {
        Instance = this;
    
    }
    private void Update()
    {
    


        if (Vector3.Distance(originalPos, transform.position) >= GetMaxRange()) {

            DestroySefl();
        }

        if (Physics.Raycast(transform.position, dir, out RaycastHit raycastHit, interactionDistant, shootAble))
        {

            
            if (raycastHit.transform.TryGetComponent(out EnemyObject Enemy))
            {


                Enemy.TakedDamege(damege,isCrit);


            }
            if (raycastHit.transform.TryGetComponent(out target target))
            {


                target.Hit();


            }



            if (raycastHit.transform.TryGetComponent(out tank player))
        {
            player.TakedDamage(damege);

         
        }   DestroySefl();
    }
   

        transform.position += dir * Time.deltaTime * attackSpeed;

    }
    private void HideText()
    {
        foreach (Transform child in Container)
        {
            Destroy(child.gameObject);
        }
    }
    public void setDamge(float damge,bool isCrit)
    {
        this.damege= damge;
        this.isCrit = isCrit;
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
