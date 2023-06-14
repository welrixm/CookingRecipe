using Coo.Components;
using Coo.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            Navigation.main = this;
            Navigation.NextPage(new Nav("Список блюд", new SpisokBlud()));
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.BackPage();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.navs.Clear();
            Navigation.NextPage(new Nav("Список блюд", new SpisokBlud()));
        }

        private void IngredientBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Список ингредиентов", new SpisokIngridient()));
        }

        private void DishBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Список блюд", new SpisokBlud()));
        }
    }
}
