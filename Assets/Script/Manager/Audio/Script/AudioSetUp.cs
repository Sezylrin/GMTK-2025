using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioSetUp : MonoBehaviour
{
    [Header("File Path to where AudioClipSO are Stored. Blank for default")]
    public string AudioClipSOLoc;

    public static string AudioRefPath;
    public static string SoundLibPath;
    public static string AudioClipSOPath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RunSetup()
    {
        string[] guid = AssetDatabase.FindAssets("t:script AudioRef");
        string audioRef = AssetDatabase.GUIDToAssetPath(guid[0]);
        guid = AssetDatabase.FindAssets("t:LoadedSoundDict");
        string soundLib = AssetDatabase.GUIDToAssetPath(guid[0]);
        
        AudioRefPath = audioRef.Replace("/AudioRef.cs", "");
        SoundLibPath = soundLib.Replace("/LoadedSoundDictionary.asset", "");
        if (AudioClipSOLoc.Equals(""))
        {
            guid = AssetDatabase.FindAssets("t:AudioClipSO Example");
            string ClipPath = AssetDatabase.GUIDToAssetPath(guid[0]);
            AudioClipSOPath = ClipPath.Replace("/Example.asset", "");
        }
        else
        {
            AudioClipSOPath = AudioClipSOLoc;
        }
        
    }
}
