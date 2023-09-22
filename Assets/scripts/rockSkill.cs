using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static armyTank;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;
using UnityEngine.SocialPlatforms;

public class rockSkill : MonoBehaviour
{

    public Transform[] damePos;
    [SerializeField] Transform explosionVisual;
   
    public static rockSkill Instance { get; private set; }
    
    public LayerMask shootAble;
   
    public float damege;
   
    public Vector3 dir;
  
    float interactionDistant = 8f;
    private float lifeTime;
    EnemyObject enemyObject;
    public bool shake;
    public bool isdamaged;
    private void Awake()
    {
        shake = false;
        isdamaged= false;
        lifeTime = 1.5f;
        Instance = this;
        explosionVisual.gameObject.SetActive(false);
       

    }
    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
        if (lifeTime <= 1f&&!shake)
        {
            shake = true;
            
        }
        if (lifeTime <= 0.5f&&!isdamaged)
        {
            isdamaged = true;
            HandleRockDamage();
            explosionVisual.gameObject.SetActive(true);
        }
    }

    public void HandleRockDamage()
    {
        foreach(Transform t in damePos)
        {
 Collider[] colliders = Physics.OverlapSphere(t.transform.position, interactionDistant, shootAble);
        if (colliders.Length > 0)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.transform.TryGetComponent(out tank player))

                {
                 
                    player.TakedDamage(damege / 5);
                        player.SetStunned(2f);

                }

            }
        }
        }
       
    }

}
