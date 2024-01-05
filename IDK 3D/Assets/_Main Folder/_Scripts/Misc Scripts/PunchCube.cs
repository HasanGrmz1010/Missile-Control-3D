using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchCube : MonoBehaviour
{
    [SerializeField] List<Transform> cubes = new List<Transform>();
    
    Rigidbody rb;

    [SerializeField] List<Vector3> cubesCollisionReadyPos = new List<Vector3>();
    private void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)// Player Missile
        {
            
            for (int i = 0; i < cubes.Count; i++)
            {
                cubes[i].transform.DOLocalMove(cubesCollisionReadyPos[i], .1f);
                cubes[i].GetComponent<Rigidbody>().isKinematic = false;
                cubes[i].GetComponent<Rigidbody>().AddExplosionForce(2.5f, other.transform.position, 1f);
            }
        }
    }
}
