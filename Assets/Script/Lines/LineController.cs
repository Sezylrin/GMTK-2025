using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Texture[] textures;

    private int animationStep;

    [SerializeField]
    private int fps;

    private float fpsStep;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fpsStep += Time.deltaTime;
        if (fpsStep >= 1f / fps)
        {
            animationStep++;
            if(animationStep == textures.Length)
                animationStep = 0;
            lineRenderer.material.SetTexture("_BaseMap", textures[animationStep]);
            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

            fpsStep = 0f;
        }
    }
}
