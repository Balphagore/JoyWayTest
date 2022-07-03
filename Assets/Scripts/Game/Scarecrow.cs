using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JoyWayTest.Core;

namespace JoyWayTest.Game
{
    public class Scarecrow : MonoBehaviour, IDamagable
    {
        [SerializeField]
        private float maximumHitPoints;
        [SerializeField]
        private float hitPoints;
        [SerializeField]
        private float maximumStatusPoints;
        [SerializeField]
        private float statusPoints;
        [SerializeField]
        private bool isBurning;
        [SerializeField]
        private float burningTimer;

        private PlayerInput playerInput;
        private List<Color> originalColorList;
        private IEnumerator burnCoroutine;

        [Header("References")]
        [SerializeField]
        private Renderer modelRenderer;

        public delegate void HitPointsChangeHandle(float hitPoints, float maximumHitPoints);
        public event HitPointsChangeHandle HitPointsChangeEvent;

        public delegate void StatusPointsChangeHandle(float statusPoints, float maximumStatusPoints);
        public event StatusPointsChangeHandle StatusPointsChangeEvent;

        private void Start()
        {
            UpdateHitPoints(maximumHitPoints);
            playerInput.TargetResetEvent += OnTargetResetEvent;
            //Сохранение оригинального цвета материалов чтобы вернуть его когда статусы снимаются.
            originalColorList = new List<Color>();
            foreach (Material material in modelRenderer.materials)
            {
                originalColorList.Add(material.color);
            }
        }

        public void Initialize(PlayerInput playerInput)
        {
            this.playerInput = playerInput;
        }

        private void OnTargetResetEvent()
        {
            ResetHitPoints();
        }

        public void TakeNormalDamage(float damage)
        {
            if (statusPoints >= maximumStatusPoints)
            {
                damage -= 10;
            }
            else
            {
                if (isBurning)
                {
                    damage += 10;
                }
            }
            UpdateHitPoints(-damage);
        }

        public void TakeStatusDamage(float statusDamage, float normalDamage)
        {
            statusPoints += statusDamage;
            if (statusPoints >= maximumStatusPoints)
            {
                //Красим пугало в синий, когда влажность достигла максимума.
                foreach (Material material in modelRenderer.materials)
                {
                    material.color = Color.blue;
                }
                statusPoints = maximumStatusPoints;
            }
            else
            {
                if (statusPoints < 0)
                {
                    statusPoints = 0;
                    //Красим пугало в красный, если влажность на минимуме и получен отрицательный урон статуса.
                    foreach (Material material in modelRenderer.materials)
                    {
                        material.color = Color.red;
                        isBurning = true;
                        //Поджигаем пугало
                        if (burnCoroutine == null)
                        {
                            burnCoroutine = BurnCoroutine(10f);
                            StartCoroutine(burnCoroutine);
                        }
                        else
                        {
                            burningTimer = 0;
                        }
                    }
                }
                else
                {
                    //Если получен статусный урон в промежутке между минимумом и максимумом, то возвращаем пугалу оригинальный цвет и останавливаем горение.
                    for (int i = 0; i < modelRenderer.materials.Length; i++)
                    {
                        modelRenderer.materials[i].color = originalColorList[i];
                    }
                    if (burnCoroutine != null)
                    {
                        StopCoroutine(burnCoroutine);
                        burnCoroutine = null;
                        burningTimer = 0;
                    }
                }
            }
            StatusPointsChangeEvent?.Invoke(statusPoints, maximumStatusPoints);
            //Партиклы огня наносят еще и обычный урон,
            //но если наносить его в функции которую используют пули, то получится что по горящему пугалу партиклы огня наносят в 10 раз больше урона.
            UpdateHitPoints(-normalDamage);
        }

        private void UpdateHitPoints(float value)
        {
            hitPoints += value;
            HitPointsChangeEvent?.Invoke(hitPoints, maximumHitPoints);
            if (hitPoints <= 0)
            {
                if (burnCoroutine != null)
                {
                    StopCoroutine(burnCoroutine);
                    for (int i = 0; i < modelRenderer.materials.Length; i++)
                    {
                        modelRenderer.materials[i].color = originalColorList[i];
                    }
                }
                gameObject.SetActive(false);
            }
        }
        private void ResetHitPoints()
        {
            hitPoints = maximumHitPoints;
            HitPointsChangeEvent?.Invoke(hitPoints, maximumHitPoints);
            if (gameObject.activeInHierarchy == false)
            {
                gameObject.SetActive(true);
                statusPoints = 0;
                StatusPointsChangeEvent?.Invoke(statusPoints, maximumStatusPoints);
            }
        }

        private IEnumerator BurnCoroutine(float burnTime)
        {
            float timer = 0;
            while (isBurning && burningTimer <= burnTime)
            {
                timer += Time.deltaTime;
                burningTimer += Time.deltaTime;
                if (timer > 1f)
                {
                    TakeNormalDamage(5f);
                    timer = 0;
                }
                yield return null;
            }
            for (int i = 0; i < modelRenderer.materials.Length; i++)
            {
                modelRenderer.materials[i].color = originalColorList[i];
            }
            StopCoroutine(burnCoroutine);
            burnCoroutine = null;
            burningTimer = 0;
        }
    }
}