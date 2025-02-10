using System;
using System.Collections.Generic;

namespace SuMamaLib
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
		private Scene _currentScene;

		private SceneManager()
		{
			_scenes = new Dictionary<string, Scene>();
		}

		public void Start()
		{
			if(_currentScene == null) throw new NullReferenceException("SceneManager.Start() Current scene is null");

			_currentScene.Start();
		}

		public void Update()
		{
			if(_currentScene == null) throw new NullReferenceException("SceneManager.Update() Current scene is null");

			_currentScene.Update();
		}

		public void Draw()
		{
			if(_currentScene == null) throw new NullReferenceException("SceneManager.Draw() Current scene is null");

			_currentScene.Draw();
		}

		public void DrawUi()
		{
			if(_currentScene == null) throw new NullReferenceException("SceneManager.DrawUi() Current scene is null");

			_currentScene.DrawUi();
		}

		//Methods for handle the current scene

		public void SwitchScene(Scene scene)
		{
			if(scene == null) throw new ArgumentNullException("SceneManager.SwitchScene() scene is null");
			if(_scenes.ContainsValue(scene))
			{
				if(_currentScene != null) _currentScene.Enter();
				_currentScene = scene;
				_currentScene.Exit();
			}
		}

		public void SwitchScene(string sceneId)
		{
			if(string.IsNullOrEmpty(sceneId)) throw new ArgumentNullException("SceneManager.SwitchScene() sceneId is empty or null");
			if(_scenes.ContainsKey(sceneId))
			{
				if(_currentScene != null) _currentScene.Enter();
				_currentScene = _scenes[sceneId];
				_currentScene.Exit();
			}

		}

		//  Methods for handle the _scenes dict

		public void AddScene(Scene scene)
		{
			if(scene == null) throw new ArgumentNullException("SceneManager.AddScene() scene is null");
			_scenes.Add(scene.Id, scene);
			if(_currentScene == null) SwitchScene(scene);
		}

		public void AddScene(string sceneId, Scene scene)
		{
			if(scene == null) throw new ArgumentNullException("SceneManager.AddScene() scene is null");
			if(string.IsNullOrEmpty(sceneId)) throw new ArgumentNullException("SceneManager.AddScene() sceneId is null or Empty");
			_scenes.Add(sceneId, scene);
			if(_currentScene == null) SwitchScene(sceneId);
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

		// Methods for handle the gameObjects in current Scene

		public void InstantiateObject(GameObject obj)
		{
			if(obj == null) throw new ArgumentNullException("Intantiate() obj is null");
			_currentScene.AddObject(obj);
		}

		public void InstantiateObject(GameObject obj, Transform transParent)
		{
			if(obj == null) throw new ArgumentNullException("Intantiate() obj is null");
			obj.Transform.Parent = transParent;
			_currentScene.AddObject(obj);
		}

		public void DestroyObject(GameObject obj)
		{
			if(obj == null) throw new ArgumentNullException("Destroy() obj is null");
			if(_currentScene.ContainsObject(obj))
			{
				_currentScene.RemoveObject(obj);
			}
		}

	}
}
