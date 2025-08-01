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
    private float drawDrainRate;
    [SerializeField]
    private BoolSO isDrawing;
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
        DrainHealthOnDraw();
        CheckForDamage();
        CheckIfDead();
    }
    public void DrainHealthOnDraw()
    {
        if (!isDrawing.Bool)
            return;
        currentHealthSO.Float -= Time.deltaTime * drawDrainRate;
        
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
            ModifyHealth(-HealthGain.Float);
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

    private void CheckIfDead()
    {
        if(currentHealthSO.Float < 0)
        {
            triggerDeath.Bool = true;
        }
    }

    public void SetMaxHealth(float value)
    {
        maxHealthSO.Float = value;
    }
}
