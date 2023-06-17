using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    [SerializeField] SaveManager saveManager;
    private PlayerController playerReference;

    private static Root instance;

    private void Awake()
    {
        instance = this;
        saveOnExit = true;
    }

    public static bool saveOnExit;

    public static UIManager UIManager
    {
        get => instance.uiManager;
    }
    public static SaveManager SaveManager
    {
        get => instance.saveManager;
    }

    public static PlayerController PlayerReference
    {
        get => instance.playerReference;
        set => instance.playerReference = value;
    }
}
