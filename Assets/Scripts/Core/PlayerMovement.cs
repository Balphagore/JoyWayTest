using UnityEngine;

namespace JoyWayTest.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [Range(0f, 10f)]
        [SerializeField]
        private float movementSpeed;
        [Range(0f, 10f)]
        [SerializeField]
        private float sensitivity;

        private Vector2 movementVector;
        private float cameraRotation;

        [Header("References")]
        [SerializeField]
        private Rigidbody playerRigidbody;
        [SerializeField]
        private Camera playerCamera;

        public void Initialize(PlayerInput playerInput)
        {
            playerInput.MovementEvent += OnMovementEvent;
            playerInput.LookEvent += OnLookEvent;
        }

        private void FixedUpdate()
        {
            if (movementVector != Vector2.zero)
            {
                playerRigidbody.velocity = transform.TransformDirection(new Vector3(movementVector.x * movementSpeed, 0, movementVector.y * movementSpeed));
            }
            else
            {
                playerRigidbody.velocity = Vector2.zero;
            }
        }

        private void OnMovementEvent(Vector2 movementVector)
        {
            this.movementVector = movementVector;
        }

        private void OnLookEvent(Vector2 mouseDeltaVector)
        {
            transform.Rotate(new Vector3(0, mouseDeltaVector.x * sensitivity, 0));
            cameraRotation -= mouseDeltaVector.y * sensitivity;
            cameraRotation = Mathf.Clamp(cameraRotation, -90, 80);
            playerCamera.transform.localRotation = Quaternion.Euler(new Vector3(cameraRotation, 0, 0));
        }
    }
}