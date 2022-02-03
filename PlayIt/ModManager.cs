using ColossalFramework;
using ColossalFramework.UI;
using System;
using UnityEngine;
using PlayIt.Panels;

namespace PlayIt
{
    public class ModManager : MonoBehaviour
    {
        private bool _initialized;

        private UITextureAtlas _playItAtlas;
        private UIPanel _buttonPanel;
        private UIButton _button;

        private MainPanel _mainPanel;

        public void Awake()
        {
            try
            {
                
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ModManager:Awake -> Exception: " + e.Message);
            }
        }

        public void Start()
        {
            try
            {
                if (_mainPanel == null)
                {
                    _mainPanel = GameObject.Find("PlayItMainPanel")?.GetComponent<MainPanel>();
                }

                if (ModConfig.Instance.ButtonPositionX == 0f && ModConfig.Instance.ButtonPositionY == 0f)
                {
                    ModProperties.Instance.ResetButtonPosition();
                }

                _playItAtlas = LoadResources();

                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ModManager:Start -> Exception: " + e.Message);
            }
        }

        public void Update()
        {
            try
            {
                if (!_initialized || ModConfig.Instance.ConfigUpdated)
                {
                    UpdateUI();
                    _mainPanel.ForceUpdateUI();

                    _initialized = true;
                    ModConfig.Instance.ConfigUpdated = false;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ModManager:Update -> Exception: " + e.Message);
            }
        }

        public void OnDestroy()
        {
            try
            {   
                if (_button != null)
                {
                    Destroy(_button.gameObject);
                }
                if (_buttonPanel != null)
                {
                    Destroy(_buttonPanel.gameObject);
                }
                if (_playItAtlas != null)
                {
                    Destroy(_playItAtlas);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ModManager:OnDestroy -> Exception: " + e.Message);
            }
        }

        private UITextureAtlas LoadResources()
        {
            try
            {
                if (_playItAtlas == null)
                {
                    string[] spriteNames = new string[]
                    {
                        "playit"
                    };

                    _playItAtlas = ResourceLoader.CreateTextureAtlas("PlayItAtlas", spriteNames, "PlayIt.Icons.");

                    UITextureAtlas defaultAtlas = ResourceLoader.GetAtlas("Ingame");
                    Texture2D[] textures = new Texture2D[]
                    {
                        defaultAtlas["OptionBase"].texture,
                        defaultAtlas["OptionBaseFocused"].texture,
                        defaultAtlas["OptionBaseHovered"].texture,
                        defaultAtlas["OptionBasePressed"].texture,
                        defaultAtlas["OptionBaseDisabled"].texture
                    };

                    ResourceLoader.AddTexturesInAtlas(_playItAtlas, textures);
                }

                return _playItAtlas;
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ModManager:LoadResources -> Exception: " + e.Message);
                return null;
            }
        }

        private void CreateUI()
        {
            try
            {
                _buttonPanel = UIUtils.CreatePanel("PlayItButtonPanel");
                _buttonPanel.zOrder = 25;
                _buttonPanel.size = new Vector2(36f, 36f);
                _buttonPanel.eventMouseMove += (component, eventParam) =>
                {
                    if (eventParam.buttons.IsFlagSet(UIMouseButton.Right))
                    {
                        var ratio = UIView.GetAView().ratio;
                        component.position = new Vector3(component.position.x + (eventParam.moveDelta.x * ratio), component.position.y + (eventParam.moveDelta.y * ratio), component.position.z);

                        ModConfig.Instance.ButtonPositionX = component.absolutePosition.x;
                        ModConfig.Instance.ButtonPositionY = component.absolutePosition.y;
                        ModConfig.Instance.Save();
                    }
                };

                _button = UIUtils.CreateButton(_buttonPanel, "PlayItButton", _playItAtlas, "playit");
                _button.tooltip = "Play It!";
                _button.size = new Vector2(36f, 36f);
                _button.relativePosition = new Vector3(0f, 0f);
                _button.eventClick += (component, eventParam) =>
                {
                    if (!eventParam.used)
                    {
                        if (_mainPanel != null)
                        {
                            if (_mainPanel.isVisible)
                            {
                                _mainPanel.Hide();
                                ModConfig.Instance.ShowPanel = false;
                                ModConfig.Instance.Save();
                            }
                            else
                            {
                                _mainPanel.Show();
                                ModConfig.Instance.ShowPanel = true;
                                ModConfig.Instance.Save();
                            }
                        }

                        eventParam.Use();
                    }
                };
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ModManager:CreateUI -> Exception: " + e.Message);
            }
        }

        private void UpdateUI()
        {
            try
            {
                _buttonPanel.isVisible = ModConfig.Instance.ShowButton;
                _buttonPanel.absolutePosition = new Vector3(ModConfig.Instance.ButtonPositionX, ModConfig.Instance.ButtonPositionY);
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] ModManager:UpdateUI -> Exception: " + e.Message);
            }
        }
    }
}
