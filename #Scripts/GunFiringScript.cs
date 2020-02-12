using UnityEngine;
using System.Collections;

public class GunFiringScript : MonoBehaviour {

    public Transform projectilePrefab;

    //public Transform Spawnpoint;

    public float shootingInitialVelocity;

    public int maxNumberOfAmmo;

    public int _numberOfAmmo; // Private actual remaining ammo (public to be seen in editor)
    public int numberOfAmmo{    // Public variable of ammo
        get
        {
            return _numberOfAmmo;
        }
        set
        {
            if (value > maxNumberOfAmmo)
                _numberOfAmmo = maxNumberOfAmmo;
            else if (value < 0)
                _numberOfAmmo = 0;
            else
                _numberOfAmmo = value;
        }
    }

    public float firingRate;
    private float deltaTimeBetweenShots;
    private float _firingTimer;
    public float firingTimer{
        get
        {
            return _firingTimer;
        }
        set
        {
            if (value < 0) _firingTimer = 0;
            else _firingTimer = value;
        }
    }

    public bool limitedRange;
    public float gunRange;

    public bool autoShootForDebug = false;
    public bool infiniteAmmoForDebug = false;

    // Use this for initialization
    void Start () {
        _numberOfAmmo = maxNumberOfAmmo;

        firingTimer = 0;
        if (firingRate == 0)
            deltaTimeBetweenShots = 100;
        else
        {
            deltaTimeBetweenShots = 60 / firingRate;
            Debug.Log("Initialized firing rate at " + (deltaTimeBetweenShots*1000).ToString("F01") + " ms between two shots");
        }
            
    }

    // Update is called once per frame (physics -->)
    void FixedUpdate () {

        // Update Firing Timer
        firingTimer -= Time.fixedDeltaTime;

        if (Input.GetButton("Fire1") || autoShootForDebug)
        {
            //Check if we can fire
            if (canFire())
            {
                // Fire
                FireProjectile(limitedRange, gunRange, shootingInitialVelocity);
                //Reset Firing timer
                firingTimer = deltaTimeBetweenShots;
                //given remaining ammo
            }
            else
            {
                //if (Input.GetButtonDown("Fire1")) // Only if this is the first time we pressed the button:
                //Give negative firing feedback
            }            
        }
    }

    void FireProjectile(bool _limitRange, float _range, float _shootingInitialVelocity){

        // Manage Firing Here

        Transform clone;

        clone = Instantiate(projectilePrefab, transform.position, transform.rotation);

        CollisionPainterCapsuleSmall colPaint = clone.GetComponent<CollisionPainterCapsuleSmall>();

        // Herit Gun characteristics to the bullet
        colPaint.bulletRange = _range;
        colPaint.limitedRange = _limitRange;

        clone.GetComponent<Rigidbody>().AddForce(transform.forward * _shootingInitialVelocity, ForceMode.VelocityChange);

        numberOfAmmo -= 1;
    }

    bool canFire(){
        if (firingTimer == 0 && (numberOfAmmo > 0 || infiniteAmmoForDebug))
            return true;
        else
            return false;
    }
}