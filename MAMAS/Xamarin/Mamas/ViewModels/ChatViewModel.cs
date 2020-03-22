using Prism.Navigation;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;
using Prism.Commands;

namespace Mamas.ViewModels
{
    class ChatViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public ChatViewModel(INavigationService navigationService) 
            : base(navigationService)
        {

        }
    }
}
