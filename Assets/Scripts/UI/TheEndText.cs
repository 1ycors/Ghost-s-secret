using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TheEndText : MonoBehaviour
{
    [SerializeField] private GameObject theEndScreen;
    [SerializeField] private TextMeshProUGUI defEnd;
    [SerializeField] private TextMeshProUGUI trueEnd;
    private void Awake()
    {
        theEndScreen.SetActive(false);
        defEnd.enabled = false;
        trueEnd.enabled = false;
    }
}
