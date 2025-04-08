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
		private Dictionary<string, Transition> _transitions;
		public Scene CurrentScene;

		private Scene _nextScene;
		private Transition _transitionIn;
		private Transition _transitionOut;
		private Scene _previousScene;
		private bool _sceneChanged;

		public bool IsChangingScene { get; private set; }

		private SceneManager()
		{
			_scenes = new Dictionary<string, Scene>();
			_transitions = new Dictionary<string, Transition>();
		}

		public void Start()
		{
			if(CurrentScene == null) throw new NullReferenceException("SceneManager.Start() Current scene is null");

			CurrentScene.Start();
		}

		public void Update()
		{
			if(CurrentScene == null) throw new NullReferenceException("SceneManager.Update() Current scene is null");

			if(IsChangingScene)
			{
				ProcessSceneChangeUpdate();
			}

			if(!IsChangingScene)
			{
				CurrentScene.Update();
			}

		}

		public void Draw()
		{
			if(CurrentScene == null) throw new NullReferenceException("SceneManager.Draw() Current scene is null");

			if(IsChangingScene)
			{
				ProcessSceneChangeDraw();
			}

			CurrentScene.Draw();

		}

		public void DrawUi()
		{
			if(CurrentScene == null) throw new NullReferenceException("SceneManager.DrawUi() Current scene is null");

			CurrentScene.DrawUi();
		}

		//Methods for handle the current scene

		public void ChangeScene(string sceneId, string transIn, string transOut)
		{
			if(string.IsNullOrEmpty(sceneId)) throw new ArgumentNullException("SceneManager.ChangeScene() sceneId is empty or null");
			if(_scenes.ContainsKey(sceneId))
			{
				if(CurrentScene == null)
				{
					_nextScene = _scenes[sceneId];
					_transitionIn = _transitions[transIn];
					_transitionOut = _transitions[transOut];
					_transitionIn.Reset();
					_transitionOut.Reset();
					_sceneChanged = false;
					IsChangingScene = true;
				}
				else
				{
					_nextScene = _scenes[sceneId];
					_previousScene = CurrentScene;
					_transitionIn = _transitions[transIn];
					_transitionOut = _transitions[transOut];
					_transitionIn.Reset();
					_transitionOut.Reset();
					_sceneChanged = false;
					IsChangingScene = true;
				}
			}
			else
			{
			    throw new Exception("SceneManager.ChangeScene() don't has a scene to correspond the sceneId");
			}

		}

		public void ChangeScene(string sceneId)
		{
			if(string.IsNullOrEmpty(sceneId)) throw new ArgumentNullException("SceneManager.ChangeScene() sceneId is empty or null");
			if(_scenes.ContainsKey(sceneId))
			{
				if(CurrentScene == null) // If this scene is the first scene of the game
				{
					_nextScene = _scenes[sceneId];
					_nextScene.Enter();
					CurrentScene = _nextScene;
				}
				else
				{
					_nextScene = _scenes[sceneId];
					_nextScene.Enter();
					_previousScene = CurrentScene;

					CurrentScene = _nextScene;
					_nextScene.Start();
					_previousScene.Exit();
				}
			}
			else
			{
			    throw new Exception("SceneManager.ChangeScene() don't has a scene to correspond the sceneId");
			}
			_previousScene = null;
			_nextScene = null;

		}

		private void ProcessSceneChangeUpdate()
		{
			_nextScene.Enter();
			_transitionIn.Update();
			if(_transitionIn.IsCompleted)
			{
				if(!_sceneChanged) 
				{
					CurrentScene = _nextScene;
					_nextScene.Start();
					_previousScene.Exit();
					_sceneChanged = true;
				}
				_transitionOut.Update();

				if(_transitionOut.IsCompleted)
				{
					IsChangingScene = false;
					_transitionIn = null;
					_transitionOut = null;
				}
			}
		}

		private void ProcessSceneChangeDraw()
		{
			if(_transitionIn.IsCompleted)
			{
				_transitionOut.Draw();
			}
			else
			{
			    _transitionIn.Draw();
			}
		}

		// Methods for Transitions

		public void AddTransition(string name, Transition transition)
		{
			if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("SceneManager.AddTransition() name is null or empty");
			if(transition == null) throw new ArgumentNullException("SceneManager.AddTransition() transition is null");
			_transitions.Add(name, transition);
		}

		public void RemoveTransition(string name)
		{
			if(string.IsNullOrEmpty(name)) throw new ArgumentNullException("SceneManager.RemoveTransition() name is null or empty");
			_transitions.Remove(name);
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
		}

		public void AddScene(string sceneId, Scene scene)
		{
			if(scene == null) throw new ArgumentNullException("SceneManager.AddScene() scene is null");
			if(string.IsNullOrEmpty(sceneId)) throw new ArgumentNullException("SceneManager.AddScene() sceneId is null or Empty");
			_scenes.Add(sceneId, scene);
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
