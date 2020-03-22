using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mamas.ViewModels
{
    class MenuChatViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public DelegateCommand navegarChatIndividualCommand { get; set; }
        public DelegateCommand navegarChatGrupalCommand { get; set; }
        public MenuChatViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            _navigationService = navigationService;
            navegarChatIndividualCommand = new DelegateCommand(NavegarChatIndividual);
            navegarChatGrupalCommand = new DelegateCommand(NavegarChatGrupal);
        }

        void NavegarChatIndividual()
        {
            _navigationService.NavigateAsync("ChatPage");
        }

        void NavegarChatGrupal()
        {
            _navigationService.NavigateAsync("ChatPage");
        }
    }
}
