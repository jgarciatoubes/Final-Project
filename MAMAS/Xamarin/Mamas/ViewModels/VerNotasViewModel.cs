using API_ProyectoPracticas.Models;
using Mamas.MamasApiRest;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mamas.ViewModels
{
    class VerNotasViewModel : ViewModelBase
    {
        private int _idAlumno;
        private ObservableCollection<Notas> _listaNotas;
        public int IdAlumno
        {
            get { return _idAlumno; }
            set { SetProperty(ref _idAlumno, value); }
        }

        public ObservableCollection<Notas> ListaNotas => _listaNotas;


        public VerNotasViewModel(INavigationService navigationService, IPageDialogService dialogService)
           : base(navigationService)
        {
            var mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            _idAlumno = ElegirHijoViewModel.alumnoCompartir.IdAlumno;
            _listaNotas = mamas.NotasGet(IdAlumno).Entries;
        }
    }
}
