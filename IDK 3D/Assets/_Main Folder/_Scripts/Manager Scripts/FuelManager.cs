using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    #region Singleton
    public static FuelManager instance;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    #endregion

    [SerializeField] Image fuel_indicator_fill_img;

    float fuel;

    private void Start()
    {
        fuel = 1f;
        fuel_indicator_fill_img.fillAmount = fuel;
    }

    public void AddFuel(float _val)
    {
        if ((fuel + _val) < 1f && _val > 0)
        {
            fuel += _val;
            fuel_indicator_fill_img.fillAmount = fuel;
        }
        else if ((fuel + _val) >= 1f)
        {
            fuel = 1f;
            fuel_indicator_fill_img.fillAmount = 1f;
        }
        else if (_val < 0) return;
    }

    public void SpendFuel(float _val)
    {
        if ((fuel - _val) > 0)
        {
            fuel -= _val;
            fuel_indicator_fill_img.fillAmount = fuel;
        }
        else
        {
            fuel = 0f;
            fuel_indicator_fill_img.fillAmount = 0f;
        }
    }

    public float GetCurrentFuel()
    {
        return fuel;
    }

    public bool hasFuel() { if (fuel > 0) return true; else return false; }
}
