using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] UIManager uiManager;

    private static Root instance;

    private void Awake()
    {
        instance = this;
    }

    public static UIManager UIManager
    {
        get => instance.uiManager;
    }
}
