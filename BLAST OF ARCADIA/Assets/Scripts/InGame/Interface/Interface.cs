interface IDamageable
{

    void TakeDemage(float amount);
    void Die();
}
interface ICollectable
{
    void Collect(PlayerController player);
}
