using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] List<Level> levels = new List<Level>();

    private Level currentLevel = null;
    private int currentLevelIndex = -1;

    private void Awake()
    {
        // For debug purposes - if the scene is run with a level already in, it will clean it up.
        currentLevel = FindObjectOfType<Level>();
        CalculateAltitudes();
    }

    private void CalculateAltitudes()
    {
        float currentAltitude = 0;
        for(int i = 0; i < levels.Count; ++i)
        {
            if(levels[i].ShouldPreviousLevelAdvanceAltitude() && i != 0)
            {
                currentAltitude += levels[i - 1].GetHeight() + 5;
            }
            levels[i].altitude = currentAltitude;
        }
    }

    public Level LoadLevel(int levelIndex)
    {
        if(currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[levelIndex]);
        currentLevelIndex = levelIndex;
        return currentLevel; 
    }

    public Level LoadNextLevel()
    {
        if (!IsOnLastLevel())
        {
            return LoadLevel(currentLevelIndex + 1);
        }
        Debug.LogError("On last level, can't load next level!");
        return currentLevel;
    }

    public Level LoadPreviousLevel()
    {
        if(!IsOnFirstLevel())
        {
            return LoadLevel(currentLevelIndex - 1);
        }
        Debug.LogError("On first level, can't load previous level!");
        return currentLevel;
    }

    public Level GetCurrentLevel()
    {
        return currentLevel;
    }

    public bool IsOnLastLevel()
    {
        return currentLevelIndex == levels.Count - 1;
    }

    public bool IsOnFirstLevel()
    {
        return currentLevelIndex == 0;
    }
}
