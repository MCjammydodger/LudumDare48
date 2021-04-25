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

    private Collider2D col = null;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        if(Progress.HasCompletedPlanet(planetType))
        {
            col.enabled = false;
        }
    }
}
