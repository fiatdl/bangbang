using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTankAnimation : MonoBehaviour
{
    private Animator animator;
    private const string IS_WALKING = "isfly";
    [SerializeField] private secondTank player;
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        animator.SetBool("isfly", player.isFly);
        animator.SetBool("spacing", player.isSpacing);
    }
}