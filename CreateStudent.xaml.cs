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

namespace LUZ_TREINAMENTO
{
    /// <summary>
    /// Lógica interna para CreateStudent.xaml
    /// </summary>
    public partial class CreateStudent : Window
    {
        public CreateStudent()
        {
            InitializeComponent();
        }

        public void BtnSave(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
