using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private float offsetFromTopAndBottom = 5;
    [SerializeField] private GameManager gameManager = null;

    private float minYPos = 0;
    private float maxYPos = 0;

    private void Awake()
    {
        gameManager.onNewLevelLoaded += OnNewLevelLoaded;
    }

    private void Start()
    {

    }
    private void Update()
    {
        float newYPos = playerTransform.position.y;
        if(newYPos < minYPos)
        {
            newYPos = minYPos;
        }
        if(newYPos > maxYPos)
        {
            newYPos = maxYPos;
        }
        transform.position = new Vector3(playerTransform.position.x, newYPos, transform.position.z);            
    }

    private void OnNewLevelLoaded(Level newLevel)
    {
        Level level = newLevel;
        Debug.Assert(level, "Level not found!");

        minYPos = offsetFromTopAndBottom;
        maxYPos = level.GetHeight() - offsetFromTopAndBottom;
    }
}
