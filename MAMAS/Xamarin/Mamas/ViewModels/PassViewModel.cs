using Mamas.MamasApiRest;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Mamas.ViewModels
{
    class PassViewModel : ViewModelBase
    {
        MamasApi mamas;

        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        private string _pass;
        private string _textoPass;
        string usernameCompartido;

        public DelegateCommand loginCommand { get; set; }

        public string Pass
        {
            get { return _pass; }
            set { SetProperty(ref _pass, value); }
        }

        public string TextoPass
        {
            get { return _textoPass; }
            set { SetProperty(ref _textoPass, value); }
        }
        public PassViewModel(INavigationService navigationService, IPageDialogService dialogService)
    : base(navigationService)
        {
            mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            _navigationService = navigationService;
            _dialogService = dialogService;

            usernameCompartido = LoginViewModel.usuarioCompartir.UsernameUsuario;

            ComprobarEstadoPassword();

            loginCommand = new DelegateCommand(Login);
        }

        private void ComprobarEstadoPassword()
        {           
            var resp = mamas.UsuariosGet(usernameCompartido);

            if (resp.EsError)
                _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
            else 
            {
                if (resp.PassUsuario.Equals("x"))
                {
                    TextoPass = "¡Es la primera vez que entras aqui! Por favor, elige una contraseña";
                }
                else
                {
                    TextoPass = "Escribe tu contraseña";
                }
            }
        }

        void Login()
        {
            if(TextoPass.Equals("¡Es la primera vez que entras aqui! Por favor, elige una contraseña"))
            {
                if(String.IsNullOrEmpty(Pass))
                    _dialogService.DisplayAlertAsync("Alert", "Debes rellenar todos los campos", "OK");
                else
                {
                    if(Pass.Length<=5)
                        _dialogService.DisplayAlertAsync("Alert", "La contraseña debe tener mas de 5 caracteres", "OK");
                    else
                    {
                        var resp = mamas.UsuariosPut(LoginViewModel.usuarioCompartir.IdUsuario, EncryptSHA1(Pass), LoginViewModel.usuarioCompartir.IdPerfil);
                        if (resp.EsError)
                            _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                        else
                        {
                            if (resp.IdPerfil == 3)
                                _navigationService.NavigateAsync("ModulosAdminPage");
                            else
                            {
                                if (resp.IdPerfil == 1)
                                    _navigationService.NavigateAsync("ElegirHijoPage");
                                else
                                    _navigationService.NavigateAsync("ElegirAlumnoPage");
                            }
                              
                        }
                        
                    }
                }

            }
            else
            {
                string passwordEncriptada = EncryptSHA1(Pass);

                var resp = mamas.UsuariosGet(usernameCompartido, passwordEncriptada);
                if (resp.EsError) //Si ya está insertada esa asignatura
                    _dialogService.DisplayAlertAsync("Alert", resp.Error.title, "OK");
                else
                {
                    if (resp.IdPerfil == 3)
                        _navigationService.NavigateAsync("ModulosAdminPage");
                    else
                    {
                        if (resp.IdPerfil == 1)
                            _navigationService.NavigateAsync("ElegirHijoPage");
                        else
                            _navigationService.NavigateAsync("ElegirAlumnoPage");
                    }
                }
            }
        }

        private string EncryptSHA1(string password)
        {
            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            Byte[] textOriginal = ASCIIEncoding.Default.GetBytes(password);
            Byte[] hash = sha1.ComputeHash(textOriginal);
            StringBuilder cadena = new StringBuilder();
            foreach (byte i in hash)
            {
                cadena.AppendFormat("{0:x2}", i);
            }
            return cadena.ToString();
        }
    }
}
