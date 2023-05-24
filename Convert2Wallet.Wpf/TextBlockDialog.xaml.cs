using System;
using System.Collections.Generic;
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
    /// Interaction logic for TextBlockDialog.xaml
    /// </summary>
    public partial class TextBlockDialog : Window
    {


        public TextBlockDialog()
        {
            InitializeComponent();
        }

        // Button zum Bestätigen
        private void Btn_Enter(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Button zum Abbrechen
        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            this.InputText.Text = "";
            this.Close();
        }
    }
}
