using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchCube : MonoBehaviour
{
    [SerializeField] List<Transform> cubes = new List<Transform>();
    [SerializeField] ParticleSystem punchFX;

    [SerializeField] List<Vector3> cubesCollisionReadyPos = new List<Vector3>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)// Player Missile
        {
            punchFX.gameObject.SetActive(true);
            punchFX.Play();
            for (int i = 0; i < cubes.Count; i++)
            {
                cubes[i].transform.DOLocalMove(cubesCollisionReadyPos[i], .1f);
                cubes[i].GetComponent<Rigidbody>().isKinematic = false;
                cubes[i].GetComponent<Rigidbody>().AddExplosionForce(2.5f, other.transform.position, 1f);
            }
            StartCoroutine(DeactivateAfterSeconds(1.5f));
        }
    }

    IEnumerator DeactivateAfterSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
        transform.parent.gameObject.SetActive(false);
        GarbageCollector.instance.HandleGarbageCollecting(transform.parent.gameObject);
    }
}
