using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Rocket.API;

namespace Unturned
{
	public class Config : IRocketPluginConfiguration, IDefaultable
	{
		public void LoadDefaults()
		{
			this.Lisanslar = new List<string>
			{
				"XXX-XXX-XXX",
				"2XXX-XXX-XXX"
			};
		}

		[XmlArray("Lisanslar")]
		[XmlArrayItem("Lisans")]
		public List<string> Lisanslar = new List<string>();
	}
}
