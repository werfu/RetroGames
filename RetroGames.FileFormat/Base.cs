using System;

namespace RetroGames.FileDescriptors
{
	public class Base
	{
		public Base ()
		{
		}

		public int offset { get; protected set; }
		public int alignment { get; protected set; }
	}
}

