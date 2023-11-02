using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10f;
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime * h);
    }
}
