using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SkeletonEngine
{
    [Serializable()]
    public enum ResourceName
    {
        Pixel,        
    }

    [Serializable()]
    public class Resource
    {
        public BigDecimal Amount;

        public Resource(BigDecimal amount)
        {
            Amount = amount;
        }
    }

    static class ResourceCreation
    {
        static public List<Dictionary<ResourceName, Resource>> CreateSingle(ResourceName name, BigDecimal baseCost, int amountToAdd, float multiplier)
        {
            List<Dictionary<ResourceName, Resource>> Levels = new List<Dictionary<ResourceName, Resource>>();

            for (int i = 0; i < amountToAdd; i++)
            {
                Dictionary<ResourceName, Resource> resource = new Dictionary<ResourceName, Resource>();

                resource.Add(name, new Resource(baseCost));

                foreach (Resource R in resource.Values)
                    R.Amount *= Math.Pow(multiplier, Levels.Count + 1);

                Levels.Add(resource);
            }

            return Levels;
        }
    }
}
