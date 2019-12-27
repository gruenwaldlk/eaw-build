namespace eaw.build.data.config.mod.v2
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.example.org/eaw-build")]
    public partial class ModNameType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private string shortNameField;

        private string longNameField;

        /// <remarks/>
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
        public string LongName
        {
            get { return this.longNameField; }
            set
            {
                this.longNameField = value;
                this.RaisePropertyChanged("LongName");
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