using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackCooldown = 2f;
    public float attackRange = 1.5f;
    public LayerMask playerLayer;
    public Transform attackPoint;

    //private float Tim1e;
    //private float MaxTime = 3f;

    private float nextAttackTime = 1.5f;

    void Update()
    {


        if (Time.time >= nextAttackTime)
        {
            Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

            foreach (Collider player in hitPlayer)
            {
                IDamageable target = player.GetComponent<IDamageable>();
                if (target != null)
                {

                    nextAttackTime = Time.time + attackCooldown;
                    Debug.Log(nextAttackTime);
                    Debug.Log(Time.time);
                    target.TakeDamage(damage);


                    HealthSystem hs = player.GetComponent<HealthSystem>();
                    if (hs != null)
                    {
                        Vector3 dir = player.transform.position - transform.position;
                        hs.ApplyKnockback(dir);
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}