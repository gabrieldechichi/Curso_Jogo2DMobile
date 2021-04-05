using System;
using UnityEngine;

public class DeathOnDamage : MonoBehaviour, IDamageable
{
    public event Action OnDeath;

    public bool IsAlive { get; private set; } = true;

    public void TakeDamage(int damage)
    {
        Die();
    }

    private void Die()
    {
        if (IsAlive)
        {
            IsAlive = false;
            OnDeath?.Invoke();
        }
    }
}
