namespace eaw.build.data.config.mod.v1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ModSettingsType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private string nameField;

        private string shortNameField;

        private string versionField;

        private string releaseTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get { return this.nameField; }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("Name");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ShortName
        {
            get { return this.shortNameField; }
            set
            {
                this.shortNameField = value;
                this.RaisePropertyChanged("ShortName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Version
        {
            get { return this.versionField; }
            set
            {
                this.versionField = value;
                this.RaisePropertyChanged("Version");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ReleaseType
        {
            get { return this.releaseTypeField; }
            set
            {
                this.releaseTypeField = value;
                this.RaisePropertyChanged("ReleaseType");
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