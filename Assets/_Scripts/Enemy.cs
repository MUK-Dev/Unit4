using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private GameObject player;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        rb.AddForce(lookDirection * speed);

        if (transform.position.y < -10) Destroy(gameObject);
    }

}
