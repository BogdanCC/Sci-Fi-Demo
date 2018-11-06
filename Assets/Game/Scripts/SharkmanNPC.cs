using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkmanNPC : MonoBehaviour {

    [SerializeField]
    private AudioClip weaponPickup;
    [SerializeField]
    private GameObject playerWeapon;

    private void OnTriggerStay(Collider other) {

        if(other.tag == "Player") {

            if(Input.GetKeyDown(KeyCode.E)) {

                if(Player.hasCoin) {

                    // Get coin, give weapon
                    Player.hasCoin = false;
                    Player.hasWeapon = true;
                    // Update the UI
                    UIManager.UpdateInventory(Player.hasCoin);
                    UIManager.UpdateAmmo(Player.MAX_AMMO);
                    // Play pickup sound
                    AudioSource.PlayClipAtPoint(weaponPickup, Camera.main.transform.position);
                    // Activate weapon game object
                    playerWeapon.SetActive(true);
                } else {
                    Debug.Log("Get out of here!");
                }
            }
        }
        
    }
        
    
}
