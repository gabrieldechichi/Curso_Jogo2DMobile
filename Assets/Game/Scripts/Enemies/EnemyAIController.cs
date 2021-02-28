using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer2D.Character;

[RequireComponent(typeof(CharacterMovement2D))]
[RequireComponent(typeof(CharacterFacing2D))]
public class EnemyAIController : MonoBehaviour
{
    CharacterMovement2D enemyMovement;
    CharacterFacing2D enemyFacing;
    
    private Vector2 movementInput;

    private bool isChasing;

    public bool IsChasing
    {
        get => isChasing;
        set => isChasing = value;
    }

    public Vector2 MovementInput
    {
        get { return movementInput; }
        set { movementInput = new Vector2(Mathf.Clamp(value.x, -1, 1), Mathf.Clamp(value.y, -1, 1)); }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<CharacterMovement2D>();
        enemyFacing = GetComponent<CharacterFacing2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyMovement.ProcessMovementInput(movementInput);
        enemyFacing.UpdateFacing(movementInput);
    }
}
