using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    #region Singleton
    public static StoreManager instance;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] GameData_SO game_data;
    [SerializeField] StoreData_SO store_data;

    [SerializeField] GameObject SelectedMissile_Object;
    [SerializeField] ParticleSystem SelectedParticle_Object;
    List<GameObject> purchasedMissiles = new List<GameObject>();
    List<ParticleSystem> purchasedParticles = new List<ParticleSystem>();

    private void Start()
    {
        //store_data.ResetPurchasedList();
    }

    public void AddMissileTo_PurchasedList(GameObject _missile)
    {
        if (_missile != null && !purchasedMissiles.Contains(_missile))
        {
            purchasedMissiles.Add(_missile);
        }
        else return;
    }

    public void ChooseSelectedMissile(GameObject _obj)
    {
        if (_obj != null)
        {
            SelectedMissile_Object = _obj;
            game_data.SetCurrentMissileObject(SelectedMissile_Object);
        }
        else return;
    }

    public GameObject GetSelectedMissile()
    {
        if (SelectedMissile_Object != null) return SelectedMissile_Object;
        else return null;
    }

    public void AddParticleTo_PurchasedList(ParticleSystem particle)
    {
        if (particle != null && !purchasedParticles.Contains(particle))
        {
            purchasedParticles.Add(particle);
        }
        else return;
    }

    public void ChooseSelectedParticle(ParticleSystem particle)
    {
        if (particle != null)
        {
            SelectedParticle_Object = particle;
            game_data.SetUpMissileFX(particle);
        }
        else return;
    }

    public ParticleSystem GetSelectedParticle()
    {
        if (SelectedParticle_Object != null) return SelectedParticle_Object;
        else return null;
    }
}
