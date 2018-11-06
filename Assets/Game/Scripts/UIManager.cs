using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static Text ammoText;
    private static Image inventoryCoin;

    private void Awake() {
        ammoText = GameObject.Find("AmmoText").GetComponent<Text>();
        inventoryCoin = GameObject.Find("CoinImage").GetComponent<Image>();
    }

    public static void UpdateAmmo(int count) {
        if (ammoText != null) {
            ammoText.text = "Ammo : " + count;
        }
    }

    public static void NoWeapon() {
        if(ammoText != null) {
            ammoText.text = "No weapon";
        }
    }

    public static void UpdateInventory(bool value) {
        inventoryCoin.enabled = value;
    }
}
