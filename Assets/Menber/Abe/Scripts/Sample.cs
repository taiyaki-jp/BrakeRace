using System;
using UnityEngine;

public class Sample : MonoBehaviour
{
    private FadeManager fadeManager;
    // Start is called before the first frame update
    void Start()
    {
        fadeManager =GameObject.Find("FadeManager").GetComponent<FadeManager>();


        _ = fadeManager.Fade<Enum>("A-Test2", Radial_360_Origin.Right, VerticalOrigin.Top);
    }
}
