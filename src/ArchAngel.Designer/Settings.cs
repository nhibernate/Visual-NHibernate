using System.Configuration;
using System.Drawing;

namespace ArchAngel.Designer.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
	[SettingsProvider(typeof(Slyce.Common.AASettingsProvider))]
    internal sealed partial class Settings {
        
        public Settings() {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            //SettingChanging += SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }
       
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add code to handle the SettingsSaving event here.
        }

    	public Color BaseColourToUse
    	{
    		get
    		{
    			return UseThemeColour ? Slyce.Common.Colors.ThemeColor : BaseColour;
    		}
			set
			{
				if (!UseThemeColour)
				{
					Slyce.Common.Colors.BaseColor = value;
				}
				BaseColour = value;
			}
    	}
    }
}
