/*
* Hierarquia
*/

public class Pokemon : IDamageable
{
    public void BattleCry()
    {
        // Toca audio
    }

    public virtual void TakeDamage(float damageAmount)
    {
        // Diminuir a health do pokmemon
        // Se 0 health play o DefeatCry
        // 
    }
}

public class WildPokemon : Pokemon
{
    public void Flee()
    {

    }
    public bool TryCapture()
    {
        return false;
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        //
    }
}

public class TrainerPokemon : Pokemon
{

}

/*
 Polimorfismo
*/

public interface IDamageable
{
    void TakeDamage(float damageAmount);
}

public class Tree : IDamageable
{
    public void TakeDamage(float damageAmount)
    {
        
    }
}

public class Rock : IDamageable
{
    public void TakeDamage(float damageAmount)
    {
        
    }
}

public class Weapon
{
    public void Attack(IDamageable damageable)
    {
        damageable.TakeDamage(10.0f);
    }
}