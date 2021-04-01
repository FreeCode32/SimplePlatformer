using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    public float speed = 20f;

    private void Start()
    {
        offset = transform.position - target.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.transform.position + offset;
        float step = Time.deltaTime * speed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, step);
    }
}
