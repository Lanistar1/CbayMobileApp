using CBayMobileApp.Helpers;
using CBayMobileApp.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CBayMobileApp.ViewModels.Common
{
    public class SelectItemPickerPopupViewModel : BaseViewModel
    {
        public List<SelectItemModel> SavedItemList;
        private ObservableCollection<SelectItemModel> itemListData;
        public ObservableCollection<SelectItemModel> ItemListData
        {
            get => itemListData;
            set
            {
                itemListData = value;
                OnPropertyChanged(nameof(ItemListData));
            }
        }
        private string selectedItem;
        public string SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private string searchEntry = string.Empty;
        public string SearchEntry
        {
            get => searchEntry;
            set
            {
                searchEntry = value;
                SearchEntryTextChangedCommand.Execute(searchEntry);
                OnPropertyChanged(nameof(SearchEntry));
            }
        }

        private bool isBusy { get; set; }
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));

            }
        }

        public EnumInputType InputType { get; set; }
        public static SelectItemPickerPopupViewModel Instance { get; private set; }

        public enum EnumInputType { Ok, YesNo, OkCancel, OkCancelWithInput }
        public enum EnumOutputType { Ok, Yes, No, Cancel }
        public Tuple<string> ReturnValue;

        public SelectItemPickerPopupViewModel(INavigation navigation, List<SelectItemModel> items)
        {
            Instance = this;
            Navigation = navigation;
            HandlePageBindings(items);
            CancelCommand = new Command(async () => await CancelCommandExecute());
            OkCommand = new Command(async () => await OkCommandExecute());

            SelectItemCommand = new Command<SelectItemModel>(async (model) => await SelectItemCommandExecute(model));
        }

        private void HandlePageBindings(List<SelectItemModel> _items)
        {
            ItemListData = _items.Count >= 1 ? new ObservableCollection<SelectItemModel>(_items) : null;
            SavedItemList = _items;
        }
        public async Task ClosePopUp(string selectItem)
        {
            if (!string.IsNullOrWhiteSpace(selectItem))
            {
                ReturnValue = new Tuple<string>(selectItem);
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            }
            else
            {
                ReturnValue = new Tuple<string>(string.Empty);
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
            }
        }


        public Command SelectItemCommand { get; protected set; }
        public Command OkCommand { get; protected set; }
        public Command CancelCommand { get; protected set; }
        public Command SearchEntryTextChangedCommand => new Command<string>((_entryTxt) => SearchBar_TextChanged(_entryTxt));

        public async Task CancelCommandExecute()
        {
            await ClosePopUp(string.Empty);
        }

        public async Task OkCommandExecute()
        {
            await ClosePopUp(SelectedItem);
        }

        public async Task SelectItemCommandExecute(SelectItemModel model)
        {
            SelectedItem = model.Value;
            await ClosePopUp(SelectedItem);
        }

        private void SearchBar_TextChanged(string _searchEntry)
        {
            if (_searchEntry.Length >= 1 && ItemListData.Count() >= 1)
            {

                var _newList = ItemListData.Where(x => x.Value.Contains(_searchEntry, StringComparison.OrdinalIgnoreCase));
                ItemListData = new ObservableCollection<SelectItemModel>(_newList);
            }
            else if (SavedItemList != null)
            {
                ItemListData = null;
                ItemListData = new ObservableCollection<SelectItemModel>(SavedItemList);
            }
            else
            {
                return;
            }
        }

    }

}
