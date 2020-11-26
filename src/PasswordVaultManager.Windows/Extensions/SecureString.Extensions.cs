using System;
using System.Runtime.InteropServices;
using System.Security;

namespace PasswordVaultManager.Windows
{
  /// <summary>
  /// Defines extensions for <see cref="SecureString"/>
  /// </summary>
  internal static class SecureStringExtensions
  {
    /// <summary>
    /// Appends a string value to a secure string
    /// </summary>
    /// <param name="s">Secure string instance</param>
    /// <param name="value">String value to add</param>
    /// <returns>Updated secure string</returns>
    /// <remarks><c>null</c>-Values will be ignored</remarks>
    public static SecureString AppendString(this SecureString s, string value)
    {
      if (value != null)
      {
        foreach (char c in value)
        {
          s.AppendChar(c);
        }
      }

      return s;
    }

    /// <summary>
    /// Sets a secure string by a given string
    /// </summary>
    /// <param name="s">Secure string instance</param>
    /// <param name="value">String value to be set</param>
    /// <returns>Set secure string</returns>
    public static SecureString SetString(this SecureString s, string value)
    {
      s.Clear();

      return AppendString(s, value);
    }

    /// <summary>
    /// Gets an indicator whether a secure string is empty or not
    /// </summary>
    /// <param name="s">Secure string instance</param>
    /// <returns><c>True</c> if secure string is empty, otherwise <c>False</c></returns>
    public static bool IsEmpty(this SecureString s)
    {
      return (s.Length == 0);
    }

    /// <summary>
    /// Gets the plain string value of a secure string
    /// </summary>
    /// <param name="s">Secure string instance</param>
    /// <returns>Plain string value of the secure string</returns>
    public static string GetString(this SecureString s)
    {
      string Result;
      IntPtr pValue = IntPtr.Zero;

      if ((s == null) || (s.Length == 0))
      {
        Result = String.Empty;
      }
      else
      {
        try
        {
          pValue = Marshal.SecureStringToBSTR(s);
          Result = Marshal.PtrToStringBSTR(pValue);
        }
        finally
        {
          if (pValue != IntPtr.Zero)
          {
            Marshal.ZeroFreeBSTR(pValue);
          }
        }
      }

      return Result;
    }
  }
}
