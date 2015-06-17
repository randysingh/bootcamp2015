using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.Storage;
using Caliburn.Micro;
using DemoApp.Common.Common;
using DemoApp.Common.Interfaces;

namespace DemoApp.Common.ViewModels
{
    public class PictureViewModel : Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IStorageProvider _storageProvider;
        private ObservableCollection<string> _photos; 

        public PictureViewModel(INavigationService navigationService, IStorageProvider storageProvider)
        {
            _navigationService = navigationService;
            _storageProvider = storageProvider;
            _photos = new ObservableCollection<string>();
        }

        public PictureViewModel()
        {
            
        }

        protected override async void OnInitialize()
        {
            var photos = await _storageProvider.GetAllAvailablePhotos();
            Photos = new ObservableCollection<string>(photos);
        }

        public ObservableCollection<string> Photos
        {
            get { return _photos; }
            set
            {
                _photos = value;
                NotifyOfPropertyChange();
            }
        }

        public ICommand TakeSelfie
        {
            get { return new DelegateCommand(() =>
            {
                if (TakeSelfieEvent != null)
                {
                    TakeSelfieEvent(null, null);
                    
                }
            });}
        }

        public ICommand GoBackCommand
        {
            get { return new DelegateCommand(() => _navigationService.GoBack());}
        }

        public ICommand SavePhoto
        {
            get { return new DelegateCommand<IStorageFile>(async t =>
            {
                await _storageProvider.SavePhoto(t);
                Photos = new ObservableCollection<string>(await _storageProvider.GetAllAvailablePhotos());
            });}
        }

        public List<string> SelectedItems
        {
            get { return _selectedItems; }
            set { _selectedItems = value; }
        }

        public EventHandler TakeSelfieEvent;
        private List<string> _selectedItems;
    }
}
