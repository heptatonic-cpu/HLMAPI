
using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace PlusLuaModding.Lua.Proxies
{
	[MoonSharpUserData]
	public class Vector3Proxy
	{
		public override string ToString()
		{
			return string.Format("{0},{1},{2}", x, y, z);
		}
		
		public static Vector3Proxy one { get { return new Vector3Proxy(Vector3.one); } }
		
		public static Vector3Proxy zero { get { return new Vector3Proxy(Vector3.zero); } }
		
		public static Vector3Proxy up { get { return new Vector3Proxy(Vector3.up); } }
		
		public static Vector3Proxy down { get { return new Vector3Proxy(Vector3.down); } }
		
		public static Vector3Proxy left { get { return new Vector3Proxy(Vector3.left); } }
		
		public static Vector3Proxy right { get { return new Vector3Proxy(Vector3.right); } }
		
		public static Vector3Proxy forward { get { return new Vector3Proxy(Vector3.forward); } }
		
		public static Vector3Proxy back { get { return new Vector3Proxy(Vector3.back); } }
		
		public float x { get; private set; }
		
		public float y { get; private set; }
		
		public float z { get; private set; }
		
		public Vector3Proxy()
		{
			x = 0f;
			y = 0f;
			z = 0f;
		}
		
		public Vector3Proxy(float x, float y, float z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		public Vector3Proxy(Vector3 vec) {
			x = vec.x;
			y = vec.y;
			z = vec.z;
		}
		
		public static Vector3Proxy @new(float x, float y, float z) {
			return new Vector3Proxy(x, y, z);
		}
		
		public static Vector3Proxy operator +(Vector3Proxy a, Vector3Proxy b) {
			return new Vector3Proxy(a.x + b.x, a.y + b.y, a.z + b.z);
		}
		
		public static Vector3Proxy operator -(Vector3Proxy a, Vector3Proxy b) {
			return new Vector3Proxy(a.x - b.x, a.y - b.y, a.z - b.z);
		}
		
		public static bool operator ==(Vector3Proxy lhs, Vector3Proxy rhs) {
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Vector3Proxy lhs, Vector3Proxy rhs) {
			return !(lhs == rhs);
		}
		
		[MoonSharpHidden]
		public Vector3 ToVector() {
			return new Vector3(x, y, z);
		}
		
		public float Distance(Vector3Proxy other) {
			return Vector3.Distance(ToVector(), other.ToVector());
		}
		
		public void Normalize(Vector3Proxy o) {
			o.ToVector().Normalize();
		}
	
		
		public override bool Equals(object obj)
		{
			Vector3Proxy other = obj as Vector3Proxy;
				if (other == null)
					return false;
						return object.Equals(this.x, other.x) && object.Equals(this.y, other.y) && object.Equals(this.z, other.z);
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			unchecked {
				hashCode += 1000000007 * x.GetHashCode();
				hashCode += 1000000009 * y.GetHashCode();
				hashCode += 1000000021 * z.GetHashCode();
			}
			return hashCode;
		}
			
	}
	
	[MoonSharpUserData]
	public class Vector2Proxy
	{
		public override string ToString()
		{
			return string.Format("{0},{1}", x, y);
		}
		
		public static Vector2Proxy one { get { return new Vector2Proxy(Vector2.one); } }
		
		public static Vector2Proxy zero { get { return new Vector2Proxy(Vector2.zero); } }
		
		public static Vector2Proxy up { get { return new Vector2Proxy(Vector2.up); } }
		
		public static Vector2Proxy down { get { return new Vector2Proxy(Vector2.down); } }
		
		public static Vector2Proxy left { get { return new Vector2Proxy(Vector2.left); } }
		
		public static Vector2Proxy right { get { return new Vector2Proxy(Vector2.right); } }
		
		public float x { get; private set; }
		
		public float y { get; private set; }
		
		
		public Vector2Proxy()
		{
			x = 0f;
			y = 0f;
		}
		
		public Vector2Proxy(float x, float y) {
			this.x = x;
			this.y = y;
		}
		
		public Vector2Proxy(Vector2 vec) {
			x = vec.x;
			y = vec.y;
		}
		
		public static Vector2Proxy @new(float x, float y) {
			return new Vector2Proxy(x, y);
		}
		
		public static Vector2Proxy operator +(Vector2Proxy a, Vector2Proxy b) {
			return new Vector2Proxy(a.x + b.x, a.y + b.y);
		}
		
		public static Vector2Proxy operator -(Vector2Proxy a, Vector2Proxy b) {
			return new Vector2Proxy(a.x - b.x, a.y - b.y);
		}
		
		public static bool operator ==(Vector2Proxy lhs, Vector2Proxy rhs) {
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Vector2Proxy lhs, Vector2Proxy rhs) {
			return !(lhs == rhs);
		}
		
		[MoonSharpHidden]
		public Vector2 ToVector() {
			return new Vector2(x, y);
		}
		
		public float Distance(Vector2Proxy other) {
			return Vector2.Distance(ToVector(), other.ToVector());
		}
		
		public void Normalize(Vector2Proxy o) {
			o.ToVector().Normalize();
		}
	
		public override bool Equals(object obj)
		{
			Vector2Proxy other = obj as Vector2Proxy;
				if (other == null)
					return false;
						return object.Equals(this.x, other.x) && object.Equals(this.y, other.y);
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			unchecked {
				hashCode += 1000000007 * x.GetHashCode();
				hashCode += 1000000009 * y.GetHashCode();
			}
			return hashCode;
		}
			
	}
	
	[MoonSharpUserData]
	public class IntVector2Proxy
	{
		public override string ToString()
		{
			return string.Format("{0},{1}", x, z);
		}

		public int x { get; private set; }
		
		public int z { get; private set; }
		
		
		public IntVector2Proxy()
		{
			x = 0;
			z = 0;
		}
		
		public IntVector2Proxy(int x, int z) {
			this.x = x;
			this.z = z;
		}
		
		public IntVector2Proxy(IntVector2 vec) {
			x = vec.x;
			z = vec.z;
		}
		
		public static IntVector2Proxy @new(int x, int z) {
			return new IntVector2Proxy(x, z);
		}
		
		public static IntVector2Proxy operator +(IntVector2Proxy a, IntVector2Proxy b) {
			return new IntVector2Proxy(a.x + b.x, a.z + b.z);
		}
		
		public static IntVector2Proxy operator -(IntVector2Proxy a, IntVector2Proxy b) {
			return new IntVector2Proxy(a.x - b.x, a.z - b.z);
		}
		
		public override int GetHashCode()
		{
			int hashCode = 0;
				unchecked {
					hashCode += 1000000007 * x.GetHashCode();
					hashCode += 1000000009 * z.GetHashCode();
				}
					return hashCode;
		}

		public override bool Equals(object obj)
		{
			IntVector2Proxy other = obj as IntVector2Proxy;
			if (other == null)
				return false;
			return object.Equals(this.x, other.x) && object.Equals(this.z, other.z);
		}

		public static bool operator ==(IntVector2Proxy lhs, IntVector2Proxy rhs) {
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}

		public static bool operator !=(IntVector2Proxy lhs, IntVector2Proxy rhs) {
			return !(lhs == rhs);
		}
		
		[MoonSharpHidden]
		public IntVector2 ToVector() {
			return new IntVector2(x, z);
		}
	}
	
	[MoonSharpUserData]
	public class ColorProxy
	{
		public float r { get; private set; }
		
		public float g { get; private set; }
		
		public float b { get; private set; }
		
		public float a { get; private set; }

		public static ColorProxy red     { get { return new ColorProxy(255f, 0f, 0f); } }
		public static ColorProxy green   { get { return new ColorProxy(0f, 255f, 0f); } }
		public static ColorProxy blue    { get { return new ColorProxy(0f, 0f, 255f); } }
		public static ColorProxy white   { get { return new ColorProxy(255f, 255f, 255f); } }
		public static ColorProxy black   { get { return new ColorProxy(0f, 0f, 0f); } }
		public static ColorProxy yellow  { get { return new ColorProxy(255f, 235f, 4f); } }
		public static ColorProxy cyan    { get { return new ColorProxy(0f, 255f, 255f); } }
		public static ColorProxy magenta { get { return new ColorProxy(255f, 0f, 255f); } }
		public static ColorProxy gray    { get { return new ColorProxy(128f, 128f, 128f); } }
		public static ColorProxy grey    { get { return new ColorProxy(128f, 128f, 128f); } }
		public static ColorProxy clear   { get { return new ColorProxy(0f, 0f, 0f, 0f); } }
		
		public ColorProxy() {
			r = 0f; 
			g = 0f;
			b = 0f;
			a = 255f;
		}
		
		public ColorProxy(float r, float g, float b, float a = 255f) {
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}
		
		public ColorProxy(Color c) {
			r = c.r * 255f;
			g = c.g * 255f;
			b = c.b * 255f;
			a = c.a * 255f;
		}
		
		public static ColorProxy @new(float r, float g, float b, float a = 255f) {
			return new ColorProxy(r, g, b, a);
		}
		
		[MoonSharpHidden]
		public Color ToColor() {
			return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
		}
		
		public override string ToString()
		{
			return string.Format("R={0}, G={1}, B={2}, A={3}", r, g, b, a);
		}

	}
}
