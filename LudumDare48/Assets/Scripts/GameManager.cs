using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player = null;
    [SerializeField] private LevelsManager levelsManager = null;
    [SerializeField] private UIManager uiManager = null;
    [SerializeField] private bool testCurrentLevelInScene = false;

    public UnityAction<Level> onNewLevelLoaded;

    private const string restartInput = "Restart";

    private Level currentLevel;

    public void FinishedLevel()
    {
        LoadNextLevel();
    }

    private void Start()
    {
        currentLevel = levelsManager.GetCurrentLevel();
#if !UNITY_EDITOR
        testCurrentLevelInScene = false;
#endif
        if (testCurrentLevelInScene)
        {
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
        uiManager.UpdateAltitude(player.transform.position.y);
#if DEBUG
        if(Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            LoadPreviousLevel();
        }
#endif
    }

    private void SpawnPlayer()
    {
        player.RestartPlayer();
        player.transform.position = currentLevel.GetPlayerSpawnPoint().position;
        player.transform.rotation = currentLevel.GetPlayerSpawnPoint().rotation;
    }

    private void StartLevel()
    {
        SpawnPlayer();
    }

    private void LoadNextLevel()
    {
        if (!levelsManager.IsOnLastLevel())
        {
            currentLevel = levelsManager.LoadNextLevel();
            StartLevel();
            onNewLevelLoaded?.Invoke(currentLevel);
        }
    }

    private void LoadPreviousLevel()
    {
        if(!levelsManager.IsOnFirstLevel())
        {
            currentLevel = levelsManager.LoadPreviousLevel();
            StartLevel();
            onNewLevelLoaded?.Invoke(currentLevel);
        }
    }
}
