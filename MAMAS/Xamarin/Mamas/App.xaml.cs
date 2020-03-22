using Prism;
using Prism.Ioc;
using Mamas.ViewModels;
using Mamas.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mamas.Views.AdminViews;
using Mamas.ViewModels.AdminViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mamas
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Pantallas de los usuarios
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<PassPage, PassViewModel>();
            containerRegistry.RegisterForNavigation<ModulosPage, ModulosViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuChatPage, MenuChatViewModel>();
            containerRegistry.RegisterForNavigation<ChatPage, ChatViewModel>();

            //Pantallas del admin
            containerRegistry.RegisterForNavigation<ConfAlumnosPage, ConfAlumnosViewModel>();
            containerRegistry.RegisterForNavigation<ConfAsignaturasPage, ConfAsignaturasViewModel>();
            containerRegistry.RegisterForNavigation<ConfCursosPage, ConfCursosViewModel>();
            containerRegistry.RegisterForNavigation<ConfPadresPage, ConfPadresViewModel>();
            containerRegistry.RegisterForNavigation<ConfProfesoresPage, ConfProfesoresViewModel>();
            containerRegistry.RegisterForNavigation<ModulosAdminPage, ModulosAdminViewModel>();
            containerRegistry.RegisterForNavigation<AlumProfPage, AlumProfViewModel>();
            containerRegistry.RegisterForNavigation<EliminarPage, EliminarViewModel>();
            containerRegistry.RegisterForNavigation<EditarAlumnoPage, EditarAlumnoViewModel>();
            containerRegistry.RegisterForNavigation<EditarAsignaturaPage, EditarAsignaturaViewModel>();
            containerRegistry.RegisterForNavigation<EditarCursoPage, EditarCursoViewModel>();
            containerRegistry.RegisterForNavigation<EditarPadrePage, EditarPadreViewModel>();
            containerRegistry.RegisterForNavigation<EditarProfesorPage, EditarProfesorViewModel>();
            containerRegistry.RegisterForNavigation<AsignCursoProfPage, AsignCursoProfViewModel>();
            containerRegistry.RegisterForNavigation<EditarAsignCursoProfPage, EditarAsignCursoProfViewModel>();
            containerRegistry.RegisterForNavigation<EditarAlumProfPage, EditarAlumProfViewModel>();
            containerRegistry.RegisterForNavigation<ElegirAlumnoPage, ElegirAlumnoViewModel>();
            containerRegistry.RegisterForNavigation<ElegirHijoPage, ElegirHijoViewModel>();
            containerRegistry.RegisterForNavigation<PonerNotasPage, PonerNotasViewModel>();
            containerRegistry.RegisterForNavigation<VerNotasPage, VerNotasViewModel>();
        }
    }
}
