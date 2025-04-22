using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage = 25;
    public float attackRange = 2f;
    public LayerMask enemyLayer;
    public Transform attackPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Left-click
        {
            Attack();
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            IDamageable target = enemy.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
