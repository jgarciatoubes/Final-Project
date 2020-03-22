using API_ProyectoPracticas.Models;
using Mamas.MamasApiRest;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mamas.ViewModels.AdminViewModels
{
    class ConfProfesoresViewModel : ViewModelBase
    {
        MamasApi mamas;

        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        public DelegateCommand insertarProfesorCommand { get; set; }

        public static int profesorCompartido {get;set;}

        private string _nombreProfesor;
        private string _apellidosProfesor;

        public string NombreProfesor
        {
            get { return _nombreProfesor; }
            set { SetProperty(ref _nombreProfesor, value); }
        }

        public string ApellidosProfesor
        {
            get { return _apellidosProfesor; }
            set { SetProperty(ref _apellidosProfesor, value); }
        }

        public ConfProfesoresViewModel(INavigationService navigationService, IPageDialogService dialogService)
    : base(navigationService)
        {
            //Definicion de la variable para usar la API
            mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            _navigationService = navigationService;
            _dialogService = dialogService;

            _nombreProfesor = String.Empty;
            _apellidosProfesor = String.Empty;

            insertarProfesorCommand = new DelegateCommand(InsertarProfesor);
        }

        void InsertarProfesor()
        {
            //Primero se comprueba si se han rellenado todos los campos
            if (String.IsNullOrEmpty(NombreProfesor) || String.IsNullOrEmpty(ApellidosProfesor))
                _dialogService.DisplayAlertAsync("Alert", "Debes rellenar todos los campos", "OK");
            else
            {
                var mamas = new MamasApi()
                {
                    Ruta = new Uri("https://localhost:44328/api"),
                };
                string username = GenerarUsername();
                var resp = mamas.UsuariosPost(username, "x", 2);
                if (resp.EsError) 
                    _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                else 
                {
                    var resp2 = mamas.ProfesoresPost(NombreProfesor, ApellidosProfesor, resp.IdUsuario);
                    if (resp.EsError)
                    {
                        _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                        NombreProfesor = String.Empty;
                        ApellidosProfesor = String.Empty;
                    }

                    else
                    {
                        profesorCompartido = resp2.IdProfesor;
                        _dialogService.DisplayAlertAsync("Alert", "Profesor insertado en la Base de Datos", "OK");
                        _navigationService.NavigateAsync("AsignCursoProfPage");
                    }
                        
                }

            }
        }

        private string GenerarUsername()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(NombreProfesor.ToLower().ToCharArray()[0]);
            sb.Append(ApellidosProfesor.ToLower().Split(' ')[0]);
            if (ApellidosProfesor.ToLower().Split(' ').Length > 1)
                sb.Append(ApellidosProfesor.ToLower().Split(' ')[1].ToCharArray()[0]);
            var resp = mamas.UsuariosGetMayorID();
            sb.Append(resp.IdUsuario + 1);

            return sb.ToString();
        }

    }
}
