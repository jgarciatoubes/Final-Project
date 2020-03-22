using API_ProyectoPracticas.Models;
using Mamas.MamasApiRest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mamas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerNotasPage : ContentPage
    {
        public VerNotasPage()
        {
            InitializeComponent();
            /*var mamas = new MamasApi()
            {
                Ruta = new Uri("https://huawei-david:8443/api"),
            };
            var notas = mamas.NotasGet(int.Parse(lblIdAlumno.Text)).Entries.ToList<Object>();*/

        }
    }
}