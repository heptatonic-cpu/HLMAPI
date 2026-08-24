
using System;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

namespace PlusLuaModding.Lua
{
	[MoonSharpUserData]
	public class EventService
	{
		private readonly Dictionary<string, List<Closure>> _listeners = new Dictionary<string, List<Closure>>();
		
		public void Register(string name, Closure cb) {
			if (!_listeners.ContainsKey(name)) {
				_listeners[name] = new List<Closure>();
			}
			
			if (!_listeners[name].Contains(cb)) {
				_listeners[name].Add(cb);
			}
		}
		
		public void Trigger(string name, params object[] args) {
            if (!_listeners.ContainsKey(name)) return;
            var list = _listeners[name];
            for (int i = 0; i < list.Count; i++) {
                list[i].Call(args);
            }
        }
		
		public void Clear() { _listeners.Clear(); }
	}
}
