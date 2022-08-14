using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : Actor
{
    [SerializeField] private int strength;
    public int Strength { get => strength; set => strength = value; }

    public bool SubtractStrength { get; set; }
    public bool EndsGame { get; set; }

    [SerializeField] private TextMeshProUGUI textbox;

    private void Start()
    {
        textbox.text = Strength.ToString();
    }

    public override void Explode()
    {
        base.Explode();

        if (EndsGame)
        {
            HUD.OnWon();
        }
    }
}
