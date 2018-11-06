using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static bool hasCoin = false;
    public static bool hasWeapon = false;

    // Variables to hold the needed components
    private CharacterController characterController;
    private MouseMovement mouseMovement;
    [SerializeField]
    private AudioSource shootingSound;
    private Destructable destructable;

    // Variables for GameObjects
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private GameObject hitMarkerPrefab;

    // Variables to hold the keyboard inputs
    private float horizontalInput;
    private float verticalInput;

    // Creating physics variables
    [SerializeField]
    private float speed = 3.5f;
    private const float GRAVITY = 9.81f;

    // Weapon variables
    public const int MAX_AMMO = 50;
    [SerializeField]
    private int currentAmmo;
    private bool isReloading = false;
    
    // Use this for initialization
    void Start () {

        // Get the CharacterController component sitting on this GameObject
        characterController = GetComponent<CharacterController>();
        mouseMovement = GetComponent<MouseMovement>();
        destructable = GameObject.Find("Wooden_Crate").GetComponent<Destructable>();

        // Hide cursor
        Cursor.visible = false;
        // Lock cursor to center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        currentAmmo = MAX_AMMO;
        UIManager.NoWeapon();
    }
	
	// Update is called once per frame
	void Update () {
        
        PlayerMovement();
        if (hasWeapon) {
            Shoot();
        }
        ShowCursor();
    }

    // Method that deals with the player movement
    private void PlayerMovement() {
        // Get the keyboard inputs for direction
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Create a Vector3 for direction given the keyboard inputs
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        // Multiply the direction by the speed, and we get velocity
        Vector3 velocity = direction * speed;
        // Subtract gravity (on the Y axis) to simulate gravity
        velocity.y -= GRAVITY;

        // Now we need to convert the player local space to world space
        velocity = transform.transform.TransformDirection(velocity);

        // Pass the velocity to the CharacterController Move() method
        characterController.Move(velocity * Time.deltaTime);

    }

    // Method that deals with the player shooting
    private void Shoot() {
        // If player has ammo and is not reloading, shoot when left click is pressed
        if (Input.GetMouseButton(0) && currentAmmo > 0 && isReloading == false) {
            // Decrease ammo with each shot and update UI
            currentAmmo--;
            UIManager.UpdateAmmo(currentAmmo);

            // Send a ray from the center of the viewport and get information of what it hit
            Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0);         
            Ray rayOrigin = Camera.main.ViewportPointToRay(screenCenter);
            RaycastHit hitInfo;

            // Enable particle effect for shooting since we are shooting
            muzzleFlash.SetActive(true);

            // Play shooting sound if it's not already playing
            if (shootingSound.isPlaying == false)
            {
                shootingSound.Play();
            }

            // If the ray hit something, instantiate a visual effect on that exact point
            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity))
            {
                Debug.Log("We hit : " + hitInfo.transform.name);
                Instantiate(hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                
                // If wooden crate is hit, instantiate destructable game object
                if (hitInfo.transform.tag == "Destructable") {
                    if (destructable != null) {
                        destructable.DestroyObject();
                    }
                }
            }
        }

        // Else, if we are not shooting, stop the shooting sound and the particle effects
        else
        {
            muzzleFlash.SetActive(false);
            shootingSound.Stop();
        }

        // If user presses R and is not already reloading, reload
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
            Reload();
        }
    }

    // Method to reload ammo
    private void Reload()
    {       
        isReloading = true;
        StartCoroutine(ReloadTimer());
    }

    // Method to make reloading last 2 seconds
    private IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(2f);
        currentAmmo = MAX_AMMO;
        isReloading = false;
        UIManager.UpdateAmmo(currentAmmo);
    }

    // Method to show and unlock the cursor when Escape key is pressed
    private void ShowCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

  
}
