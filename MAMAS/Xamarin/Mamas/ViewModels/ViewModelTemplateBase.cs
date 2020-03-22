using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mamas.ViewModels
{
    public class ViewModelTemplateBase<TType> : BindableBase, INavigationAware, IDestructible
        where TType : class
    {
        protected TType InjectionObject { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelTemplateBase()
        {
        }

        public ViewModelTemplateBase(TType injectionObject)
        {
            InjectionObject = injectionObject;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
