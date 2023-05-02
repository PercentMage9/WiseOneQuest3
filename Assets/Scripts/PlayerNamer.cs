using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNamer : MonoBehaviour
{
    public GameObject NameText;
    public GameObject TextBox;
    
    public void onEnter()
    {
        NameText.GetComponent<TMPro.TextMeshProUGUI>().text = TextBox.GetComponent<TMPro.TextMeshProUGUI>().text;
    }
}
