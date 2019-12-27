namespace eaw.build.data.config.mod.v2
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.example.org/eaw-build")]
    public partial class DirectoryType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private string directoryPathField;

        private string fileSearchPatternField;

        private bool recursiveField;

        private bool recursiveFieldSpecified;

        /// <remarks/>
        public string DirectoryPath
        {
            get { return this.directoryPathField; }
            set
            {
                this.directoryPathField = value;
                this.RaisePropertyChanged("DirectoryPath");
            }
        }

        /// <remarks/>
        public string FileSearchPattern
        {
            get { return this.fileSearchPatternField; }
            set
            {
                this.fileSearchPatternField = value;
                this.RaisePropertyChanged("FileSearchPattern");
            }
        }

        /// <remarks/>
        public bool Recursive
        {
            get { return this.recursiveField; }
            set
            {
                this.recursiveField = value;
                this.RaisePropertyChanged("Recursive");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RecursiveSpecified
        {
            get { return this.recursiveFieldSpecified; }
            set
            {
                this.recursiveFieldSpecified = value;
                this.RaisePropertyChanged("RecursiveSpecified");
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