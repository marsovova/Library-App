﻿using System;
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
using Library.Model;
using Library.ViewModel;

namespace Library.View
{
    /// <summary>
    /// Interaction logic for BooksListView.xaml
    /// </summary>
    public partial class BooksListView : UserControl
    {
        public BooksListView()
        {
            InitializeComponent();       
        }

        /// <summary>
        /// Reacting to the change of selection and showing the selected book in the adjacent view
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The instance containing the event data</param>
        private void LstBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book selected = (Book)lstBooks.SelectedItem;
            var mw = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(window => window is MainWindow) as MainWindow;
            
            mw.DataContext = new
            {
                collection = new BooksListViewModel(),
                detail = new BookDetailViewModel(selected)
            };
        }

    }
}
