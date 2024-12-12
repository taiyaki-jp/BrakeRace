using System;
using UnityEngine;

public class Sumple : MonoBehaviour
{
    private FadeManager fadeManager;
    // Start is called before the first frame update
    void Start()
    {
        fadeManager =GameObject.Find("FadeManager").GetComponent<FadeManager>();


        fadeManager.Fade<Enum>("A-Test2", Radial_360_Origin.Right, VerticalOrigin.Top);
    }
}
