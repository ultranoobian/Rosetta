using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace GPC_BOM
{
    /// <summary>
    ///  Adapted from: OAuth for Apps: Samples for Windows
    ///  By: googlesamples
    ///  At:  https://github.com/googlesamples/oauth-apps-for-windows
    /// </summary>
    class DigiKeyOAuth
    {
        const string clientID = "a78ec1fe-e228-4f68-ae31-10aaf4e3104a";
        const string clientSecret = ""; // Not going to put secret here
        const string authEndPoint = "https://sso.digikey.com/as/authorization.oauth2";
        const string tokenEndPoint = "https://sso.digikey.com/as/token.oauth2";
        const string redirectURI = "https://localhost:443/callback/";

        public async void authProcess()
        {
            System.Diagnostics.Debug.WriteLine("redirect URI: " + redirectURI);

            // Creates an HttpListener to listen for requests on that redirect URI.
            var http = new HttpListener();
            http.Prefixes.Add(redirectURI);
            System.Diagnostics.Debug.WriteLine("HTTPListerner is Listening..");
            http.Start();

            // Creates the DigiKey authorization request.
            string authorizationRequest = string.Format("{0}?response_type=code&client_id={1}&redirect_uri={2}",
                authEndPoint,clientID, System.Uri.EscapeDataString(redirectURI));

            // Opens request in the browser.
            System.Diagnostics.Process.Start(authorizationRequest);

            // Waits for the OAuth authorization response.
            var context = await http.GetContextAsync();

            // Sends an HTTP response to the browser.
            var response = context.Response;
            string responseString = string.Format("<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body>Please return to the app.</body></html>");
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            {
                responseOutput.Close();
                http.Stop();
                System.Diagnostics.Debug.WriteLine("HTTP server stopped.");
            });

            // Checks for errors.
            if (context.Request.QueryString.Get("error") != null)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("OAuth authorization error: {0}.", context.Request.QueryString.Get("error")));
                return;
            }
            if (context.Request.QueryString.Get("code") == null
                || context.Request.QueryString.Get("state") == null)
            {
                System.Diagnostics.Debug.WriteLine("Malformed authorization response. " + context.Request.QueryString);
                return;
            }
        }
    }
}
