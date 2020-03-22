using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using XamForms = Xamarin.Forms;
using System.Collections.Generic;
using System.Text;
using Mamas.MamasApiRest;
using System.Security.Cryptography;

namespace Mamas.ViewModels.AdminViewModels
{
    class ConfPadresViewModel : ViewModelBase
    {
        MamasApi mamas;

        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        public DelegateCommand insertarPadreCommand { get; set; }

        private string _nombrePadre;
        private string _apellidosPadre;
        private string _direccionPadre;
        private string _telefonoPadre;

        public string NombrePadre
        {
            get { return _nombrePadre; }
            set { SetProperty(ref _nombrePadre, value); }
        }

        public string ApellidosPadre
        {
            get { return _apellidosPadre; }
            set { SetProperty(ref _apellidosPadre, value); }
        }

        public string DireccionPadre
        {
            get { return _direccionPadre; }
            set { SetProperty(ref _direccionPadre, value); }
        }

        public string TelefonoPadre
        {
            get { return _telefonoPadre; }
            set { SetProperty(ref _telefonoPadre, value); }
        }
        public ConfPadresViewModel(INavigationService navigationService, IPageDialogService dialogService)
    : base(navigationService)
        {
            mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            _dialogService = dialogService;

            _nombrePadre = String.Empty;
            _apellidosPadre = String.Empty;
            _direccionPadre = String.Empty;
            _telefonoPadre = String.Empty;

            insertarPadreCommand = new DelegateCommand(InsertarPadre);
        }

        private void InsertarPadre()
        {
            //Miramos si están o no los campos vacios
            if (String.IsNullOrEmpty(NombrePadre) || String.IsNullOrEmpty(ApellidosPadre) || String.IsNullOrEmpty(DireccionPadre) || String.IsNullOrEmpty(TelefonoPadre))
                _dialogService.DisplayAlertAsync("Alert", "Debes rellenar todos los campos", "OK");
            else
            {
                //Generamos la contraseña a partir del nombre y apellidos
                string username = GenerarUsername();
                string password = String.Empty;

                //Añadimos el usuario con perfil 1 (padre)


                var resp = mamas.UsuariosPost(username, "x", 1);
                if (resp.EsError) //Si ya está insertada esa asignatura
                    _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                else //Si no, se inserta el usuario
                {
                    var resp2 = mamas.PadresPost(resp.IdUsuario, NombrePadre, ApellidosPadre, DireccionPadre, TelefonoPadre);
                    if (resp.EsError) //Si ya está insertada esa asignatura
                        _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                    else //Si no, se inserta la asignatura 
                        _dialogService.DisplayAlertAsync("Alert", "Padre insertado en la Base de Datos", "OK");
                }

            }
        }

        private string GenerarUsername()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(NombrePadre.ToLower().ToCharArray()[0]);
            sb.Append(ApellidosPadre.ToLower().Split(' ')[0]);
            if(ApellidosPadre.ToLower().Split(' ').Length>1)
                sb.Append(ApellidosPadre.ToLower().Split(' ')[1].ToCharArray()[0]);
            var resp = mamas.UsuariosGetMayorID();
            sb.Append(resp.IdUsuario + 1);

            return sb.ToString();
        }

    }
}
