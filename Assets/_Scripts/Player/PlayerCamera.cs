using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);

    private void Awake()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        transform.position = target.position + offset;
    }
}
