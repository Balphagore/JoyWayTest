using UnityEngine;

namespace JoyWayTest.Core
{
    public class PlayerFiring : MonoBehaviour
    {
        [SerializeField]
        private WorldItem leftWorldItem;
        [SerializeField]
        private WorldItem rightWorldItem;
        [SerializeField]
        private WorldItem triggeredWorldItem;

        private bool isLeftItemFiring;
        private bool isRightItemFiring;
        private Vector3 target;

        [Header("References")]
        [SerializeField]
        private Camera playerCamera;
        [SerializeField]
        private Transform leftItemSlot;
        [SerializeField]
        private PlayerItem leftItem;
        [SerializeField]
        private Transform rightItemSlot;
        [SerializeField]
        private PlayerItem rightItem;
        [SerializeField]
        private Transform projectileContainer;
        [SerializeField]
        private Transform effectContainer;

        public void Initialize(PlayerInput playerInput, Transform projectileContainer, Transform effectContainer)
        {
            this.projectileContainer = projectileContainer;
            this.effectContainer = effectContainer;
            playerInput.FireLeftEvent += OnFireLeftEvent;
            playerInput.FireRightEvent += OnFireRightEvent;
            playerInput.ActivateLeftEvent += OnActivateLeftEvent;
            playerInput.ActivateRightEvent += OnActivateRightEvent;

            if (leftWorldItem != null)
            {
                ActivateLeftItem(leftWorldItem);
            }
            if (rightWorldItem != null)
            {
                ActivateRightItem(rightWorldItem);
            }
        }

        private void Update()
        {
            if (leftItem != null)
            {
                if (isLeftItemFiring)
                {
                    leftItem.Activate(target);
                }
                else
                {
                    leftItem.Deactivate();
                }
            }
            if (rightItem != null)
            {
                if (isRightItemFiring)
                {
                    rightItem.Activate(target);
                }
                else
                {
                    rightItem.Deactivate();
                }
            }
        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
            {
                target = hit.point;
            }
            else
            {
                target = ray.GetPoint(100);
            }
            Debug.DrawLine(playerCamera.transform.position, target, Color.green);
        }

        private void OnTriggerStay(Collider other)
        {
            WorldItem worldItem = other.GetComponent<WorldItem>();
            if (worldItem != null)
            {
                triggeredWorldItem = worldItem;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            WorldItem worldItem = other.GetComponent<WorldItem>();
            if (triggeredWorldItem == worldItem)
            {
                triggeredWorldItem = null;
            }
        }

        private void OnFireLeftEvent(float firingValue)
        {
            isLeftItemFiring = firingValue > 0 ? true : false;
        }

        private void OnFireRightEvent(float firingValue)
        {
            isRightItemFiring = firingValue > 0 ? true : false;
        }

        private void OnActivateLeftEvent()
        {
            if (leftWorldItem == null)
            {
                if (triggeredWorldItem != null)
                {
                    ActivateLeftItem(triggeredWorldItem);
                }
            }
            else
            {
                Destroy(leftItem.gameObject);
                leftWorldItem.Drop(transform.position);
                leftWorldItem = null;
            }
        }

        private void OnActivateRightEvent()
        {
            if (rightWorldItem == null)
            {
                if (triggeredWorldItem != null)
                {
                    ActivateRightItem(triggeredWorldItem);
                }
            }
            else
            {
                Destroy(rightItem.gameObject);
                rightWorldItem.Drop(transform.position);
                rightWorldItem = null;
            }
        }

        private void ActivateLeftItem(WorldItem worldItem)
        {
            leftWorldItem = worldItem;
            leftWorldItem.PickUp();
            triggeredWorldItem = null;
            GameObject instance;
            switch (leftWorldItem.ItemScriptableObject.itemType)
            {
                case ItemType.Projectile:
                    instance = Instantiate(leftWorldItem.ItemScriptableObject.itemPrefab, leftItemSlot);
                    ProjectileItemScriptableObject leftProjectileItemParameters = leftWorldItem.ItemScriptableObject as ProjectileItemScriptableObject;
                    leftProjectileItemParameters.projectileContainer = projectileContainer;
                    leftProjectileItemParameters.effectContainer = effectContainer;
                    leftItem = instance.GetComponent<PlayerItem>();
                    leftItem.Initialize(leftProjectileItemParameters);
                    break;
                case ItemType.Particle:
                    instance = Instantiate(leftWorldItem.ItemScriptableObject.itemPrefab, leftItemSlot);
                    ParticleItemScriptableObject leftParticleItemParameters = leftWorldItem.ItemScriptableObject as ParticleItemScriptableObject;
                    leftItem = instance.GetComponent<PlayerItem>();
                    leftItem.Initialize(leftParticleItemParameters);
                    break;
            }
        }

        private void ActivateRightItem(WorldItem worldItem)
        {
            rightWorldItem = worldItem;
            rightWorldItem.PickUp();
            triggeredWorldItem = null;
            GameObject instance;
            switch (rightWorldItem.ItemScriptableObject.itemType)
            {
                case ItemType.Projectile:
                    instance = Instantiate(rightWorldItem.ItemScriptableObject.itemPrefab, rightItemSlot);
                    ProjectileItemScriptableObject rightProjectileItemParameters = rightWorldItem.ItemScriptableObject as ProjectileItemScriptableObject;
                    rightProjectileItemParameters.projectileContainer = projectileContainer;
                    rightProjectileItemParameters.effectContainer = effectContainer;
                    rightItem = instance.GetComponent<PlayerItem>();
                    rightItem.Initialize(rightProjectileItemParameters);
                    break;
                case ItemType.Particle:
                    instance = Instantiate(rightWorldItem.ItemScriptableObject.itemPrefab, rightItemSlot);
                    ParticleItemScriptableObject rightParticleItemParameters = rightWorldItem.ItemScriptableObject as ParticleItemScriptableObject;
                    rightItem = instance.GetComponent<PlayerItem>();
                    rightItem.Initialize(rightParticleItemParameters);
                    break;
            }
        }
    }
}