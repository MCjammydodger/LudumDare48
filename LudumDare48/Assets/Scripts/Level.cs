using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private float width = 80;
    [SerializeField] private float height = 40;
    [SerializeField] private SpriteRenderer leftBorder = null;
    [SerializeField] private SpriteRenderer rightBorder = null;
    [SerializeField] private SpriteRenderer topBorder = null;
    [SerializeField] private SpriteRenderer bottomBorder = null;

    private const float screenWidth = 40;
    private const float screenHeight = 20;

    private void Awake()
    {
        SetupBoundaries();
    }

    
    private void SetupBoundaries()
    {
        float halfWidth = width / 2;
        float halfHeight = height / 2;

        leftBorder.size = new Vector2(screenWidth, height + 20);
        rightBorder.size = new Vector3(screenWidth, height + 20);
        topBorder.size = new Vector3(width + 40, screenHeight);
        bottomBorder.size = new Vector3(width + 40, screenHeight);

        leftBorder.transform.position = new Vector2(-halfWidth - (leftBorder.size.x / 2), halfHeight);
        rightBorder.transform.position = new Vector2(halfWidth + (rightBorder.size.x / 2), halfHeight);
        topBorder.transform.position = new Vector2(0, height + (topBorder.size.y / 2));
        bottomBorder.transform.position = new Vector2(0, -bottomBorder.size.y / 2);
    }
}
