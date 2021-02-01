using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanObjects : MonoBehaviour
{
    [SerializeField] private List<GameObject> gunLists;
    [SerializeField] private List<GameObject> zombieLists;
    [SerializeField] private List<GameObject> amooLists;
    [SerializeField] private List<Vector3> spawnLists;

    private Vector3 fixGunSpawnPos = new Vector3(0, 1f, 0);
    private GameObject zombie;
    private int score;
    private bool level1;
    private bool level2;
    private bool level3;
    private bool level4;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnZombie", 0.0f, 4.0f);
        InvokeRepeating("spawnAmmo", 0.0f, 20.0f);
        level1 = false;
        level2 = false;
        level3 = false;
        level4 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!level1 && GameManage.kills < 10) 
        {
            spawnGun(1);
            spawnGun(2);
            level1 = true;
        }
        else if (!level2 && GameManage.kills > 10)
        {
            spawnGun(3);
            spawnGun(4);
            level2 = true;
        }
        else if (!level3 && GameManage.kills > 20)
        {
            spawnGun(5);
            spawnGun(6);
            spawnGun(7);
            level3 = true;
        }
        else if (!level4 && GameManage.kills > 30)
        {
            spawnGun(8);
            level4 = true;
        }
    }

    void spawnZombie()
    {
        zombie = Instantiate(zombieLists[Random.Range(0, zombieLists.Count)], spawnLists[Random.Range(0, spawnLists.Count)], new Quaternion(0, 0, 0, 0));
        zombie.SetActive(true);
    }

    void spawnGun(int gunNumber)
    {
        gunLists[gunNumber].SetActive(true);
        gunLists[gunNumber].transform.position = spawnLists[Random.Range(0, spawnLists.Count)] + fixGunSpawnPos;
        fixGunSpawnPos.x++;
    }

    void spawnAmmo()
    {
        Instantiate(amooLists[Random.Range(0, amooLists.Count)], spawnLists[Random.Range(0, spawnLists.Count)], new Quaternion(0, 0, 0, 0));
    }
}
