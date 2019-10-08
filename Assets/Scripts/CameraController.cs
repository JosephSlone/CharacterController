using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineFreeLook freeLookCamera;
    public Vector2 input;

    void Start()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
    }

    void FixedUpdate()
    {
        input.x = Input.GetAxis("Camera X");
        input.y = Input.GetAxis("Camera Y");

        freeLookCamera.m_XAxis.m_InputAxisValue = input.x;
        freeLookCamera.m_YAxis.m_InputAxisValue = input.y;     
    }
}
