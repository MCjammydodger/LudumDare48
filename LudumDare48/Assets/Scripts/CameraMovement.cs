using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private float offsetFromSides = 5;
    [SerializeField] private GameManager gameManager = null;

    private float minYPos = 0;
    private float maxYPos = 0;
    private float minXPos = 0;
    private float maxXPos = 0;

    private bool deepSpace = false;

    private Camera cam = null;

    private void Awake()
    {
        gameManager.onNewLevelLoaded += OnNewLevelLoaded;
        cam = GetComponent<Camera>();
    }

    private void Start()
    {

    }
    private void Update()
    {
        float newYPos = playerTransform.position.y;
        float newXPos = playerTransform.position.x;
        if (!deepSpace)
        {
            if (newYPos < minYPos)
            {
                newYPos = minYPos;
            }
            if (newYPos > maxYPos)
            {
                newYPos = maxYPos;
            }
            if(newXPos < minXPos)
            {
                newXPos = minXPos;
            }
            if(newXPos > maxXPos)
            {
                newXPos = maxXPos;
            }
        }
        transform.position = new Vector3(newXPos, newYPos, transform.position.z);            
    }

    private void OnNewLevelLoaded(Level newLevel)
    {
        Level level = newLevel;
        Debug.Assert(level, "Level not found!");

        minYPos = offsetFromSides;
        maxYPos = level.GetHeight() - offsetFromSides;
        minXPos = -(level.GetWidth() / 2) + offsetFromSides;
        maxXPos = (level.GetWidth() / 2) - offsetFromSides;

        deepSpace = level.IsDeepSpace();
        cam.orthographicSize = deepSpace ? 20 : 10;
    }
}
