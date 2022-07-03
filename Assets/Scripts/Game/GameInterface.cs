using UnityEngine;
using UnityEngine.UI;

namespace JoyWayTest.Game
{
    public class GameInterface : MonoBehaviour
    {
        private Scarecrow scarecrow;
        private Transform targetTransform;

        [Header("References")]
        [SerializeField]
        private GameObject barsPanel;
        [SerializeField]
        private Slider hitPointsSlider;
        [SerializeField]
        private Slider statusPointsSlider;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Initialize(Scarecrow scarecrow)
        {
            this.scarecrow = scarecrow;
            this.scarecrow.HitPointsChangeEvent += OnHitPointsChangeEvent;
            this.scarecrow.StatusPointsChangeEvent += OnStatusPointsChangeEvent;
            targetTransform = this.scarecrow.transform;
        }

        private void OnHitPointsChangeEvent(float hitPoints, float maximumHitPoints)
        {
            hitPointsSlider.value = hitPoints / maximumHitPoints;
            if (hitPoints <= 0)
            {
                barsPanel.gameObject.SetActive(false);
            }
            else
            {
                barsPanel.gameObject.SetActive(true);
            }
        }

        private void OnStatusPointsChangeEvent(float statusPoints, float maximumStatusPoints)
        {
            statusPointsSlider.value = statusPoints / maximumStatusPoints;
        }

        private void Update()
        {
            //ѕеремещает панель с индикаторами над пугалом.
            Vector3 wantedPos = Camera.main.WorldToScreenPoint(targetTransform.position + Vector3.up * 3f);
            barsPanel.transform.position = wantedPos;
        }
    }
}