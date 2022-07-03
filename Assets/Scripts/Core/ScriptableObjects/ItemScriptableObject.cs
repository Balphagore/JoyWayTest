using UnityEngine;

namespace JoyWayTest.Core
{
    public class ItemScriptableObject : ScriptableObject
    {
        public GameObject itemPrefab;
        public ItemType itemType;
    }

    public enum ItemType
    {
        Projectile,
        Particle
    }
}