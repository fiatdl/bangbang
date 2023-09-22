using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poliAnimation : MonoBehaviour
{
    private Animator animator;
    private const string IS_WALKING = "isfly";
    [SerializeField] private poligonal player;
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        animator.SetBool("Walk Forward", player.isMoving);
        animator.SetBool("Smash Attack", player.isAttack);
        animator.SetBool("Defend", player.isDefen);

    }
}