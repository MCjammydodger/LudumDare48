using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    [SerializeField] private bool deepSpace = false;
    [SerializeField] private float width = 80;
    [SerializeField] private float height = 40;
    [SerializeField] private float maxFuel = 100;
    [SerializeField] private float gravityMultiplier = 1;
    [SerializeField] private Sprite background = null;
    [SerializeField] private SpriteRenderer leftBorder = null;
    [SerializeField] private SpriteRenderer rightBorder = null;
    [SerializeField] private SpriteRenderer topBorder = null;
    [SerializeField] private SpriteRenderer bottomBorder = null;
    [SerializeField] private Transform playerSpawnPoint = null;
    [SerializeField] private Transform finishTransform = null;
    [SerializeField] private Dialogue[] levelDialogue = null;

    [HideInInspector] public float altitude = 0;

    private const float screenWidth = 40;
    private const float screenHeight = 20;

    public bool IsDeepSpace()
    {
        return deepSpace;
    }

    public float GetWidth()
    {
        return width;
    }

    public float GetHeight()
    {
        return height;
    }

    public float GetMaxFuel()
    {
        return maxFuel;
    }

    public float GetGravityMultiplier()
    {
        return gravityMultiplier;
    }

    public Transform GetPlayerSpawnPoint()
    {
        return playerSpawnPoint;
    }

    public Transform GetFinishTransform()
    {
        return finishTransform;
    }

    public Dialogue[] GetDialogue()
    {
        return levelDialogue;
    }

    private void Awake()
    {
        SetupBoundaries();
    }

    
    public void SetupBoundaries()
    {
        float halfWidth = width / 2;
        float halfHeight = height / 2;
        float halfScreenWidth = screenWidth / 2;
        float halfScreenHeight = screenHeight / 2;

        leftBorder.size = new Vector2(halfScreenWidth, height + halfScreenHeight);
        rightBorder.size = new Vector3(halfScreenWidth, height + halfScreenHeight);
        topBorder.size = new Vector3(width + halfScreenWidth, halfScreenHeight);
        bottomBorder.size = new Vector3(width + halfScreenWidth, halfScreenHeight);

        leftBorder.transform.position = new Vector2(-halfWidth - (leftBorder.size.x / 2), halfHeight);
        rightBorder.transform.position = new Vector2(halfWidth + (rightBorder.size.x / 2), halfHeight);
        topBorder.transform.position = new Vector2(0, height + (topBorder.size.y / 2));
        bottomBorder.transform.position = new Vector2(0, -bottomBorder.size.y / 2);
    }

    public Sprite GetBackground()
    {
        return background;
    }
}
