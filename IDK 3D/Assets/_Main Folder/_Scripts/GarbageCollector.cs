using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    #region Singleton
    public static GarbageCollector instance;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    #endregion

    public void HandleGarbageCollecting(GameObject _object)
    {
        StartCoroutine(DeleteGarbageObject(_object));
    }

    IEnumerator DeleteGarbageObject(GameObject _obj)
    {
        Destroy(_obj);
        yield return new WaitForSeconds(.2f);
    }
}
