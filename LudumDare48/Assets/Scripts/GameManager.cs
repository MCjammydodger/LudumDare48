﻿using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player = null;
    [SerializeField] private LevelsManager levelsManager = null;
    [SerializeField] private UIManager uiManager = null;
    [SerializeField] private SpriteRenderer background = null;
    [SerializeField] private bool testCurrentLevelInScene = false;
    [SerializeField] private Planets startingPlanet = Planets.GreenPlanet;

    public UnityAction<Level> onNewLevelLoaded;

    private const string restartInput = "Restart";

    private Level currentLevel;
    private Lerp playerLerp;

    private Planet currentPlanet = null;

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void AtPlanet(Planet planet)
    {
        currentPlanet = planet;
        PauseGame();
        uiManager.ShowPlanetPanel(PlanetPanelResponse);
    }

    public void PlanetPanelResponse(bool teleport)
    {
        player.RestartPlayer();
        ResumeGame();
        if(teleport)
        {
            levelsManager.LoadPlanet(currentPlanet.planetType);
            currentPlanet = null;
            LoadNextLevel();
        }
        else
        {
            currentPlanet = null;
        }
    }

    public void FinishedLevel()
    {
        player.PausePlayer();
        void OnFinishAnimComplete()
        {
            player.ResumePlayer();
            if (testCurrentLevelInScene)
            {
                StartLevel();
            }
            else
            {
                LoadNextLevel();
            }
        }
        Lerp.LerpTo[] lerpTos = { 
            new Lerp.LerpTo() { toVec = currentLevel.GetFinishTransform().position, 
                                time = 0.1f}, 
            new Lerp.LerpTo() { toVec = currentLevel.GetFinishTransform().position + (Vector3.up * 20),
                                time = 0.5f} };
        playerLerp.DoLerp(player.transform.position, lerpTos, OnFinishAnimComplete);
    }

    private void Start()
    {
        playerLerp = player.GetComponent<Lerp>();
        currentLevel = levelsManager.GetCurrentLevel();
#if !UNITY_EDITOR
        testCurrentLevelInScene = false;
#endif
        levelsManager.LoadPlanet(startingPlanet);
        if (testCurrentLevelInScene)
        {
            StartNewLevel();
            onNewLevelLoaded?.Invoke(currentLevel);
        }
        else
        {
            LoadNextLevel();
        }
        Debug.Assert(currentLevel, "Level not found!");
    }

    private void Update()
    {
        if (Input.GetButtonDown(restartInput))
        {
            StartLevel();
        }
        if (currentLevel.IsDeepSpace())
        {
            uiManager.UpdateVelocity(player.GetVelocity());
        }
        else
        {
            uiManager.UpdateAltitude(player.transform.position.y + currentLevel.altitude);
        }
        uiManager.UpdateFuelLevel(player.GetFuelLevel(), player.GetMaxFuel());
        uiManager.UpdateGravityLevel(currentLevel.GetGravityMultiplier());
#if DEBUG
        if (!currentLevel.IsDeepSpace())
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                LoadNextLevel();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                LoadPreviousLevel();
            }
        }
#endif
    }

    private void SpawnPlayer()
    {
        player.RestartPlayer();
        Transform spawnPoint;
        if(currentLevel.IsDeepSpace())
        {
            Planet planet = FindObjectOfType<PlanetLocator>().FindPlanetOfType(levelsManager.GetCurrentPlanet());
            spawnPoint = planet.transform;
            player.SetCurrentPlanet(planet);
        }
        else
        {
            spawnPoint = currentLevel.GetPlayerSpawnPoint();
        }
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
    }

    private void StartNewLevel()
    {
        player.SetMaxFuel(currentLevel.GetMaxFuel());
        player.SetGravityMultiplier(currentLevel.GetGravityMultiplier());
        player.SetDeepSpace(currentLevel.IsDeepSpace());
        StartLevel();
        if (!levelsManager.IsOnFirstLevel() && !currentLevel.IsDeepSpace())
        {
            player.PausePlayer();
            Vector3 spawnPos = currentLevel.GetPlayerSpawnPoint().position;
            void OnStartAnimFinished()
            {
                player.ResumePlayer();
            }
            playerLerp.DoLerp(spawnPos - (Vector3.up * 20), spawnPos, 1, OnStartAnimFinished);
        }
    }

    private void StartLevel()
    {
        background.sprite = currentLevel.GetBackground();
        SpawnPlayer();
    }

    private void LoadNextLevel()
    {
        currentLevel = levelsManager.LoadNextLevel();
        StartNewLevel();
        onNewLevelLoaded?.Invoke(currentLevel);
    }

    private void LoadPreviousLevel()
    {
        if(!levelsManager.IsOnFirstLevel())
        {
            currentLevel = levelsManager.LoadPreviousLevel();
            StartNewLevel();
            onNewLevelLoaded?.Invoke(currentLevel);
        }
    }
}
