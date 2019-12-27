namespace eaw.build.data.config.mod.v2
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.example.org/eaw-build")]
    public partial class BuildSettingsType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private LocalisationSettingsType localisationSettingsField;

        private CookSettingsType cookSettingsField;

        /// <remarks/>
        public LocalisationSettingsType LocalisationSettings
        {
            get { return this.localisationSettingsField; }
            set
            {
                this.localisationSettingsField = value;
                this.RaisePropertyChanged("LocalisationSettings");
            }
        }

        /// <remarks/>
        public CookSettingsType CookSettings
        {
            get { return this.cookSettingsField; }
            set
            {
                this.cookSettingsField = value;
                this.RaisePropertyChanged("CookSettings");
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