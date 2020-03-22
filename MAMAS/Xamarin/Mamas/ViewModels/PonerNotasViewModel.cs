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
    class PonerNotasViewModel : ViewModelBase
    {
        MamasApi mamas;

        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        private Asignaturas _asignaturaSeleccionada;
        private Evaluaciones _evaluacionSeleccionada;
        private Cursos _cursoSeleccionado;
        private ObservableCollection<Asignaturas> _listaAsignaturas;
        private ObservableCollection<Evaluaciones> _listaEvaluaciones;
        private ObservableCollection<Cursos> _listaCursos;
        private string _nota;

        public Asignaturas AsignaturaSeleccionada
        {
            get { return _asignaturaSeleccionada; }
            set { SetProperty(ref _asignaturaSeleccionada, value); }
        }
        public ObservableCollection<Asignaturas> ListaAsignaturas => _listaAsignaturas;

        public Cursos CursoSeleccionado
        {
            get { return _cursoSeleccionado; }
            set { SetProperty(ref _cursoSeleccionado, value); }
        }
        public ObservableCollection<Cursos> ListaCursos => _listaCursos;

        public Evaluaciones EvaluacionSeleccionada
        {
            get { return _evaluacionSeleccionada; }
            set { SetProperty(ref _evaluacionSeleccionada, value); }
        }
        public ObservableCollection<Evaluaciones> ListaEvaluaciones => _listaEvaluaciones;

        public string Nota
        {
            get { return _nota; }
            set { SetProperty(ref _nota, value); }
        }

        public DelegateCommand insertarNotaCommand { get; set; }

        public PonerNotasViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            _navigationService = navigationService;
            _dialogService = dialogService;

            _listaAsignaturas = mamas.AsignaturasGet().Entries;
            _listaEvaluaciones = mamas.EvaluacionesGet().Entries;
            _listaCursos = mamas.CursosGet().Entries;

            insertarNotaCommand = new DelegateCommand(InsertarNota);
        }

        void InsertarNota()
        {
            var resp = mamas.NotasPost(Nota, AsignaturaSeleccionada.IdAsignatura, EvaluacionSeleccionada.IdEvaluacion, CursoSeleccionado.IdCurso, ElegirAlumnoViewModel.alumnoCompartir.IdAlumno);
            
            if(resp.EsError)
            {
                var answer = _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                /*if(answer.Equals("SI"))
                {
                    var resp2 = mamas.NotasGet(AsignaturaSeleccionada.IdAsignatura, EvaluacionSeleccionada.IdEvaluacion, CursoSeleccionado.IdCurso, ElegirAlumnoViewModel.alumnoCompartir.IdAlumno);
                    if(resp2.EsError)
                        _dialogService.DisplayAlertAsync("Alert", resp2.Error.detail, "OK");
                    var resp3 = mamas.NotasPut(resp2.IdNota, Nota, AsignaturaSeleccionada.IdAsignatura, EvaluacionSeleccionada.IdEvaluacion, CursoSeleccionado.IdCurso, ElegirAlumnoViewModel.alumnoCompartir.IdAlumno);
                    if (resp3.EsError)
                        _dialogService.DisplayAlertAsync("Alert", resp3.Error.detail, "OK");
                    else
                        _dialogService.DisplayAlertAsync("Alert", "Nota modificada", "OK");
                }*/
            }
            else
            {
                _dialogService.DisplayAlertAsync("Alert", "Nota añadida", "OK");
            }
                
        }
    }
}
