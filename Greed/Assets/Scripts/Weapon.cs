using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float damage = 1f;
    public float range = 50f;
    public float fireRate = 1f;
    public ParticleSystem muzzleflash;

    public float changeCost;

    public Player player;
    public Camera camera;

    public enum TypeOfWeapon { Midas, Bankzooka, ATM};

    public TypeOfWeapon TOW;

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
	}

    private void Shoot()
    {
        player.loseChange(changeCost);

        RaycastHit hit;
        Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range);

        muzzleflash.Play();

        if(TOW == TypeOfWeapon.Midas)
        {
            // Do specific Midas things...

        }

        if (hit.transform != null)
        {
            Debug.Log(hit.transform.name);
            Entity hitItem = hit.transform.gameObject.GetComponent<Entity>();

            if (hitItem != null)
                hitItem.TakeDamage(damage);

            
        }
    }
}
