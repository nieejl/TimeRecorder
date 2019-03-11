using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                OnPropertyChanged("SearchText");
            }
        }
        private T AutoCompleteItem;
        public T Item {
            get { return AutoCompleteItem; }
            set {
                AutoCompleteItem = value;
                OnPropertyChanged("Item");
            }
        }

        public ObservableCollection<string> Filtered { get; private set; }
        private List<T> FilteredItems { get; set; }
        private List<T> SearchableContent;
        private Func<T, string> ConversionFunction;

        public SearchBoxVM(List<T> items, Func<T, string> conversionFunction)
        {
            SearchableContent = items;
            FilteredItems = SearchableContent;
            ConversionFunction = conversionFunction;
        }

        public virtual void FilterItems()
        {
            FilteredItems = SearchableContent.FindAll(
                item => ConversionFunction(item).Contains(SearchText));
            Filtered = new ObservableCollection<string>(
                FilteredItems.Select(items => ConversionFunction(items)));
            if (FilteredItems.Count == 1)
                Item = FilteredItems[0];
            else
                Item = default(T);
        }


    }
}
