using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;

[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    CharacterMovement2D playerMovement;
    SpriteRenderer spriteRenderer;
    PlayerInput playerInput;

    public Sprite crouchedSprite;
    public Sprite idleSprite;

    [Header("Camera")]
    public Transform cameraTarget;
    [Range(0, 5)]
    public float cameraTargetOffsetX;
    [Range(0.5f, 5.0f)]
    public float cameraTargetFlipSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<CharacterMovement2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimentacao
        Vector2 movementInput = playerInput.GetMovementInput();
        playerMovement.ProcessMovementInput(movementInput);

        if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        bool isFacingRight = spriteRenderer.flipX == false;
        float offsetTargetX = isFacingRight ? cameraTargetOffsetX : -cameraTargetOffsetX;
        Vector3 cameraTargetLocalPosition = new Vector3(
            offsetTargetX, 
            cameraTarget.localPosition.y, 
            cameraTarget.localPosition.z);

        cameraTarget.localPosition = Vector3.Lerp(
            cameraTarget.localPosition, 
            cameraTargetLocalPosition, 
            Time.deltaTime * cameraTargetFlipSpeed);

        //Pulo
        if (playerInput.IsJumpButtonDown())
        {
            playerMovement.Jump();
        }
        if (playerInput.IsJumpButtonHeld() == false)
        {
            playerMovement.AbortJump();
        }

        //Agachar
        if (playerInput.IsCrouchButtonDown())
        {
            playerMovement.Crouch();

            //TODO: Remover quando adicionarmos animacao
            spriteRenderer.sprite = crouchedSprite;
            //
        }
        else if (playerInput.IsCrouchButtonUp())
        {
            playerMovement.UnCrouch();

            //TODO: Remover quando adicionarmos animacao
            spriteRenderer.sprite = idleSprite;
            //
        }
    }
}
