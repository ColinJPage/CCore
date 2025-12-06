using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VersionNumber : MonoBehaviour
{
    [SerializeField] string prefix = "v";
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();

        text.text = prefix + Application.version;
    }
}
