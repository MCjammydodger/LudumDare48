using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] private float forceToApply = 10000;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.transform.parent.parent.GetComponent<Rigidbody2D>();
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.angularVelocity = 0;
            playerRigidbody.transform.position = transform.position;
            playerRigidbody.transform.rotation = transform.rotation;
            playerRigidbody.AddForce(forceToApply * transform.up, ForceMode2D.Impulse);
        }
    }
}
