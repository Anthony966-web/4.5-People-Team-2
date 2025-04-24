using System.Collections;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    
    public static PlayerState Instance { get; set; }

    // ---- Player Health ---- //
    public float currentHealth;
    public float maxHealth;

    // ---- Player Hunger ---- //
    public float currentHunger;
    public float maxHunger;

    float distanceTravelled = 0;
    Vector3 lastPosition;

    public GameObject playerBody;

    // ---- Player Toxic ---- //
    public float currentToxicImmunity;
    public float maxToxicImmunity;

    public float ToxicRate = 0.35f;
    public float ToxicTick = 1.1f;

    public bool ToxicImmunity;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        lastPosition = playerBody.transform.position;

        currentHealth = maxHealth;
        currentHunger = maxHunger;
        currentToxicImmunity = maxToxicImmunity;

        StartCoroutine(DecreaseToxicImmunity());
        StartCoroutine(TickHunger());
    }

    IEnumerator DecreaseToxicImmunity()
    {
        while (true)
        {
            if(ToxicImmunity == false)
            {
                currentToxicImmunity -= ToxicRate;
            }

            yield return new WaitForSeconds(ToxicTick);
        }
    }



    void Update()
    {
        distanceTravelled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        if(distanceTravelled >= 5)
        {
            distanceTravelled = 0;
            currentHunger -= 0.75f;
        }


    }

    IEnumerator TickHunger()
    {
        while(true)
        {
            if (currentHunger <= 0)
            {
                currentHealth -= 3f;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void setHealth(float newHealth)
    {
        currentHealth = newHealth;
    }

    public void setHunger(float newHunger)
    {
        currentHunger = newHunger;
    }
}
