using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player = null;
    [SerializeField] private LevelsManager levelsManager = null;
    [SerializeField] private UIManager uiManager = null;
    [SerializeField] private SpriteRenderer background = null;
    [SerializeField] private bool testCurrentLevelInScene = false;

    public UnityAction<Level> onNewLevelLoaded;

    private const string restartInput = "Restart";

    private Level currentLevel;
    private Lerp playerLerp;

    public void FinishedLevel()
    {
        player.PausePlayer();
        void OnFinishAnimComplete()
        {
            player.ResumePlayer();
            LoadNextLevel();
        }
        playerLerp.DoLerp(player.transform.position, player.transform.position + (Vector3.up * 20), 1, OnFinishAnimComplete);
    }

    private void Start()
    {
        playerLerp = player.GetComponent<Lerp>();
        currentLevel = levelsManager.GetCurrentLevel();
#if !UNITY_EDITOR
        testCurrentLevelInScene = false;
#endif
        if (testCurrentLevelInScene)
        {
            StartLevel();
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
        uiManager.UpdateAltitude(player.transform.position.y + currentLevel.altitude);
        uiManager.UpdateFuelLevel(player.GetFuelLevel(), player.GetMaxFuel());
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
        player.PausePlayer();
        Vector3 spawnPos = currentLevel.GetPlayerSpawnPoint().position;
        void OnStartAnimFinished()
        {
            player.ResumePlayer();
            player.RestartPlayer();
            player.transform.position = spawnPos;
            player.transform.rotation = currentLevel.GetPlayerSpawnPoint().rotation;
        }
        if (!levelsManager.IsOnFirstLevel())
        {
            playerLerp.DoLerp(spawnPos - (Vector3.up * 20), spawnPos, 1, OnStartAnimFinished);
        }
        else
        {
            OnStartAnimFinished();
        }
    }

    private void StartLevel()
    {
        background.sprite = currentLevel.GetBackground();
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
