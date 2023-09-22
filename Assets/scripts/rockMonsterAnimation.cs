using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockMonsterAnimation : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private rockMonster rockMonster;
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        animator.SetBool("isRock", rockMonster.isAttack);

        animator.SetBool("isWalk", !rockMonster.isAttack);

    }
}