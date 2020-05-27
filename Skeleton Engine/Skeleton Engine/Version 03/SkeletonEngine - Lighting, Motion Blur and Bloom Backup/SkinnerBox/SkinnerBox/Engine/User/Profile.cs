using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    [Serializable()]
    public class Profile
    {
        public string Name;
        public Color PrimaryColor;
        public Dictionary<ResourceName, Resource> Resource = new Dictionary<ResourceName,Resource>();

        public Profile(string name, Color color)
        {
            Name = name;
            PrimaryColor = color;
            Resource.Add(ResourceName.Pixel, new Resource(0));
        }
    }

    [Serializable()]
    static class Profiles
    {
        static public Dictionary<string, Profile> profiles = new Dictionary<string, Profile>();
        


    }
}
