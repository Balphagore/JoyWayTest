using UnityEngine;

namespace JoyWayTest.Core
{
    //Менеджер для тестовой сцены с минимальной функциональностью
    public class CoreManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private PlayerInput playerInput;
        [SerializeField]
        private PlayerMovement playerMovement;
        [SerializeField]
        private PlayerFiring playerFiring;
        [SerializeField]
        private Transform projectileContainer;
        [SerializeField]
        private Transform effectContainer;

        public PlayerInput PlayerInput { get => playerInput; set => playerInput = value; }

        public virtual void Start()
        {
            playerMovement.Initialize(playerInput);
            playerFiring.Initialize(playerInput, projectileContainer, effectContainer);
        }
    }
}