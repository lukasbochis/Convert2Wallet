using Passbook.Generator;
using Passbook.Generator.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert2Wallet.Core
{
    public class Passbook
    {
        private StandardField _primaryField = new StandardField("", "", "");
        private StandardField _backfield = new StandardField("", "", "Created with Convert2Wallet");
        private LinkedList<StandardField> _secondaryFields = new LinkedList<StandardField>();        
        private LinkedList<StandardField> _auxFields = new LinkedList<StandardField>();

        private string _fileName;
        private string _logoText;
        private readonly PassStyle _passStyle = PassStyle.Generic;

        #region Properties

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public PassStyle PassStyle
        {
            get { return _passStyle; }
        }

        public string LogoText
        {
            get { return _logoText; }
        }

        public StandardField PrimaryField
        {
            get { return _primaryField; }
        }

        public StandardField BackField
        {
            get { return _backfield; }
        }

        public LinkedList<StandardField> SecondaryFields
        {
            get { return _secondaryFields; }
        }

        public LinkedList<StandardField> AuxFields
        {
            get { return _auxFields; }
        }

        #endregion

        #region Methods

        public void AddLogoText(string logoText)
        {
            _logoText = logoText;
        }

        public bool AddPrimaryField(string key, string label, string value)
        {
            _primaryField.Key = key;
            _primaryField.Label = label;
            _primaryField.Value = value;
            return true;
        }

        public bool AddSecondaryField(string key, string label, string value)
        {
            if (_secondaryFields.Count < 2)
            {
                StandardField secondaryField = new StandardField(key, label, value);
                _secondaryFields.AddLast(secondaryField);
                return true;
            }

            return false;
        }

        public bool AddAuxField(string key, string label, string value)
        {
            if (_auxFields.Count < 2)
            {
                StandardField secondaryField = new StandardField(key, label, value);
                _auxFields.AddLast(secondaryField);
                return true;
            }

            return false;
        }

        #endregion
    }
}
