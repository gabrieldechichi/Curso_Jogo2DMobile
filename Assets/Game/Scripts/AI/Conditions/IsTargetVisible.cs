using BBUnity.Conditions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Condition("Game/Perception/IsTargetVisible")]
public class IsTargetVisible : GOCondition
{
    [InParam("Target")]
    private GameObject target;

    [InParam("AIVision")]
    private AIVision aiVision;

    [InParam("TargetMemoryDuration")]
    private float targetMemoryDuration;

    private float forgetTargetTime;

    public override bool Check()
    {
        if (!IsTargetAlive())
        {
            forgetTargetTime = 0;
            return false;
        }

        if (aiVision.IsVisible(target))
        {
            forgetTargetTime = Time.time + targetMemoryDuration;
            return true;
        }
        return Time.time < forgetTargetTime;
    }

    private bool IsTargetAlive()
    {
        // TODO: Criar interface generica para saber se o target esta disponivel
        // TODO: Nao fazer GetComponent toda frame
        var damageable = target.GetComponent<IDamageable>();
        return damageable == null || damageable.IsAlive;
    }
}
