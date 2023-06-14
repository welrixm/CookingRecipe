using Coo.Components;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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


namespace Coo.Pages
{
    /// <summary>
    /// Логика взаимодействия для Recipe.xaml
    /// </summary>
    public partial class Recipe : Page
    {
        public static Recipe Instance { get; private set; }

        public double TotalCost
        {
            get { return (double)GetValue(TotalCostProperty); }
            set { SetValue(TotalCostProperty, value); }
        }

        public static readonly DependencyProperty TotalCostProperty =
            DependencyProperty.Register("TotalCost", typeof(double), typeof(Recipe));


        public IEnumerable<CookingStage> CookingStage
        {
            get { return (IEnumerable<CookingStage>)GetValue(CookingStageProperty); }
            set { SetValue(CookingStageProperty, value); }
        }

        public static readonly DependencyProperty CookingStageProperty =
            DependencyProperty.Register("CookingStage", typeof(IEnumerable<CookingStage>), typeof(Recipe));

        public IEnumerable<IngredientOfStage> IngredientOfStage
        {
            get { return (IEnumerable<IngredientOfStage>)GetValue(IngredientOfStageProperty); }
            set { SetValue(IngredientOfStageProperty, value); }
        }

        public static readonly DependencyProperty IngredientOfStageProperty =
            DependencyProperty.Register("IngredientOfStage", typeof(IEnumerable<IngredientOfStage>), typeof(Recipe));

        

        public Dish DishObject
        {
            get { return (Dish)GetValue(DishesProperty); }
            set { SetValue(DishesProperty, value); }
        }

        public Dish Dish { get; }

        public static readonly DependencyProperty DishesProperty =
            DependencyProperty.Register("DishObject", typeof(Dish), typeof(Recipe));

        public int AllCount;
        //Components.Dish dish;
        public Recipe(Components.Dish _dish)
        {
            DBConnect.db.IngredientOfStage.Load();
            IngredientOfStage = DBConnect.db.IngredientOfStage.Local;
            CookingStage = _dish.CookingStage;
            IngredientOfStage = _dish.IngredientOfStage;
            DishObject = _dish;
            InitializeComponent();
            
            //dish = _dish;
            //DataContext = dish;
            Instance = this;
            Dish = _dish;
            CulcCostDishWithCount();

            //IngredList.ItemsSource = DBConnect.db.IngredientOfStage.ToList();
        }

        public void CulcCostDishWithCount()
        {
            if (CountCulc == null || int.TryParse(CountCulc.Text.Trim(), out AllCount) == false || Dish == null)
            {
                CountCulc.Text = "1";
                return;
            }
             TotalCost = Dish.TotalSumDish * AllCount;
        }
        private void TimeTbx_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        

        private void Less_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(CountCulc.Text.Trim()) <= 1)
            {
                CountCulc.Text = "1";
                return;
            }
            CountCulc.Text = (int.Parse(CountCulc.Text) - 1).ToString();
        }   

        //private void Count_TextChanged(object sender, TextChangedEventArgs e)
        //{
            
        //}

        private void More_Click(object sender, RoutedEventArgs e)
        {
            CountCulc.Text = (int.Parse(CountCulc.Text) + 1).ToString();
        }

        private void CountCulc_TextChanged(object sender, TextChangedEventArgs e)
        {
            CulcCostDishWithCount();
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Dish.RecipeLink.ToString());
        }
    }
}
