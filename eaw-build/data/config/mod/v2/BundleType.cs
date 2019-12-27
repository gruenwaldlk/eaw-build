namespace eaw.build.data.config.mod.v2
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.example.org/eaw-build")]
    public partial class BundleType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private string nameField;

        private BundleContentType contentField;

        /// <remarks/>
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
        public BundleContentType Content
        {
            get { return this.contentField; }
            set
            {
                this.contentField = value;
                this.RaisePropertyChanged("Content");
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