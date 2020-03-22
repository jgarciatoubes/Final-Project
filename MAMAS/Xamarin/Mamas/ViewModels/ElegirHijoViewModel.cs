using API_ProyectoPracticas.Models;
using Mamas.MamasApiRest;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mamas.ViewModels
{
    class ElegirHijoViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        IPageDialogService _dialogService;

        public static Alumnos alumnoCompartir { get; set; }

        private Alumnos _alumnoSeleccionado;
        private ObservableCollection<Alumnos> _listaAlumnos;

        public Alumnos AlumnoSeleccionado
        {
            get { return _alumnoSeleccionado; }
            set { SetProperty(ref _alumnoSeleccionado, value); }
        }
        public ObservableCollection<Alumnos> ListaAlumnos => _listaAlumnos;

        public DelegateCommand navegarModulosCommand { get; set; }
        public ElegirHijoViewModel(INavigationService navigationService, IPageDialogService dialogService)
    : base(navigationService)
        {
            var mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            _navigationService = navigationService;
            _dialogService = dialogService;

            var resp = mamas.PadrePorIdUsuGet(LoginViewModel.usuarioCompartir.IdUsuario);
            _listaAlumnos = mamas.AlumnosGet(resp.IdPadre).Entries; //BUSCAMOS UNA LISTA DE SUS HIJOS
 
            navegarModulosCommand = new DelegateCommand(NavegarModulos);
        }

        void NavegarModulos()
        {
            if (AlumnoSeleccionado == null)
                _dialogService.DisplayAlertAsync("Alert", "Debes elegir un alumno", "OK");
            else
            {
                alumnoCompartir = AlumnoSeleccionado;
                _navigationService.NavigateAsync("ModulosPage");
            }

        }
    }
}
