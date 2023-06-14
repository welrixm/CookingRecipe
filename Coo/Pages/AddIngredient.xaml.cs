using Coo.Components;
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
using System.Data.Entity;
using System.IO;
using System.Data.Entity.Core.Common;
using System.Text.RegularExpressions;

namespace Coo.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddIngredient.xaml
    /// </summary>
    public partial class AddIngredient : Page
    {
        Components.Ingredient ingredient;
        public AddIngredient(Components.Ingredient _ingredient)
        {
            InitializeComponent();
            ingredient = _ingredient;
            DataContext = ingredient;
            //FilmCbx.ItemsSource = DBConnect.db.Ingredient.ToList();
            //FilmCbx.DisplayMemberPath = "Name";
            UnitCbx.ItemsSource = DBConnect.db.Unit.ToList();
            UnitCbx.DisplayMemberPath = "Name";
        }
        

        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            Navigation.BackPage();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NameTb.Text.Length > 0 && CostTb.Text.Length > 0 && CountTb.Text.Length > 0 && UnitCbx.Text.Length > 0 && FridgeTb.Text.Length > 0)
            {
                if (ingredient.Id == 0)
                {

                    DBConnect.db.Ingredient.Add(ingredient);

                }
                if (MessageBox.Show("Вы точно хотите сохранить", "Уведомления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DBConnect.db.SaveChanges();
                    MessageBox.Show("Успешно сохранено!");
                    Navigation.NextPage(new Nav("Ингредиенты", new SpisokIngridient()));
                }
            }
            else
            {
                MessageBox.Show("Пусто! Пожалуйста, заполните все поля!");

                
            }
            

        }

        private void NameTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CostTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void FridgeTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
