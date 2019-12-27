namespace eaw.build.data.config.mod.v1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LocalisationType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private string localisationFileField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LocalisationFile
        {
            get { return this.localisationFileField; }
            set
            {
                this.localisationFileField = value;
                this.RaisePropertyChanged("LocalisationFile");
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