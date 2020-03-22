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
    class ElegirAlumnoViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        IPageDialogService _dialogService;

        public static AlumnoProfesor alumnoCompartir { get; set; }

        private AlumnoProfesor _alumnoSeleccionado;
        private ObservableCollection<AlumnoProfesor> _listaAlumnos;

        public AlumnoProfesor AlumnoSeleccionado
        {
            get { return _alumnoSeleccionado; }
            set { SetProperty(ref _alumnoSeleccionado, value); }
        }
        public ObservableCollection<AlumnoProfesor> ListaAlumnos => _listaAlumnos;

        public DelegateCommand navegarModulosCommand { get; set; }
        public ElegirAlumnoViewModel(INavigationService navigationService, IPageDialogService dialogService)
    : base(navigationService)
        {
            var mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            _navigationService = navigationService;
            _dialogService = dialogService;

            var resp = mamas.ProfesoresGet(LoginViewModel.usuarioCompartir.IdUsuario, "aux");
            ObservableCollection<AlumnoProfesor> listaAlumProf = mamas.AlumnoProfesorGet(resp.IdProfesor).Entries; //BUSCAMOS UNA LISTA DE SUS ALUMNOS
            _listaAlumnos = listaAlumProf;

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
