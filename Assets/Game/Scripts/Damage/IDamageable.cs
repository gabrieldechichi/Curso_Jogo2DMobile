using System;

public static class DamageableAnimationKeys
{
    public const string TriggerDead = "Dead";
}
public interface IDamageable
{
    void TakeDamage(int damageAmount);
    bool IsAlive { get; }
    event Action OnDeath;
}
