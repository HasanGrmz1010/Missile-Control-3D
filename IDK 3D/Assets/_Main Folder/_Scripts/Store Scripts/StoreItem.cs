using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    public enum Type
    {
        missile, particle
    }
    public Type type = new Type();

    [SerializeField] int COST;
    [SerializeField] GameData_SO game_data;
    [SerializeField] StoreData_SO store_data;

    [SerializeField] GameObject this_missile_obj;
    [SerializeField] ParticleSystem this_particle_obj;

    [SerializeField] Image Lock_img;
    [SerializeField] Image Locked_Fade_img;
    [SerializeField] TextMeshProUGUI costText;

    [SerializeField] Button Buy_Button;
    [SerializeField] Button Select_Button;

    private void Start()
    {
        if (store_data.ContainsPurchasedItem(this.gameObject))
        {
            UI_ChangeToBought();
        }

        if (COST == 0) costText.text = "FREE";
        else costText.text = COST.ToString();

        // Button Subscribtions
        Buy_Button.onClick.AddListener(Purchase_Item);
        Select_Button.onClick.AddListener(SelectItem);
    }

    public void Purchase_Item()
    {
        if (type == Type.missile)
        {
            if (game_data.GetTotalCoinValue() >= COST)
            {
                UI_ChangeToBought();

                StoreManager.instance.AddMissileTo_PurchasedList(this_missile_obj);
                store_data.AddItem_toList(this.gameObject);

                game_data.DecreaseTotalCoin(COST);
                EconomyManager.instance.UpdateCoinValueText();
                SoundManager.instance.PlaySoundFX("ching", 1f);
            }

            else
            {
                // -- Cant Afford -- Text Pop
            }
        }

        else if (type == Type.particle)
        {
            if (game_data.GetTotalCoinValue() >= COST)
            {
                UI_ChangeToBought();

                StoreManager.instance.AddParticleTo_PurchasedList(this_particle_obj);
                store_data.AddItem_toList(this.gameObject);

                game_data.DecreaseTotalCoin(COST);
                EconomyManager.instance.UpdateCoinValueText();
                SoundManager.instance.PlaySoundFX("ching", 1f);
            }

            else
            {
                // -- Cant Afford -- Text Pop
            }
        }
    }

    public void SelectItem()
    {
        if (this_missile_obj != null)
        {
            StoreManager.instance.ChooseSelectedMissile(this_missile_obj);
        }
        else if (this_particle_obj != null)
        {
            StoreManager.instance.ChooseSelectedParticle(this_particle_obj);
        }
    }

    void UI_ChangeToBought()
    {
        Buy_Button.gameObject.SetActive(false);
        Select_Button.gameObject.SetActive(true);
        Lock_img.gameObject.SetActive(false);
        Locked_Fade_img.gameObject.SetActive(false);
    }
}
