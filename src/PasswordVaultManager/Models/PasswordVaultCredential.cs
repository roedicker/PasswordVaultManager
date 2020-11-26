using System;
using System.Security;

namespace PasswordVaultManager.Models
{
  /// <summary>
  /// Defines a credential of a password vault
  /// </summary>
  public class PasswordVaultCredential
  {
    /// <summary>
    /// Gets the resource name of the credential
    /// </summary>
    public string ResourceName
    {
      get;
    }

    /// <summary>
    /// Gets the user name of the credential
    /// </summary>
    public string UserName
    {
      get;
    }

    /// <summary>
    /// Gets the password of the credential
    /// </summary>
    public SecureString Password
    {
      get;
    }

    /// <summary>
    /// Creates a new instance of a password vault credential
    /// </summary>
    /// <param name="resourceName">Resource name of the credential</param>
    /// <param name="userName">User name of the credential</param>
    /// <param name="password">Password of the credential</param>
    public PasswordVaultCredential(string resourceName, string userName, SecureString password)
    {
      if (String.IsNullOrWhiteSpace(resourceName))
      {
        throw new ArgumentException(nameof(resourceName));
      }

      if (String.IsNullOrWhiteSpace(userName))
      {
        throw new ArgumentException(nameof(userName));
      }

      this.ResourceName = resourceName;
      this.UserName = userName;
      this.Password = password;
    }
  }
}
