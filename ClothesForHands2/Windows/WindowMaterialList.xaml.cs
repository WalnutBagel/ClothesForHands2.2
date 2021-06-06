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
using ClothesForHands2.SystemAppClass;

namespace ClothesForHands2.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowMaterialList.xaml
    /// </summary>
    public partial class WindowMaterialList : Window
    {
        public WindowMaterialList()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AppFrame.SelectedFrame = FrmMain;
            FrmMain.Navigate(new Pages.ListOfMaterialPage());
        }
    }
}
