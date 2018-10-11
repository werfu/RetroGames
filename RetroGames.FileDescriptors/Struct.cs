using System;
using System.Collections;
using System.Collections.Generic;

namespace RetroGames.FileDescriptors
{
	public class Struct : Base, IEnumerable
	{
		#region IEnumerable implementation
		private List<KeyValuePair<string, Base>> _list = new List<KeyValuePair<string, Base>>();
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return _list.GetEnumerator ();
		}

		public void Add(string name, Base b)
		{
			_list.Add (new KeyValuePair<string, Base> (name, b));
		}

		public void Add(string name, Type t)
		{
			_list.Add(new KeyValuePair<string, Base>(name, (Field) t));
		}
		#endregion

		public Struct ()
		{
		}
	}
}

