using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNamerOnStart : MonoBehaviour
{
    public GameObject NameText;
    public string[] NameList = {"Olius","Dhesorin","Elzofaris","Uharad","Onzozohr","Abahn","Ondivior","Ozahl","Tilius","Edius"};

    void Start()
    {
            var RandNumber = Random.Range(0,9);
            NameText.GetComponent<TMPro.TextMeshProUGUI>().text = NameList[RandNumber];
    }
}