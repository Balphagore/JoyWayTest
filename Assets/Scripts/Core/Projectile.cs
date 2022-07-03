using UnityEngine;
using UnityEngine.VFX;

namespace JoyWayTest.Core
{
    public class Projectile : MonoBehaviour
    {
        private float damage;
        private float statusDamage;

        [Header("References")]
        [SerializeField]
        private Rigidbody projectileRigidbody;
        [SerializeField]
        private Transform modelTransform;
        [SerializeField]
        private GameObject hitEffectPrefab;
        [SerializeField]
        private Transform effectContainer;

        public Rigidbody ProjectileRigidbody { get => projectileRigidbody; set => projectileRigidbody = value; }

        //public void Initialize(Transform effectContainer, Vector3 targetPosition, float initialSpeed, float projectileMass, float damage, float statusDamage)
        public void Initialize(Vector3 targetPosition,ProjectileItemScriptableObject projectileItem)
        {
            effectContainer = projectileItem.effectContainer;
            Debug.DrawLine(transform.position, targetPosition, Color.yellow, 10f);
            transform.LookAt(targetPosition);
            projectileRigidbody.velocity = transform.rotation * Vector3.forward * projectileItem.projectileInitialSpeed;
            projectileRigidbody.mass = projectileItem.projectileMass;
            damage = projectileItem.projectileDamage;
            statusDamage = projectileItem.statusDamage;
            Destroy(gameObject, 5f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.DrawLine(collision.contacts[0].point, collision.contacts[0].point + collision.contacts[0].normal * 2f, Color.red, 10f, false);
            if (hitEffectPrefab != null)
            {
                GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity, effectContainer);
                hitEffect.transform.LookAt(collision.contacts[0].point + collision.contacts[0].normal);
                Destroy(hitEffect, 1f);
            }
            IDamagable damagable = collision.gameObject.GetComponentInParent<IDamagable>();
            if (damagable != null)
            {
                if (damage > 0)
                {
                    damagable.TakeNormalDamage(damage);
                }
                if (statusDamage != 0)
                {
                    damagable.TakeStatusDamage(statusDamage, damage);
                }
            }
            Destroy(gameObject);
        }

        private void Update()
        {
            Debug.DrawLine(transform.position, transform.position + projectileRigidbody.velocity);
            modelTransform.up = projectileRigidbody.velocity;
        }
    }
}