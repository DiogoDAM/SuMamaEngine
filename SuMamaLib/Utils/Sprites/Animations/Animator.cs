using System;
using System.Collections.Generic;

namespace SuMamaLib.Utils.Sprites
{
    public class Animator
    {
        private Dictionary<string, Animation> _animationList;
        private string _animationCurrent;

        public string AnimationCurrent { get { return _animationCurrent; }}
        public bool IsActive { get { return _animationList[_animationCurrent].IsActive; }}
        public bool WasEnded { get { return _animationList[_animationCurrent].WasEnded; }}
        public int CurrentFrame { get { return _animationList[_animationCurrent].CurrentFrame; }}

        public Animator()
        {
            _animationList = new();
        }

        public void AddAnimation(string name, Animation anim)
        {
            if(String.IsNullOrEmpty(name)) {throw new Exception("The name of animation cannot be null or empty"); }
            if(_animationList.Count == 0) { _animationCurrent = name; }
            _animationList.Add(name, anim);
        }

        public void RemoveAnimation(string name)
        {
            if(!_animationList.ContainsKey(name)){ throw new Exception("The Animator don`t has this animation");}
            _animationList.Remove(name);
        }

        public void SetCurrentAnimation(string animName)
        {
            if(!_animationList.ContainsKey(animName)){ throw new Exception("The Animator don`t has this animation");}
            _animationCurrent = animName;
        }

        public Animation GetCurrentAnimation()
        {
            return _animationList[_animationCurrent];
        }

        public void Update()
        {
            _animationList[_animationCurrent].Update();
        }

        public void Start()
        {
            _animationList[_animationCurrent].Start();
        }

        public void Resume()
        {
            _animationList[_animationCurrent].Resume();
        }

        public void Stop()
        {
            _animationList[_animationCurrent].Stop();
        }

        public void Restart()
        {
            _animationList[_animationCurrent].Restart();
        }
    }
}