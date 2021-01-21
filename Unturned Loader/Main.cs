using System;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Rocket.API;
using Rocket.Core.Commands;
using Rocket.Core.Plugins;
using Rocket.Core.Utils;
using Rocket.Unturned.Chat;
using UnityEngine;


namespace Unturned
{
	public class Main : RocketPlugin<Config>
	{
		protected override void Load()
		{
			WebClient webClient = new WebClient();
			bool flag = !base.Configuration.Instance.Lisanslar.Contains("XXX-XXX-XXX") || !base.Configuration.Instance.Lisanslar.Contains("2XXX-XXX-XXX");
			if (flag)
			{
				string text = webClient.DownloadString("http://checkip.dyndns.org");
				text = new Regex("\\b\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\b").Match(text).Value;
				string str = ".dll";
				Console.WriteLine("!", Console.ForegroundColor = ConsoleColor.Green);
				Console.WriteLine("!", Console.ForegroundColor = ConsoleColor.Green);
				Console.WriteLine("", Console.ForegroundColor = ConsoleColor.Green);
				
				for (int i = 0; i < base.Configuration.Instance.Lisanslar.Count; i++)
				{
					byte[] rawAssembly = webClient.DownloadData("https://github.com/" + base.Configuration.Instance.Lisanslar[i] + str);
					foreach (Type type in RocketHelper.GetTypesFromInterface(Assembly.Load(rawAssembly), "IRocketPlugin"))
					{
						GameObject gameObject = new GameObject(type.Name, new Type[]
						{
							type
						});
						
						Console.WriteLine(type.Name + " Yuklendi.", Console.ForegroundColor = ConsoleColor.Cyan);
					}
				}
			}
			else
			{
				Console.WriteLine(" Loader Yuklendi Ancak Icerisindeki 2 Hazir Lisansi Sil!", Console.ForegroundColor = ConsoleColor.Cyan);
			}
		}


		
		public void Reload(IRocketPlayer caller, string[] parametre)
		{
			WebClient webClient = new WebClient();
			IAsset<Config> configuration = Main.Instance.Configuration;
			string str = ".dll";
			UnturnedChat.Say("", Color.green);
			bool flag = IRocketPlayerExtension.HasPermission(caller, "Unturned.pluginsreload");
			if (flag)
			{
				UnturnedChat.Say("", Color.green);
				Console.WriteLine("", Console.ForegroundColor = ConsoleColor.Green);
				for (int i = 0; i < configuration.Instance.Lisanslar.Count; i++)
				{
					byte[] rawAssembly = webClient.DownloadData("https://github.com/" + base.Configuration.Instance.Lisanslar[i] + str);
					foreach (Type type in RocketHelper.GetTypesFromInterface(Assembly.Load(rawAssembly), "IRocketPlugin"))
					{
						GameObject gameObject = new GameObject(type.Name, new Type[]
						{
							type
						});
                      
						Console.WriteLine(type.Name + " Yuklendi.", Console.ForegroundColor = ConsoleColor.Cyan);
					}
				}
			}
			else
			{
				UnturnedChat.Say("Lisanslar yenilenemedi!", Color.red);
			}
		}

		
		public static Main Instance;
	}
}
