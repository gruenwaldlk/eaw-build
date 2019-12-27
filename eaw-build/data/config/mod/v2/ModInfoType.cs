namespace eaw.build.data.config.mod.v2
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.example.org/eaw-build")]
    public partial class ModInfoType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private ModNameType nameField;

        private ModDescriptionType descriptionField;

        private ModVersionType versionField;

        private string iconPathField;

        private SteamMetaDataType steamMetaDataField;

        /// <remarks/>
        public ModNameType Name
        {
            get { return this.nameField; }
            set
            {
                this.nameField = value;
                this.RaisePropertyChanged("Name");
            }
        }

        /// <remarks/>
        public ModDescriptionType Description
        {
            get { return this.descriptionField; }
            set
            {
                this.descriptionField = value;
                this.RaisePropertyChanged("Description");
            }
        }

        /// <remarks/>
        public ModVersionType Version
        {
            get { return this.versionField; }
            set
            {
                this.versionField = value;
                this.RaisePropertyChanged("Version");
            }
        }

        /// <remarks/>
        public string IconPath
        {
            get { return this.iconPathField; }
            set
            {
                this.iconPathField = value;
                this.RaisePropertyChanged("IconPath");
            }
        }

        /// <remarks/>
        public SteamMetaDataType SteamMetaData
        {
            get { return this.steamMetaDataField; }
            set
            {
                this.steamMetaDataField = value;
                this.RaisePropertyChanged("SteamMetaData");
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