using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mamas.ViewModels
{
    public class ModulosViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        public DelegateCommand chatCommand { get; set; }
        public DelegateCommand notasCommand { get; set; }

        public ModulosViewModel(INavigationService navigationService, IPageDialogService dialogService)
: base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            chatCommand = new DelegateCommand(NavegarChat);
            notasCommand = new DelegateCommand(NavegarNotas);
            Title = "Login";
        }

        void NavegarChat()
        {
            _dialogService.DisplayAlertAsync("Alert", "Se implementará próximamente", "OK");
            //_navigationService.NavigateAsync("MenuChatPage");
        }

        async void NavegarNotas()
        {
            if(LoginViewModel.usuarioCompartir.IdPerfil == 2)
                await _navigationService.NavigateAsync("PonerNotasPage");
            else
                await _navigationService.NavigateAsync("VerNotasPage");
        }
    }
}
