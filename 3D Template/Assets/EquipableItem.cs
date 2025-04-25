using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EquipableItem : MonoBehaviour
{

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && InventorySystem.Instance.isOpen == false) // Left Mouse Button
        {
            animator.SetTrigger("hit");
        }
    }
}
