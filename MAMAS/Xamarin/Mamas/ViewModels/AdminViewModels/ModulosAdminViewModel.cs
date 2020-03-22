using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mamas.ViewModels.AdminViewModels
{
    public class ModulosAdminViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        //Comandos para cada uno de los botones del menu del Admin
        public DelegateCommand confAsignaturasCommand { get; set; }
        public DelegateCommand confCursosCommand { get; set; }
        public DelegateCommand confProfesoresCommand { get; set; }
        public DelegateCommand confPadresCommand { get; set; }
        public DelegateCommand confAlumnosCommand { get; set; }
        public DelegateCommand navegarEliminarCommand { get; set; }
        public ModulosAdminViewModel(INavigationService navigationService)
    : base(navigationService)
        {
            _navigationService = navigationService;

            //Asociacion de los comandos con la pagina a la que tienen que ir
            confAsignaturasCommand = new DelegateCommand(NavegarConfAsignaturas);
            confCursosCommand = new DelegateCommand(NavegarConfCursos);
            confProfesoresCommand = new DelegateCommand(NavegarConfProfesores);
            confPadresCommand = new DelegateCommand(NavegarConfPadres);
            confAlumnosCommand = new DelegateCommand(NavegarConfAlumnos);
            navegarEliminarCommand = new DelegateCommand(NavegarEliminar);
        }

        //Metodos para la navegacion
        void NavegarConfAsignaturas()
        {
            _navigationService.NavigateAsync("ConfAsignaturasPage");
        }

        void NavegarConfCursos()
        {
            _navigationService.NavigateAsync("ConfCursosPage");
        }

        void NavegarConfProfesores()
        {
            _navigationService.NavigateAsync("ConfProfesoresPage");
        }

        void NavegarConfPadres()
        {
            _navigationService.NavigateAsync("ConfPadresPage");
        }

        void NavegarConfAlumnos()
        {
            _navigationService.NavigateAsync("ConfAlumnosPage");
        }

        void NavegarEliminar()
        {
            _navigationService.NavigateAsync("EliminarPage");
        }
    }
}
