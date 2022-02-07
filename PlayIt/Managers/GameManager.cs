using System;
using UnityEngine;

namespace PlayIt.Managers
{
    public class GameManager : MonoBehaviour
    {
        private float previousTimeScale;

        private static GameManager instance;

        public static GameManager Instance
        {
            get
            {
                return instance ?? (instance = new GameManager());
            }
        }
        public float GameSpeed
        {
            get
            {
                return Time.timeScale;
            }
            set
            {
                Time.timeScale = value;
            }
        }

        public void Awake()
        {
            try
            {
                previousTimeScale = Time.timeScale;
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] GameManager:Awake -> Exception: " + e.Message);
            }
        }

        public void Start()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] GameManager:Start -> Exception: " + e.Message);
            }
        }

        public void Update()
        {
            try
            {
                if (ModConfig.Instance.GameSpeed != previousTimeScale)
                {
                    Time.timeScale = ModConfig.Instance.GameSpeed;
                }

                previousTimeScale = Time.timeScale;
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] GameManager:Update -> Exception: " + e.Message);
            }
        }

        public void OnDestroy()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] GameManager:OnDestroy -> Exception: " + e.Message);
            }
        }
    }
}
