using UnityEngine;
public interface IEnemy
{
    float _health { get; set; }
    float _maxHealth { get; set; }
    float _atackDamage { get; set; }
    float _speed { get; set; }
    float _isAlive { get; set; }


    public void TakeDamage(float damage);

    public void MoveTo(Vector3 destination);

    public void Attack(Vector3 destination);

    public void Die();

}
