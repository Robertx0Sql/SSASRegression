using System.Drawing;

namespace SSASRegressionUI.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    public sealed partial class Settings {
        internal Point frmMain_Location;

        public Settings() {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }

        public Point frmEditMdx_Location { get; internal set; }
        public Size frmEditMdx_Size { get; internal set; }
        public bool frmEditMdx_Maximised { get; internal set; }
        public Point frmEditTests_Location { get; internal set; }
        public Size frmEditTests_Size { get; internal set; }
        public bool frmEditTests_Maximised { get; internal set; }
        public Size frmMain_Size { get; internal set; }
        public bool frmMain_Maximised { get; internal set; }

        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Add code to handle the SettingChangingEvent event here.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add code to handle the SettingsSaving event here.
        }
    }
}
