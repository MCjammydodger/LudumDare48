using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLocator : MonoBehaviour
{
    [SerializeField] private Planet[] planets = null;
    [SerializeField] private Transform pointerPrefab = null;

    private Transform player;
    private Transform[] pointers;

    public Planet FindPlanetOfType(Planets planetType)
    {
        for(int i = 0; i < planets.Length; ++i)
        {
            if(planets[i].planetType == planetType)
            {
                return planets[i];
            }
        }
        return null;
    }

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().transform;

        pointers = new Transform[planets.Length];

        for(int i = 0; i < planets.Length; ++i)
        {
            pointers[i] = Instantiate(pointerPrefab, transform);

        }
    }

    private void Update()
    {
        for(int i = 0; i < planets.Length; ++i)
        {
            if (!Progress.HasCompletedPlanet(planets[i].planetType) && (planets[i].planetType != Planets.Sun || Progress.HasCompletedAllPlanets()))
            {
                Transform planet = planets[i].transform;
                Transform pointer = pointers[i];
                pointer.GetComponent<SpriteRenderer>().color = planets[i].colour;
                pointer.position = player.position;
                Vector3 direction = planet.position - player.position;
                pointer.gameObject.SetActive(direction.sqrMagnitude > 20);
                pointer.position = player.position + (direction.normalized * 20f);
                Vector3 relative = pointer.InverseTransformPoint(planet.position);
                float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
                pointer.Rotate(0, 0, -angle);
            }
            else
            {
                pointers[i].gameObject.SetActive(false);
            }
        }
    }
}
