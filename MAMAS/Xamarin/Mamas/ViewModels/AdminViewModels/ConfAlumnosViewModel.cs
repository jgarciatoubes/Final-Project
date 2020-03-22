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
    class ConfAlumnosViewModel : ViewModelBase
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

        public static int alumnoCompartir { get; set; }

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

        public ConfAlumnosViewModel(INavigationService navigationService, IPageDialogService dialogService)
    : base(navigationService)
        {
            mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            _navigationService = navigationService;
            _dialogService = dialogService;

            _nombreAlumno = String.Empty;
            _apellidosAlumno = String.Empty;
            _fechaNacimientoAlumno = new DateTime();
            _listaPadres = mamas.PadresGet().Entries;

            navegarProfAlumCommand = new DelegateCommand(NavegarAlumProf);
        }

        void NavegarAlumProf()
        {
            if(String.IsNullOrEmpty(NombreAlumno) || String.IsNullOrEmpty(ApellidosAlumno) || PadreSeleccionado == null)
            {
                _dialogService.DisplayAlertAsync("Alert", "Debes rellenar todos los campos", "OK");
            }
            else
            {
                var resp = mamas.AlumnosPost(NombreAlumno, ApellidosAlumno, FechaNacimientoAlumno, PadreSeleccionado.IdPadre);

                if(resp.EsError)
                {
                    _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                }
                else
                {
                    _dialogService.DisplayAlertAsync("Alert", "Alumno insertado en la base de datos", "OK");
                    alumnoCompartir = resp.IdAlumno;
                    _navigationService.NavigateAsync("AlumProfPage");
                }
            }
            
        }
    }
}
