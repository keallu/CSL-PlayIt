using ColossalFramework.UI;
using ICities;
using System;
using UnityEngine;
using PlayIt.Panels;
using PlayIt.Managers;

namespace PlayIt
{

    public class Loading : LoadingExtensionBase
    {
        private GameObject _modManagerGameObject;
        private GameObject _gameManagerGameObject;
        private GameObject _dayNightManagerGameObject;
        private GameObject _mainPanelGameObject;
        private GameObject _clockPanelGameObject;

        public override void OnLevelLoaded(LoadMode mode)
        {
            try
            {
                _modManagerGameObject = new GameObject("PlayItModManager");
                _modManagerGameObject.AddComponent<ModManager>();

                _gameManagerGameObject = new GameObject("PlayItGameManager");
                _gameManagerGameObject.AddComponent<GameManager>();

                _dayNightManagerGameObject = new GameObject("PlayItDayNightManager");
                _dayNightManagerGameObject.AddComponent<DayNightManager>();

                UIView uiView = UnityEngine.Object.FindObjectOfType<UIView>();
                if (uiView != null)
                {
                    _mainPanelGameObject = new GameObject("PlayItMainPanel");
                    _mainPanelGameObject.transform.parent = uiView.transform;
                    _mainPanelGameObject.AddComponent<MainPanel>();

                    _clockPanelGameObject = new GameObject("PlayItClockPanel");
                    _clockPanelGameObject.transform.parent = uiView.transform;
                    _clockPanelGameObject.AddComponent<ClockPanel>();
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] Loading:OnLevelLoaded -> Exception: " + e.Message);
            }
        }

        public override void OnLevelUnloading()
        {
            try
            {
                if (_clockPanelGameObject != null)
                {
                    UnityEngine.Object.Destroy(_clockPanelGameObject);
                }

                if (_mainPanelGameObject != null)
                {
                    UnityEngine.Object.Destroy(_mainPanelGameObject);
                }

                if (_dayNightManagerGameObject != null)
                {
                    UnityEngine.Object.Destroy(_dayNightManagerGameObject);
                }

                if (_gameManagerGameObject != null)
                {
                    UnityEngine.Object.Destroy(_gameManagerGameObject);
                }

                if (_modManagerGameObject != null)
                {
                    UnityEngine.Object.Destroy(_modManagerGameObject);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] Loading:OnLevelUnloading -> Exception: " + e.Message);
            }
        }
    }
}