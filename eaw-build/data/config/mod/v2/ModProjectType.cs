namespace eaw.build.data.config.mod.v2
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.example.org/eaw-build")]
    [System.Xml.Serialization.XmlRootAttribute("ModProject", Namespace = "http://www.example.org/eaw-build",
        IsNullable = false)]
    public partial class ModProjectType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private ModInfoType modInfoField;

        private BuildSettingsType buildSettingsField;

        /// <remarks/>
        public ModInfoType ModInfo
        {
            get { return this.modInfoField; }
            set
            {
                this.modInfoField = value;
                this.RaisePropertyChanged("ModInfo");
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