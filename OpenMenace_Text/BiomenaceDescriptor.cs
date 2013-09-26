using System;
using RetroGames.FileFormat;

namespace OpenMenace_Text
{
	public class BiomenaceDescriptors
	{
		public static var LevelHeaderDescriptor
		{
			get {
				return new {
					Offsets = { type = UInt32, length = 3 },
					Lengths = { type = UInt16, length = 3 },
					Width = UInt16,
					Height = UInt16,
					Name = { Type = Char, length = 16 }
				};
			}
		}

		public static var MapHeaderDescriptor {
			get {
				return new {
					MagicWord = UInt16,
					LevelPtr = { Type = UInt32, length = 100 },
				};
			}
		}

		public static var GFXInfoe {
			get {
				Struct Tiles = new Struct { "Background" = UInt16, "Foreground" = UInt16 };

				return new {
					Tiles8 = {
						Background = UInt16,
						Foreground = UInt16
					},
					Tiles16 = {
						Background = UInt16,
						Foreground = UInt16
					},
					Tiles32 = {
						Background = UInt16,
						Foreground = UInt32
					},
					Starts = {
						BackTile8 = UInt16,
						ForeTile8 = UInt16,
						BackTile16 = UInt16,
						ForeTile16 = UInt16,
						BackTile32 = UInt16,
						ForeTile32 = UInt16,
					},
					UnknownText = { type = Char, length = 20 }
				};
			}
		}
	}
}

