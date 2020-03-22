using Mamas.MamasApiRest;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mamas.ViewModels.AdminViewModels
{
    class EditarProfesorViewModel : ViewModelBase
    {
        MamasApi mamas;

        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        public DelegateCommand actualizarProfesorCommand { get; set; }

        public static int profesorCompartido { get; set; }

        private string _nombreProfesor;
        private string _apellidosProfesor;

        public string NombreProfesor
        {
            get { return _nombreProfesor; }
            set { SetProperty(ref _nombreProfesor, value); }
        }

        public string ApellidosProfesor
        {
            get { return _apellidosProfesor; }
            set { SetProperty(ref _apellidosProfesor, value); }
        }


        public EditarProfesorViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            //Definicion de la variable para usar la API
            mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            _navigationService = navigationService;
            _dialogService = dialogService;

            _nombreProfesor = EliminarViewModel.profesorCompartir.NombreProfesor;
            _apellidosProfesor = EliminarViewModel.profesorCompartir.ApellidosProfesor;

            actualizarProfesorCommand = new DelegateCommand(ActualizarProfesor);

        }
        
        void ActualizarProfesor()
        {
            if(String.IsNullOrEmpty(NombreProfesor) || String.IsNullOrEmpty(ApellidosProfesor))
                _dialogService.DisplayAlertAsync("Alert", "Debes rellenar todos los campos", "OK");
            else
            {
                var respAsign = mamas.ProfesorAsignaturaDelete(EliminarViewModel.profesorCompartir.IdProfesor);
                var respCurso = mamas.ProfesorCursoDelete(EliminarViewModel.profesorCompartir.IdProfesor);
                var resp = mamas.ProfesoresPut(EliminarViewModel.profesorCompartir.IdProfesor, NombreProfesor, ApellidosProfesor);
                if (resp.EsError)
                    _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                else
                {
                    _dialogService.DisplayAlertAsync("Alert", "Profesor modificado. Se han borrado susasignaturas y cursos. Añádelos de nuevo", "OK");
                    _navigationService.NavigateAsync("EditarAsignCursoProfPage");
                }
                    }

        }
        void NavegarCursos()
        {
            _navigationService.NavigateAsync("ConfCursosPage");
        }
        void NavegarAsignaturas()
        {
            _navigationService.NavigateAsync("ConfAsignaturasPage");
        }
    }
}
