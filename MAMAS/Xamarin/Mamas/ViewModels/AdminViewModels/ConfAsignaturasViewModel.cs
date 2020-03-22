using Mamas.MamasApiRest;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mamas.ViewModels.AdminViewModels
{
    class ConfAsignaturasViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        private string _nombreAsignatura;
        public DelegateCommand insertarAsignaturaCommand { get; set; }

        public string NombreAsignatura
        {
            get { return _nombreAsignatura; }
            set { SetProperty(ref _nombreAsignatura, value); }
        }
        public ConfAsignaturasViewModel(INavigationService navigationService, IPageDialogService dialogService)
    : base(navigationService)
        {
            _dialogService = dialogService;

            _nombreAsignatura = String.Empty;

            insertarAsignaturaCommand = new DelegateCommand(InsertarAsignatura);
        }

        void InsertarAsignatura()
        {
            //Primero se comprueba si se han rellenado todos los campos
            if (String.IsNullOrEmpty(NombreAsignatura))
                _dialogService.DisplayAlertAsync("Alert", "Debes rellenar todos los campos", "OK");
            else
            {
                var mamas = new MamasApi()
                {
                    Ruta = new Uri("https://localhost:44328/api"),
                };

                var resp = mamas.AsignaturasPost(NombreAsignatura);
                if(resp.EsError) //Si ya está insertada esa asignatura
                    _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                else //Si no, se inserta la asignatura 
                    _dialogService.DisplayAlertAsync("Alert", "Asignatura insertada en la Base de Datos", "OK");

                //Tambien se vaciará el campo nombreAsignatura para seguir añadiendo
                NombreAsignatura = String.Empty;
            }


        }
    }
}
