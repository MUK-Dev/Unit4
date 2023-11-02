using UnityEngine;

public class HomingRocket : MonoBehaviour
{
    [SerializeField] private float forceFactor = 30;
    [SerializeField] private float speed = 20f;

    private GameObject closestEnemy;

    private void Update()
    {
        if (closestEnemy == null)
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            if (enemies.Length > 0)
            {
                closestEnemy = enemies[0].gameObject;
                foreach (Enemy enemy in enemies)
                {
                    float distanceFromThisEnemy = Vector3.Distance(transform.position, enemy.gameObject.transform.position);
                    float distanceFromCurrentClosestEnemy = Vector3.Distance(transform.position, closestEnemy.transform.position);
                    if (distanceFromThisEnemy < distanceFromCurrentClosestEnemy)
                    {
                        closestEnemy = enemy.gameObject;
                    }
                }
            }
        }

        if (closestEnemy != null)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            transform.LookAt(Vector3.RotateTowards(transform.position, closestEnemy.transform.position, 5f, 360f));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRB = other.gameObject.GetComponent<Rigidbody>();
            Destroy(gameObject);
            Vector3 awayFromThis = other.gameObject.transform.position - transform.position;
            enemyRB.AddForce(awayFromThis * forceFactor, ForceMode.Impulse);
        }
    }
}
