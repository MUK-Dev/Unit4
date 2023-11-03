using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform focalPoint;
    [SerializeField] private float forceFactor = 10f;
    [SerializeField] private float powerupStrength = 15.0f;
    [SerializeField] private GameObject powerUpIndicator;
    [SerializeField] private GameObject rocketPrefab;

    private Rigidbody rb;
    private bool hasPowerup;

    private float rocketsDelay = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        powerUpIndicator.SetActive(false);
    }

    private void Update()
    {
        rocketsDelay -= Time.deltaTime;
        if (hasPowerup && rocketsDelay <= 0)
        {
            rocketsDelay = 2f;
            Vector3 spawnPos1 = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            Instantiate(rocketPrefab, spawnPos1, rocketPrefab.transform.rotation);
            Vector3 spawnPos2 = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
            Instantiate(rocketPrefab, spawnPos2, rocketPrefab.transform.rotation);
            Vector3 spawnPos3 = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            Instantiate(rocketPrefab, spawnPos3, rocketPrefab.transform.rotation);
            Vector3 spawnPos4 = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            Instantiate(rocketPrefab, spawnPos4, rocketPrefab.transform.rotation);
        }

        float v = Input.GetAxis("Vertical");

        powerUpIndicator.transform.position = new Vector3(transform.position.x, powerUpIndicator.transform.position.y, transform.position.z);

        rb.AddForce(focalPoint.forward * forceFactor * Time.deltaTime * v);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerUpIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

            enemyRB.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerUpIndicator.SetActive(false);
    }
}
