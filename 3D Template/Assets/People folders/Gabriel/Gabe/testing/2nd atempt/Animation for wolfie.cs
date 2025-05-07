using UnityEngine;
using UnityEngine.Splines;

public class Animationforwolfie : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animator;
    private bool IsMoving;
    private float startposition;

    void Start()
    {

    }
    private Vector3 oldposition;
    private Vector3 newposition;

    // Update is called once per frame
    void Update()
    {
        newposition = transform.position;
        Animate();
        startposition = Vector3.Distance(newposition, oldposition);
        oldposition = transform.position;
    }
    private void Animate()
    {
        
        if (newposition != oldposition) //|| input.magnitude < -0.1f)
        {
            IsMoving = true;
        }
        else { IsMoving = false; }
        animator.SetBool("IsMoving", IsMoving);

    }
}
