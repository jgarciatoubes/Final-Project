using API_ProyectoPracticas.Models;
using Mamas.MamasApiRest;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Mamas.ViewModels.AdminViewModels
{
    class EliminarViewModel : ViewModelBase
    {
        MamasApi mamas; 
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        public static Asignaturas asignaturaCompartir { get; set; }
        public static Cursos cursoCompartir { get; set; }
        public static Padres padreCompartir { get; set; }
        public static Profesores profesorCompartir { get; set; }
        public static Alumnos alumnoCompartir { get; set; }

        public DelegateCommand eliminarAlumnoCommand { get; set; }
        public DelegateCommand eliminarPadreCommand { get; set; }
        public DelegateCommand eliminarProfesorCommand { get; set; }
        public DelegateCommand eliminarCursoCommand { get; set; }
        public DelegateCommand eliminarAsignaturaCommand { get; set; }
        public DelegateCommand editarAlumnoCommand { get; set; }
        public DelegateCommand editarPadreCommand { get; set; }
        public DelegateCommand editarProfesorCommand { get; set; }
        public DelegateCommand editarAsignaturaCommand { get; set; }
        public DelegateCommand editarCursoCommand { get; set; }


        private ObservableCollection<Alumnos> _listaAlumnos;
        private ObservableCollection<Padres> _listaPadres;
        private ObservableCollection<Profesores> _listaProfesores;
        private ObservableCollection<Cursos> _listaCursos;
        private ObservableCollection<Asignaturas> _listaAsignaturas;

        private Alumnos _alumnoSeleccionado;
        private Padres _padreSeleccionado;
        private Profesores _profesorSeleccionado;
        private Cursos _cursoSeleccionado;
        private Asignaturas _asignaturaSeleccionada;

        public ObservableCollection<Alumnos> ListaAlumnos => _listaAlumnos;
        public ObservableCollection<Padres> ListaPadres => _listaPadres;
        public ObservableCollection<Profesores>  ListaProfesores => _listaProfesores;
        public ObservableCollection<Cursos> ListaCursos => _listaCursos;
        public ObservableCollection<Asignaturas> ListaAsignaturas => _listaAsignaturas;

        public Alumnos AlumnoSeleccionado
        {
            get { return _alumnoSeleccionado; }
            set { SetProperty(ref _alumnoSeleccionado, value); }
        }
        public Padres PadreSeleccionado
        {
            get { return _padreSeleccionado; }
            set { SetProperty(ref _padreSeleccionado, value); }
        }
        public Profesores ProfesorSeleccionado
        {
            get { return _profesorSeleccionado; }
            set { SetProperty(ref _profesorSeleccionado, value); }
        }
        public Cursos CursoSeleccionado
        {
            get { return _cursoSeleccionado; }
            set { SetProperty(ref _cursoSeleccionado, value); }
        }
        public Asignaturas AsignaturaSeleccionada
        {
            get { return _asignaturaSeleccionada; }
            set { SetProperty(ref _asignaturaSeleccionada, value); }
        }

        public EliminarViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            //Definicion de la variable para usar la API
            mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            _navigationService = navigationService;
            _dialogService = dialogService;

            //Llenamos las listas
            _listaAlumnos = mamas.AlumnosGet().Entries;
            _listaPadres = mamas.PadresGet().Entries;
            _listaProfesores = mamas.ProfesoresGet().Entries;
            _listaCursos = mamas.CursosGet().Entries;
            _listaAsignaturas = mamas.AsignaturasGet().Entries;


            eliminarAlumnoCommand = new DelegateCommand(EliminarAlumno);
            eliminarPadreCommand = new DelegateCommand(EliminarPadre);
            eliminarProfesorCommand = new DelegateCommand(EliminarProfesor);
            eliminarCursoCommand = new DelegateCommand(EliminarCurso);
            eliminarAsignaturaCommand = new DelegateCommand(EliminarAsignatura);

            editarAlumnoCommand = new DelegateCommand(EditarAlumno);
            editarPadreCommand = new DelegateCommand(EditarPadre);
            editarProfesorCommand = new DelegateCommand(EditarProfesor);
            editarCursoCommand = new DelegateCommand(EditarCurso);
            editarAsignaturaCommand = new DelegateCommand(EditarAsignatura);
        }

        async void EliminarAlumno()
        {
            if(AlumnoSeleccionado == null)
                await _dialogService.DisplayAlertAsync("Alert", "Elige algún alumno", "OK");
            else
            {
                var respAlumProf = mamas.AlumnoProfesorDelete(AlumnoSeleccionado.IdAlumno);
                if (respAlumProf.EsError)
                    await _dialogService.DisplayAlertAsync("Alert", respAlumProf.Error.detail, "OK");
                var resp = mamas.AlumnosDelete(AlumnoSeleccionado.IdAlumno);
                if (resp.EsError)
                    await _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                else
                {
                    await _dialogService.DisplayAlertAsync("Alert", "Alumno eliminado", "OK");
                    _listaAlumnos.Remove(AlumnoSeleccionado);
                }
                    
            }
        }
        async void EliminarPadre()
        {
            if (PadreSeleccionado == null)
                await _dialogService.DisplayAlertAsync("Alert", "Elige algún padre", "OK");
            else
            {
                var resp = mamas.PadresDelete(PadreSeleccionado.IdPadre);
                if (resp.EsError)
                    await _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                else
                {
                    var resp2 = mamas.UsuariosDelete(PadreSeleccionado.IdUsuario);
                    if (resp.EsError)
                        await _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                    else
                    {
                        await _dialogService.DisplayAlertAsync("Alert", "Padre eliminado", "OK");
                        _listaPadres.Remove(PadreSeleccionado);
                    }

                }
                   
            }
        }
        async void EliminarProfesor()
        {
            if (ProfesorSeleccionado == null)
                await _dialogService.DisplayAlertAsync("Alert", "Elige algún profesor", "OK");
            else
            {
                int idUsuarioProfesor = ProfesorSeleccionado.IdUsuario;
                var respCurso = mamas.ProfesorCursoDelete(ProfesorSeleccionado.IdProfesor);
                if (respCurso.EsError)
                    await _dialogService.DisplayAlertAsync("Alert", respCurso.Error.detail, "OK");
                var respAsig = mamas.ProfesorAsignaturaDelete(ProfesorSeleccionado.IdProfesor);
                if (respAsig.EsError)
                    await _dialogService.DisplayAlertAsync("Alert", respAsig.Error.detail, "OK");
                var resp = mamas.ProfesoresDelete(ProfesorSeleccionado.IdProfesor);
                if (resp.EsError)
                    await _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                else
                {
                    var resp2 = mamas.UsuariosDelete(idUsuarioProfesor);
                    if (resp2.EsError)
                        await _dialogService.DisplayAlertAsync("Alert", resp2.Error.detail, "OK");
                    await _dialogService.DisplayAlertAsync("Alert", "Profesor eliminado", "OK");
                    _listaProfesores.Remove(ProfesorSeleccionado);
                }
                    
            }
        }
        async void EliminarCurso()
        {
            if (CursoSeleccionado == null)
                await _dialogService.DisplayAlertAsync("Alert", "Elige algún curso", "OK");
            else
            {
                var resp = mamas.CursosDelete(CursoSeleccionado.NumeroCurso, CursoSeleccionado.LetraCurso);
                if (resp.EsError)
                    await _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                else
                {
                    await _dialogService.DisplayAlertAsync("Alert", "Curso eliminado", "OK");
                    _listaCursos.Remove(CursoSeleccionado);
                }
                    
            }
        }
        async void EliminarAsignatura()
        {
            if (AsignaturaSeleccionada == null)
                await _dialogService.DisplayAlertAsync("Alert", "Elige alguna asignatura", "OK");
            else
            {
                var resp = mamas.AsignaturasDelete(AsignaturaSeleccionada.NombreAsignatura);
                if(resp.EsError)
                    await _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                else
                {
                    await _dialogService.DisplayAlertAsync("Alert", "Asignatura eliminada", "OK");
                    _listaAsignaturas.Remove(AsignaturaSeleccionada);
                }
                    
            }
                
        }

        async void EditarAlumno()
        {
            if (AlumnoSeleccionado == null)
                await _dialogService.DisplayAlertAsync("Alert", "Elige algún alumno", "OK");
            else
            {
                alumnoCompartir = AlumnoSeleccionado;
                await _navigationService.NavigateAsync("EditarAlumnoPage");
            }
                
        }
        async void EditarPadre()
        {
            if (PadreSeleccionado == null)
                await _dialogService.DisplayAlertAsync("Alert", "Elige algún padre", "OK");
            else
            {
                padreCompartir = PadreSeleccionado;
                await _navigationService.NavigateAsync("EditarPadrePage");
            }
        }
        async void EditarProfesor()
        {
            if (ProfesorSeleccionado == null)
                await _dialogService.DisplayAlertAsync("Alert", "Elige algún profesor", "OK");
            else
            {
                profesorCompartir = ProfesorSeleccionado;
                await _navigationService.NavigateAsync("EditarProfesorPage");
            }
        }
        async void EditarCurso()
        {
            if (CursoSeleccionado == null)
                await _dialogService.DisplayAlertAsync("Alert", "Elige algún curso", "OK");
            else
            {
                cursoCompartir = CursoSeleccionado;
                await _navigationService.NavigateAsync("EditarCursoPage");
            }
        }
        async void EditarAsignatura()
        {
            if (AsignaturaSeleccionada == null)
                await _dialogService.DisplayAlertAsync("Alert", "Elige alguna asignatura", "OK");
            else
            {
                asignaturaCompartir = AsignaturaSeleccionada;
                await _navigationService.NavigateAsync("EditarAsignaturaPage");
            }
        }
    }
}
