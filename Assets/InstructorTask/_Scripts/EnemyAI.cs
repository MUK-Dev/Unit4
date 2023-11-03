using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private float forceFactor = 5;
    private Rigidbody enemyRB;
    private GameObject closestEnemy;
    private GameObject player;
    private GameObject lastTarget;

    private void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (gameObject != null)
        {
            if (closestEnemy == null)
            {
                FindTarget();
            }

            if (closestEnemy != null)
            {
                Vector3 lookDirection = (closestEnemy.transform.position - transform.position).normalized;
                enemyRB.AddForce(lookDirection * speed * Time.deltaTime, ForceMode.Impulse);

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
    }

    private void FindTarget()
    {
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        if (enemies.Length > 0 && player != null)
        {
            closestEnemy = player;
            foreach (EnemyAI enemy in enemies)
            {
                float distanceFromThisEnemy = Vector3.Distance(transform.position, enemy.gameObject.transform.position);
                float distanceFromCurrentClosestEnemy = Vector3.Distance(transform.position, closestEnemy.transform.position);
                if (distanceFromThisEnemy < distanceFromCurrentClosestEnemy && gameObject != enemy.gameObject)
                {
                    closestEnemy = enemy.gameObject;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            closestEnemy = null;
            Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromThis = other.gameObject.transform.position - transform.position;
            otherRB.AddForce(awayFromThis * forceFactor, ForceMode.Impulse);
            lastTarget = other.gameObject;
        }
    }
}
