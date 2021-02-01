using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private GameObject ammoAmount;
    [SerializeField] private int maxAmmo;
    [SerializeField] private AudioClip reloadClip;

    public static int fullAmmo;
    public static int loadedAmmo;
    public static bool isReloading;

    private int reloadAmount;
    private Animation reloading;
    private AudioSource reloadSound;
    private float initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.eulerAngles.y;
        loadedAmmo = 0;
        fullAmmo = 28;

        ammoAmount.GetComponent<Text>().text = loadedAmmo + "/" + fullAmmo;
        reloading = GetComponent<Animation>();
        reloadSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoAmount.GetComponent<Text>().text = loadedAmmo + "/" + fullAmmo;

        if (Input.GetKeyDown(KeyCode.R) && GunShot.isHoldingGun)
        {
            if (loadedAmmo < maxAmmo && fullAmmo != 0)
            {
                isReloading = true;
                reloadAmmo();
            }
        }

        if (!reloading.IsPlaying("ReloadGun") && !reloading.IsPlaying("ReloadGun2"))
        {
            isReloading = false;
        }
    }

    private void reloadAmmo()
    {
        reloadSound.clip = reloadClip;
        reloadSound.Play();
        reloadAmount = maxAmmo - loadedAmmo;
        if (reloadAmount <= fullAmmo)
        {
            fullAmmo -= reloadAmount;
        }
        else
        {
            reloadAmount = fullAmmo;
            fullAmmo = 0;
        }

        if (initialRotation == 180f)
        {
            reloading.Play("ReloadGun");
        }
        else
        {
            reloading.Play("ReloadGun2");
        }
        
        loadedAmmo = reloadAmount + loadedAmmo;
    }
}
