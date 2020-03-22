using Mamas.MamasApiRest;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mamas.ViewModels.AdminViewModels
{
    class EditarCursoViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        public DelegateCommand actualizarCursoCommand { get; set; }

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


        public EditarCursoViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            _letraCurso = EliminarViewModel.cursoCompartir.LetraCurso;
            _numeroCurso = EliminarViewModel.cursoCompartir.NumeroCurso.ToString();           

            actualizarCursoCommand = new DelegateCommand(ActualizarCurso);
        }
        
        void ActualizarCurso()
        {
            var mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            var resp = mamas.CursosPut(EliminarViewModel.cursoCompartir.IdCurso, int.Parse(NumeroCurso), LetraCurso);
            if (resp.EsError)
                _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
            else
            {
                _dialogService.DisplayAlertAsync("Alert", "Curso modificado", "OK");
            }
        }
    }
}
