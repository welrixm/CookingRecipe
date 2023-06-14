using Coo.Components;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mail;
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
    /// Логика взаимодействия для SpisokIngridient.xaml
    /// </summary>
    public partial class SpisokIngridient : Page
    {
        private int CountEntriestOnPage = 4;
        public int CountIngredient
        {
            get
            {
                return (int)GetValue(CountIngredientProperty);
            }
            set
            {
                SetValue(CountIngredientProperty, value);
            }
        }
        public static readonly DependencyProperty CountIngredientProperty = DependencyProperty.Register("CountIngredient", typeof(int), typeof(MainWindow));
        public decimal AllIngredient
        {
            get
            {
                return (decimal)GetValue(AllIngredientProperty);

            }
            set
            {
                SetValue(AllIngredientProperty, value);
            }
        }
        public static readonly DependencyProperty AllIngredientProperty = DependencyProperty.Register("AllIngredient", typeof(decimal), typeof(MainWindow));
       
        public static SpisokIngridient Instance { get; private set; }
        private IEnumerable<Ingredient> TestingIEnumerableIngredients;

        public SpisokIngridient()
        {
            DBConnect.db.Ingredient.Load();
            Ingredient = DBConnect.db.Ingredient.Local;
            
            TestingIEnumerableIngredients = Ingredient;
            NumberPage = 1;
            TotalNumberPage = 0;
            NumberEntriestOnOnePage = new List<int>();
            CallingMethodBeforeInitialization();
            AllIngredient = DBConnect.db.Ingredient.Sum(x => x.Cost);

            InitializeComponent();
           

            Instance = this;
            //DGridIngredient.ItemsSource = TestingIEnumerableIngredients.ToList();

        }
        private void CallingMethodBeforeInitialization()
        {
            ValidateCountIngredient();
            ValidateTotalCountPage();
            PageProcessing();

            
        }

        //private void SessionDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        //private void AddNewBtn_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void LinkEdit_Click(object sender, RoutedEventArgs e)
        {
            var selIngredient = (sender as Hyperlink).DataContext as Ingredient;
            Navigation.NextPage(new Nav("Редактирование ингредиента", new AddIngredient(selIngredient)));
     
        }

        private void LinkDelete_Click(object sender, RoutedEventArgs e)
        {

            var selIngredient = (sender as Hyperlink).DataContext as Ingredient;
            if (MessageBox.Show("Вы точно хотите удалить эту запись ", "Уведомление ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // удалить вообще из базы
               // DBConnect.db.Ingredient.Remove(selIngredient);

                var inredientStagesToRemove = DBConnect.db.IngredientOfStage.Where(ios => ios.IngredientId == selIngredient.Id);
                DBConnect.db.IngredientOfStage.RemoveRange(inredientStagesToRemove);

                DBConnect.db.Ingredient.Remove(selIngredient);

                //помечает на удаление 
                //selIngredient. = true;
                MessageBox.Show("Запись удалена");
                DBConnect.db.SaveChanges();
                //DGridIngredient.ItemsSource = DBConnect.db.Ingredient.ToList();
            }
            NumberPage = 1;
            ValidateCountIngredient();
            ValidateTotalCountPage();
            PageProcessing();

        }
        private void ValidateCountIngredient()
        {
            CountIngredient = TestingIEnumerableIngredients.Count();

        }
        private void ValidateTotalCountPage()
        {
            TotalNumberPage = (int)Math.Ceiling(Convert.ToDouble(TestingIEnumerableIngredients.Cast<Ingredient>().Count()) / Convert.ToDouble(CountEntriestOnPage));
            ValidateNumberEntriestOnOnePage();
        }
        private void PageProcessing()
        {
            Ingredient = TestingIEnumerableIngredients;

            Ingredient = Ingredient.Cast<Ingredient>()
                                   .Skip((NumberPage - 1) * CountEntriestOnPage)
                                   .Take(CountEntriestOnPage);
        }
        private void ValidateNumberEntriestOnOnePage()
        {
            int n = 6, sum = 0;

            if (NumberEntriestOnOnePage.Count() == 0)
                for (int i = 1; i <= n; i++)
                {
                    sum += i;
                    NumberEntriestOnOnePage.Add(sum);
                }
        }
        private void ValidateCountEntriestOnPage()
        {
            if (CbCount.SelectedItem == null)
                return;

            CountEntriestOnPage = (int)CbCount.SelectedItem;
        }
        private void BtnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            NumberPage = 1;

            PageProcessing();
        }

        private void BtnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if ((NumberPage - 1) <= 0)
                return;

            NumberPage--;

            PageProcessing();
        }

        private void BtnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (TotalNumberPage <= NumberPage)
                return;

            NumberPage++;

            PageProcessing();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            NumberPage = TotalNumberPage;

            PageProcessing();
        }

        private void BorderPlus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Navigation.NextPage(new Nav("Добавление ингредиента", new AddIngredient(new Ingredient())));
            
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //if (Visibility == Visibility.Visible)
            //{
            //    DBConnect.db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            //    DGridIngredient.ItemsSource = DBConnect.db.Ingredient.ToList();
            //}
        }

        private void CbCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateCountEntriestOnPage();
            ValidateTotalCountPage();

            if (NumberPage >= TotalNumberPage)
                NumberPage = TotalNumberPage;

            PageProcessing();
        }

        //private void LbTotalQuantity_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{

        //}
    }
}
