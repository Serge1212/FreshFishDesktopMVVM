using FreshFishDesktopMVVM.Helpers;
using FreshFishDesktopMVVM.Models;
using FreshFishDesktopMVVM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreshFishDesktopMVVM.ViewModels
{
    class SelectedWorkerViewModel : ObservableObject
    {
        #region Fields  
        private WorkersHelper workersHelper = new WorkersHelper();
        private bool edited = true;
        public bool isDeleteButtonHidden { get; set; }
        private Workers _selectedWorker;
        #endregion

        #region Properties
        public Workers SelectedWorker
        {
            get { return _selectedWorker; }
            set
            {
                _selectedWorker = value;
                OnPropertyChanged("SelectedWorker");
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??= new RelayCommand(SaveWorker, obj => SelectedWorker.CanSave == true);
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??= new RelayCommand(async obj =>
                {
                    Workers worker = obj as Workers;
                    if (worker != null)
                    {
                        await workersHelper.DeleteWorker(worker.W_id);
                    }
                });
            }
        }
        #endregion

        #region Helpers
        private async void SaveWorker(object obj)
        {
            if (edited == false)
            {
                await workersHelper.AddWorker(
                    SelectedWorker?.Name ?? "no data",
                    SelectedWorker?.Surname ?? "no data",
                    SelectedWorker?.Patronymic ?? "no data",
                    SelectedWorker?.Position ?? "no data",
                    SelectedWorker?.Salary ?? "0",
                    SelectedWorker?.PhoneNumber ?? "no data",
                    SelectedWorker?.Address ?? "no data",
                    SelectedWorker?.AdditionalInfo ?? "no data"



                    );
            }
            if (edited == true)
            {
                await workersHelper.UpdateWorker(
                    SelectedWorker.W_id,
                    SelectedWorker?.Name ?? "no data",
                    SelectedWorker?.Surname ?? "no data",
                    SelectedWorker?.Patronymic ?? "no data",
                    SelectedWorker?.Position ?? "no data",
                    SelectedWorker?.Salary ?? "0",
                    SelectedWorker?.PhoneNumber ?? "no data",
                    SelectedWorker?.Address ?? "no data",
                    SelectedWorker?.AdditionalInfo ?? "no data");
            }

        }

        #endregion
        public SelectedWorkerViewModel(Workers worker = null)
        {
            SelectedWorker = worker;
            if (worker == null)
            {
                SelectedWorker = new Workers();
                edited = false;
                isDeleteButtonHidden = false;
            }
            else
            {
                isDeleteButtonHidden = true;
            }

        }
    }
}
