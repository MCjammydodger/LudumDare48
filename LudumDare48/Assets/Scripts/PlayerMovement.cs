using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float engineForce = 10;

    private const string engineInput = "Jump";
    private Rigidbody2D mRigidbody = null;
    private Vector2 mForceToApply = Vector2.zero;

    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
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
            mForceToApply = engineForce * transform.up;
        }
        else
        {
            mForceToApply = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        mRigidbody.AddForce(mForceToApply * Time.fixedDeltaTime, ForceMode2D.Force);
    }
}
