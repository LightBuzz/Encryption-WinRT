# Data encryption for WinRT apps

A simple encryption provider for WinRT apps. Supports Windows Phone and Windows 8/8.1 apps. Can be used with SQLite.

You can use SHA1, SHA256, SHA384, SHA512 or MD5 algorithms. Defaults to SHA256.

## Installation
You can download and build this project or simply install it via NuGet:
	
	PM> Install-Package encryption-winrt

## Examples
Import the assembly to your project and include its namespace:
	
	using LightBuzz.Encryption;

### Encrypt a string
*You can use this method to store passwords into a database*
	
	string text = "This is plain text";

	IEncryption encryption = new Encryption();
	string encrypted = encryption.Encrypt(text);

### Decrypt a string
*You can use this method to retrieve passwords from a database*
	
	string text = "Encrypted text from database";

	IEncryption encryption = new Encryption();
	string decrypted = encryption.Decrypt(text);

### Specify a different algorithm
*The default algorithm used is SHA256*
	
	IEncryption encryption = new Encryption();
	encryption.Algorithm = HashAlgorithmNames.Sha512;

## Contributors
* [Vangos Pterneas](http://pterneas.com) from [LightBuzz](http://lightbuzz.com)

## License
You are free to use these libraries in personal and commercial projects by attributing the original creator of the project. [View full License](https://github.com/LightBuzz/Encryption-WinRT/blob/master/LICENSE).
