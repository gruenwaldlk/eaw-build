namespace eaw.build.data.config.mod.v1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class BundleType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private DirectoryType[] directoryField;

        private string[] fileField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Directory")]
        public DirectoryType[] Directory
        {
            get { return this.directoryField; }
            set
            {
                this.directoryField = value;
                this.RaisePropertyChanged("Directory");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("File")]
        public string[] File
        {
            get { return this.fileField; }
            set
            {
                this.fileField = value;
                this.RaisePropertyChanged("File");
            }
        }

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