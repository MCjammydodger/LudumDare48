using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class PlanetLevels
{
    public Planets planet = Planets.GreenPlanet;
    public List<Level> levels = null;
}

public class LevelsManager : MonoBehaviour
{
    //[SerializeField] List<Level> levels = new List<Level>();
    [SerializeField] List<PlanetLevels> planetLevels = new List<PlanetLevels>();
    [SerializeField] Level space = null;

    private PlanetLevels currentPlanet = null;
    private Level currentLevel = null;
    private int currentLevelIndex = -1;

    private void Awake()
    {
        // For debug purposes - if the scene is run with a level already in, it will clean it up.
        currentLevel = FindObjectOfType<Level>();
    }

    private void CalculateAltitudes()
    {
        float currentAltitude = 0;
        for(int i = 0; i < currentPlanet.levels.Count; ++i)
        {
            currentPlanet.levels[i].altitude = currentAltitude;
            currentAltitude += currentPlanet.levels[i].GetHeight() + 40;
        }
    }

    public void LoadPlanet(Planets planet)
    {
        for(int i = 0; i < planetLevels.Count; ++i)
        {
            if(planetLevels[i].planet == planet)
            {
                currentPlanet = planetLevels[i];
                CalculateAltitudes();
                return;
            }
        }
        Debug.LogError("Could not find planet: " + planet);
    }

    public Level LoadLevel(int levelIndex)
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        if (levelIndex < 0 || levelIndex >= currentPlanet.levels.Count)
        {
            currentLevel = Instantiate(space);
            currentLevelIndex = -1;
        }
        else
        {
            currentLevel = Instantiate(currentPlanet.levels[levelIndex]);
            currentLevelIndex = levelIndex;
        }
        return currentLevel; 
    }

    public Level LoadNextLevel()
    {
        return LoadLevel(currentLevelIndex + 1);
    }

    public Level LoadPreviousLevel()
    {
        return LoadLevel(currentLevelIndex - 1);
    }

    public Level GetCurrentLevel()
    {
        return currentLevel;
    }

    public Planets GetCurrentPlanet()
    {
        return currentPlanet.planet;
    }

    public bool IsOnLastLevel()
    {
        return currentLevelIndex == currentPlanet.levels.Count - 1;
    }

    public bool IsOnFirstLevel()
    {
        return currentLevelIndex == 0;
    }
}
