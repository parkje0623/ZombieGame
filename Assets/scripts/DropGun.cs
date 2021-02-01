using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGun : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider parentCollider;
    private BoxCollider boxCollider;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && GunShot.isHoldingGun)
        {
            rb = GetComponent<Rigidbody>();
            parentCollider = transform.parent.GetComponent<BoxCollider>();
            boxCollider = GetComponent<BoxCollider>();

            AmmoDisplay.fullAmmo += AmmoDisplay.loadedAmmo;
            AmmoDisplay.loadedAmmo = 0;

            if (gameObject.name != "M1911")
            {
                transform.localScale = new Vector3(2, 2, 2);
            }
            transform.SetParent(null);
            rb.isKinematic = false;
            boxCollider.isTrigger = false;
            parentCollider.isTrigger = false;
            //rb.AddForce(transform.forward * 2, ForceMode.Impulse);
            GunShot.isHoldingGun = false;
        }
    }
}
