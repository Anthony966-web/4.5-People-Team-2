using UnityEngine;

public class DamageTesting : MonoBehaviour
{
    public AttributesManager PlayerATM;
    public AttributesManager enemyATM;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            PlayerATM.DealDamage(enemyATM.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            enemyATM.DealDamage(PlayerATM.gameObject);
        }
    }
}
