using UnityEngine;

namespace JoyWayTest.Core
{
    //—крипт логики оружи€ стрел€ющего партиклами. Ќаследуетс€ от абстрактного класса PlayerItem, в котором находитс€ обща€ с другими видами оружи€ логика.
    public class ParticleItem : PlayerItem
    {
        [SerializeField]
        private ParticleSystem itemParticleSystem;
        [SerializeField]
        private ParticleCollisionDetector particleCollisionDetector;

        //¬ ScriptableObject'e хран€тс€ все основные параметры конкретного оружи€.
        private ParticleItemScriptableObject particleItemScriptableObject;

        private void Start()
        {
            particleCollisionDetector.Initialize(this);
        }

        public override void Initialize(ItemScriptableObject itemScriptableObject)
        {
            ItemScriptableObject = itemScriptableObject;
            ParticleSystem.EmissionModule emissionModule = itemParticleSystem.emission;
            particleItemScriptableObject = ItemScriptableObject as ParticleItemScriptableObject;
            emissionModule.rateOverTime = particleItemScriptableObject.emissionSpeed;
            emissionModule.enabled = false;
        }

        public override void Activate(Vector3 target)
        {
            ParticleSystem.EmissionModule emissionModule = itemParticleSystem.emission;
            emissionModule.enabled = true;
            itemParticleSystem.transform.LookAt(target);
        }

        public override void Deactivate()
        {
            ParticleSystem.EmissionModule emissionModule = itemParticleSystem.emission;
            emissionModule.enabled = false;
        }

        public void ParticleCollision(GameObject other)
        {
            IDamagable damagable = other.GetComponentInParent<IDamagable>();
            if (damagable != null)
            {
                if (particleItemScriptableObject.statusDamage != 0)
                {
                    damagable.TakeStatusDamage(particleItemScriptableObject.statusDamage, particleItemScriptableObject.particleDamage);
                }
            }
        }
    }
}