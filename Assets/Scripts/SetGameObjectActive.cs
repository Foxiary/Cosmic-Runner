using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObjectActive : MonoBehaviour
{
    public void SetGameObjectTrue()
    {
        gameObject.SetActive(true);
    }
    public void SetGameObjectFalse()
    {
        gameObject.SetActive(false);
    }
}
