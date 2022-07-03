using UnityEngine;
using UnityEngine.InputSystem;

namespace JoyWayTest.Core
{
    public class PlayerInput : MonoBehaviour
    {
        public delegate void MovementHandle(Vector2 movementVector);
        public event MovementHandle MovementEvent;

        public delegate void LookHandle(Vector2 mouseDeltaVector);
        public event LookHandle LookEvent;

        public delegate void FireLeftHandle(float firingValue);
        public event FireLeftHandle FireLeftEvent;

        public delegate void FireRightHandle(float firingValue);
        public event FireRightHandle FireRightEvent;

        public delegate void ActivateLeftHandle();
        public event ActivateLeftHandle ActivateLeftEvent;

        public delegate void ActivateRightHandle();
        public event ActivateRightHandle ActivateRightEvent;

        public delegate void TargetResetHandle();
        public event TargetResetHandle TargetResetEvent;

        public void OnMove(InputAction.CallbackContext value)
        {
            MovementEvent?.Invoke(value.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext value)
        {
            LookEvent?.Invoke(value.ReadValue<Vector2>());
        }

        public void OnFireLeft(InputAction.CallbackContext value)
        {
            FireLeftEvent?.Invoke(value.ReadValue<float>());
        }

        public void OnFireRight(InputAction.CallbackContext value)
        {
            FireRightEvent?.Invoke(value.ReadValue<float>());
        }

        public void OnActivateLeft(InputAction.CallbackContext value)
        {
            if (value.action.triggered)
            {
                ActivateLeftEvent?.Invoke();
            }
        }

        public void OnActivateRight(InputAction.CallbackContext value)
        {
            if (value.action.triggered)
            {
                ActivateRightEvent?.Invoke();
            }
        }

        public void OnTargetReset(InputAction.CallbackContext value)
        {
            if (value.action.triggered)
            {
                TargetResetEvent?.Invoke();
            }
        }
    }
}