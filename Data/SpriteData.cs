using System;

namespace PlusLuaModding.Data
{
    public class SpriteData
    {
        public string key { get; set; }
        public float[] offsets { get; set; }
        public float pixelsPerUnit { get; set; }
        
        public SpriteData()
        {
        	offsets = new float[2];
        	pixelsPerUnit = 10f;
        }
    }
}