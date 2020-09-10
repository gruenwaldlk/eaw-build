namespace eaw.build.data.config.text.v1
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.example.org/eaw-translation/")]
    public class LocalisationType : object, System.ComponentModel.INotifyPropertyChanged {
    
        private TranslationType[] _translationDataField;
    
        private string _keyField;
    
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Translation", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public TranslationType[] TranslationData {
            get => _translationDataField;
            set {
                _translationDataField = value;
                RaisePropertyChanged("TranslationData");
            }
        }
    
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Key {
            get => _keyField;
            set {
                _keyField = value;
                RaisePropertyChanged("Key");
            }
        }
    
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = PropertyChanged;
            propertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}