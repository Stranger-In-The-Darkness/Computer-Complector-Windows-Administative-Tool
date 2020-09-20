using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security.Cryptography;
using ViewModel;
using System.IO;

namespace ComputerComplectorAdministrativeTool
{
	/// <summary>
	/// Логика взаимодействия для LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		private bool _rememberLogin = false;

		public LoginWindow()
		{
			InitializeComponent();

			RememberMeCheckBox.IsChecked = Properties.Settings.Default.RememberLogin;

			DataContext = new LoginViewModel(string.Format(Properties.Settings.Default.AuthentificationRequestFormat, Properties.Settings.Default.APIURIString));

			if (DataContext is LoginViewModel model)
			{
				password.PasswordChanged += (sender, args) =>
				{
					if (sender is PasswordBox pass)
					{
						model.Password = pass.Password;
					}
				};

				model.PropertyChanged += (sender, property) =>
				{
					if (property.PropertyName == "IsAuthorized" && model.IsAuthorized)
					{
						IsSuccess = true;

						MainWindow window = new MainWindow();

						if (model.User.Role.ToUpper() == "ADMIN" ||
							model.User.Role.ToUpper() == "MODER")
						{
							if (_rememberLogin && !Properties.Settings.Default.RememberLogin)
							{
								Properties.Settings.Default.RememberLogin = true;

								byte[] username = Encrypt(model.Username, out byte[] key, out byte[] IV);
								byte[] password = Encrypt(model.Password, key, IV);

								string usernameString = ConvertToString(username);
								string passwrodString = ConvertToString(password);

								string keyString = ConvertToString(key);
								string IVString = ConvertToString(IV);

								Properties.Settings.Default.Login = usernameString;
								Properties.Settings.Default.Password = passwrodString;

								Properties.Settings.Default.Key = keyString;
								Properties.Settings.Default.IV = IVString;

								Properties.Settings.Default.Save();
							}

							window.User = model.User;
							Application.Current.MainWindow = window;
							window.Show();
							Close();
						}
						else
						{
							model.Error = $"Access denied. You shall not pass {model.User.Name}!";

							var row = body.RowDefinitions[3];

							if (model.Error == "")
							{
								row.Height = new GridLength(0);
							}
							else
							{
								row.Height = new GridLength(1, GridUnitType.Star);
							}
						}
					}
					else if (property.PropertyName == "Error")
					{
						var row = body.RowDefinitions[2];

						if (model.Error == "")
						{
							row.Height = new GridLength(0);
						}
						else
						{
							row.Height = new GridLength(1, GridUnitType.Star);
						}
					}
				};

				if (Properties.Settings.Default.RememberLogin)
				{
					byte[] key = ConvertToByteArray(Properties.Settings.Default.Key);
					byte[] IV = ConvertToByteArray(Properties.Settings.Default.IV);
					byte[] login = ConvertToByteArray(Properties.Settings.Default.Login);
					byte[] password = ConvertToByteArray(Properties.Settings.Default.Password);

					string loginDecrypt = Decrypt(login, key, IV);
					string passwordDecrypt = Decrypt(password, key, IV);

					model.Username = loginDecrypt;
					model.Password = passwordDecrypt;
					model.Authorize.Execute(null);
				}
			}
		}

		public bool IsSuccess { get; private set; } = false;

		private void CloseBtn_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			base.OnMouseLeftButtonDown(e);

			// Begin dragging the window
			this.DragMove();
		}

		private byte[] Encrypt(string data, out byte[] key, out byte[] IV)
		{
			if (data == null || data.Length <= 0)
				throw new ArgumentNullException("data");

			byte[] encrypted;

			// Create an Aes object
			// with the specified key and IV.
			using (Aes aesAlg = Aes.Create())
			{
				key = aesAlg.Key;
				IV = aesAlg.IV;

				// Create an encryptor to perform the stream transform.
				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				// Create the streams used for encryption.
				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							//Write all data to the stream.
							swEncrypt.Write(data);
						}
						encrypted = msEncrypt.ToArray();
					}
				}
			}

			// Return the encrypted bytes from the memory stream.
			return encrypted;
		}

		private byte[] Encrypt(string data, byte[] key, byte[] IV)
		{
			if (data == null || data.Length <= 0)
				throw new ArgumentNullException("data");
			if (key == null || key.Length <= 0)
				throw new ArgumentNullException("key");
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException("IV");

			byte[] encrypted;

			// Create an Aes object
			// with the specified key and IV.
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = key;
				aesAlg.IV = IV;

				// Create an encryptor to perform the stream transform.
				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				// Create the streams used for encryption.
				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							//Write all data to the stream.
							swEncrypt.Write(data);
						}
						encrypted = msEncrypt.ToArray();
					}
				}
			}

			// Return the encrypted bytes from the memory stream.
			return encrypted;
		}

		private string Decrypt(byte[] cipherText, byte[] key, byte[] IV)
		{
			// Check arguments.
			if (cipherText == null || cipherText.Length <= 0)
				throw new ArgumentNullException("cipherText");
			if (key == null || key.Length <= 0)
				throw new ArgumentNullException("Key");
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException("IV");

			// Declare the string used to hold
			// the decrypted text.
			string plaintext = null;

			// Create an Aes object
			// with the specified key and IV.
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = key;
				aesAlg.IV = IV;

				// Create a decryptor to perform the stream transform.
				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				// Create the streams used for decryption.
				using (MemoryStream msDecrypt = new MemoryStream(cipherText))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{

							// Read the decrypted bytes from the decrypting stream
							// and place them in a string.
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}
			}

			return plaintext;
		}

		private byte[] ConvertToByteArray(string array)
		{
			return array.Split('.').Select(e => byte.Parse(e)).ToArray();
		}

		private string ConvertToString(byte[] array)
		{
			return string.Join(".", array);
		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			var checkBox = sender as CheckBox;
			bool value = checkBox.IsChecked.Value;
			if (value)
			{
				_rememberLogin = true;
			}
			else
			{
				_rememberLogin = false;
			}
		}
	}
}
