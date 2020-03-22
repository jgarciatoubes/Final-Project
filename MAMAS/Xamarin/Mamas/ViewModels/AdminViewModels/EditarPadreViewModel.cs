using Mamas.MamasApiRest;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mamas.ViewModels.AdminViewModels
{
    class EditarPadreViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        public DelegateCommand actualizarPadreCommand { get; set; }

        private string _nombrePadre;
        private string _apellidosPadre;
        private string _direccionPadre;
        private string _telefonoPadre;

        public string NombrePadre
        {
            get { return _nombrePadre; }
            set { SetProperty(ref _nombrePadre, value); }
        }

        public string ApellidosPadre
        {
            get { return _apellidosPadre; }
            set { SetProperty(ref _apellidosPadre, value); }
        }

        public string DireccionPadre
        {
            get { return _direccionPadre; }
            set { SetProperty(ref _direccionPadre, value); }
        }

        public string TelefonoPadre
        {
            get { return _telefonoPadre; }
            set { SetProperty(ref _telefonoPadre, value); }
        }

        public EditarPadreViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            _navigationService = navigationService;

            _nombrePadre = EliminarViewModel.padreCompartir.NombrePadre;
            _apellidosPadre = EliminarViewModel.padreCompartir.ApellidosPadre;
            _direccionPadre = EliminarViewModel.padreCompartir.DireccionPadre;
            _telefonoPadre = EliminarViewModel.padreCompartir.TelefonoPadre;

            actualizarPadreCommand = new DelegateCommand(ActualizarPadre);
        }

        void ActualizarPadre()
        {
            var mamas = new MamasApi()
            {
                Ruta = new Uri("https://localhost:44328/api"),
            };

            var resp = mamas.PadresPut(EliminarViewModel.padreCompartir.IdPadre, NombrePadre, ApellidosPadre, DireccionPadre, TelefonoPadre);
            if(resp.EsError)
                _dialogService.DisplayAlertAsync("Alert", resp.Error.detail, "OK");
            else
            {
                _dialogService.DisplayAlertAsync("Alert", "Padre modificado", "OK");
            }
        }
    }
}
