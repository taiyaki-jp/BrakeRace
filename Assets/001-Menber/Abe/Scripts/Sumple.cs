using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEST : MonoBehaviour
{
    private FadeManager fadeManager;
    // Start is called before the first frame update
    void Start()
    {
        fadeManager =GameObject.Find("FadeManager").GetComponent<FadeManager>();


        fadeManager.Fade<Enum>("A-Test2", Image.FillMethod.Radial360, Radial_360_Origin.Right, Image.FillMethod.Vertical, VerticalOrigin.Top);
    }
}
