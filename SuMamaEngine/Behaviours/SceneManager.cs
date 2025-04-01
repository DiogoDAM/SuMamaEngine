using System;
using System.Collections.Generic;

namespace SuMamaEngine
{
	public class SceneManager
	{
		private static SceneManager _instance;
		public static SceneManager Instance
		{
			get 
			{
				if(_instance == null) _instance = new SceneManager();

				return _instance;
			}

			private set
			{
				_instance = value;
			}
		}

		private Dictionary<string, Scene> _scenes;
		public Scene CurrentScene;

		private SceneManager()
		{
			_scenes = new Dictionary<string, Scene>();
		}

		public void Start()
		{
			if(CurrentScene == null) throw new NullReferenceException("SceneManager.Start() Current scene is null");

			CurrentScene.Start();
		}

		public void Update()
		{
			if(CurrentScene == null) throw new NullReferenceException("SceneManager.Update() Current scene is null");

			CurrentScene.Update();
		}

		public void Draw()
		{
			if(CurrentScene == null) throw new NullReferenceException("SceneManager.Draw() Current scene is null");

			CurrentScene.Draw();
		}

		public void DrawUi()
		{
			if(CurrentScene == null) throw new NullReferenceException("SceneManager.DrawUi() Current scene is null");

			CurrentScene.DrawUi();
		}

		//Methods for handle the current scene

		public void ChangeScene(Scene scene)
		{
			if(scene == null) throw new ArgumentNullException("SceneManager.SwitchScene() scene is null");
			if(_scenes.ContainsValue(scene))
			{
				if(CurrentScene != null) CurrentScene.Enter();
				CurrentScene = scene;
				CurrentScene.Exit();
			}
		}

		public void ChangeScene(string sceneId)
		{
			if(string.IsNullOrEmpty(sceneId)) throw new ArgumentNullException("SceneManager.SwitchScene() sceneId is empty or null");
			if(_scenes.ContainsKey(sceneId))
			{
				if(CurrentScene != null) CurrentScene.Enter();
				CurrentScene = _scenes[sceneId];
				CurrentScene.Exit();
			}

		}

		//  Methods for handle the _scenes dict
		
		public Scene GetScene(string sceneId)
		{
			if(string.IsNullOrEmpty(sceneId)) throw new ArgumentNullException("SceneManager.GetScene() sceneId is null or Empty");
			if(_scenes.ContainsKey(sceneId)) return _scenes[sceneId];
			return null;
		}

		public void AddScene(Scene scene)
		{
			if(scene == null) throw new ArgumentNullException("SceneManager.AddScene() scene is null");
			_scenes.Add(scene.Id, scene);
			if(CurrentScene == null) ChangeScene(scene);
		}

		public void AddScene(string sceneId, Scene scene)
		{
			if(scene == null) throw new ArgumentNullException("SceneManager.AddScene() scene is null");
			if(string.IsNullOrEmpty(sceneId)) throw new ArgumentNullException("SceneManager.AddScene() sceneId is null or Empty");
			_scenes.Add(sceneId, scene);
			if(CurrentScene == null) ChangeScene(sceneId);
		}

		public void RemoveScene(Scene scene)
		{
			if(scene == null) throw new ArgumentNullException("SceneManager.RemoveScene() scene is null");
			if(_scenes.ContainsValue(scene))
			{
				_scenes.Remove(scene.Id);
			}
		}

		public void RemoveScene(string sceneId)
		{
			if(string.IsNullOrEmpty(sceneId)) throw new ArgumentNullException("SceneManager.RemoveScene() sceneId is null or Empty");
			if(_scenes.ContainsKey(sceneId))
			{
				_scenes.Remove(sceneId);
			}
		}

		public bool ContainsScene(Scene scene)
		{
			if(scene == null) throw new ArgumentNullException("SceneManager.ContainsScene() scene is null");
			return _scenes.ContainsValue(scene);
		}

		public bool ContainsScene(string sceneId)
		{
			if(string.IsNullOrEmpty(sceneId)) throw new ArgumentNullException("SceneManager.ContainsScene() sceneId is null or Empty");
			return _scenes.ContainsKey(sceneId);
		}

		public Scene FindScene(Scene scene)
		{
			if(scene == null) throw new ArgumentNullException("SceneManager.FindScene() scene is null");
			foreach(var s in _scenes)
			{
				if(s.Value == scene) return s.Value;
			}

			return null;
		}

		public Scene FindScene(string sceneId)
		{
			if(string.IsNullOrEmpty(sceneId)) throw new ArgumentNullException("SceneManager.FindScene() sceneId is null or Empty");
			foreach(var s in _scenes)
			{
				if(s.Key == sceneId) return s.Value;
			}

			return null;
		}
	}
}
