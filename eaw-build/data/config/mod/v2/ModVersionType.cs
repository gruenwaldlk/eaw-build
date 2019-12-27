namespace eaw.build.data.config.mod.v2
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.example.org/eaw-build")]
    public partial class ModVersionType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private int majorField;

        private int minorField;

        private int patchField;

        private int buildField;

        /// <remarks/>
        public int Major
        {
            get { return this.majorField; }
            set
            {
                this.majorField = value;
                this.RaisePropertyChanged("Major");
            }
        }

        /// <remarks/>
        public int Minor
        {
            get { return this.minorField; }
            set
            {
                this.minorField = value;
                this.RaisePropertyChanged("Minor");
            }
        }

        /// <remarks/>
        public int Patch
        {
            get { return this.patchField; }
            set
            {
                this.patchField = value;
                this.RaisePropertyChanged("Patch");
            }
        }

        /// <remarks/>
        public int Build
        {
            get { return this.buildField; }
            set
            {
                this.buildField = value;
                this.RaisePropertyChanged("Build");
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