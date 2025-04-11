using UnityEngine;

public class JoystickController : MonoBehaviour
{
    private FixedJoystick joystick;

    private void Awake() => joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();

    public float Horizontal()
    {
        if (joystick.Horizontal != 0) return joystick.Horizontal;
        else return Input.GetAxis("Horizontal");

    }

    public float Vertical()
    {
        if (joystick.Vertical != 0) return joystick.Vertical;
        else return Input.GetAxis("Vertical");

    }
}
