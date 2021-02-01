using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunShot : MonoBehaviour
{
    [SerializeField] private float firerate;
    [SerializeField] private AudioClip gunClip;
    [SerializeField] private float damage;
    [SerializeField] private GameObject gunShotFlash;
    [SerializeField] private GameObject gunName;
    [SerializeField] private bool isBackwards;
    [SerializeField] private Vector3 originalRotation;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Vector3 originalScale;
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private GameObject blood;
    [SerializeField] private float crossRecoil;

    public static bool stopShooting;
    public static bool isHoldingGun;
    private float initialRotation;
    private Animation recoil;
    private AudioSource gunSound;
    private Vector3 forward;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private GameObject aimFix;
    private GameObject moveCrosshair;
    private RectTransform rect;
    private float randomX;
    private float randomY;
    private float randomZ;
    private Vector3 originalCross;
    private int startRecoil;

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        gunSound = GetComponent<AudioSource>();
        recoil = GetComponent<Animation>();
        initialRotation = originalRotation.y;
        stopShooting = false;
        aimFix = GameObject.Find("AimFix");
        moveCrosshair = GameObject.Find("GunCrosshair");
        rect = moveCrosshair.GetComponent<RectTransform>();
        originalCross = rect.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent != null)
        {
            transform.localRotation = Quaternion.Euler(originalRotation.x, originalRotation.y, originalRotation.z);
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z);
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
            isHoldingGun = true;
            rb.isKinematic = true;
            boxCollider.isTrigger = true;
            gunName.GetComponent<Text>().text = gameObject.name;
        }
        else
        {
            isHoldingGun = false;
            gameObject.GetComponent<GunShot>().enabled = false;
            gameObject.GetComponent<AmmoDisplay>().enabled = false;
            gunName.GetComponent<Text>().text = "";
            gameObject.GetComponent<DropGun>().enabled = false;
            gameObject.GetComponent<Crosshair>().enabled = false;
        }

        if (AmmoDisplay.loadedAmmo <= 0 || AmmoDisplay.isReloading)
        {
            startRecoil = 0;
            rect.transform.position = originalCross;
            CancelInvoke("Shoot");
        }

        if (isHoldingGun && AmmoDisplay.loadedAmmo > 0 && !AmmoDisplay.isReloading)
        {
            if (firerate <= 0f)
            {
                if (Input.GetMouseButtonDown(0) && !stopShooting)
                {
                    Shoot();
                    StartCoroutine(stopGunFire());
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startRecoil = 0;
                    InvokeRepeating("Shoot", 0f, 1f/firerate);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    startRecoil = 0;
                    rect.transform.position = originalCross;
                    CancelInvoke("Shoot");
                }
            } 
        }
    }

    private void Shoot()
    {
        //Sound and recoil animation
        gunSound.clip = gunClip;
        gunSound.Play();
        gunShotFlash.SetActive(true);

        if (gameObject.name == "AWP")
        {
            stopShooting = true;
        }
        else
        {
            stopShooting = false;
        }

        StartCoroutine(stopGunFlash());
        if (initialRotation == 180f)
        {
            recoil.Play("GunRecoil");
        }
        else
        {
            recoil.Play("GunRecoil2");
        }

        gameObject.SendMessage("CrosshairRecoil", true, SendMessageOptions.DontRequireReceiver);
        AmmoDisplay.loadedAmmo -= 1;

        //Shooting 
        RaycastHit shot;
        forward = transform.parent.TransformDirection(Vector3.forward);
        rect.transform.position = originalCross;
        startRecoil++;
        if (gameObject.name != "AWP")
        {
            forward.y -= 0.02f;
            if (startRecoil > 3)
            {
                randomX = Random.Range(-crossRecoil, crossRecoil);
                randomY = Random.Range(0, crossRecoil);
                randomZ = Random.Range(-crossRecoil, crossRecoil);
                forward.x += randomX;
                forward.y += randomY;
                forward.z += randomZ;
                rect.transform.position += new Vector3(randomX * 200, randomY * 200, randomZ * 200);
            }
        }
        if (Physics.Raycast(aimFix.transform.position, forward, out shot))
        {
            shot.transform.SendMessage("hitObject", damage, SendMessageOptions.DontRequireReceiver);
            if (shot.transform.gameObject.tag != "targets" && shot.transform.gameObject.tag != "Gun")
            {
                GameObject mark = Instantiate(bulletHole);
                mark.transform.position = shot.point;
                mark.transform.forward = shot.normal * -1f;
            }  
            else if (shot.transform.gameObject.tag == "targets")
            {
                GameObject bloodPoint = Instantiate(blood);
                bloodPoint.transform.position = shot.point;
                bloodPoint.transform.forward = shot.normal;
            }
        }
    } 

    IEnumerator stopGunFlash()
    {
        yield return new WaitForSeconds(0.1f);
        gunShotFlash.SetActive(false);
    }

    IEnumerator stopGunFire()
    {
        yield return new WaitForSeconds(1.5f);
        stopShooting = false;
    }
}
