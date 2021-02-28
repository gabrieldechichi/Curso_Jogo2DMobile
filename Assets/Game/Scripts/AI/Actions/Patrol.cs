using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Platformer2D.Character;
using System.Collections;
using UnityEngine;

[Action("Game/Patrol")]
public class Patrol : BasePrimitiveAction
{
    [InParam("AIController")]
    private EnemyAIController aiController;

    [InParam("PatrolSpeed")]
    private float patrolSpeed;

    [InParam("CharacterMovement")]
    private CharacterMovement2D charMovement;

    public override void OnStart()
    {
        base.OnStart();
        aiController.StartCoroutine(TEMP_Walk());
        charMovement.MaxGroundSpeed = patrolSpeed;
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.RUNNING;
    }

    public override void OnAbort()
    {
        base.OnAbort();

        // TODO: Remover Corotina
        aiController.StopAllCoroutines();
    }

    IEnumerator TEMP_Walk()
    {
        while (true)
        {
            aiController.MovementInput = new Vector2(1, 0);
            yield return new WaitForSeconds(1.0f);
            aiController.MovementInput = new Vector2(0, 0);
            yield return new WaitForSeconds(2.0f);
            aiController.MovementInput = new Vector2(-1, 0);
            yield return new WaitForSeconds(1.0f);
            aiController.MovementInput = new Vector2(0, 0);
            yield return new WaitForSeconds(2.0f);
        }
    }
}
