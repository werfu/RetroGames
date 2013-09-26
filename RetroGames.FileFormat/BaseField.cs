using System;

namespace RetroGames.FileFormat
{
	public class BaseField : Base
	{
		public BaseField ()
		{
		}

		public System.Type type { get; protected set; }
	}
}

