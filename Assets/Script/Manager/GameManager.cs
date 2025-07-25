using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance { get; private set; }
    public TimerManager TimerManager { get; private set; }

    #region Unity Functions
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }
    }
    void Start()
    {
        if (Instance != this) return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Utility

    #endregion

    #region Input Action
    
    
    #endregion

#if UNITY_EDITOR
    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        UpdateScripts();
    }
    public static void UpdateScripts()
    {
        string assetPath = "Assets/Prefabs/GameManager.prefab";

        GameObject contentsRoot = PrefabUtility.LoadPrefabContents(assetPath);
        contentsRoot.GetComponentInChildren<GameManager>().SetValues();

        PrefabUtility.SaveAsPrefabAsset(contentsRoot, assetPath);
        PrefabUtility.UnloadPrefabContents(contentsRoot);
    }

    public void SetValues()
    {
        TimerManager = GetComponentInChildren<TimerManager>();
    }
#endif

    public Vector2 CurrentScreenSize()
    {
        return new Vector2(Screen.width, Screen.height);
    }

}
