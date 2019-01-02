using LetsEncrypt.Azure.Core.Models;

namespace OhadSoft.AzureLetsEncrypt.Renewal.Configuration
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Build failing :(")]
    internal class AuthProviderConfig : IAuthorizationChallengeProviderConfig
    {
        public bool DisableWebConfigUpdate => false;
    }
}