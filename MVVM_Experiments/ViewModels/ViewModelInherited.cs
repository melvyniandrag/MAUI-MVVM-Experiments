
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
namespace MVVM_Experiments.ViewModels
{
    public partial class ViewModelInherited : ViewModelBase
    {
        [ObservableProperty]
        int count = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        string lastName = "Cienfuegos";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        private string firstName = "Julian";

        public String FullName
        {
            get => String.Format("{0} {1}", FirstName, LastName);
        }

        [ObservableProperty]
        ObservableCollection<MyListItem> myItems = new();

        [ObservableProperty]
        ObservableCollection<object> selectedItems = new();

        [RelayCommand]
        private void OnChangeName()
        {
            ChangeName();
        }

        [RelayCommand]
        private void OnSelectionChanged()
        {
            Debug.WriteLine("SelectionChanged() called!");
            Debug.WriteLine($"number of selected items: {SelectedItems.Count}");
            foreach(var item in MyItems)
            {
                if (SelectedItems.Contains(item))
                {
                    item.IsSelected = true;
                }
                else
                {
                    item.IsSelected = false;
                }
            }
        }

        private bool CanChangeName()
        {
            return true;
        }

        private void ChangeName()
        {
            Debug.WriteLine("Change name called");
            Count++;
            if (Count % 2 == 0)
            {
                FirstName = "Julian";
                LastName = "Cienfuegos";
            }
            else
            {
                FirstName = "Florencia";
                LastName = "Cienfuegos";
            }
            if (Count > 10)
            {
                BaseProp = "Stop Poking Me!";
            }
            else
            {
                BaseProp = "You Clicked Me!";
            }
        }

        public ViewModelInherited()
        {
            MyItems.Add(new MyListItem());
            MyItems.Add(new MyListItem());
            MyItems.Add(new MyListItem());
            MyItems.Add(new MyListItem());
        }

        public class MyListItem : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged([CallerMemberName] string name = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

            private Color _bgColor = Color.FromRgb(0xFF, 0x00, 0x00);
            public Color BGColor
            {
                get {
                    if (IsSelected)
                    {
                        return Color.FromRgb(0x00, 0xFF, 0x00);
                    }
                    else
                    {
                        return Color.FromRgb(0xFF, 0x00, 0x00);
                    }
                }
            }


            private bool _isSelected = false;
            private string _name = "in nomini patri";
            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
            public bool IsSelected
            {
                get => _isSelected;
                set
                {
                    _isSelected = value;
                    if (value)
                    {
                        Name = "selected";
                    }
                    else
                    {
                        Name = "not selected";
                    }
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(BGColor));
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
    }
}
