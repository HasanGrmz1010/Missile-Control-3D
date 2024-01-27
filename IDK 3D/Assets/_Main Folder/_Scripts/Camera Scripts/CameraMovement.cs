using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    bool eliminated;

    [SerializeField] Transform followTarget;
    [SerializeField] Vector3 offset;
    [SerializeField] float camMoveSpeed;
    private void Start()
    {
        followTarget = GameObject.FindGameObjectWithTag("Player").transform;
        eliminated = false;
    }

    private void LateUpdate()
    {
        HandleCamMovement();
    }

    void HandleCamMovement()
    {
        if (!eliminated)
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(followTarget.position.x - offset.x, transform.position.y, transform.position.z),
                camMoveSpeed * Time.deltaTime);
        }
    }

    public void onPlayerEliminated()
    {
        eliminated = true;
    }

    public void SetOffset(Vector3 _param)
    {
        offset = _param;
    }
}
