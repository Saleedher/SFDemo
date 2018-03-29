namespace HelloWorldStatelessService
{
    using System;
    using System.Fabric;
    using System.Fabric.Description;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Owin.Hosting;
    using Microsoft.ServiceFabric.Services.Communication.Runtime;

    public class OwinCommunicationListener : ICommunicationListener
    {
        private readonly IOwinAppBuilder _startup;
        private readonly string _appRoot;
        private readonly StatelessServiceContext _parameters;

        private string _listeningAddress;
        private IDisposable _serverHandle;

        public OwinCommunicationListener(
            string appRoot,
            IOwinAppBuilder startup,
            //  Use StatelessServiceContext, NOT ServiceInitializationParameters 
            StatelessServiceContext serviceInitializationParameters
                )
        {
            _startup = startup;
            _appRoot = appRoot;
            _parameters = serviceInitializationParameters;
        }
        public void Abort()
        {
            ServiceEventSource.Current.Message("Abort");

            this.StopWebServer();
        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            ServiceEventSource.Current.Message("Close");
            this.StopWebServer();

            return Task.FromResult(true);

        }

        public Task<string> OpenAsync(CancellationToken cancellationToken)
        {
            EndpointResourceDescription serviceEndpoint = _parameters.CodePackageActivationContext.GetEndpoint("ServiceEndpoint");
            int port = serviceEndpoint.Port;

            this._listeningAddress = String.Format(
                CultureInfo.InvariantCulture,
                "http://+:{0}/{1}",
                port,
                String.IsNullOrWhiteSpace(this._appRoot)
                    ? String.Empty
                    : this._appRoot.TrimEnd('/') + '/');

            this._serverHandle = WebApp.Start(this._listeningAddress, appBuilder => this._startup.Configuration(appBuilder));
            string publishAddress = this._listeningAddress.Replace("+", FabricRuntime.GetNodeContext().IPAddressOrFQDN);

            ServiceEventSource.Current.Message("Listening on {0}", publishAddress);

            return Task.FromResult(publishAddress);

        }

        private void StopWebServer()
        {
            if (this._serverHandle != null)
            {
                try
                {
                    this._serverHandle.Dispose();
                }
                catch (ObjectDisposedException)
                {
                    // no-op
                }
            }
        }

    }
}
