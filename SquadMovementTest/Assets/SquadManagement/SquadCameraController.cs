using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace SquadManagement
{
    public class SquadCameraController : MonoBehaviour
    {
        [SerializeField] private GameObject _squadCameraRoot;
        
        [Header("Cinemachine")]
        [Tooltip("How far in degrees can you move the camera up")]
        [SerializeField] private float _topClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        [SerializeField] private float _bottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        [SerializeField] private float _cameraAngleOverride = 0.0f;

        private SquadController _squadController;
        private PlayerInput _playerInput;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        private const float Threshold = 0.01f;
        private bool IsCurrentDeviceMouse => _playerInput.currentControlScheme == "KeyboardMouse";

        private void Start()
        {
            _cinemachineTargetYaw = _squadCameraRoot.transform.rotation.eulerAngles.y;
            _squadController = GetComponent<SquadController>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            CameraRotation();
        }
        
        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_squadController.NewLookDirection.sqrMagnitude >= Threshold)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += _squadController.NewLookDirection.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _squadController.NewLookDirection.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);

            // Cinemachine will follow this target
            _squadCameraRoot.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + _cameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }
        
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}