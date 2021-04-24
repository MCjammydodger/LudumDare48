using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float engineForce = 10;
    [SerializeField] private float rotateSpeed = 30;

    private const string engineInput = "Jump";
    private const string rotateInput = "Horizontal";
    private Rigidbody2D rigidBody = null;
    private Vector2 forceToApply = Vector2.zero;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton(engineInput))
        {
            forceToApply = engineForce * transform.up;
        }
        else
        {
            forceToApply = Vector2.zero;
        }

        transform.Rotate(0, 0, -Input.GetAxis(rotateInput) * rotateSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(forceToApply * Time.fixedDeltaTime, ForceMode2D.Force);
    }
}
