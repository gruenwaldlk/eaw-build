namespace eaw.build.data.config.text.v1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.example.org/eaw-translation/")]
    [System.Xml.Serialization.XmlRootAttribute("LocalisationData", Namespace="http://www.example.org/eaw-translation/", IsNullable=false)]
    public class LocalisationDataType : object, System.ComponentModel.INotifyPropertyChanged {
    
        private LocalisationType[] _localisationField;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Localisation", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public LocalisationType[] Localisation {
            get => _localisationField;
            set {
                _localisationField = value;
                RaisePropertyChanged("Localisation");
            }
        }
    
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = PropertyChanged;
            propertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}