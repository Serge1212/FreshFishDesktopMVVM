using Firebase.Database;
using FreshFishDesktopMVVM.Helpers;
using FreshFishDesktopMVVM.Models;
using FreshFishDesktopMVVM.Utilities;
using FreshFishDesktopMVVM.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text;

namespace FreshFishDesktopMVVM.ViewModels
{
    public class WorkersViewModel : ObservableObject
    {
        private WorkersHelper workersHelper = new WorkersHelper();
        static bool executed = true;
        public ObservableCollection<Workers> WorkersCollection { get; set; }
        private Workers _selectedWorker;
        public Workers SelectedWorker
        {
            get { return _selectedWorker; }
            set
            {
                _selectedWorker = value;
                OnPropertyChanged("SelectedWorker");
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??= new RelayCommand(OpenWindowForNewWorker);
            }
        }

        private RelayCommand editCommand;

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??= new RelayCommand(OpenWindowForSelectedWorkerEdit, (obj) => SelectedWorker != null);
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
                },
                 (obj) => WorkersCollection.Count > 0 && SelectedWorker != null);
            }
        }

        private void OpenWindowForSelectedWorkerEdit(object obj)
        {
            var selectedWorkertWindow = new SelectedWorkerWindow();
            var selectedWorkerViewModel = new SelectedWorkerViewModel(SelectedWorker);
            selectedWorkertWindow.DataContext = selectedWorkerViewModel;
            selectedWorkertWindow.Show();
        }

        private void OpenWindowForNewWorker(object obj)
        {
            var selectedWorkertWindow = new SelectedWorkerWindow();
            var selectedWorkerViewModel = new SelectedWorkerViewModel(null);
            selectedWorkertWindow.DataContext = selectedWorkerViewModel;
            selectedWorkertWindow.Show();


        }

        public WorkersViewModel()
        {
            if (executed)
            {
                WorkersCollection = workersHelper.client
                .Child("workers")
                .AsObservable<Workers>()
                .ObserveOnDispatcher()
                .AsObservableCollection();

                executed = false;
            }
        }
    }
}
