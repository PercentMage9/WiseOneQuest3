using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNamer : MonoBehaviour
{
    public GameObject NameText;
    public string[] NameList = {"Olius","Dhesorin","Elzofaris","Uharad","Onzozohr","Abahn","Ondivior","Ozahl","Tilius","Edius"};

    public void RandomName()
    {
            var RandNumber = Random(0,9);
            NameText.GetComponent<TMPro.TextMeshProUGUI>().text = NameList[RandNumber];
    }
}