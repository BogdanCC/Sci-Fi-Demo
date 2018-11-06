using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField]
    private AudioClip coinPickupSound;

    // OnTriggerStay gets called once per frame while 'other' is in the collider range
    private void OnTriggerStay(Collider other) {

        if (other.gameObject.tag == "Player") {

            if (Input.GetKeyDown(KeyCode.E)) {

                // Set player hasCoin to true
                Player.hasCoin = true;

                // Update inventory
                UIManager.UpdateInventory(Player.hasCoin);

                // PlayClipAtPoint will play a sound at a specified position even after this game object is destoyed
                AudioSource.PlayClipAtPoint(coinPickupSound, transform.position, 1f);

                // Finally destroy this game object
                Destroy(this.gameObject);
            }
        }
    }
}

