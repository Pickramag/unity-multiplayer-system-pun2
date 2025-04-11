using UnityEngine;

public class SmoothlyCameraFollow : MonoBehaviour
{
    public static SmoothlyCameraFollow instance;

    [SerializeField] private float smoothSpeed = 0.009f;
    [SerializeField] private Vector3 offset;

    [HideInInspector] public Transform target { private get; set; }

    private void Awake() => instance = this;

    private void LateUpdate()
    {
        Vector3 newPosition = new Vector3(target.position.x + offset.x, transform.position.y, target.position.z + offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
