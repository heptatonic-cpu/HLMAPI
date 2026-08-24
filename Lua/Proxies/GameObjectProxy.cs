
using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace PlusLuaModding.Lua.Proxies
{
	[MoonSharpUserData]
	public class GameObjectProxy
	{
		GameObject obj;
		
		public GameObjectProxy(GameObject obj)
		{
			this.obj = obj;
		}
		
		public string name
		{
			get { return obj.name; }
			set { obj.name = value; }
		}
		
		public Vector3Proxy position
		{
			get { return new Vector3Proxy(obj.transform.position); }
			set { obj.transform.position = value.ToVector(); }
		}
		
		public Vector3Proxy localPosition
		{
			get { return new Vector3Proxy(obj.transform.localPosition); }
			set { obj.transform.localPosition = value.ToVector(); }
		}
		
		public Vector3Proxy eulerAngles
		{
			get { return new Vector3Proxy(obj.transform.eulerAngles); }
			set { obj.transform.eulerAngles = value.ToVector(); }
		}
		
		public Vector3Proxy localEulerAngles
		{
			get { return new Vector3Proxy(obj.transform.localEulerAngles); }
			set { obj.transform.localEulerAngles = value.ToVector(); }
		}
		
		public Vector3Proxy localScale
		{
			get { return new Vector3Proxy(obj.transform.localScale); }
			set { obj.transform.localScale = value.ToVector(); }
		}
		
		public Vector3Proxy forward
		{
			get { return new Vector3Proxy(obj.transform.forward); }
		}
		
		public float direction {
			get {
				return obj.transform.eulerAngles.y;
			} set {
				obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x,
				                                        value,
				                                        obj.transform.eulerAngles.z);
			}
		}
		
		public bool CompareTag(string tag) {
			return obj.CompareTag(tag);
		}
		
		public void Destroy() {
			UnityEngine.Object.Destroy(obj);
		}
	}
}
