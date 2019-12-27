namespace eaw.build.data.config.mod.v2
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.example.org/eaw-build")]
    public partial class BundleContentType : object, System.ComponentModel.INotifyPropertyChanged
    {
        private DirectoryType[] directoriesField;

        private string[] filesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Directory", IsNullable = false)]
        public DirectoryType[] Directories
        {
            get { return this.directoriesField; }
            set
            {
                this.directoriesField = value;
                this.RaisePropertyChanged("Directories");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("File", IsNullable = false)]
        public string[] Files
        {
            get { return this.filesField; }
            set
            {
                this.filesField = value;
                this.RaisePropertyChanged("Files");
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