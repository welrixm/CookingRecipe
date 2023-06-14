using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Coo.Components;

namespace Coo.Pages
{
    partial class SpisokIngridient
    {
        public int TotalNumberPage
        {
            get
            {
                return (int)GetValue(TotalNumberPageProperty);
            }
            set
            {
                SetValue(TotalNumberPageProperty, value); 
            }
             
        }
        public static readonly DependencyProperty TotalNumberPageProperty =
        DependencyProperty.Register("TotalNumberPage", typeof(int), typeof(SpisokIngridient));
        public int NumberPage // номер страницы на которой находится пользователь
        {
            get { return (int)GetValue(NumberPageProperty); }
            set { SetValue(NumberPageProperty, value); }
        }

        public static readonly DependencyProperty NumberPageProperty =
           DependencyProperty.Register("NumberPage", typeof(int), typeof(SpisokIngridient));
        public IEnumerable<Ingredient> Ingredient // все записи
        {
            get { return (IEnumerable<Ingredient>)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("Ingredient", typeof(IEnumerable<Ingredient>), typeof(SpisokIngridient));
        public List<int> NumberEntriestOnOnePage // количество всех записей 
        {
            get { return (List<int>)GetValue(NumberEntriestOnOnePageProperty); }
            set { SetValue(NumberEntriestOnOnePageProperty, value); }
        }

        public static readonly DependencyProperty NumberEntriestOnOnePageProperty =
            DependencyProperty.Register("NumberEntriestOnOnePage", typeof(List<int>), typeof(SpisokIngridient));
    }
}
