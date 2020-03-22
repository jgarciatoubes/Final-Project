using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net;


namespace Mamas.MamasApiRest
{
    /// <summary>
    /// class con la lógica para hacer llamadas a los WS de la API de Intendia
    /// </summary>
    public abstract class MamasApiRestResponseArrayBase<TType> : MamasApiRestResponseBase
        where TType : MamasApiRestResponseBase
    {

#region private Fields with public Properties


        private ObservableCollection<TType> _entries;
        /// <summary>
        /// Obtiene el JSON en bruto devuelto por el WS
        /// </summary>
        public ObservableCollection<TType> Entries
        {
            get { return _entries; }
            internal set { _entries = value; }
        }

#endregion // private Fields with public Properties
#region Constructores - Destructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public MamasApiRestResponseArrayBase()
        {
            Entries = new ObservableCollection<TType>();
        }   // public IntendiaApiRestResponseArrayBase()

        #endregion // Constructores - Destructor
    }   // public abstract class IntendiaApiRestResponseArrayBase<TType> : IntendiaApiRestResponseBase
}   // namespace InComercial.MaponApiRest
