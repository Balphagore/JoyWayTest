using UnityEngine;

namespace JoyWayTest.Core
{
    //Скрипт оружия находящегося в сцене. Когда его подбирает игрок, оно передает ему свои параметры из ScriptableObject для реализации логики стрельбы.
    public class WorldItem : MonoBehaviour
    {
        [SerializeField]
        private ItemScriptableObject itemScriptableObject;

        [Header("References")]
        [SerializeField]
        private Transform spawnPoint;
        [SerializeField]
        private GameObject placeHolder;

        public ItemScriptableObject ItemScriptableObject { get => itemScriptableObject; set => itemScriptableObject = value; }

        private void Start()
        {
            placeHolder.SetActive(false);
            Instantiate(itemScriptableObject.itemPrefab, spawnPoint);
        }

        public void PickUp()
        {
            gameObject.SetActive(false);
        }

        public void Drop(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }
    }
}