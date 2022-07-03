using UnityEngine;

namespace JoyWayTest.Core
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ParticleItemScriptableObject")]
    public class ParticleItemScriptableObject : ItemScriptableObject
    {
        public float emissionSpeed;
        public float particleDamage;
        public float statusDamage;
    }
}