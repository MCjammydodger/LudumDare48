using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float engineForce = 10;
    [SerializeField] private float rotateSpeed = 30;
    [SerializeField] private float fuelPerSecond = 5;
    [SerializeField] private GameObject flameEffect = null;
    [SerializeField] private Collider2D playerCollider = null;
    [SerializeField] private GameManager gameManager = null;

    private const string engineInput = "Jump";
    private const string rotateInput = "Horizontal";
    private Rigidbody2D rigidBody = null;
    private Vector2 forceToApply = Vector2.zero;
    private float currentFuel = 0;
    private float maxFuel = 0;
    private bool paused = false;
    private float gravityMultiplier = 0;
    private bool deepSpace = false;
    private Planet currentPlanet = null;

    public float GetVelocity()
    {
        return rigidBody.velocity.magnitude;
    }

    public void SetDeepSpace(bool isDeepSpace)
    {
        deepSpace = isDeepSpace;
    }

    public void SetMaxFuel(float fuel)
    {
        maxFuel = fuel;
    }

    public void SetGravityMultiplier(float multiplier)
    {
        gravityMultiplier = multiplier;
        rigidBody.gravityScale = multiplier;
    }

    public void PausePlayer()
    {
        rigidBody.isKinematic = true;
        playerCollider.enabled = false;
        paused = true;
    }

    public void ResumePlayer()
    {
        rigidBody.isKinematic = false;
        playerCollider.enabled = true;
        paused = false;
    }

    public void RestartPlayer()
    {
        rigidBody.isKinematic = true;
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0;
        rigidBody.isKinematic = false;
        currentFuel = maxFuel;
    }
    public float GetMaxFuel()
    {
        return maxFuel;
    }

    public float GetFuelLevel()
    {
        return currentFuel;
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        flameEffect.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (paused) return;

        if(Input.GetButton(engineInput) && currentFuel > 0)
        {
            forceToApply = engineForce * transform.up;
            flameEffect.SetActive(true);
            currentFuel -= fuelPerSecond * Time.deltaTime;
        }
        else
        {
            forceToApply = Vector2.zero;
            flameEffect.SetActive(false);
        }
        transform.Rotate(0, 0, -Input.GetAxis(rotateInput) * rotateSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (paused) return;
        rigidBody.AddForce(forceToApply * Time.fixedDeltaTime, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Finish"))
        {
            gameManager.FinishedLevel();
        }
        if(collision.gameObject.CompareTag("Planet") && currentPlanet == null)
        {
            currentPlanet = collision.GetComponent<Planet>();
            gameManager.AtPlanet(currentPlanet);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            currentPlanet = null;
        }
    }
}
