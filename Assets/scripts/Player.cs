using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    [SerializeField] private GameObject playerAmmo;
    [SerializeField] private GameObject healthUI;
    [SerializeField] private GameObject playerBlood;
    [SerializeField] private GameObject timer;

    public static bool gameover;
    private GameObject gunHolder;
    private float timeleft;

    // Start is called before the first frame update
    void Start()
    {
        gameover = false;
        timeleft = 400.0f;
    }

    // Update is called once per frame
    void Update()
    {
        playerAmmo.GetComponent<Text>().text = AmmoDisplay.loadedAmmo + "/" + AmmoDisplay.fullAmmo;
        timeleft -= Time.deltaTime;
        timer.GetComponent<Text>().text = ((int) timeleft).ToString();
        if (timeleft < 0)
        {
            playerHealth = 0;
        }

        healthUI.GetComponent<Text>().text = "HP: " + playerHealth;
        if (playerHealth <= 0 && !gameover)
        {
            gameover = true;
            gunHolder = GameObject.Find("Arm");
            Destroy(gunHolder);
        }
    }

    void Attacked(float attackDamage)
    {
        playerBlood.SetActive(true);
        StartCoroutine(notAttacked());
        playerHealth -= attackDamage;
    }

    IEnumerator notAttacked()
    {
        yield return new WaitForSeconds(1f);
        playerBlood.SetActive(false);
    }
}
