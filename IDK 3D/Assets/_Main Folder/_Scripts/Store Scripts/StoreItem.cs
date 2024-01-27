using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    [SerializeField] GameObject this_missile_obj;
    [SerializeField] ParticleSystem this_particle_obj;

    [SerializeField] Image Lock_img;
    [SerializeField] Image Locked_Fade_img;
    [SerializeField] TextMeshProUGUI costText;

    [SerializeField] Button Buy_Button;
    [SerializeField] Button Select_Button;

    private void Start()
    {
        if (COST == 0) costText.text = "FREE";
        else costText.text = COST.ToString();

        Buy_Button.onClick.AddListener(Purchase_Item);
        Select_Button.onClick.AddListener(SelectItem);
    }

    public void Purchase_Item()
    {
        if (type == Type.missile)
        {
            if (game_data.GetTotalCoinValue() >= COST)
            {
                Buy_Button.gameObject.SetActive(false);
                Select_Button.gameObject.SetActive(true);
                Lock_img.gameObject.SetActive(false);
                Locked_Fade_img.gameObject.SetActive(false);

                StoreManager.instance.AddMissileTo_PurchasedList(this_missile_obj);

                game_data.DecreaseTotalCoin(COST);
                EconomyManager.instance.UpdateCoinValueText();
                SoundManager.instance.PlayChaChingSoundFX();
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
                Buy_Button.gameObject.SetActive(false);
                Select_Button.gameObject.SetActive(true);
                Lock_img.gameObject.SetActive(false);
                Locked_Fade_img.gameObject.SetActive(false);

                StoreManager.instance.AddParticleTo_PurchasedList(this_particle_obj);

                game_data.DecreaseTotalCoin(COST);
                EconomyManager.instance.UpdateCoinValueText();
                SoundManager.instance.PlayChaChingSoundFX();
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
}
