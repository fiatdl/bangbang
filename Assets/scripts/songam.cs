using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SocialPlatforms;

public class songam : MonoBehaviour
{

    public Transform Container;
    public static songam Instance { get; private set; }
    public Vector3 originalPos;
    public LayerMask shootAble;
    public float attackSpeed;
    private float damege;
    public float maxrange;
    public Vector3 dir;
    private bool hitted;
    float interactionDistant = 5f;
    EnemyObject enemyObject;
    private float lifeRemain;
    private bool damaged;
    private void Awake()
    {
        hitted = false;
        damaged = false;
       lifeRemain = secondTank.Instance.timeValidToCastEAgain;
        Instance = this;

    }
    private void Update()
    {
        if(!secondTank.Instance.ECastAgain&&secondTank.Instance.EHit)
        {
            lifeRemain-=Time.deltaTime;
        }

        if (Vector3.Distance(originalPos, transform.position) >= GetMaxRange())
        {
            DestroySefl();
        }
        if (secondTank.Instance.ECastAgain||lifeRemain<=0)
        {
           
            secondTank.Instance.EHit = false;
            DestroySefl();
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, shootAble);
        if(colliders.Length > 0 )
        {

            if (!damaged)
            {
               EnemyObject target= colliders[0].transform.GetComponent<EnemyObject>();
                damaged = true;
                target.TakedDamege(damege, false);
                Debug.Log("dame");

            }
            transform.position = colliders[0].transform.position; transform.parent = colliders[0].transform;
            secondTank.Instance.EHit = true;
            secondTank.Instance.ETranformHitted = colliders[0].transform;

           

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
