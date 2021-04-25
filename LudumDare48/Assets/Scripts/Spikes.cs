using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float fuelToDeplete = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.DepleteFuel(fuelToDeplete);
        }
    }
}
