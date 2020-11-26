using System;
using System.Collections.Generic;

using PasswordVaultManager.Models;

namespace PasswordVaultManager
{
  /// <summary>
  /// Defines the password vault service
  /// </summary>
  public interface IPasswordVaultManager
  {
    /// <summary>
    /// Gets all password vault credentials of the current user
    /// </summary>
    /// <returns>Read-only list of all available credentials of the current user</returns>
    /// <remarks>>The credentials do not contain any password information. Use <see cref="GetCredential(string, string)"/> instead.</remarks>
    IReadOnlyList<PasswordVaultCredential> GetAllCredentials();

    /// <summary>
    /// Gets all password vault credentials for a given resource-name of the current user
    /// </summary>
    /// <param name="resourceName">Resource name of filtered credentials</param>
    /// <returns>Read-only list of all available credentials for given resource name of the current user</returns>
    IReadOnlyList<PasswordVaultCredential> GetResourceCredentials(string resourceName);

    /// <summary>
    /// Gets all password vault credentials for a given user-name of the current user
    /// </summary>
    /// <param name="userName">User name of filtered credentials</param>
    /// <returns>Read-only list of all available credentials for given user name of the current user</returns>
    IReadOnlyList<PasswordVaultCredential> GetUserCredentials(string userName);

    /// <summary>
    /// Adds a credential to the password vault
    /// </summary>
    /// <param name="credential">Credential to add</param>
    void AddCredential(PasswordVaultCredential credential);

    /// <summary>
    /// Gets a credential from the password vault
    /// </summary>
    /// <param name="resourceName">Resource name of the credential</param>
    /// <param name="userName">User name of the credential</param>
    /// <returns>Password vault credential for given resource name and user name</returns>
    PasswordVaultCredential GetCredential(string resourceName, string userName);

    /// <summary>
    /// Removes a credential from the password vault
    /// </summary>
    /// <param name="resourceName">Resource name of the credential</param>
    /// <param name="userName">User name of the credential</param>
    void RemoveCredential(string resourceName, string userName);
  }
}
