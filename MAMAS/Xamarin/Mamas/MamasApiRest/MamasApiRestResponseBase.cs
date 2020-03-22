using Newtonsoft.Json;
using System;
using System.Net;


namespace Mamas.MamasApiRest
{
    /// <summary>
    /// class con la lógica para hacer llamadas a los WS de la API de Intendia
    /// </summary>
    public abstract class MamasApiRestResponseBase
    {

#region private Fields with public Properties

        private String _json;
        /// <summary>
        /// Obtiene el JSON en bruto devuelto por el WS
        /// </summary>
        [JsonIgnore]
        public String Json
        {
            get { return _json; }
            internal set { _json = value; }
        }

        private Boolean _esError;
        /// <summary>
        /// Obtiene si la respuesta del WS es un error o no
        /// </summary>
        [JsonIgnore]
        public Boolean EsError
        {
            get { return _esError; }
            internal set { _esError = value; }
        }

        private DetallesError _error;
        /// <summary>
        /// Obtiene el error devuelto por el WS si es un error
        /// </summary>
        [JsonIgnore]
        public DetallesError Error
        {
            get { return _error; }
            internal set { _error = value; }
        }

        private Exception _excepcion;
        /// <summary>
        /// Obtiene la excepción generada por el procesamiento de la respuesta del WS, si hay
        /// </summary>
        [JsonIgnore]
        public Exception Excepcion
        {
            get { return _excepcion; }
            internal set { _excepcion = value; }
        }

        private HttpStatusCode _codigoRespuesta;
        /// <summary>
        /// Obtiene el código de respuesta de la llamada al WS
        /// </summary>
        [JsonIgnore]
        public HttpStatusCode CodigoRespuesta
        {
            get { return _codigoRespuesta; }
            internal set { _codigoRespuesta = value; }
        }

#endregion // private Fields with public Properties

#region Constructores - Destructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public MamasApiRestResponseBase()
        {
        }   // public IntendiaApiRestResponseBase()


        #endregion // Constructores - Destructor
    }   // public abstract class IntendiaApiRestResponseBase : ObservableObject
}   // namespace InComercial.IntendiaApiRest
