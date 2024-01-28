using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStoreData", menuName = "Store Data")]
public class StoreData_SO : ScriptableObject
{
    [SerializeField] List<GameObject> Cards = new List<GameObject>();

    public static List<GameObject> purchasedItems = new List<GameObject>();

    public void AddItem_toList(GameObject _obj)
    {
        if (_obj != null)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                if (_obj.name == Cards[i].name && !purchasedItems.Contains(Cards[i]))
                {
                    purchasedItems.Add(Cards[i]);
                }
            }
        }
        else return;
        
        //if (_obj != null && !purchasedItems.Contains(_obj))
        //{
        //    purchasedItems.Add(_obj);
        //}

    }

    public bool ContainsPurchasedItem(GameObject _val)
    {
        if (_val != null)
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                if (_val.name == Cards[i].name)
                {
                    if (purchasedItems.Contains(Cards[i])) return true;
                }
            }
            return false;
            //if (purchasedItems.Contains(_val)) return true;
            //else return false;
        }
        else return false;
    }

    public void ResetPurchasedList()
    {
        if (purchasedItems.Count > 0)
            purchasedItems.Clear();
        else return;
    }
}
