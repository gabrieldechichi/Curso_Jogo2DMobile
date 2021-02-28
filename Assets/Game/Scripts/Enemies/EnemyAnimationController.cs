using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : CharacterAnimationController
{
    EnemyAIController aiController;

    protected override void Awake()
    {
        base.Awake();
        aiController = GetComponent<EnemyAIController>();
    }

    protected override void Update()
    {
        base.Update();
        animator.SetBool(EnemyAnimationKeys.IsChasing, aiController.IsChasing);
    }
}
