using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player = null;

    private const string restartInput = "Restart";

    private Level currentLevel;

    private void Start()
    {
        currentLevel = FindObjectOfType<Level>();
        Debug.Assert(currentLevel, "Level not found!");
        StartLevel();
    }

    private void Update()
    {
        if (Input.GetButtonDown(restartInput))
        {
            StartLevel();
        }
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
}
