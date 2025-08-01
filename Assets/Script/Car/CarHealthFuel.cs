using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHealthFuel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private FloatSO maxHealthSO;
    [SerializeField]
    private FloatSO currentHealthSO;
    [SerializeField]
    private float maxHealthFuel;
    [SerializeField]
    private float healthDrainRate;
    [SerializeField]
    private FloatSO DoDamage;
    [SerializeField]
    private FloatSO HealthGain;
    [SerializeField]
    private BoolSO triggerDeath;

    void Start()
    {
        SetMaxHealth(maxHealthFuel);
        currentHealthSO.Float = maxHealthFuel;
    }

    // Update is called once per frame
    void Update()
    {
        DrainHealth();
        CheckForDamage();
        CheckIfDead();
    }

    private void CheckForDamage()
    {
        if (DoDamage.Float > 0)
        {
            ModifyHealth(DoDamage.Float);
            DoDamage.Float = 0;
        }
        if(HealthGain.Float > 0)
        {
            AddHealth(HealthGain.Float);
            HealthGain.Float = 0;
        }

    }
    public void DrainHealth()
    {
        currentHealthSO.Float -= Time.deltaTime * healthDrainRate;
    }
    public void ModifyHealth(float value)
    {
        currentHealthSO.Float -= value;
        CheckIfDead();
    }

    public void AddHealth(float value)
    {
        currentHealthSO.Float += value;
        if (currentHealthSO.Float > maxHealthFuel)
            currentHealthSO.Float = maxHealthFuel;
        
    }
    private void CheckIfDead()
    {
        if(currentHealthSO.Float < 0)
        {
            triggerDeath.Bool = true;
        }
        else if (triggerDeath.Bool)
        {
            Debug.Log("temporary solution");
            triggerDeath.Bool = false;
        }
    }

    public void SetMaxHealth(float value)
    {
        maxHealthSO.Float = value;
    }
}
