using UnityEngine;

namespace JoyWayTest.Core
{
    public abstract class PlayerItem : MonoBehaviour, IItem
    {
        [SerializeField]
        private ItemScriptableObject itemScriptableObject;
        [SerializeField]
        private GameObject itemGameObject;

        public ItemScriptableObject ItemScriptableObject { get => itemScriptableObject; set => itemScriptableObject = value; }
        public GameObject ItemGameObject { get => itemGameObject; set => itemGameObject = value; }

        public virtual void Activate(Vector3 target)
        {

        }

        public virtual void Deactivate()
        {

        }

        public virtual void Initialize(ItemScriptableObject itemScriptableObject)
        {
            this.itemScriptableObject = itemScriptableObject;
        }
    }
}