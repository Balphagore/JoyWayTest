using UnityEngine;

namespace JoyWayTest.Core
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ProjectileItemScriptableObject")]
    public class ProjectileItemScriptableObject : ItemScriptableObject
    {
        public GameObject projectilePrefab;
        public float fireCooldown;
        public float projectileMass;
        public float projectileInitialSpeed;
        public float projectileDamage;
        public float statusDamage;
        public Transform projectileContainer;
        public Transform effectContainer;
    }
}