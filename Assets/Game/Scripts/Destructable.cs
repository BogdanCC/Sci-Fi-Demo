using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

    [SerializeField]
    private GameObject crackedCrate;

	public void DestroyObject() {

        Instantiate(crackedCrate, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
