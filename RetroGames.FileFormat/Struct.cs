using System;
using System.Collections;
using System.Collections.Generic;

namespace RetroGames.FileFormat
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
		#endregion

		public Struct ()
		{
		}
	}
}

