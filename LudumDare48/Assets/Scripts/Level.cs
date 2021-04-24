using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private float width = 80;
    [SerializeField] private float height = 40;
    [SerializeField] private Sprite background = null;
    [SerializeField] private SpriteRenderer leftBorder = null;
    [SerializeField] private SpriteRenderer rightBorder = null;
    [SerializeField] private SpriteRenderer topBorder = null;
    [SerializeField] private SpriteRenderer bottomBorder = null;
    [SerializeField] private Transform playerSpawnPoint = null;

    [HideInInspector] public float altitude = 0;

    private const float screenWidth = 40;
    private const float screenHeight = 20;

    public float GetWidth()
    {
        return width;
    }

    public float GetHeight()
    {
        return height;
    }

    public Transform GetPlayerSpawnPoint()
    {
        return playerSpawnPoint;
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
