using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botanimator : MonoBehaviour
{
    private Animator animator;
    
    [SerializeField] private enemyBot robot;
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        animator.SetBool("attack", robot.isAttack);
      

    }
}