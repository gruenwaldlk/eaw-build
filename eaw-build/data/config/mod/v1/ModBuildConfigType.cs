namespace eaw.build.data.config.mod.v1
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("ModBuildConfig", Namespace = "", IsNullable = false)]
    public partial class ModBuildConfigType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private ModSettingsType modSettingsField;

        private BuildSettingsType buildSettingsField;

        /// <remarks/>
        public ModSettingsType ModSettings
        {
            get { return this.modSettingsField; }
            set
            {
                this.modSettingsField = value;
                this.RaisePropertyChanged("ModSettings");
            }
        }

        /// <remarks/>
        public BuildSettingsType BuildSettings
        {
            get { return this.buildSettingsField; }
            set
            {
                this.buildSettingsField = value;
                this.RaisePropertyChanged("BuildSettings");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}