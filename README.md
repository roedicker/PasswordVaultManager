# Introduction
``PasswordVaultManager`` is an encapsulation for managing credentials via a password vault

Its purpose is to abstract the OS layer to the application.

# PasswordVaultManager
The core package holds the required interface and model to be able to separate the definition from the descrete implementation. This can also be used for DI in a clean way.

# Features
With this package you are able to manage credentials of a password vault like the following:
* Get all credentials from the password vault of the current _OS_-user
* Get all "resource"-related credentials from the password vault of the current _OS_-user
* Get all "user-name"-related credentials from the password vault of the current _OS_-user
* Add / update a credential to/in the password vault for the current _OS_-user
* Remove a credential from the password vault for the current _OS_-user

# PasswordVaultManager.Windows
The package ``PasswordVaultManager.Windows`` wraps the management of credentials for the Windows Web credential store. This uses the _Microsoft.Windows.SDK.Contracts_.

In addition to this it stores passwords in a _SecureString_ instance rather than in a standard _string_.

# Extensibility
With this architecture you are able to implement you own _OS_-specific manager if required.

