using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyTurret : EnemyObject
{

   


    
    private float relax;
    
    private Vector3 targetPosition;

    public static EnemyTurret Instance { get; private set; }
    void Start()
    {
        destroyVisual.gameObject.SetActive(false);
        gun.targetPosition = target.transform.position; relax = attackSpeed;
        Instance = this;
        hp = 1000f;
        damage = 55f;
        CurrentHp = hp;
        bulletSpeed = 15f;
        rotateSpeed = 10f;
        attackSpeed = 0.9f;
        gold.text = "+" + enemySO.coin.ToString();
        gold.gameObject.SetActive(false);


    }
    
    private void Awake()
    {
       
        
     
    }

    // Update is called once per frame
    void Update()
    {
          gun.targetPosition = target.transform.position;
        relax -= Time.deltaTime;
        if (Vector3.Distance(transform.position, target.transform.position) <= maxRange&&relax<=0)
        {   
            Transform bulletTransform = Instantiate(bulletSO.preFab, this.transform.position, Quaternion.identity, bullets);
            bullet kitchenObject = bulletTransform.GetComponent<bullet>();
             targetPosition=target.transform.position;
            targetPosition.y = 0f;
            targetPosition.x -= transform.position.x;
            targetPosition.z -= transform.position.z;
            kitchenObject.attackSpeed = bulletSpeed;
            kitchenObject.originalPos = transform.position;
            kitchenObject.transform.right = targetPosition;
            kitchenObject.dir = targetPosition.normalized;
            kitchenObject.shootAble = shootAble;
            kitchenObject.maxrange = maxRange;
            kitchenObject.setDamge(damage, false);
            relax = attackSpeed;
        }
        
      
    }

    public Vector3 GetPlayerPos()
    {
        return target.transform.position;
    }


    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 10f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * 2f);
    }
}
