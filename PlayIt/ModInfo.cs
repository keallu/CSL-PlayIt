using ICities;
using System.Reflection;

namespace PlayIt
{
    public class ModInfo : IUserMod
    {
        public string Name => "Play It!";
        public string Description => "Allows to change different aspects of game simulation.";

        public void OnEnabled()
        {
            
        }

        public void OnDisabled()
        {
            
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group;
            bool selected;

            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();

            group = helper.AddGroup(Name + " - " + assemblyName.Version.Major + "." + assemblyName.Version.Minor);

            selected = ModConfig.Instance.ShowButton;
            group.AddCheckbox("Show Button", selected, sel =>
            {
                ModConfig.Instance.ShowButton = sel;
                ModConfig.Instance.Save();
            });

            group.AddSpace(10);

            group.AddButton("Reset Positioning of Panel", () =>
            {
                ModProperties.Instance.ResetPanelPosition();
            });

            group.AddSpace(10);

            group.AddButton("Reset Positioning of Button", () =>
            {
                ModProperties.Instance.ResetButtonPosition();
            });
        }
    }
}