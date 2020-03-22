using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Security;
//using Xamarin.Forms.Internals;

namespace Mamas.MamasApiRest
{
    /// <summary>
    /// class con la lógica para hacer llamadas a los WS de la API de Intendiaj
    /// </summary>
    public partial class MamasApi
    {
        private System.Net.Security.RemoteCertificateValidationCallback _remoteCertificateValidationCallbackPrevious;
        private SecurityProtocolType _securityProtocolPrevious;

#region private Fields with public Properties

        private Uri _ruta;
        /// <summary>
        /// Obtiene o establece la ruta a los WS. P. ej.  http://185.14.58.156/posicionamientotaller/web_service/ws_posicionamiento
        /// </summary>
        public Uri Ruta
        {
            get { return _ruta; }
            set { _ruta = value; }
        }

#endregion // private Fields with public Properties

#region Constructores - Destructor

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public MamasApi()
        {
        } 

#endregion // Constructores - Destructor

#region private Methods

        /// <summary>
        /// Comprueba si los valores de las Properties son válidos para hacer las llamadas a los WS
        /// </summary>
        /// <returns>true si son válidos, false si no</returns>
        private Boolean TieneConfiguracionValida()
        {
            var result = false;

            result = (Ruta != null);

            return result;
        }   // private Boolean TieneConfiguracionValida()

        /// <summary>
        /// Prepara los parámetros restClient y restRequest para el método indicado del tipo indicado
        /// </summary>
        /// <param name="metodo">Método del WS a llamar</param>
        /// <param name="tipoMetodo">Tipo de petición (normalmente GET)</param>
        /// <param name="restClient"><see cref="RestClient"/> preparado para ejecutar el parámetro restRequest</param>
        /// <param name="restRequest"><see cref="RestRequest"/> con la configuración y parámetros del método del WS a llamar</param>
        /// <returns>true si se han podido preparar restClient y restRequest, false si no</returns>
        private Boolean PreparaPeticion(String metodo, Method tipoMetodo, out IRestClient restClient, out IRestRequest restRequest, params String[] parametrosMetodo)
        {
            var result = false;

            restClient = null;
            restRequest = null;

            if (!TieneConfiguracionValida() ||
                String.IsNullOrWhiteSpace(metodo))
            { return result; }

            try
            {
                if (parametrosMetodo == null)
                { parametrosMetodo = new String[] { }; }

                var peticionTmp = new List<String>()
                {
                    Ruta.PathAndQuery,
                    metodo,
                };
                peticionTmp.AddRange(parametrosMetodo);

                restClient = new RestClient(Ruta.GetLeftPart(UriPartial.Authority));
                restRequest = new RestRequest(String.Join("/", peticionTmp), tipoMetodo);

                

                //DesactivaSeguridadCertificadosHttps();
                EstableceSeguridadRequerida();
                

                result = true;
            }   // try
            catch (Exception ex)
            {
                restClient = null;
                restRequest = null;
               
            }   // catch (Exception ex)

            return result;
        }   // private Boolean PreparaPeticion(MetodoRest metodo, Method tipoMetodo, out IRestClient restClient, out IRestRequest restRequest)

        /// <summary>
        /// Comprueba si la respuesta de la llamada al WS es un error o no, asignando los valores a las properties correspondientes en el
        /// parámetro respuestaMetodo
        /// </summary>
        /// <param name="restResponse"><see cref="RestResponse"/> de la llamada al WS</param>
        /// <param name="respuestaMetodo">Result que devuelven los métodos propios</param>
        /// <returns>true si es una respuesta de error, false si no</returns>
        private Boolean EsRespuestaDeError(IRestResponse restResponse, MamasApiRestResponseBase respuestaMetodo)
        {
            var result = false;

            respuestaMetodo.EsError = (!restResponse.StatusCode.Equals(HttpStatusCode.OK) && !restResponse.StatusCode.Equals(HttpStatusCode.Created));
            respuestaMetodo.CodigoRespuesta = restResponse.StatusCode;

            if (respuestaMetodo.EsError)
            {
                respuestaMetodo.Error = JsonConvert.DeserializeObject<DetallesError>(restResponse.Content);
            }   // if (respuestaMetodo.EsError)
            else
            { respuestaMetodo.Json = restResponse.Content; }

            result = respuestaMetodo.EsError;

            return result;
        }   // private Boolean EsRespuestaDeError(IRestResponse restResponse, ref LocalizacionPasivaApiRestResponse respuestaMetodo)

        /// <summary>
        /// Desactiva la validación de certificados en conexiones HTTPS
        /// </summary>
        private void DesactivaSeguridadCertificadosHttps()
        {
            _remoteCertificateValidationCallbackPrevious = ServicePointManager.ServerCertificateValidationCallback;
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }

        /// <summary>
        /// Activa la validación de certificados en conexiones HTTPS
        /// </summary>
        private void ActivaSeguridadCertificadosHttps()
        {
            ServicePointManager.ServerCertificateValidationCallback = _remoteCertificateValidationCallbackPrevious;
            _remoteCertificateValidationCallbackPrevious = null;
        }

        private void EstableceSeguridadTransporte()
        {
            _securityProtocolPrevious = ServicePointManager.SecurityProtocol;
            SecurityProtocolType tls12 = SecurityProtocolType.Tls |
#if NET46
                 SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                
#else
                (SecurityProtocolType)768 | (SecurityProtocolType)3072;
#endif // NET46
                //             if (!ServicePointManager.SecurityProtocol.HasFlag(tls12))
//             {
                ServicePointManager.SecurityProtocol = tls12;
//             }
        }

        private void RestauraSeguridadTransporte()
        {
            ServicePointManager.SecurityProtocol = _securityProtocolPrevious;
        }

        private void EstableceSeguridadRequerida()
        {
            DesactivaSeguridadCertificadosHttps();
            EstableceSeguridadTransporte();
        }

        private void RestauraSeguridadPrevia()
        {
            ActivaSeguridadCertificadosHttps();
            RestauraSeguridadTransporte();
        }
        private void AjustaTipoParametros(ref Parameter[] parametros)
        {
            AjustaTipoParametros(ref parametros, ParameterType.GetOrPost);
        }   // private void AjustaTipoParametros(ref Parameter[] parametros)

        private void AjustaTipoParametros(ref Parameter[] parametros, ParameterType tipo)
        {
            if (parametros == null)
            {
                parametros = new Parameter[] { };
                return;
            }   // if (parametros == null)

            for (var idx = 0; idx < parametros.Length; idx++)
            { parametros[idx].Type = tipo; }
        }   // private void AjustaTipoParametros(ref Parameter[] parametros, ParameterType tipo)

        private TType CallWebServiceCommon<TType>(Method metodo, String uri, String wsMethod, Object body, params Parameter[] parametros)
            where TType : MamasApiRestResponseBase, new()
        {
            var result = default(TType);

            try
            {
//                 if (parametros == null)
//                 { parametros = new Parameter[] { }; }
                AjustaTipoParametros(ref parametros);

                IRestClient restClient;
                IRestRequest restRequest;

                if (PreparaPeticion(uri, metodo, out restClient, out restRequest))
                {
//                    parametros.ForEach(p => restRequest.AddParameter(p));
                    foreach(var p in parametros)
                        restRequest.AddParameter(p);

                    if (body != null)
                    { restRequest.AddJsonBody(body); }

                    var restResponse = restClient.Execute(restRequest);
                    result = new TType();

//                     if (metodo.Equals(Method.DELETE))
                    if (!EsRespuestaDeError(restResponse, result) &&
                        result.Json != "[ ]" &&
                        result.Json != "{ }")
                    {
                        var resultTmp = result;
                        result = JsonConvert.DeserializeObject<TType>(restResponse.Content);
                        result.Json = resultTmp.Json;
                    }
                }   // if (PreparaPeticion(uri, Method.GET, out restClient, out restRequest))
            }   // try
            catch (Exception ex)
            {
                result.Excepcion = ex;
            }   // catch(Exception ex)
            finally
            {
                if (result != null)
                {
                    //ActivaSeguridadCertificadosHttps(); 

                    RestauraSeguridadPrevia();
                }
            }   // finally

            return result;
        }   // private TType CallWebServiceCommon<TType>(Method metodo, String uri, String wsMethod, Object body, params Parameter[] parametros)

        private TResultType CallWebServiceCommon<TResultType, TArrayType>(Method metodo, String uri, String wsMethod, Object body, params Parameter[] parametros)
            where TResultType : MamasApiRestResponseArrayBase<TArrayType>, new()
            where TArrayType : MamasApiRestResponseBase
        {
            var result = default(TResultType);

            try
            {
//                 if (parametros == null)
//                 { parametros = new Parameter[] { }; }
                AjustaTipoParametros(ref parametros);

                IRestClient restClient;
                IRestRequest restRequest;

                if (PreparaPeticion(uri, metodo, out restClient, out restRequest))
                {
//                    parametros.ForEach(p => restRequest.AddParameter(p));

                    if (body != null)
                    { restRequest.AddJsonBody(body); }

                    var restResponse = restClient.Execute(restRequest);
                    result = new TResultType();

//                     if (metodo.Equals(Method.DELETE))
                    if (!EsRespuestaDeError(restResponse, result) &&
                        result.Json != "[ ]" &&
                        result.Json != "{ }")
                    {
                        var arList = JsonConvert.DeserializeObject<List<TArrayType>>(restResponse.Content);
                        arList.ForEach(p => result.Entries.Add(p));
                    }
                }   // if (PreparaPeticion(uri, Method.GET, out restClient, out restRequest))
            }   // try
            catch (Exception ex)
            {
                result.Excepcion = ex;
            }   // catch(Exception ex)
            finally
            {
                if (result != null)
                {
                    //ActivaSeguridadCertificadosHttps(); 

                    RestauraSeguridadPrevia();
                }
            }   // finally

            return result;
        }   // private TResultType CallWebServiceCommon<TResultType, TArrayType>(Method metodo, String uri, String wsMethod, Object body, params Parameter[] parametros)

        private TType CallWebServiceGetCommon<TType>(String uri, String wsMethod, params Parameter[] parametros)
            where TType : MamasApiRestResponseBase, new()
        {
            var result = CallWebServiceCommon<TType>(Method.GET, uri, wsMethod, null, parametros);
            return result;
        }   // private TType CallWebServiceGetCommon<TType>(String uri, String wsMethod, params Parameter[] parametros)

        private TResultType CallWebServiceGetCommon<TResultType, TArrayType>(String uri, String wsMethod, params Parameter[] parametros)
            where TResultType : MamasApiRestResponseArrayBase<TArrayType>, new()
            where TArrayType : MamasApiRestResponseBase
        {
            var result = CallWebServiceCommon<TResultType, TArrayType>(Method.GET, uri, wsMethod, null, parametros);
            return result;
        }   // private TResultType CallWebServiceGetCommon<TResultType, TArrayType>(String uri, String wsMethod, params Parameter[] parametros)

        private TType CallWebServicePostCommon<TType>(String uri, String wsMethod, Object body, params Parameter[] parametros)
            where TType : MamasApiRestResponseBase, new()
        {
            var result = CallWebServiceCommon<TType>(Method.POST, uri, wsMethod, body, parametros);
            return result;
        }   // private TType CallWebServicePostCommon<TType>(String uri, String wsMethod, Object body, params Parameter[] parametros)

        private TResultType CallWebServicePostCommon<TResultType, TArrayType>(String uri, String wsMethod, Object body, params Parameter[] parametros)
            where TResultType : MamasApiRestResponseArrayBase<TArrayType>, new()
            where TArrayType : MamasApiRestResponseBase
        {
            var result = CallWebServiceCommon<TResultType, TArrayType>(Method.POST, uri, wsMethod, body, parametros);
            return result;
        }   // private TResultType CallWebServiceGetCommon<TResultType, TArrayType>(String uri, String wsMethod, Object body, params Parameter[] parametros)

        private TType CallWebServicePutCommon<TType>(String uri, String wsMethod, Object body, params Parameter[] parametros)
            where TType : MamasApiRestResponseBase, new()
        {
            var result = CallWebServiceCommon<TType>(Method.PUT, uri, wsMethod, body, parametros);
            return result;
        }   // private TType CallWebServicePutCommon<TType>(String uri, String wsMethod, Object body, params Parameter[] parametros)

        private TType CallWebServiceDeleteCommon<TType>(String uri, String wsMethod, params Parameter[] parametros)
            where TType : MamasApiRestResponseBase, new()
        {
            var result = CallWebServiceCommon<TType>(Method.DELETE, uri, wsMethod, null, parametros);
            return result;
        }   // private TType CallWebServiceDeleteCommon<TType>(String uri, String wsMethod, params Parameter[] parametros)

#endregion // private Methods


    }   // public class IntendiaApi : ObservableObject
}   // namespace InComercial.IntendiaApiRest
