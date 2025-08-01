using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Meteor : MonoBehaviour
{
    [SerializeField]
    private LayerMask player;
    private float anticipation;
    private VisualEffect effect;
    private bool effectPlayed = false;
    [SerializeField]
    private FloatSO damageToDO;
    private float damage;

    private bool doDamage = false;
    // Start is called before the first frame update
    public void init(Vector3 spawnPos,float damage)
    {
        effect = gameObject.GetComponent<VisualEffect>();
        anticipation = effect.GetFloat("Anticipation");
        effect.SetVector3("MeteorSpawn", spawnPos);
        effect.Play();
        this.damage = damage;
        DOTween.To(() => anticipation, x => anticipation = x, 0f, anticipation);
    }

    // Update is called once per frame
    void Update()
    {
        if (effect.aliveParticleCount > 0 && !effectPlayed)
        {
            effectPlayed = true;
        }

        if (effect.aliveParticleCount == 0 && effectPlayed)
        {
            Destroy(gameObject);
        }
        if(anticipation <= 0 && !doDamage)
        {
            if(Physics.OverlapSphereNonAlloc(transform.position, transform.localScale.x * 0.5f, new Collider[1], player) == 1)
            {
                damageToDO.Float += damage;
                doDamage = true;
                
            }
        }
    }
}
