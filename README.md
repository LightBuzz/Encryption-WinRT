# Data encryption for WinRT apps

An MD5 encryption provider for WinRT apps. Supports Windows Phone and Windows 8/8.1 apps. Can be used with SQLite.

## Installation
You can download and build this project or simply install it via NuGet:
	PM> Install-Package encryption-winrt

## Examples
Import the assembly to your project and include its namespace:
	using LightBuzz.Encryption;

### Encrypt a string
*You can use this method to store passwords into a database*
	string text = "This is plain text";

	IEncryption encryption = new MD5Encryption();
	string encrypted = encryption.Encrypt(text);

### Decrypt a string
*You can use this method to retrieve passwords from a database*
	string text = "Encrypted text from databse";

	IEncryption encryption = new MD5Encryption();
	string decrypted = encryption.Decrypt(text);

## Contributors
* [Vangos Pterneas](http://pterneas.com) from [LightBuzz](http://lightbuzz.com)

## License
You are free to use these libraries in personal and commercial projects by attributing the original creator of EncryptionWinRT. Licensed under [MIT License](https://github.com/LightBuzz/encryption-winrt/blob/master/LICENSE).