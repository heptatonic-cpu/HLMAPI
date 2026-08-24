
using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace PlusLuaModding.Lua
{
	[MoonSharpUserData]
	public class InputService
	{
		public bool anyKey {
			get {
				return Input.anyKey;
			}
		}
		
		public bool anyKeyDown {
			get {
				
				return Input.anyKeyDown;
			}
		}
		
		public string inputString {
			get {
				return Input.inputString;
			}
		}
		
	
		
		public bool GetKey(string key) { return Input.GetKey(key); }
		public bool GetKeyDown(string key) { return Input.GetKeyDown(key); }
		public bool GetKeyUp(string key) { return Input.GetKeyUp(key); }
		public bool GetMouseButton(int button) { return Input.GetMouseButton(button); }
		public bool GetMouseButtonDown(int button) { return Input.GetMouseButtonDown(button); }
		public bool GetMouseButtonUp(int button) { return Input.GetMouseButtonUp(button); }
	}
}
