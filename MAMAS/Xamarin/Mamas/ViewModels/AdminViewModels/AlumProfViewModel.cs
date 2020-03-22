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
    class AlumProfViewModel : ViewModelBase
    {
        MamasApi mamas;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        public DelegateCommand insertarAlumProfCommand { get; set; }

        int idAlumnoCompartir;
        private Profesores _profesorSeleccionado;
        private ObservableCollection<Profesores> _listaProfesores;

        public Profesores ProfesorSeleccionado
        {
            get { return _profesorSeleccionado; }
            set { SetProperty(ref _profesorSeleccionado, value); }
        }

        public ObservableCollection<Profesores> ListaProfesores => _listaProfesores;

        public AlumProfViewModel(INavigationService navigationService, IPageDialogService dialogService)
: base(navigationService)
        {
            mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            _navigationService = navigationService;
            _dialogService = dialogService;

            idAlumnoCompartir = Mamas.ViewModels.AdminViewModels.ConfAlumnosViewModel.alumnoCompartir;

            _listaProfesores = mamas.ProfesoresGet().Entries;

            insertarAlumProfCommand = new DelegateCommand(InsertarAlumProf);
        }

        void InsertarAlumProf()
        {
            //Miramos si esta o no el campo vacio
            if (ProfesorSeleccionado == null)
                _dialogService.DisplayAlertAsync("Alert", "Debes rellenar todos los campos", "OK");
            else
            {
                var resp = mamas.AlumnoProfesorPost(idAlumnoCompartir, ProfesorSeleccionado.IdProfesor);
                if(resp.EsError)
                {
                    _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                }
                else
                {
                    _dialogService.DisplayAlertAsync("Alert", "Profesor añadido", "OK");
                }
            }
        }

    }
}
