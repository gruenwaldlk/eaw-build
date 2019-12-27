namespace eaw.build.data.config.mod.v2
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.example.org/eaw-build")]
    public partial class CookSettingsType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private string outputDirectoryField;

        private BundleType[] bundleDefinitionsField;

        private MoveDefinitionsType moveDefinitionsField;

        /// <remarks/>
        public string OutputDirectory
        {
            get { return this.outputDirectoryField; }
            set
            {
                this.outputDirectoryField = value;
                this.RaisePropertyChanged("OutputDirectory");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Bundle", IsNullable = false)]
        public BundleType[] BundleDefinitions
        {
            get { return this.bundleDefinitionsField; }
            set
            {
                this.bundleDefinitionsField = value;
                this.RaisePropertyChanged("BundleDefinitions");
            }
        }

        /// <remarks/>
        public MoveDefinitionsType MoveDefinitions
        {
            get { return this.moveDefinitionsField; }
            set
            {
                this.moveDefinitionsField = value;
                this.RaisePropertyChanged("MoveDefinitions");
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