﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class Genre : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int GenreId { get; set; }

        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                OnPropertyChanged("Name");
            }
        }

        private List<Book> _booksList = new List<Book>();
        public List<Book> BooksList
        {
            get { return this._booksList; }
            set
            {
                this._booksList = value;
                OnPropertyChanged("BooksList");
            }
        }

        public Genre() { }
        public Genre(int id, string name)
        {
            GenreId = id;
            Name = name;
            BooksList = new List<Book>();
        }

        public void AddBook(Book book)
        {
            /* tarvitseeko tarkastaa että kirja on jo listalla?
            foreach (var item in BooksList)
            {
                if (item == book) return;
            } */
            BooksList.Add(book);
        }
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
