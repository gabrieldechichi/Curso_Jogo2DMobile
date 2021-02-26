using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterFacing2D))]
public class AIVision : MonoBehaviour
{
    [Range(0.5f, 10.0f)]
    public float visionRange = 5;

    [Range(0, 180)]
    public float visionAngle = 30;

    private CharacterFacing2D charFacing;

    private void Awake()
    {
        charFacing = GetComponent<CharacterFacing2D>();
    }

    public bool IsVisible(GameObject target)
    {
        if (target == null)
        {
            return false;
        }

        // Se estiver fora do visionRange retorna falso
        if (Vector2.Distance(transform.position, target.transform.position) > visionRange)
        {
            return false;
        }

        Vector2 toTarget = target.transform.position - transform.position;
        Vector2 visionDirection = GetVisionDirection();

        if (Vector2.Angle(visionDirection, toTarget) > visionAngle / 2)
        {
            return false;
        }

        // TODO: Checar objetos bloqueando visao

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Vector3 visionDirection = GetVisionDirection();
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, visionAngle/2)*visionDirection * visionRange);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, -visionAngle / 2) * visionDirection * visionRange);
    }

    private Vector2 GetVisionDirection()
    {
        if (charFacing == null)
        {
            return Vector2.right;
        }
        return charFacing.IsFacingRight() ? Vector2.right : Vector2.left;
    }
}
