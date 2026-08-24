
using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace PlusLuaModding.Lua.Proxies
{
	[MoonSharpUserData]
	public class CellProxy
	{
		Cell cell;
		
		public CellProxy(Cell cell) {
			this.cell = cell;
		}
		
		public Vector3Proxy centerWorldPosition
		{
			get { return new Vector3Proxy(cell.CenterWorldPosition); }
		}
		public Vector3Proxy floorWorldPosition
		{
			get { return new Vector3Proxy(cell.FloorWorldPosition); }
		}
		public Vector3Proxy randomCenterWorldPosition
		{
			get { return new Vector3Proxy(cell.RandomCenterWorldPosition); }
		}
		public int constBin
		{
			get { return cell.ConstBin; }
		}
		public ColorProxy currentColor
		{
			get { return new ColorProxy(cell.CurrentColor); }
		}
		
		public void SetLight(bool on) {
			cell.SetLight(on);
		}
		
		public void SetPower(bool power) {
			cell.SetPower(power);
		}
	}
}
