using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRecorder.ViewModels.Interfaces;

namespace TimeRecorder.ViewModels
{
    public class SearchBoxVM<T> : BaseViewModel, ISearchBoxVM
    {
        private string searchText;
        public string SearchText {
            get { return searchText; }
            set {
                searchText = value;
                OnPropertyChanged();
                FilterItems();
                ShowDropDown = true;
            }
        }

        private bool showDropDown = false;
        public bool ShowDropDown {
            get { return showDropDown; }
            set {
                showDropDown = value;
                OnPropertyChanged();
            }
        }

        private T autoCompleteItem;
        public T AutoCompleteItem {
            get { return autoCompleteItem; }
            set {
                autoCompleteItem = value;
                OnPropertyChanged("AutoCompleteItem");
            }
        }
        private ObservableCollection<string> filtered;
        public ObservableCollection<string> Filtered {
            get { return filtered; }
            private set {
                filtered = value;
                OnPropertyChanged();
            }
        }
        private List<T> FilteredItems { get; set; }
        private List<T> SearchableContent;
        private Func<T, string> ConversionFunction;

        public SearchBoxVM(List<T> items, Func<T, string> conversionFunction)
        {
            SearchableContent = items;
            FilteredItems = SearchableContent;
            ConversionFunction = conversionFunction;
            Filtered = new ObservableCollection<string>(
                FilteredItems.Select(item => ConversionFunction(item)));
        }

        public virtual void FilterItems()
        {
            FilteredItems = SearchableContent.FindAll(
                item => ConversionFunction(item).ToLower().Contains(SearchText.ToLower()));
            Filtered = new ObservableCollection<string>(
                FilteredItems.Select(item => ConversionFunction(item)));
            if (FilteredItems.Count == 1)
                AutoCompleteItem = FilteredItems[0];
            else
                AutoCompleteItem = default(T);
        }


    }
}
