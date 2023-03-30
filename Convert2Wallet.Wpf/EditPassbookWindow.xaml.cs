using Convert2Wallet.Core;
using Convert2Wallet_Core;
using Microsoft.Win32;
using Passbook.Generator.Fields;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Convert2Wallet.Wpf
{
    /// <summary>
    /// Interaction logic for EditPassbookWindow.xaml
    /// </summary>
    public partial class EditPassbookWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Fields

        private string _txt_logoText = "";
        private string _txt_primaryHeader = "";
        private string _txt_primaryField = "";
        private string _txt_secondaryHeaderOne = "";
        private string _txt_secondaryFieldOne = "";
        private string _txt_secondaryHeaderTwo = "";
        private string _txt_secondaryFieldTwo = "";

        private string _txt_auxHeaderOne = "";
        private string _txt_auxFieldOne = "";
        private string _txt_auxHeaderTwo = "";
        private string _txt_auxFieldTwo = "";
        private string _txt_auxHeaderThree = "";
        private string _txt_auxFieldThree = "";
        private string _txt_auxHeaderFour = "";
        private string _txt_auxFieldFour = "";
        private string _txt_auxHeaderFive = "";
        private string _txt_auxFieldFive = "";
        private string _txt_auxHeaderSix = "";
        private string _txt_auxFieldSix = "";

        #endregion

        #region Properties

        public string LogoText
        {
            get { return _txt_logoText; }
            set
            {
                _txt_logoText = value;
                OnPropertyChanged("LogoText");
            }
        }

        public string PrimaryHeader
        {
            get { return _txt_primaryHeader; }
            set
            {
                _txt_primaryHeader = value;
                OnPropertyChanged("PrimaryHeader");
            }
        }
        public string PrimaryField
        {
            get { return _txt_primaryField; }
            set
            {
                _txt_primaryField = value;
                OnPropertyChanged("PrimaryField");
            }
        }
        public string SecondaryHeaderOne
        {
            get { return _txt_secondaryHeaderOne; }
            set
            {
                _txt_secondaryHeaderOne = value;
                OnPropertyChanged("SecondaryHeaderOne");
            }
        }
        public string SecondaryFieldOne
        {
            get { return _txt_secondaryFieldOne; }
            set
            {
                _txt_secondaryFieldOne = value;
                OnPropertyChanged("SecondaryFieldOne");
            }
        }
        public string SecondaryHeaderTwo
        {
            get { return _txt_secondaryHeaderTwo; }
            set
            {
                _txt_secondaryHeaderTwo = value;
                OnPropertyChanged("SecondaryHeaderTwo");
            }
        }
        public string SecondaryFieldTwo
        {
            get { return _txt_secondaryFieldTwo; }
            set
            {
                _txt_secondaryFieldTwo = value;
                OnPropertyChanged("SecondaryFieldTwo");
            }
        }

        public string AuxHeaderOne
        {
            get { return _txt_auxHeaderOne; }
            set
            {
                _txt_auxHeaderOne = value;
                OnPropertyChanged("AuxHeaderOne");
            }
        }
        public string AuxFieldOne
        {
            get { return _txt_auxFieldOne; }
            set
            {
                _txt_auxFieldOne = value;
                OnPropertyChanged("AuxFieldOne");
            }
        }
        public string AuxHeaderTwo
        {
            get { return _txt_auxHeaderTwo; }
            set
            {
                _txt_auxHeaderTwo = value;
                OnPropertyChanged("AuxHeaderTwo");
            }
        }
        public string AuxFieldTwo
        {
            get { return _txt_auxFieldTwo; }
            set
            {
                _txt_auxFieldTwo = value;
                OnPropertyChanged("AuxFieldTwo");
            }
        }
        public string AuxHeaderThree
        {
            get { return _txt_auxHeaderThree; }
            set
            {
                _txt_auxHeaderThree = value;
                OnPropertyChanged("AuxHeaderThree");
            }
        }
        public string AuxFieldThree
        {
            get { return _txt_auxFieldThree; }
            set
            {
                _txt_auxFieldThree = value;
                OnPropertyChanged("AuxFieldThree");
            }
        }
        public string AuxHeaderFour
        {
            get { return _txt_auxHeaderFour; }
            set
            {
                _txt_auxHeaderFour = value;
                OnPropertyChanged("AuxHeaderFour");
            }
        }
        public string AuxFieldFour
        {
            get { return _txt_auxFieldFour; }
            set
            {
                _txt_auxFieldFour = value;
                OnPropertyChanged("AuxFieldFour");
            }
        }
        public string AuxHeaderFive
        {
            get { return _txt_auxHeaderFive; }
            set
            {
                _txt_auxHeaderFive = value;
                OnPropertyChanged("AuxHeaderFive");
            }
        }
        public string AuxFieldFive
        {
            get { return _txt_auxFieldFive; }
            set
            {
                _txt_auxFieldFive = value;
                OnPropertyChanged("AuxFieldFive");
            }
        }
        public string AuxHeaderSix
        {
            get { return _txt_auxHeaderSix; }
            set
            {
                _txt_auxHeaderSix = value;
                OnPropertyChanged("AuxHeaderSix");
            }
        }
        public string AuxFieldSix
        {
            get { return _txt_auxFieldSix; }
            set
            {
                _txt_auxFieldSix = value;
                OnPropertyChanged("AuxFieldSix");
            }
        }


        #endregion


        public EditPassbookWindow()
        {
            InitializeComponent();
        }

        public EditPassbookWindow(int i)
        {
            InitializeComponent();
        }

        public EditPassbookWindow(string inputText)
        {
            char[] delimiters = { '.', ' ', ',', '!', '?', '\"', '»', '«', ':', '|', '(', ')' };
            string[] separatedInput = inputText.Split(delimiters);

            foreach (var word in separatedInput)
            {
                //Load sample data
                var sampleData = new WordDistincterModel.ModelInput()
                {
                    Value = word,
                };

                //Load model and predict output
                var result = WordDistincterModel.Predict(sampleData);

                if (result.PredictedLabel == "name" && result.Score[1] >= 0.90)
                {
                    PrimaryHeader = "Name";
                    PrimaryField = word;
                }
                else if (result.PredictedLabel == "datum" && result.Score[0] >= 0.90)
                {
                    SecondaryHeaderOne = "Datum";
                    SecondaryFieldOne = word;
                }
                    
            }


            InitializeComponent();
        }

        private void Btn_SaveAndGenerate(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Passbook File (*.pkpass)|*.pkpass";


            if (saveFileDialog.ShowDialog() == true)
            {
                Convert2Wallet.Core.Passbook passbook = new Convert2Wallet.Core.Passbook();

                if (LogoText != null && LogoText.Length != 0)
                    passbook.AddLogoText(LogoText);
                if (PrimaryField != null && PrimaryField.Length != 0)
                    passbook.AddPrimaryField("primary field", PrimaryHeader, PrimaryField);

                if (SecondaryFieldOne != null && SecondaryFieldOne.Length != 0)
                    passbook.AddSecondaryField("secondary field", SecondaryHeaderOne, SecondaryFieldOne);
                if (SecondaryFieldTwo != null && SecondaryFieldTwo.Length != 0)
                    passbook.AddSecondaryField("secondary field", SecondaryHeaderTwo, SecondaryFieldTwo);

                if (AuxFieldOne != null && AuxFieldOne.Length != 0)
                    passbook.AddAuxField("aux field", AuxHeaderOne, AuxFieldOne);
                if (AuxFieldTwo != null && AuxFieldTwo.Length != 0)
                    passbook.AddAuxField("aux field", AuxHeaderTwo, AuxFieldTwo);
                if (AuxFieldThree != null && AuxFieldThree.Length != 0)
                    passbook.AddAuxField("aux field", AuxHeaderThree, AuxFieldThree);
                if (AuxFieldFour != null && AuxFieldFour.Length != 0)
                    passbook.AddAuxField("aux field", AuxHeaderFour, AuxFieldFour);
                if (AuxFieldFive != null && AuxFieldFive.Length != 0)
                    passbook.AddAuxField("aux field", AuxHeaderFive, AuxFieldFive);
                if (AuxFieldSix != null && AuxFieldSix.Length != 0)
                    passbook.AddAuxField("aux field", AuxHeaderSix, AuxFieldSix);


                passbook.FileName = saveFileDialog.FileName;

                try
                {
                    PassbookCreator.GeneratePass(passbook);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Leider ist ein Fehler passiert. Bitte versuchen Sie es noch einmal.", ex.Message);
                }
            }
        }
    }
}
