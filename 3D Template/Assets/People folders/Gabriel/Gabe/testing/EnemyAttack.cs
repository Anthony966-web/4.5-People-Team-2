using NUnit.Framework.Internal;
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
    public float stunDuration = 3f;
    private bool isStunned = false;
    public float knockbackForce = 10f;
    public GameObject explosion;
    public Camera cam;
    public Animator animator;
    public bool IsAtking;
    
    private void Start()
    {
    }



    //private float Tim1e;
    //private float MaxTime = 3f;

    private float nextAttackTime = 3;

    void Update()
    {
        StartCoroutine(atk());

        //if (Time.time >= nextAttackTime)
        //{
        //    Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
        //    IsAtking = false;
        //    foreach (Collider player in hitPlayer)
        //    {
        //        IDamageable target = player.GetComponent<IDamageable>();
        //        if (target != null)
        //        {

        //            nextAttackTime = Time.time + attackCooldown;
        //            Debug.Log(nextAttackTime);
        //            Debug.Log(Time.time);
        //            PlayerParry parry = player.GetComponent<PlayerParry>();
        //            if (parry != null && parry.IsParrying)
        //            {
        //                Debug.Log("Enemy stunned by parry!");
        //                StartCoroutine(StunRoutine()); 
        //                return;
        //            }
        //            if (isStunned == true)
        //            {
        //                return;
        //            }
        //            IsAtking = true;
        //            target.TakeDamage(damage);
        //            animator.SetBool("IsAtking", IsAtking);
        //            while (knobackvelocity != Vector3.zero)
        //            {
        //                knobackvelocity = Vector3.Lerp(knobackvelocity, Vector3.zero, 1 * Time.deltaTime);
        //                if (knobackvelocity.magnitude < 0.1f)
        //                    knobackvelocity = Vector3.zero;
        //                return;
        //            }

        //            HealthSystem hs = player.GetComponent<HealthSystem>();

        //            if (hs != null)
        //            {
        //                Vector3 dir = player.transform.position - transform.position;
        //                hs.ApplyKnockback(dir);
        //                CharacterMovement.Instance.Knockback();
        //            }
        //        }
        //    }
        //}
        animator.SetBool("IsAtking", IsAtking);
    }
    private System.Collections.IEnumerator atk()
    {
        IsAtking = false;
        if (Time.time >= nextAttackTime)
        {
            Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
          //  IsAtking = false;
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
                        yield break;
                    }
                   
                    IsAtking = true;
                    yield return new WaitForSeconds(1f); 
                    if (isStunned == true)
                    {
                        yield break;
                    }
                    target.TakeDamage(damage);
                    
                    while (knobackvelocity != Vector3.zero)
                    {
                        knobackvelocity = Vector3.Lerp(knobackvelocity, Vector3.zero, 1 * Time.deltaTime);
                        if (knobackvelocity.magnitude < 0.1f)
                            knobackvelocity = Vector3.zero;
                        yield break; 
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
        IsAtking = false;
        test();
        Vector3 dir = this.gameObject.transform.position - transform.position;
        isStunned = true;
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }
    void test()
    {
        Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}