﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Library.View;
using Library.Model;
using Library.ViewModel;

namespace Library
{
    /*
    * author: Markéta Sovová, M1482@student.jamk.fi
    * author: Arttu Rousku, M1484@student.jamk.fi
    * time: 31/5/2019 18:22 PM
    * 
    * Library program provides a tool to store library data, which include books and their
    * associated authors and genres. Application enables data addition, removal and update.
    */



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            MSAConnectionDB.LoadData();
            //DatabaseConnection.ReadDataFromDB();
            DataContext = new
            {
                collection = new BooksListViewModel(),
                detail = new BookDetailViewModel()
            };
            
        }

        private void BooksSelection_Selected(object sender, RoutedEventArgs e)
        {
            
            DataContext = new
            {
                collection = new BooksListViewModel(),      
                detail = new BookDetailViewModel()       
            }; 
        }

        private void AuthorsSelection_Selected(object sender, RoutedEventArgs e)
        {
            DataContext = new
            {
                collection = new AuthorsListViewModel(),
                detail = new AuthorDetailViewModel()
            };

        }

        private void GenresSelection_Selected(object sender, RoutedEventArgs e)
        {
            DataContext = new
            {
                collection = new GenresListViewModel(),
                detail = new GenreDetailViewModel()
            };
        }

        private void Item_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            ItemAdd itemAdd = new ItemAdd();
            itemAdd.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    DataContext = new
                    {
                        collection = new BooksListViewModel(),
                        detail = new BookDetailViewModel(Books.BooksList[0])
                    };
                    break;
                case 1:
                    DataContext = new
                    {
                        collection = new AuthorsListViewModel(),
                        detail = new AuthorDetailViewModel(Authors.AuthorsList[0])
                    };
                    break;
                case 2:
                    DataContext = new
                    {
                        collection = new GenresListViewModel(),
                        detail = new GenreDetailViewModel(Genres.GenresList[0])
                    };
                    break;
                default:
                    break;
            }
        }
        
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (LibraryModel.AreItemsRemoved() || LibraryModel.AreItemsUpdated())
            {
                if (MessageBox.Show("All unsaved changes will be lost. Are you sure you want to exit?", "Exit Program", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    LibraryModel.ChangesSaved();
                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckEmptyValues())
            {
                if (MSAConnectionDB.SaveDataToDB()) MessageBox.Show("Changes saved");
                else MessageBox.Show("No changes");
            }           
        }

        // this is probably not necessary, as there are other mechanisms to check for empty values
        private bool CheckEmptyValues()
        {
            bool isEmptyField = false;
            foreach  (Genre genre in Genres.GenresList)
            {
                if (string.IsNullOrEmpty(genre.Name))
                {
                    MessageBox.Show("The name of a genre cannot be empty");
                    isEmptyField = true;
                }
            }
            foreach (Author author in Authors.AuthorsList)
            {
                if (string.IsNullOrEmpty(author.LastName))
                {
                    MessageBox.Show("The author's last name cannot be empty");
                    isEmptyField = true;
                }
            }
            foreach (Book book in Books.BooksList)
            {
                if (string.IsNullOrEmpty(book.Title))
                {
                    MessageBox.Show("The book's title cannot be empty");
                    isEmptyField = true;
                }
            }
            return isEmptyField;
        }   

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (LibraryModel.AreItemsRemoved() || LibraryModel.AreItemsUpdated())
            {
                if (MessageBox.Show("All unsaved changes will be lost. Are you sure you want to exit?", "Exit Program", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }          
            }
        }
    }
}