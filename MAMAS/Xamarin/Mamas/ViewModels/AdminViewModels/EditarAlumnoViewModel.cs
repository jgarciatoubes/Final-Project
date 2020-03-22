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
    class EditarAlumnoViewModel : ViewModelBase
    {
        MamasApi mamas;

        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        public DelegateCommand navegarProfAlumCommand { get; set; }

        private string _nombreAlumno;
        private string _apellidosAlumno;
        private DateTime _fechaNacimientoAlumno;
        private ObservableCollection<Padres> _listaPadres;
        private Padres _padreSeleccionado;

        public string NombreAlumno
        {
            get { return _nombreAlumno; }
            set { SetProperty(ref _nombreAlumno, value); }
        }

        public string ApellidosAlumno
        {
            get { return _apellidosAlumno; }
            set { SetProperty(ref _apellidosAlumno, value); }
        }

        public DateTime FechaNacimientoAlumno
        {
            get { return _fechaNacimientoAlumno; }
            set { SetProperty(ref _fechaNacimientoAlumno, value); }
        }

        public ObservableCollection<Padres> ListaPadres => _listaPadres;

        public Padres PadreSeleccionado
        {
            get { return _padreSeleccionado; }
            set { SetProperty(ref _padreSeleccionado, value); }
        }

        public EditarAlumnoViewModel(INavigationService navigationService, IPageDialogService dialogService) 
            :base(navigationService)
        {
            mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            _navigationService = navigationService;
            _dialogService = dialogService;

            _nombreAlumno = EliminarViewModel.alumnoCompartir.NombreAlumno;
            _apellidosAlumno = EliminarViewModel.alumnoCompartir.ApellidosAlumnos;
            _fechaNacimientoAlumno = EliminarViewModel.alumnoCompartir.FechaNacimientoAlumno;
            _listaPadres = mamas.PadresGet().Entries;
            var resp = mamas.PadresGet(EliminarViewModel.alumnoCompartir.IdPadre);
            Padres padre = new Padres();
            padre.IdPadre = resp.IdPadre;
            padre.IdUsuario = resp.IdUsuario;
            padre.NombrePadre = resp.NombrePadre;
            padre.ApellidosPadre = resp.ApellidosPadre;
            padre.DireccionPadre = resp.DireccionPadre;
            padre.TelefonoPadre = resp.TelefonoPadre;
            PadreSeleccionado = padre;

            navegarProfAlumCommand = new DelegateCommand(ActualizarAlumno);
        }

        void ActualizarAlumno()
        {
            var respProf = mamas.AlumnoProfesorDelete(EliminarViewModel.alumnoCompartir.IdAlumno);
            var resp = mamas.AlumnosPut(EliminarViewModel.alumnoCompartir.IdAlumno, NombreAlumno, ApellidosAlumno, FechaNacimientoAlumno, PadreSeleccionado.IdPadre);
            if(resp.EsError)
                _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
            else
            {
                _dialogService.DisplayAlertAsync("Alert", "Alumno modificado. Se han borrado sus profesores. Añádelos de nuevo", "OK");
                _navigationService.NavigateAsync("EditarAlumProfPage");
            }
        }
    }
}
