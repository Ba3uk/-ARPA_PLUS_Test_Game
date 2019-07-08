using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _mixY;
    [SerializeField] private float _maxY;

    [SerializeField] private float _sensivityY;
    [SerializeField] private float _sensivityX;

    [SerializeField] private float _distance;

    [SerializeField] private bl_Joystick _joystick;

    private float currentX;
    private float currentY;


    // Start is called before the first frame update
    void Start()
    {
        currentY = 32f;
    }

    // Update is called once per frame
    void Update()
    {
        currentX -= _joystick.InputValue.x * _sensivityX;
        currentY -= _joystick.InputValue.z * _sensivityY;

        currentY = Mathf.Clamp(currentY, _mixY, _maxY);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -_distance);
        Quaternion q = Quaternion.Euler(currentY, currentX, 0);

        transform.position =   _target.position + q * dir ;
        transform.LookAt(_target);
    }
}
