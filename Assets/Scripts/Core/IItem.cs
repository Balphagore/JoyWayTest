using UnityEngine;

namespace JoyWayTest.Core
{
    public interface IItem
    {
        void Initialize(ItemScriptableObject itemScriptableObject);

        void Activate(Vector3 target);

        void Deactivate();
    }
}