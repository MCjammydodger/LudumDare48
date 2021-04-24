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
                                time = 0.5f}, 
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
        player.RestartPlayer();
        player.transform.position = currentLevel.GetPlayerSpawnPoint().position;
        player.transform.rotation = currentLevel.GetPlayerSpawnPoint().rotation;
    }

    private void StartNewLevel()
    {
        player.SetMaxFuel(currentLevel.GetMaxFuel());
        StartLevel();
        player.PausePlayer();
        Vector3 spawnPos = currentLevel.GetPlayerSpawnPoint().position;
        void OnStartAnimFinished()
        {
            player.ResumePlayer();
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
            StartNewLevel();
            onNewLevelLoaded?.Invoke(currentLevel);
        }
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
