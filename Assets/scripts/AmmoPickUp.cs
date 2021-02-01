using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    [SerializeField] private int ammoBox;

    private bool pickedUp = false;

    void OnTriggerStay(Collider collider)
    {
        if (Input.GetKeyDown(KeyCode.E) && collider.gameObject.tag == "Player" && !pickedUp)
        {
            pickedUp = true;
            AmmoDisplay.fullAmmo += ammoBox;
            Destroy(transform.parent.gameObject);
        }
    }
}
