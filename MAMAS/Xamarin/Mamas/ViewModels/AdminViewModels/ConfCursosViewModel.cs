using Mamas.MamasApiRest;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mamas.ViewModels.AdminViewModels
{
    class ConfCursosViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        public DelegateCommand insertarCursoCommand { get; set; }

        private string _numeroCurso;
        private string _letraCurso;

        
        public string NumeroCurso
        {
            get { return _numeroCurso; }
            set { SetProperty(ref _numeroCurso, value); }
        }

        public string LetraCurso
        {
            get { return _letraCurso; }
            set { SetProperty(ref _letraCurso, value); }
        }

        
        public ConfCursosViewModel(INavigationService navigationService, IPageDialogService dialogService)
    : base(navigationService)
        {
            _dialogService = dialogService;

            _numeroCurso = "1";
            _letraCurso = "A";

            insertarCursoCommand = new DelegateCommand(InsertarCurso);
        }

        void InsertarCurso()
        {
            //Primero se comprueba si se han rellenado todos los campos
            if (String.IsNullOrEmpty(NumeroCurso) || String.IsNullOrEmpty(LetraCurso))
                _dialogService.DisplayAlertAsync("Alert", "Debes rellenar todos los campos", "OK");
            else
            {
                //Aqui se insertará el numero y la letra del curso en la base de datos

                var mamas = new MamasApi()
                {
                    Ruta = new Uri("https://localhost:44328/api"),
                };

                var resp = mamas.CursosPost(int.Parse(NumeroCurso), LetraCurso);
                if (resp.EsError) //Si ya está insertado ese curso
                    _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
                else //Si no, se inserta el curso
                    _dialogService.DisplayAlertAsync("Alert", "Curso insertado en la Base de Datos", "OK");

                //También se vaciarán los campos para seguir añadiendo cursos
                NumeroCurso = String.Empty;
                LetraCurso = String.Empty;
            }



        }
    }
}
