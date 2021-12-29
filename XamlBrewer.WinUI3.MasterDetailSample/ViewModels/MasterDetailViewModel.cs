﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using XamlBrewer.WinuI3.Services;

namespace XamlBrewer.WinUI3.ViewModels
{
    public partial class MasterDetailViewModel<T> : ObservableObject where T : IMasterDetail
    {
        private ObservableCollection<T> items = new ObservableCollection<T>();

        // Too bad these don't work (yet?).
        // [ObservableProperty]
        // [AlsoNotifyChangeFor(nameof(HasCurrent))]
        private T current;

        // [ObservableProperty]
        // [AlsoNotifyChangeFor(nameof(Items))]
        private string filter;

        public T Current
        {
            get => current;
            set
            {
                SetProperty(ref current, value);
                OnPropertyChanged(nameof(HasCurrent));
            }
        }

        public string Filter
        {
            get => filter;
            set
            {
                SetProperty(ref filter, value);
                OnPropertyChanged(nameof(Items));
            }
        }

        public ObservableCollection<T> Items => filter is null ? items : new ObservableCollection<T>(items.Where(i => i.ApplyFilter(filter)));

        public bool HasCurrent => current is not null;

        public T AddItem(T item)
        {
            items.Add(item);
            OnPropertyChanged(nameof(Items));

            return item;
        }

        public void RemoveItem(T item)
        {
            items.Remove(item);
            OnPropertyChanged(nameof(Items));
        }
    }
}