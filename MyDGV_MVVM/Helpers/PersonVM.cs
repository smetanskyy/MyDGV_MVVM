﻿using System;
using System.ComponentModel;

namespace MyDGV_MVVM.Helpers
{
    public class PersonVM : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    this.NotifyPropertyChanged("Surname");
                }
            }
        }

        private DateTime _birthdate;
        public DateTime Birthdate
        {
            get { return _birthdate; }
            set
            {
                if (_birthdate != value)
                {
                    _birthdate = value;
                    this.NotifyPropertyChanged("Birthdate");
                }
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    this.NotifyPropertyChanged("Email");
                }
            }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                if (_imageUrl != value)
                {
                    _imageUrl = value;
                    this.NotifyPropertyChanged("ImageUrl");
                }
            }
        }

        private bool _isMale;
        public bool IsMale
        {
            get { return _isMale; }
            set
            {
                if (_isMale != value)
                {
                    _isMale = value;
                    this.NotifyPropertyChanged("IsMale");
                }
            }
        }

        private bool _isFemale;
        public bool IsFemale
        {
            get { return _isFemale; }
            set
            {
                if (_isFemale != value)
                {
                    _isFemale = value;
                    this.NotifyPropertyChanged("IsFemale");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
