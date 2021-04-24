using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] private float forceToApply = 10000;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player trigger");
            collision.transform.parent.parent.GetComponent<Rigidbody2D>().AddForce(forceToApply * transform.up, ForceMode2D.Impulse);
        }
    }
}
