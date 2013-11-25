using System;

namespace RetroGames.FileDescriptors
{
	public class Field : BaseField
	{
		public Field ()
		{
		}

		public static implicit operator Field(System.Type t)
		{
			return new Field { type = t };
		}

	}
}

