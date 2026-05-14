using UnityEngine;
using System;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;
    private EnemyEntity enemyMove;

    void Start()
    {
        anim = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyEntity>();
    }

    void Update()
    {
        float absX = Math.Abs(enemyMove.movementDir.x);
        anim.SetFloat("dirX", absX);
        
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        
        if (state.IsName("EnemyIdle") || state.IsName("EnemyWalking"))
        {
            if (enemyMove.isAttacking) 
            {
                enemyMove.isAttacking = false;
            }
        }
    }
}