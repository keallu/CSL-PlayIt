using ColossalFramework;
using System;
using UnityEngine;

namespace PlayIt.Managers
{
    public class DayNightManager : MonoBehaviour
    {
        private SimulationManager simulationManager = Singleton<SimulationManager>.instance;

        private uint previousCurrentFrameIndex;
        private uint previousDayTimeOffsetFrame;

        private static DayNightManager instance;

        public static DayNightManager Instance
        {
            get
            {
                return instance ?? (instance = new DayNightManager());
            }
        }

        public float DayTimeHour
        {
            get
            {
                return simulationManager.m_currentDayTimeHour;
            }
            set
            {
                float newCurrentDayTimeHour = value;
                uint newFrameIndex = (uint)(newCurrentDayTimeHour / 24f * SimulationManager.DAYTIME_FRAMES);
                uint currentFrameIndex = simulationManager.m_currentFrameIndex;
                uint dayTimeOffsetFrames = (newFrameIndex - currentFrameIndex) & (SimulationManager.DAYTIME_FRAMES - 1);
                simulationManager.m_dayTimeOffsetFrames = dayTimeOffsetFrames;
            }
        }

        public void Awake()
        {
            try
            {
                previousCurrentFrameIndex = simulationManager.m_currentFrameIndex;
                previousDayTimeOffsetFrame = simulationManager.m_dayTimeOffsetFrames;
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] DayNightManager:Awake -> Exception: " + e.Message);
            }
        }

        public void Start()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] DayNightManager:Start -> Exception: " + e.Message);
            }
        }

        public void Update()
        {
            try
            {
                bool forcePause = false;

                if (ModConfig.Instance.PauseDayNightCycleOnSimulationPause)
                {
                    forcePause = simulationManager.SimulationPaused || simulationManager.ForcedSimulationPaused;
                }

                if (ModConfig.Instance.DayNightSpeed == -1f)
                {
                    if (!simulationManager.SimulationPaused && !simulationManager.ForcedSimulationPaused)
                    {
                        if (simulationManager.m_dayTimeOffsetFrames == previousDayTimeOffsetFrame)
                        {
                            uint offset = (uint)((int)simulationManager.m_currentFrameIndex - (int)previousCurrentFrameIndex);
                            simulationManager.m_dayTimeOffsetFrames = previousDayTimeOffsetFrame - offset;
                        }
                    }
                }
                else if (ModConfig.Instance.DayNightSpeed == 0f)
                {
                    if (!forcePause)
                    {
                        simulationManager.m_dayTimeOffsetFrames = simulationManager.m_dayTimeOffsetFrames + 1;
                    }
                }
                else
                {
                    if (!forcePause)
                    {
                        float factor = Time.deltaTime * ModConfig.Instance.DayNightSpeed * ModConfig.Instance.DayNightSpeed / 576f;
                        uint offset = (uint)(factor * SimulationManager.DAYTIME_FRAMES);
                        uint dayTimeOffsetFrames = (simulationManager.m_dayTimeOffsetFrames + offset) & (SimulationManager.DAYTIME_FRAMES - 1);
                        simulationManager.m_dayTimeOffsetFrames = dayTimeOffsetFrames;
                    }
                }

                previousCurrentFrameIndex = simulationManager.m_currentFrameIndex;
                previousDayTimeOffsetFrame = simulationManager.m_dayTimeOffsetFrames;
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] DayNightManager:Update -> Exception: " + e.Message);
            }
        }

        public void OnDestroy()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] DayNightManager:OnDestroy -> Exception: " + e.Message);
            }
        }
    }
}
