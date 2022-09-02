using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    public int pesos;
    public int experience;

    public bool TryUpgradeWeapon()
    {
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;
        
        if(pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);

    }
    public void SaveState()
    {
        string s = "";

        /*
         * INT PreferedSkin
         * INT pesos
         * INT experience
         * INT weaponLevel
         */

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();


        PlayerPrefs.SetString("saveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //Change Skin
        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        //Change weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));
        Debug.Log("Load");
    }
}
