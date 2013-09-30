using System;
using RetroGames.FileFormat;

namespace OpenMenace_Text
{
	public class BiomenaceDescriptors
	{
		public static RetroGames.FileFormat.Struct LevelHeaderDescriptor
		{
			get {
				return new RetroGames.FileFormat.Struct {
					{ "Offsets", new RetroGames.FileFormat.Array { type = typeof(UInt32), length = 3 } },
					{ "Lengths", new RetroGames.FileFormat.Array { type = typeof(UInt16), length = 3 } },
					{ "Width", typeof(UInt16) },
					{ "Height", typeof(UInt16) },
					{ "Name", new RetroGames.FileFormat.Array { type = typeof(Char), length = 16 } }
				};
			}
		}

		public static RetroGames.FileFormat.Struct MapHeaderDescriptor {
			get {
				return new RetroGames.FileFormat.Struct {
					{ "MagicWord" , typeof(UInt16) },
					{ "LevelPtr",  new RetroGames.FileFormat.Array { type = typeof(UInt32), length = 100 } }
				};
			}
		}

		public static RetroGames.FileFormat.Struct GFXInfoe {
			get {
				RetroGames.FileFormat.Struct Tiles = new RetroGames.FileFormat.Struct { {"Background", typeof(UInt16)} , { "Foreground", typeof(UInt16) } };
				RetroGames.FileFormat.Struct TilesTripplet = new RetroGames.FileFormat.Struct {
					{ "Tiles8", Tiles },
					{ "Tiles16", Tiles},
					{ "Tiles32" , Tiles }
				};

				return new RetroGames.FileFormat.Struct {
					{ "Count", TilesTripplet },
					{ "Starts", TilesTripplet },
					{ "UnknownText",  new RetroGames.FileFormat.Array { type = typeof(Char), length = 20 } }
				};
			}
		}
	}
}

