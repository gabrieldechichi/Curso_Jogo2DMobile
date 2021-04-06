using System.Collections;
using UnityEngine;

public class MeleeWeapon : TriggerDamage, IWeapon
{
    [SerializeField]
    [Min(0.1f)]
    private float attackTime = 0.1f;

    public bool IsAttacking { get; private set; }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Attack()
    {
        if (!IsAttacking)
        {
            IsAttacking = true;
            gameObject.SetActive(true);
            StartCoroutine(DisableAfterTime());
        }
    }

    private IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(attackTime);
        gameObject.SetActive(false);
        IsAttacking = false;
    }
}
