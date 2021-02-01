using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private GameObject TopCross;
    [SerializeField] private GameObject BottomCross;
    [SerializeField] private GameObject LeftCross;
    [SerializeField] private GameObject RightCross;

    void CrosshairRecoil(bool isFire)
    {
        TopCross.GetComponent<Animator>().enabled = true;
        BottomCross.GetComponent<Animator>().enabled = true;
        LeftCross.GetComponent<Animator>().enabled = true;
        RightCross.GetComponent<Animator>().enabled = true;
        StartCoroutine(Recoil());
    }

    IEnumerator Recoil()
    {
        yield return new WaitForSeconds(0.1f);
        TopCross.GetComponent<Animator>().enabled = false;
        BottomCross.GetComponent<Animator>().enabled = false;
        LeftCross.GetComponent<Animator>().enabled = false;
        RightCross.GetComponent<Animator>().enabled = false;
    }
}
