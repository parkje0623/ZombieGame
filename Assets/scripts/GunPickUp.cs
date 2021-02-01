using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    [SerializeField] private GameObject displayName;
    [SerializeField] private GameObject displayPanel;

    private GameObject pickUpGun;
    private BoxCollider parentCollider;
    private Vector3 forward;

    void Start()
    {
        parentCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (transform.childCount == 0)
        {
            StartCoroutine(setTrigger());
        }

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.1f, transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.distance <= 2f && hit.transform.gameObject.tag == "Gun" && !transform.Find(hit.transform.gameObject.name))
            {
                displayName.SetActive(true);
                displayPanel.SetActive(true);
                displayName.GetComponent<Text>().text = hit.transform.gameObject.name;
            }
            else
            {
                displayName.GetComponent<Text>().text = "";
                displayName.SetActive(false);
                displayPanel.SetActive(false);
            }
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Gun")
        {
            pickUpGun = collider.gameObject;
            if ((Input.GetKeyDown(KeyCode.F) && !GunShot.isHoldingGun) || (!GunShot.isHoldingGun))
            { 
                pickUpGun.GetComponent<GunShot>().enabled = true;
                pickUpGun.GetComponent<AmmoDisplay>().enabled = true;
                pickUpGun.GetComponent<DropGun>().enabled = true;
                pickUpGun.GetComponent<Crosshair>().enabled = true;

                pickUpGun.transform.SetParent(transform);
                GunShot.isHoldingGun = true;
            }
        }
    }

    IEnumerator setTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        parentCollider.isTrigger = true;
    }
}
