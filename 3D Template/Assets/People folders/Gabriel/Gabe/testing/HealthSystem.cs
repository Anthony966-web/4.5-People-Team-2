using UnityEngine;
using UnityEngine.Playables;

public interface IDamageable
{
    void TakeDamage(int damage);
}
public class HealthSystem : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    public int currentHealth;
    public PlayableDirector youdied;
    public GameObject gameover;

    public float knockbackForce = 5f;
    private Rigidbody rb;

    public bool isPlayer = false;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        // Flash red if enemy
        if (!isPlayer)
        {
            EnemyFlashOnHit flash = GetComponent<EnemyFlashOnHit>();
            if (flash != null)
            {
                flash.Flash();
            }
            //ScreenShake shaker = GetComponent<ScreenShake>();
           // if (shaker != null) shaker.Shake(0.5f); // maybe smaller shake
        }
        if (isPlayer)
        {
            PlayerParry parry = GetComponent<PlayerParry>();
            if (parry != null && parry.IsParrying)
            {
                Debug.Log("Attack parried!");
                // You can reflect damage, stun enemy, etc. here
                return;
                //  ScreenShake shaker = GetComponent<ScreenShake>();
                //  if (shaker != null)
                // {
                // shaker.Shake();
                //   }
            }
        }



    }
    public void ApplyKnockback(Vector3 direction)
    {
        if (rb == null) return;
        direction.y = 0; // prevent upward knockback
        rb.linearVelocity = direction.normalized * knockbackForce;
        if (isPlayer)
        {
            //rb.linearVelocity *= knockbackForce;
            rb.gameObject.GetComponent<CharacterMovement>().knobackvelocity = direction.normalized * knockbackForce;
        }
    }
    void Die()
    {
        if (isPlayer)
        {
            Debug.Log("Player died!");
            Destroy(this.gameObject);
            youdied.Play();
            // Disable movement or show respawn screen
        }
        else
        {
            
            Destroy(this.gameObject);
        }
    }
}