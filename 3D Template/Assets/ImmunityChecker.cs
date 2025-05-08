using UnityEngine;

public class ImmunityChecker : MonoBehaviour
{
    public float rayLength = 10.0f;
    public LayerMask collisionLayer;

    public bool OnFoundationCheck;

    public bool OnRoofCheck;

    // Update is called once per frame
    void Update()
    {
        Ray ray1 = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray1, out hit, rayLength, collisionLayer))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green, 2f);
            OnFoundationCheck = true;
        }
        else
        {
            OnFoundationCheck = false;
        }

            Ray ray2 = new Ray(transform.position, Vector3.up);
        if (Physics.Raycast(ray2, out hit, rayLength, collisionLayer))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green, 2f);
            OnRoofCheck = true;
        }
        else
        {
            OnRoofCheck = false;
        }

        if(OnFoundationCheck && OnRoofCheck)
        {
            print("Toxic Immunity += 1");
            PlayerState.Instance.ToxicImmunity = true;
        }
        else
        {
            if(PlayerState.Instance != null)
            {
                PlayerState.Instance.ToxicImmunity = false;
            }
        }
    }
}
