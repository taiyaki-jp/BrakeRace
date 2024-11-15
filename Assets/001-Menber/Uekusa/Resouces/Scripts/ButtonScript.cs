using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private CarController _carController;

    public void AccelePush()
    {
        _carController.StartAccelerating();
    }

    public void AcceleRelease()
    {
        _carController.StopAccelerating();
    }
    public void BrakePush()
    {
        _carController.StartBraking();
    }

    public void BrakeRelease()
    {
        _carController.StopBraking();
    }
}
