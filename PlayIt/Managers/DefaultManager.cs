using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayIt.Managers
{
    public class DefaultManager
    {
        private readonly Dictionary<string, object> _defaults = new Dictionary<string, object>();

        private static DefaultManager instance;

        public static DefaultManager Instance
        {
            get
            {
                return instance ?? (instance = new DefaultManager());
            }
        }

        public void Initialize()
        {
            try
            {
                _defaults.Clear();

                _defaults.Add("Latitude", DayNightProperties.instance.m_Latitude);
                _defaults.Add("Longitude", DayNightProperties.instance.m_Longitude);
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] DefaultManager:Initialize -> Exception: " + e.Message);
            }
        }

        public object Get(string name)
        {
            object obj = null;

            try
            {
                if (_defaults == null || _defaults.Count < 1)
                {
                    Initialize();
                }

                _defaults.TryGetValue(name, out object value);

                if (value != null)
                {
                    obj = value;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Play It!] DefaultManager:Get -> Exception: " + e.Message);
            }

            return obj;
        }
    }
}
