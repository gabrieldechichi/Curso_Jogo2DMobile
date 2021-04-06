using UnityEngine;

public static class PlayerAnimationKeys
{
    public const string IsAttacking = "IsAttacking";
}

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : CharacterAnimationController
{
    PlayerController playerController;
    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();
    }

    protected override void Update()
    {
        base.Update();
        animator.SetBool(CharacterMovementAnimationKeys.IsCrouching, characterMovement.IsCrouching);
        animator.SetFloat(CharacterMovementAnimationKeys.VerticalSpeed, characterMovement.CurrentVelocity.y / characterMovement.JumpSpeed);
        animator.SetBool(CharacterMovementAnimationKeys.IsGrounded, characterMovement.IsGrounded);

        animator.SetBool(PlayerAnimationKeys.IsAttacking,
            playerController.Weapon != null && playerController.Weapon.IsAttacking);
    }
}
