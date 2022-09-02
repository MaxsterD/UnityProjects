using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // Text Fields
    public Text levelText, hitpointText, pesosText, updgradeCostText, xpText;

    // Logic Fields
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //Character Selection

    public void OnArrowclick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;

            if (currentCharacterSelection < 0)
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChanged();
        }
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }

    //weapon Upgrade

    public void OnUpgradeClick()
    {

    }

    //update Character Information
    public void UpdateMenu()
    {
        //weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[0];
        updgradeCostText.text = "NOT IMPLEMENTED";

        //Meta
        levelText.text = "NOT IMPLEMENTED";
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();

        //xp Bar
        xpText.text = "NOT IMPLEMENTED";
        xpBar.localScale = new Vector3(0.5f, 0, 0);
    }
}
