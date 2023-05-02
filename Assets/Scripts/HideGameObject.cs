using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGameObject : MonoBehaviour
{
    public GameObject Target;

    public void HideObject()
    {
        Target.SetActive(false);
    }
}
