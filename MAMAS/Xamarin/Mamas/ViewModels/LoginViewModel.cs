using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using XamForms = Xamarin.Forms;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mamas.MamasApiRest;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using API_ProyectoPracticas.Models;

namespace Mamas.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        private string _username;

        public static Usuarios usuarioCompartir { get; set; }
        
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        
        public DelegateCommand submitLoginCommand { get; set; }


        public LoginViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            submitLoginCommand = new DelegateCommand(CompruebaLogin);
            Title = "Login";
        }

        async void CompruebaLogin()
        {
            var mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            var resp = mamas.UsuariosGet(Username);
            if (resp.EsError) //Si ya está insertada esa asignatura
            {
                await _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                Username = String.Empty;
            }
                
            else
            {
                Usuarios usuario = new Usuarios();
                usuario.IdPerfil = resp.IdPerfil;
                usuario.UsernameUsuario = Username;
                usuario.PassUsuario = resp.PassUsuario;
                usuario.IdUsuario = resp.IdUsuario;
                usuarioCompartir = usuario;
                await _navigationService.NavigateAsync("PassPage");
            }
                     
                      
        }

    }
}
