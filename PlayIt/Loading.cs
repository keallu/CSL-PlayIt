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
        private LoadMode _loadMode;
        private GameObject _modManagerGameObject;
        private GameObject _dayNightManagerGameObject;
        private GameObject _mainPanelGameObject;

        public override void OnLevelLoaded(LoadMode mode)
        {
            try
            {
                _loadMode = mode;

                if (_loadMode != LoadMode.NewGame && _loadMode != LoadMode.LoadGame && _loadMode != LoadMode.NewGameFromScenario)
                {
                    return;
                }

                _modManagerGameObject = new GameObject("PlayItModManager");
                _modManagerGameObject.AddComponent<ModManager>();

                _dayNightManagerGameObject = new GameObject("PlayItDayNightManager");
                _dayNightManagerGameObject.AddComponent<DayNightManager>();

                UIView uiView = UnityEngine.Object.FindObjectOfType<UIView>();
                if (uiView != null)
                {
                    _mainPanelGameObject = new GameObject("PlayItMainPanel");
                    _mainPanelGameObject.transform.parent = uiView.transform;
                    _mainPanelGameObject.AddComponent<MainPanel>();
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
                if (_loadMode != LoadMode.NewGame && _loadMode != LoadMode.LoadGame && _loadMode != LoadMode.NewGameFromScenario)
                {
                    return;
                }

                if (_mainPanelGameObject != null)
                {
                    UnityEngine.Object.Destroy(_mainPanelGameObject);
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