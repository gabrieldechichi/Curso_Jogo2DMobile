using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Platformer2D.Character;
using UnityEngine;

[Action("Game/ChaseTarget")]
public class ChaseTarget : BasePrimitiveAction
{
    [InParam("Target")]
    private GameObject target;

    [InParam("AIController")]
    private EnemyAIController aiController;

    [InParam("ChaseSpeed")]
    private float chaseSpeed;

    [InParam("CharacterMovement")]
    private CharacterMovement2D charMovement;

    public override void OnStart()
    {
        base.OnStart();
        aiController.IsChasing = true;
        charMovement.MaxGroundSpeed = chaseSpeed;
    }

    public override void OnAbort()
    {
        base.OnAbort();
        aiController.IsChasing = false;
    }

    public override TaskStatus OnUpdate()
    {
        Vector2 toTarget = target.transform.position - aiController.transform.position;
        aiController.MovementInput = new Vector2(Mathf.Sign(toTarget.x) * 500.0f, 0);
        return TaskStatus.RUNNING;
    }
}
