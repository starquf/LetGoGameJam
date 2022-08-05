using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEffect : Effect
{
    public void SetColor(Color color1, Color color2)
    {
        var main = _particleSystem.main;

        main.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
    }
}
