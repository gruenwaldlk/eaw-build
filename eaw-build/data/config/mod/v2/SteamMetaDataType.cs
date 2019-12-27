namespace eaw.build.data.config.mod.v2
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.example.org/eaw-build")]
    public partial class SteamMetaDataType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private string publishedFileIdField;

        private SteamVisibilityType visibilityField;

        private SteamGameModeType gameModeField;

        private SteamTagType[] tagsField;

        private string contentFolderField;

        private string additionalMetaDataField;

        /// <remarks/>
        public string PublishedFileId
        {
            get { return this.publishedFileIdField; }
            set
            {
                this.publishedFileIdField = value;
                this.RaisePropertyChanged("PublishedFileId");
            }
        }

        /// <remarks/>
        public SteamVisibilityType Visibility
        {
            get { return this.visibilityField; }
            set
            {
                this.visibilityField = value;
                this.RaisePropertyChanged("Visibility");
            }
        }

        /// <remarks/>
        public SteamGameModeType GameMode
        {
            get { return this.gameModeField; }
            set
            {
                this.gameModeField = value;
                this.RaisePropertyChanged("GameMode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Tag", IsNullable = false)]
        public SteamTagType[] Tags
        {
            get { return this.tagsField; }
            set
            {
                this.tagsField = value;
                this.RaisePropertyChanged("Tags");
            }
        }

        /// <remarks/>
        public string ContentFolder
        {
            get { return this.contentFolderField; }
            set
            {
                this.contentFolderField = value;
                this.RaisePropertyChanged("ContentFolder");
            }
        }

        /// <remarks/>
        public string AdditionalMetaData
        {
            get { return this.additionalMetaDataField; }
            set
            {
                this.additionalMetaDataField = value;
                this.RaisePropertyChanged("AdditionalMetaData");
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