using System;

public interface IHealth
{
    float Health { get; }
    float MaxHealth { get; }

    public event Action OnDie;
    public event Action<float> OnTakeDamage;
    public void TakeDamage(float damage);
}
