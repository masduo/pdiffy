using Microsoft.Win32;

namespace PDiffy.Web.Infrastructure
{
	public static class Environment
	{
		public static string ImageStorePath
		{
			get
			{
				var imageStorePath = Get("pdiffyImageStorePath");
				return imageStorePath == null ? string.Empty : imageStorePath.ToString();
			}
			set { Set("pdiffyImageStorePath", value); }
		}

		public static string DataStorePath
		{
			get
			{
				var dataStorePath = Get("pdiffyDataStorePath");
				return dataStorePath == null ? string.Empty : dataStorePath.ToString();
			}
			set { Set("pdiffyDataStorePath", value); }
		}

		const string SubKeyPath = @"SYSTEM\CurrentControlSet\Control\Session Manager\Environment";

		static object Get(string name)
		{
			object value = null;
			using (RegistryKey envKey = Registry.LocalMachine.OpenSubKey(SubKeyPath))
				if (envKey != null) value = envKey.GetValue(name);

			return value;
		}

		static void Set(string name, string value)
		{
			using (var envKey = Registry.LocalMachine.OpenSubKey(SubKeyPath, true))
				if (envKey != null) envKey.SetValue(name, value);
		}
	}
}