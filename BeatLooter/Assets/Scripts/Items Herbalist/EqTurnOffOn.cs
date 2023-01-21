using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqTurnOffOn : MonoBehaviour
{
    [SerializeField]
    GameObject eqView;

    public void SwitchActive()
    {
        eqView.SetActive(!eqView.activeSelf);
    }
}
