using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Coo.Components;

namespace Coo.Pages
{
    /// <summary>
    /// Логика взаимодействия для SpisokBlud.xaml
    /// </summary>
    public partial class SpisokBlud : Page
    {
        DishesEntities _context = new DishesEntities();
        public SpisokBlud()
        {
            InitializeComponent();
            List<Category> listCategories = _context.Category.ToList();
            listCategories.Insert(0, new Category { Name = "Все категории" });
            CmbCategory.ItemsSource = listCategories;
            CmbCategory.SelectedIndex = 0;
            RefreshData();

           
        }

        private void RefreshData()
        {
            List<Dish> listDishes = _context.Dish.ToList();
            if (CmbCategory.SelectedIndex !=0)
            {
                Category selectedCategory = (Category)CmbCategory.SelectedItem;
                listDishes = listDishes.Where(x => x.CategoryId == selectedCategory.Id).ToList();
            }
            var searchString = TxtSearch.Text;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                listDishes = listDishes.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            listDishes = listDishes.OrderByDescending(x => x.ServingPrice).ToList();
            LViewDishes.ItemsSource = listDishes;
        }
        

        private void CmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            RefreshData();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshData();
        }

        //private void LViewDishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        //private void Recipe_Click(object sender, RoutedEventArgs e)
        //{
            
        //}

        private void LViewDishes_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //var selRecipe = LViewDishes.SelectedItem as Dish;
            Navigation.NextPage(new Nav("Рецепт", new Recipe(LViewDishes.SelectedItem as Dish)));
        }
    }
}
