﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

/// <summary>
/// Interaction logic for GenresListView.xaml
/// </summary>
namespace Library.ViewModel
{
    public class GenresListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Genre> _myGenreList = new ObservableCollection<Genre>();

        public ObservableCollection<Genre> MyGenreList
        {
            get { return this._myGenreList; }
            set
            {
                this._myGenreList = value;
                OnPropertyChanged("MyGenreList");
            }
        }

        public GenresListViewModel()
        {
            foreach (var item in Genres.GenresList)
            {
                MyGenreList.Add(item);
            }
        }
        /// <summary>
        /// Raises an event, when property is changed
        /// </summary>
        /// <param name="name">changed property</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
