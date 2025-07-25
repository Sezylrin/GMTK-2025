using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioManager AudioManager;
    void Start()
    {
        AudioManager.PlaySound(AudioRef.Example);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
