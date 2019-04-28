namespace Versiones.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class UsersViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<User> users;
        private List<User> usersList;
        private ObservableCollection<Land> lands;
        private List<Land> landsList;
        private bool isRefreshing;
        private string filter;
        #endregion

        #region Properties

        public ObservableCollection<User> Users
        {
            get { return this.users; }
            set { SetValue(ref this.users, value); }
        }


        public List<User> UsersList
        {
            get { return this.usersList; }
            set { SetValue(ref this.usersList, value); }
        }
        public ObservableCollection<Land> Lands
        {
            get { return this.lands; }
            set { SetValue(ref this.lands, value); }
        }


        public List<Land> LandsList
        {
            get { return this.landsList; }
            set { SetValue(ref this.landsList, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }
        #endregion

        #region Constructors
        public UsersViewModel()
        {
            this.apiService = new ApiService();
         //   this.LoadLands();
            this.LoadUsers();
        }


        #endregion

        #region Methods

      

    private async void LoadUsers()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<User>(
                "http://192.168.1.77:1812/xad/login",
                "/jalax",
                "/34819650L");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            this.UsersList = (List<User>)response.Result;
            this.Users = new ObservableCollection<User>(this.UsersList);
            this.IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadUsers);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Users = new ObservableCollection<User>(this.UsersList);
            }
            else
            {
                this.Users = new ObservableCollection<User>(this.UsersList.Where(
                        l => l.Usuario.ToLower().Contains(this.Filter.ToLower()) ||
                             l.Nombre.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion
    }
}