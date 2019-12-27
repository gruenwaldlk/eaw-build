namespace eaw.build.data.config.mod.v1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DirectoryType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private string filePatternField;

        private string recurseField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FilePattern
        {
            get { return this.filePatternField; }
            set
            {
                this.filePatternField = value;
                this.RaisePropertyChanged("FilePattern");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Recurse
        {
            get { return this.recurseField; }
            set
            {
                this.recurseField = value;
                this.RaisePropertyChanged("Recurse");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get { return this.valueField; }
            set
            {
                this.valueField = value;
                this.RaisePropertyChanged("Value");
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