namespace eaw.build.data.config.mod.v2
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.example.org/eaw-build")]
    public partial class LocalisationSettingsType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private string localisationProjectFileField;

        private LocalisationProjectVersionType localisationProjectVersionField;

        /// <remarks/>
        public string LocalisationProjectFile
        {
            get { return this.localisationProjectFileField; }
            set
            {
                this.localisationProjectFileField = value;
                this.RaisePropertyChanged("LocalisationProjectFile");
            }
        }

        /// <remarks/>
        public LocalisationProjectVersionType LocalisationProjectVersion
        {
            get { return this.localisationProjectVersionField; }
            set
            {
                this.localisationProjectVersionField = value;
                this.RaisePropertyChanged("LocalisationProjectVersion");
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