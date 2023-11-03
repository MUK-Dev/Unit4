using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Transform focalPoint;
    [SerializeField] private float forceFactor = 10;
    [SerializeField] private float speed = 3;
    private Rigidbody rb;
    private GameObject lastTarget;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (gameObject != null)
        {

            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            rb.AddForce(speed * Time.deltaTime * new Vector3(focalPoint.right.x * h, 0, focalPoint.forward.z * v), ForceMode.Impulse);

            if (transform.position.y < -10)
            {
                Destroy(gameObject);
                if (lastTarget != null && lastTarget.TryGetComponent<ScaleIncreaser>(out var lastTargetScaler))
                {
                    lastTargetScaler.Increase();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromThis = other.gameObject.transform.position - transform.position;
            otherRB.AddForce(awayFromThis * forceFactor, ForceMode.Impulse);
            lastTarget = other.gameObject;
        }
    }
}
