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
    class EditarAsignCursoProfViewModel : ViewModelBase
    {
        MamasApi mamas;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        private Cursos _cursoSeleccionado;
        private Asignaturas _asignaturaSeleccionada;
        private ObservableCollection<Cursos> _listaCursos;
        private ObservableCollection<Asignaturas> _listaAsignaturas;

        public DelegateCommand insertarAsignCursoProfCommand { get; set; }
        public Cursos CursoSeleccionado
        {
            get { return _cursoSeleccionado; }
            set { SetProperty(ref _cursoSeleccionado, value); }
        }

        public ObservableCollection<Cursos> ListaCursos => _listaCursos;

        public Asignaturas AsignaturaSeleccionada
        {
            get { return _asignaturaSeleccionada; }
            set { SetProperty(ref _asignaturaSeleccionada, value); }
        }

        public ObservableCollection<Asignaturas> ListaAsignaturas => _listaAsignaturas;
        public EditarAsignCursoProfViewModel(INavigationService navigationService, IPageDialogService dialogService)
: base(navigationService)
        {
            mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            _navigationService = navigationService;
            _dialogService = dialogService;

            _listaCursos = mamas.CursosGet().Entries;
            _listaAsignaturas = mamas.AsignaturasGet().Entries;

            insertarAsignCursoProfCommand = new DelegateCommand(ActualizarAsignCursoProf);
        }

        void ActualizarAsignCursoProf()
        {
            if(CursoSeleccionado == null || AsignaturaSeleccionada == null)
            {
                _dialogService.DisplayAlertAsync("Alert", "Debes rellenar todos los campos", "OK");
            }
            else
            {
                int idProfesor = Mamas.ViewModels.AdminViewModels.EliminarViewModel.profesorCompartir.IdProfesor;
                var respCurso = mamas.ProfesorCursoPost(idProfesor, CursoSeleccionado.IdCurso);
                if (respCurso.EsError)
                {
                    _dialogService.DisplayAlertAsync("Alert", respCurso.Error.detail, "OK");
                }
                else
                {
                    var respAsignatura = mamas.ProfesorAsignaturaPost(idProfesor, AsignaturaSeleccionada.IdAsignatura);
                    if (respAsignatura.EsError)
                    {
                        _dialogService.DisplayAlertAsync("Alert", respAsignatura.Error.detail, "OK");
                    }
                    else
                    {
                        _dialogService.DisplayAlertAsync("Alert", "Curso y asignatura añadidos", "OK");
                    }
                }
            }
        }
    }
}
