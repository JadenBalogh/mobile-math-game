using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : Actor
{
    [SerializeField] private int strength;
    public int Strength { get => strength; set => strength = value; }

    [SerializeField] private TextMeshProUGUI textbox;

    private void Start()
    {
        textbox.text = Strength.ToString();
    }
}
