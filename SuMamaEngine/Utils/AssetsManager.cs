using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace SuMamaEngine
{
	public class AssetsManager : IDisposable
	{
		private readonly ContentManager _content;
		private readonly Dictionary<string, Texture2D> _textures;
		private readonly Dictionary<string, SoundEffect> _sounds;
		private readonly Dictionary<string, Song> _songs;
		private readonly Dictionary<string, Effect> _effects;

		public AssetsManager(ContentManager content)
		{
			_content = content;
			_textures = new();
			_sounds = new();
			_songs = new();
			_effects = new();
		}

		//Load Assets Synchronous

		public void LoadTexture(string path)
		{
			if(string.IsNullOrEmpty(path)) throw new ArgumentNullException("AssetsManager.LoadTexture() path is null or emtpy");
			if(_textures.ContainsKey(path)) return;

			_textures.Add(path, _content.Load<Texture2D>(path));
		}

		public void LoadSounds(string path)
		{
			if(string.IsNullOrEmpty(path)) throw new ArgumentNullException("AssetsManager.LoadSounds() path is null or emtpy");
			if(_sounds.ContainsKey(path)) return;

			_sounds.Add(path, _content.Load<SoundEffect>(path));
		}

		public void LoadSongs(string path)
		{
			if(string.IsNullOrEmpty(path)) throw new ArgumentNullException("AssetsManager.LoadSongs() path is null or emtpy");
			if(_songs.ContainsKey(path)) return;

			_songs.Add(path, _content.Load<Song>(path));
		}

		public void LoadEffect(string path)
		{
			if(string.IsNullOrEmpty(path)) throw new ArgumentNullException("AssetsManager.LoadEffect() path is null or emtpy");
			if(_effects.ContainsKey(path)) return;

			_effects.Add(path, _content.Load<Effect>(path));
		}

		//Get Assets

		public Texture2D GetTexture(string assetName)
		{
			if(string.IsNullOrEmpty(assetName)) throw new ArgumentNullException("AssetsManager.GetTexture() assetName is null or emtpy");
			if(!_textures.ContainsKey(assetName)) throw new ArgumentException("AssetsManager.GetTexture() assetName does not match to any Texture2D in Textures");

			return _textures[assetName];
		}

		public SoundEffect GetSound(string assetName)
		{
			if(string.IsNullOrEmpty(assetName)) throw new ArgumentNullException("AssetsManager.GetSound() assetName is null or emtpy");
			if(!_sounds.ContainsKey(assetName)) throw new ArgumentException("AssetsManager.GetSound() assetName does not match to any SoundEffect in Sounds");

			return _sounds[assetName];
		}

		public Song GetSong(string assetName)
		{
			if(string.IsNullOrEmpty(assetName)) throw new ArgumentNullException("AssetsManager.GetSong() assetName is null or emtpy");
			if(!_songs.ContainsKey(assetName)) throw new ArgumentException("AssetsManager.GetSong() assetName does not match to any Song in Songs");

			return _songs[assetName];
		}

		public Effect GetEffect(string assetName)
		{
			if(string.IsNullOrEmpty(assetName)) throw new ArgumentNullException("AssetsManager.GetEffect() assetName is null or emtpy");
			if(!_effects.ContainsKey(assetName)) throw new ArgumentException("AssetsManager.GetEffect() assetName does not match to any Effect in Effects");

			return _effects[assetName];
		}

		private void UnloadAssets()
		{
			foreach(var texture in _textures.Values)
			{
				texture.Dispose();
			}

			foreach(var sound in _sounds.Values)
			{
				sound.Dispose();
			}

			foreach(var song in _songs.Values)
			{
				song.Dispose();
			}

			foreach(var effect in _effects.Values)
			{
				effect.Dispose();
			}

			_textures.Clear();
			_sounds.Clear();
			_songs.Clear();
			_effects.Clear();
		}

		public void Dispose()
		{
			UnloadAssets();
			GC.SuppressFinalize(this);
		}
	}
}
