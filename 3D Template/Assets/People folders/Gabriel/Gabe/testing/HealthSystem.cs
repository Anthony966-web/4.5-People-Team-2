using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
}
public class HealthSystem : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool isPlayer = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
        // Flash red if enemy
        if (!isPlayer)
        {
            EnemyFlashOnHit flash = GetComponent<EnemyFlashOnHit>();
            if (flash != null)
            {
                flash.Flash();
            }
            ScreenShake shaker = GetComponent<ScreenShake>();
            if (shaker != null) shaker.Shake(0.5f); // maybe smaller shake
        }
        if (isPlayer)
        {
            ScreenShake shaker = GetComponent<ScreenShake>();
            if (shaker != null)
            {
                shaker.Shake();
            }
        }

    }

    void Die()
    {
        if (isPlayer)
        {
            Debug.Log("Player died!");
            // Disable movement or show respawn screen
        }
        else
        {
            Destroy(gameObject); // Enemy dies
        }
    }
}