﻿namespace BingoCoop
{
	public static class Log
	{
		public enum ErrorType { FileHandling, ShouldNotHappen }
		public enum WarningType { VarIsNull, PortError, IPError }

		public static void AssureLogs(string filePath)
		{
			// Assure log folder
			try
			{
				if (!Directory.Exists(Const.logsFolderPath))
				{
					Directory.CreateDirectory(Const.logsFolderPath);

					// Assure log file
					try
					{
						if (!File.Exists(filePath))
						{
							File.WriteAllText(filePath, string.Empty);
							Log.Message($"File created successfully at: {filePath}");
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Error creating logs file.\nTry restarting the application.", "Error");
						Application.Exit();
					}

					Log.Message($"Logs folder created successfully at: {filePath}");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error creating logs folder.\nTry restarting the application.", "Error");
				Application.Exit();
			}
		}

		public static void Message(string message)
		{
			File.AppendAllText(Const.logsFilePath, $"{GetTimestamp()}: [INFO] {message}\n");
		}

		public static void Warning(string message, WarningType warningType)
		{
			File.AppendAllText(Const.logsFilePath, $"{GetTimestamp()}: [Warning] (Type: {warningType}) {message}\n");
		}

		public static void Error(string message, ErrorType errorType)
		{
			File.AppendAllText(Const.logsFilePath, $"{GetTimestamp()}: [ERROR] (Type: {errorType}) {message}\n");
		}

		private static string GetTimestamp()
		{
			DateTime currentDateTime = DateTime.Now;
			return $"{currentDateTime:yyyy-MM-dd HH:mm:ss.fff}";
		}
	}
}
