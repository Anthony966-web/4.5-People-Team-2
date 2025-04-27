using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    public float parryWindow = 0.3f; // Time the parry is active
    public float parryCooldown = 1f; // Time before you can parry again
    public KeyCode parryKey = KeyCode.F;
    private bool isParrying = false;
    private bool canParry = true;
    public bool IsParrying => isParrying;
    void Update()
    {
        if (Input.GetKeyDown(parryKey) && canParry)
        {
            StartCoroutine(ParryRoutine());
        }
    }
    private System.Collections.IEnumerator ParryRoutine()
    {
        isParrying = true;
        canParry = false;
        Debug.Log("Parry active!");
        yield return new WaitForSeconds(parryWindow);
        isParrying = false;
        Debug.Log("Parry ended.");
        yield return new WaitForSeconds(parryCooldown);
        canParry = true;
    }
}
