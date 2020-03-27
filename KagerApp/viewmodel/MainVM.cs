using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Perception.Provider;
using Windows.UI.Xaml.Controls;
using ClassDemoKageLib.model;
using KagerApp.Annotations;
using KagerApp.common;
using KagerApp.model.persistence;

namespace KagerApp.viewmodel
{
    class MainVM:INotifyPropertyChanged
    {
        // backing fields for properties
        private ObservableCollection<Kage> _kager;
        private ICommand _load;
        private ICommand _save;
        private ICommand _updateOne;
        private ICommand _deleteOne;
        private ICommand _createOne;
        private ICommand _clearCreateOne;
        private Kage _selectedKage;
        private Kage _kageToBeCreated;


        // helping objects - not properties
        private IPersistence _persitence;

        public MainVM()
        {
            _kager = new ObservableCollection<Kage>();
            _selectedKage = new Kage();
            _kageToBeCreated = new Kage();

            // commands
            _load = new RelayCommand(LoadMethod);
            _save = new RelayCommand(SaveMethod);
            _updateOne = new RelayCommand(UpdateKage);
            _deleteOne = new RelayCommand(DeleteKage);
            _createOne = new RelayCommand(CreateKage);
            _clearCreateOne = new RelayCommand(ClearCreate);

            // helping
            _persitence = PersitenceFactory.GetPersistency(PersistenceType.Database);
            LoadMethod(); // initilize kager
        }

        /*
         * Properties
         */
        public Kage SelectedKage
        {
            get => _selectedKage;
            set
            {
                if (Equals(value, _selectedKage)) return;
                _selectedKage = value;
                OnPropertyChanged();
            }
        }

        public Kage KageToBeCreated
        {
            get => _kageToBeCreated;
            set
            {
                if (Equals(value, _kageToBeCreated)) return;
                _kageToBeCreated = value;
                OnPropertyChanged();
            }
        }

        /*
         * Only get properties
         */
        public ObservableCollection<Kage> Kager => _kager;

        public ICommand Load => _load;

        public ICommand Save => _save;

        public ICommand UpdateOne => _updateOne;

        public ICommand DeleteOne => _deleteOne;

        public ICommand CreateOne => _createOne;

        public ICommand ClearCreateOne => _clearCreateOne;


        /*
         * help methods to commands
         */
        private async void LoadMethod()
        {
            _kager.Clear();
            var liste = await _persitence.LoadKager();
            foreach (Kage k in liste)
            {
                _kager.Add(k);
            }
        }

        private void SaveMethod()
        {
            _persitence.SaveKager(_kager);
        }

        private void UpdateKage()
        {
            if (_selectedKage != null)
            {
                //todo give error message
                _persitence.UpdateKage(_selectedKage);
            }
        }

        private void DeleteKage()
        {
            if (_selectedKage != null)
            {
                //todo give error message
                _persitence.DeleteKage(_selectedKage);
                _kager.Remove(_selectedKage);
            }
        }

        private void CreateKage()
        {
            if (_kageToBeCreated != null && _kageToBeCreated.Id != -1)
            {
                //todo give error message
                _persitence.CreateKage(_kageToBeCreated);
                _kager.Add(_kageToBeCreated);
            }
        }

        private void ClearCreate()
        {
            KageToBeCreated = new Kage();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
