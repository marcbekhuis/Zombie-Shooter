using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform playerLocation;

    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = playerLocation.position + cameraOffset;
    }
}
