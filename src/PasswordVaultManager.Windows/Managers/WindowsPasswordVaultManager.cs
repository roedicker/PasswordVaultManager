using System;
using System.Collections.Generic;
using System.Security;

using Windows.Security.Credentials;

using PasswordVaultManager.Models;

namespace PasswordVaultManager.Windows.Managers
{
  /// <summary>
  /// Defines the Windows password vault manager
  /// </summary>
  public class WindowsPasswordVaultManager : IPasswordVaultManager
  {
    /// <summary>
    /// Gets the Windows password vault
    /// </summary>
    protected PasswordVault Vault
    {
      get;
    }

    /// <summary>
    /// Creates a new instance of the Windows password vault manager
    /// </summary>
    public WindowsPasswordVaultManager()
    {
      this.Vault = new PasswordVault();
    }

    /// <summary>
    /// Adds a credential to the Windows password vault
    /// </summary>
    /// <param name="credential">Credential to add</param>
    public void AddCredential(PasswordVaultCredential credential)
    {
      if (credential == null)
      {
        throw new ArgumentNullException(nameof(credential));
      }

      this.Vault.Add(new PasswordCredential()
      {
        Resource = CompleteResourceName(credential.ResourceName),
        UserName = credential.UserName,
        Password = credential.Password.GetString()
      });
    }

    /// <summary>
    /// Gets all Windows password vault credentials of the current user
    /// </summary>
    /// <returns>Read-only list of all available Windows credentials of the current user</returns>
    public IReadOnlyList<PasswordVaultCredential> GetAllCredentials()
    {
      List<PasswordVaultCredential> Result = new List<PasswordVaultCredential>();

      foreach (PasswordCredential oCredential in this.Vault.RetrieveAll())
      {
        oCredential.RetrievePassword();

        Result.Add(new PasswordVaultCredential(oCredential.Resource, oCredential.UserName, oCredential?.Password == null ? new SecureString() : new SecureString().SetString(oCredential.Password)));
      }

      return Result.AsReadOnly();
    }

    /// <summary>
    /// Gets a credential from the Windows password vault
    /// </summary>
    /// <param name="resourceName">Resource name of the credential</param>
    /// <param name="userName">User name of the credential</param>
    /// <returns>Windows password vault credential for given resource name and user name</returns>
    public PasswordVaultCredential GetCredential(string resourceName, string userName)
    {
      if (String.IsNullOrWhiteSpace(resourceName))
      {
        throw new ArgumentException(Resources.MissingRequiredParameterValueErrorMessage, nameof(resourceName));
      }

      if (String.IsNullOrWhiteSpace(userName))
      {
        throw new ArgumentException(Resources.MissingRequiredParameterValueErrorMessage, nameof(userName));
      }

      resourceName = CompleteResourceName(resourceName);
      PasswordCredential oCredential = this.Vault.Retrieve(resourceName, userName);

      return new PasswordVaultCredential(resourceName, userName, oCredential?.Password == null ? new SecureString() : new SecureString().SetString(oCredential.Password));
    }

    /// <summary>
    /// Gets all Windows password vault credentials for a given resource-name of the current user
    /// </summary>
    /// <param name="resourceName">Resource name of filtered credentials</param>
    /// <returns>Read-only list of all available Windows credentials for given resource name of the current user</returns>
    public IReadOnlyList<PasswordVaultCredential> GetResourceCredentials(string resourceName)
    {
      if (String.IsNullOrWhiteSpace(resourceName))
      {
        throw new ArgumentException(Resources.MissingRequiredParameterValueErrorMessage, nameof(resourceName));
      }

      resourceName = CompleteResourceName(resourceName);

      List<PasswordVaultCredential> Result = new List<PasswordVaultCredential>();

      foreach (PasswordCredential oCredential in this.Vault.FindAllByResource(resourceName))
      {
        oCredential.RetrievePassword();

        Result.Add(new PasswordVaultCredential(oCredential.Resource, oCredential.UserName, oCredential?.Password == null ? new SecureString() : new SecureString().SetString(oCredential.Password)));
      }

      return Result.AsReadOnly();
    }

    /// <summary>
    /// Gets all Windows password vault credentials for a given user-name of the current user
    /// </summary>
    /// <param name="userName">User name of filtered credentials</param>
    /// <returns>Read-only list of all available Windows credentials for given user name of the current user</returns>
    public IReadOnlyList<PasswordVaultCredential> GetUserCredentials(string userName)
    {
      if (String.IsNullOrWhiteSpace(userName))
      {
        throw new ArgumentException(Resources.MissingRequiredParameterValueErrorMessage, nameof(userName));
      }

      List<PasswordVaultCredential> Result = new List<PasswordVaultCredential>();

      foreach (PasswordCredential oCredential in this.Vault.FindAllByUserName(userName))
      {
        oCredential.RetrievePassword();

        Result.Add(new PasswordVaultCredential(oCredential.Resource, oCredential.UserName, oCredential == null ? new SecureString() : new SecureString().SetString(oCredential.Password)));
      }

      return Result.AsReadOnly();
    }

    /// <summary>
    /// Removes a credential from the Windows password vault
    /// </summary>
    /// <param name="resourceName">Resource name of the credential</param>
    /// <param name="userName">User name of the credential</param>
    public void RemoveCredential(string resourceName, string userName)
    {
      if (String.IsNullOrWhiteSpace(resourceName))
      {
        throw new ArgumentException(Resources.MissingRequiredParameterValueErrorMessage, nameof(resourceName));
      }

      if (String.IsNullOrWhiteSpace(userName))
      {
        throw new ArgumentException(Resources.MissingRequiredParameterValueErrorMessage, nameof(userName));
      }

      resourceName = CompleteResourceName(resourceName);

      try
      {
        this.Vault.Remove(this.Vault.Retrieve(resourceName, userName));
      }
      catch
      {
        // ignore any exception for removal (e.g. credential does not exist anymore)
      }
    }

    /// <summary>
    /// Completes a resource name by given name to ensure it follows the naming conventions of Windows password vault entries
    /// </summary>
    /// <param name="resourceName">Resource name to complete</param>
    /// <returns>Completed resource, or original resource if already completed</returns>
    private string CompleteResourceName(string resourceName)
    {
      if (String.IsNullOrWhiteSpace(resourceName))
      {
        throw new ArgumentException(Resources.MissingRequiredParameterValueErrorMessage, nameof(resourceName));
      }

      if (!resourceName.EndsWith(ResourceNameTail, StringComparison.InvariantCultureIgnoreCase))
      {
        resourceName += ResourceNameTail;
      }

      return resourceName;
    }

    private const string ResourceNameTail = "/";
  }
}
