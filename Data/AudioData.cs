
using System;

namespace PlusLuaModding.Data
{
	public class AudioData
	{
		public string key { get; set; }
		public string caption { get; set; }
		public string soundType { get; set; }
		public float[] captionColor { get; set; }
		public float sublength { get; set; }
		public bool hasCaption { get; set; }
		
		public AudioData()
		{
			caption = "Unknown";
			soundType = "Effect";
			captionColor = new float[4];
			sublength = -1f;
			hasCaption = true;
		}
	}
}
