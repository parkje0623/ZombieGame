using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> dropLists;
    [SerializeField] private Transform target;
    [SerializeField] private float health;
    [SerializeField] private AudioClip zombieRunning;
    [SerializeField] private AudioClip zombieAttacking;

    private bool playerSpotted;
    private bool zombieAttack;
    private bool zombieDead;
    private float playerDistance = 50.0f;
    private float hitDistance = 2.0f;
    private Animator zombieMove;
    private AudioSource zombieRunSound;
    private AudioSource zombieAttackSound;

    void Start()
    {
        zombieMove = GetComponent<Animator>();
        zombieDead = false;
        zombieRunSound = GetComponent<AudioSource>();
        zombieAttackSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !zombieDead)
        {
            zombieDead = true;
            zombieMove.SetBool("isDead", true);
            GameManage.kills++;
            dropItems();
            StartCoroutine(killZombie());
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 1.0f, out hit))
        {
            if (hit.transform.gameObject.tag == "Player" && hit.distance <= playerDistance)
            {
                playerSpotted = true;
            }

            if (hit.transform.gameObject.tag == "Player" && hit.distance <= hitDistance)
            {
                zombieAttack = true;
            }
            else
            {
                zombieAttack = false;
            }
        }

        if (playerSpotted)
        {
            if (!zombieRunSound.isPlaying)
            {
                zombieRunSound.clip = zombieRunning;
                zombieRunSound.Play();
            }

            transform.LookAt(target.position);
            zombieMove.SetBool("isRunning", true);
        }

        if (zombieAttack)
        {
            if (!zombieAttackSound.isPlaying)
            {
                zombieAttackSound.clip = zombieAttacking;
                zombieAttackSound.Play();
            }
            zombieMove.SetBool("targetDetected", true);
        }
        else
        {
            zombieMove.SetBool("targetDetected", false);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !zombieDead)
        {
            collider.gameObject.SendMessage("Attacked", 10f, SendMessageOptions.DontRequireReceiver);
        }
        else if (collider.gameObject.tag == "targets")
        {
            transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Map")
        {
            transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        }
    }

    void hitObject(float damage)
    {
        health -= damage;
    }

    private void dropItems()
    {
        Instantiate(dropLists[Random.Range(0, dropLists.Count)], transform.position, new Quaternion(0, 0, 0, 0));
    }

    IEnumerator killZombie()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
