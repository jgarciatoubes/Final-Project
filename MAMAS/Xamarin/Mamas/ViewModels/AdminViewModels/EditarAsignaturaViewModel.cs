using Mamas.MamasApiRest;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mamas.ViewModels.AdminViewModels
{
    class EditarAsignaturaViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        private string _nombreAsignatura;
        public DelegateCommand actualizarAsignaturaCommand { get; set; }

        public string NombreAsignatura
        {
            get { return _nombreAsignatura; }
            set { SetProperty(ref _nombreAsignatura, value); }
        }

        public EditarAsignaturaViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            _nombreAsignatura = EliminarViewModel.asignaturaCompartir.NombreAsignatura;

            actualizarAsignaturaCommand = new DelegateCommand(ActualizarAsignatura);
        }

        void ActualizarAsignatura()
        {
            if (String.IsNullOrEmpty(NombreAsignatura))
                _dialogService.DisplayAlertAsync("Alert", "Debes rellenar todos los campos", "OK");
            else
            {
                var mamas = new MamasApi()
                {
                    Ruta = new Uri("https://localhost:44328/api"),
                };

                var resp = mamas.AsignaturasPut(EliminarViewModel.asignaturaCompartir.IdAsignatura, NombreAsignatura);
                if(resp.EsError)
                    _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                else
                {
                    _dialogService.DisplayAlertAsync("Alert", "Asignatura modificada", "OK");
                }
            }
        }
    }
}
