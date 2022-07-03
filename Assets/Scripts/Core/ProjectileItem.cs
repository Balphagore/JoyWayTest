using UnityEngine;
using UnityEngine.VFX;

namespace JoyWayTest.Core
{
    //Скрипт логики оружия стреляющего проджектайлами. Наследуется от абстрактного класса PlayerItem, в котором находится общая с другими видами оружия логика.
    public class ProjectileItem : PlayerItem
    {
        private float fireTime;
        private bool isReady;

        [Header("References")]
        [SerializeField]
        private Transform projectileSpawnPoint;
        [SerializeField]
        private VisualEffect visualEffect;

        public override void Initialize(ItemScriptableObject itemScriptableObject)
        {
            ItemScriptableObject = itemScriptableObject;
            fireTime = 0;
        }

        public override void Activate(Vector3 targetPosition)
        {
            if (isReady)
            {
                ProjectileItemScriptableObject projectileItemScriptableObject = ItemScriptableObject as ProjectileItemScriptableObject;
                GameObject instance = Instantiate(projectileItemScriptableObject.projectilePrefab, projectileSpawnPoint.position, Quaternion.identity, projectileItemScriptableObject.projectileContainer);
                Projectile projectile = instance.GetComponent<Projectile>();
                projectile.Initialize(targetPosition, projectileItemScriptableObject);
                isReady = false;
                fireTime = projectileItemScriptableObject.fireCooldown;
                if (visualEffect != null)
                {
                    visualEffect.Play();
                }
            }
        }

        private void Update()
        {
            if (visualEffect != null)
            {
                visualEffect.SetVector3("Position", projectileSpawnPoint.position);
            }
            if (fireTime > 0)
            {
                fireTime -= Time.deltaTime;
            }
            else
            {
                fireTime = 0;
                isReady = true;
            }
        }
    }
}