using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private CinemachineFreeLook freeLookCamera;
    private InputController controls;
    private Vector2 input;

    private void Awake()
    {
        controls = new InputController();

        controls.GamePlay.Camera.performed += ctx => input = ctx.ReadValue<Vector2>();
        controls.GamePlay.Camera.canceled += ctx => input = Vector2.zero;
    }

    void Start()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
    }


    void Update()
    {
        freeLookCamera.m_XAxis.m_InputAxisValue = input.x;
        freeLookCamera.m_YAxis.m_InputAxisValue = input.y;     
    }

    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }
}
