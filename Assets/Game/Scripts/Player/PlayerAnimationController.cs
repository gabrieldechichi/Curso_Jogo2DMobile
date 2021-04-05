using UnityEngine;

[RequireComponent(typeof(IDamageable))]
public class PlayerAnimationController : CharacterAnimationController
{
    private IDamageable damageable;

    protected override void Awake()
    {
        base.Awake();
        damageable = GetComponent<IDamageable>();
        damageable.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        damageable.OnDeath -= OnDeath;
    }

    protected override void Update()
    {
        base.Update();
        animator.SetBool(CharacterMovementAnimationKeys.IsCrouching, characterMovement.IsCrouching);
        animator.SetFloat(CharacterMovementAnimationKeys.VerticalSpeed, characterMovement.CurrentVelocity.y / characterMovement.JumpSpeed);
        animator.SetBool(CharacterMovementAnimationKeys.IsGrounded, characterMovement.IsGrounded);
    }

    private void OnDeath()
    {
        animator.SetTrigger(DamageableAnimationKeys.TriggerDead);
    }
}
