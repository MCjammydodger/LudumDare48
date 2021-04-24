using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Planets
{
    GreenPlanet
}

public class Planet : MonoBehaviour
{
    public Planets planetType = Planets.GreenPlanet;
    public Color colour = Color.white;
}
