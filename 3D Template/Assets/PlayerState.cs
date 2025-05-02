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

    public float HungerDown = 0.75f;

    float distanceTravelled = 0;
    Vector3 lastPosition;

    public bool CanRun;

    public GameObject playerBody;

    // ---- Player Toxic ---- //
    public float currentToxicImmunity;
    public float maxToxicImmunity;

    public float ToxicRate = 0.35f;
    public float ToxicTick = 1.1f;

    public bool ToxicImmunity;

    // ---- Player Currancy ---- //

    public float Money = 100;

    public bool SpendMoney(float Amount)
    {
        if (Money >= Amount)
        {
            Money -= Amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddMoney(float Amount)
    {
        Money += Amount;
    }

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


    void Update()
    {
        distanceTravelled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        if(distanceTravelled >= 5)
        {
            distanceTravelled = 0;
            if(currentHunger >= HungerDown)
            {
                currentHunger -= HungerDown;
            }
            else
            {
                currentHunger = 0;
            }
            
        }


    }

    IEnumerator DecreaseToxicImmunity()
    {
        while (true)
        {
            if (ToxicImmunity == false)
            {
                if (currentToxicImmunity >= ToxicRate)
                {
                    currentToxicImmunity -= ToxicRate;
                }
                else
                {
                    currentToxicImmunity = 0;
                }

                if (currentToxicImmunity == 0)
                {
                    currentHealth -= ToxicRate * 3;
                }
            }
            else
            {
                float ToxicPlus = ToxicRate * 5;
                if (currentToxicImmunity + ToxicPlus <= maxToxicImmunity)
                {
                    currentToxicImmunity += ToxicPlus;
                }
                else
                {
                    currentToxicImmunity = maxToxicImmunity;
                }
               
            }

            yield return new WaitForSeconds(ToxicTick);
        }
    }

    IEnumerator TickHunger()
    {
        while(true)
        {
            if (currentHunger <= 0)
            {
                CanRun = false;
                currentHealth -= ToxicRate * 3;
            }
            else
            {
                CanRun = true;
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
