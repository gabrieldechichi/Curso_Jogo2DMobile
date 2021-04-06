using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;

public static class CharacterMovementAnimationKeys
{
    public const string IsCrouching = "IsCrouching";
    public const string HorizontalSpeed = "HorizontalSpeed";
    public const string VerticalSpeed = "VerticalSpeed";
    public const string IsGrounded = "IsGrounded";
}

public static class EnemyAnimationKeys
{
    public const string IsChasing = "IsChasing";
}

public class CharacterAnimationController : MonoBehaviour
{
    protected Animator animator;
    protected CharacterMovement2D characterMovement;

    private IDamageable damageable;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement2D>();

        damageable = GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.OnDeath += OnDeath;
        }
    }

    private void OnDestroy()
    {
        if (damageable != null)
        {
            damageable.OnDeath -= OnDeath;
        }
    }

    protected virtual void Update()
    {
        animator.SetFloat(CharacterMovementAnimationKeys.HorizontalSpeed, characterMovement.CurrentVelocity.x / characterMovement.MaxGroundSpeed);
    }

    private void OnDeath()
    {
        animator.SetTrigger(DamageableAnimationKeys.TriggerDead);
    }
}
