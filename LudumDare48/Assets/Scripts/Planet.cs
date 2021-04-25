using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Planets
{
    GreenPlanet,
    Sun,
    BluePlanet,
    PurplePlanet
}

public class Planet : MonoBehaviour
{
    public Planets planetType = Planets.GreenPlanet;
    public Color colour = Color.white;
}
