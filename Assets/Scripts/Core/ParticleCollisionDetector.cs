using UnityEngine;

namespace JoyWayTest.Core
{
    public class ParticleCollisionDetector : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private ParticleItem particleItem;

        public void Initialize(ParticleItem particleItem)
        {
            this.particleItem = particleItem;
        }

        void OnParticleCollision(GameObject other)
        {
            particleItem.ParticleCollision(other);
        }
    }
}