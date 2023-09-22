using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockAnimation : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private rockSkill robot;
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        animator.SetBool("isAttack", robot.shake);


    }
}