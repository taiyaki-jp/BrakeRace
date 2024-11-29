using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private CarController _carController;
    private void Start()
    {
        _carController=GameObject.Find("Player").GetComponent<CarController>();
    }
    public void AccelePush()
    {
        _carController.Accel=true;
    }

    public void AcceleRelease()
    {
        _carController.Accel = false;
    }
    public void BrakePush()
    {
        _carController.Braki = true;
    }

    public void BrakeRelease()
    {
        _carController.Braki = false;
    }
}
