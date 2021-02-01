using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperScope : MonoBehaviour
{
    [SerializeField] private GameObject normalCrosshair;
    [SerializeField] private GameObject scope;
    [SerializeField] private GameObject playerCamera;

    private bool isScoped;
    private bool isFullScoped;
    private bool isAwp;

    void Start()
    {
        isScoped = false;
        isFullScoped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount != 0)
        {
            if (transform.Find("AWP"))
            {
                isAwp = true;
                normalCrosshair.SetActive(false);
            }
            else
            {
                isAwp = false;
                normalCrosshair.SetActive(true);
            }
        }
        else
        {
            normalCrosshair.SetActive(false);
        }
        
        if (isAwp && GunShot.isHoldingGun)
        {
            if (Input.GetMouseButtonDown(1) && !isScoped && !AmmoDisplay.isReloading && !GunShot.stopShooting)
            {
                playerCamera.GetComponent<Camera>().fieldOfView = 25;
                scope.SetActive(true);
                isScoped = true;
            }
            else if (Input.GetMouseButtonDown(1) && isScoped && !isFullScoped && !AmmoDisplay.isReloading && !GunShot.stopShooting)
            {
                playerCamera.GetComponent<Camera>().fieldOfView = 10;
                isFullScoped = true;
            }
            else if ((Input.GetMouseButtonDown(1) && isFullScoped) || Input.GetMouseButtonDown(0))
            {
                playerCamera.GetComponent<Camera>().fieldOfView = 60;
                scope.SetActive(false);
                isScoped = false;
                isFullScoped = false;
            }
        }
    }
}
