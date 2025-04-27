using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackCooldown = 2f;
    public float attackRange = 1.5f;
    public LayerMask playerLayer;
    public Transform attackPoint;
    public Vector3 knobackvelocity;
    public float stunDuration = 1f;
    private bool isStunned = false;
    public float knockbackForce = 10f;
    private Rigidbody rb;

    private void Start()
    {
        rb.gameObject.GetComponent<Rigidbody>();
    }



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
                    PlayerParry parry = player.GetComponent<PlayerParry>();
                    if (parry != null && parry.IsParrying)
                    {
                        Debug.Log("Enemy stunned by parry!");
                        StartCoroutine(StunRoutine()); 
                        return;
                    }
                    target.TakeDamage(damage);
                    while (knobackvelocity != Vector3.zero)
                    {
                        knobackvelocity = Vector3.Lerp(knobackvelocity, Vector3.zero, 1 * Time.deltaTime);
                        if (knobackvelocity.magnitude < 0.1f)
                            knobackvelocity = Vector3.zero;
                        return;
                    }

                    HealthSystem hs = player.GetComponent<HealthSystem>();

                    if (hs != null)
                    {
                        Vector3 dir = player.transform.position - transform.position;
                        hs.ApplyKnockback(dir);
                        CharacterMovement.Instance.Knockback();
                    }
                }
            }
        }
    }
    private System.Collections.IEnumerator StunRoutine()
    {
        Vector3 dir = this.gameObject.transform.position - transform.position;
        isStunned = true;
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}