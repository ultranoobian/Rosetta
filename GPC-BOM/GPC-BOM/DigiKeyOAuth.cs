using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
