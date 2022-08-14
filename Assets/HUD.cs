using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    private static HUD instance;

    [SerializeField] private Menu deathScreen;
    [SerializeField] private Menu winScreen;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public static void OnDied()
    {
        instance.deathScreen.SetVisible(true);
    }

    public static void OnWon()
    {
        instance.winScreen.SetVisible(true);
    }
}
