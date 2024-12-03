using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    Vector3 targetPos;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offsetPos;
    [SerializeField] private float smooth;
    private Vector3 velocity = Vector3.zero;
    void LateUpdate()
    {
        MoveWithTarget();
    }
    void MoveWithTarget()
    {
        targetPos = target.transform.position + offsetPos;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smooth);
    }
}
