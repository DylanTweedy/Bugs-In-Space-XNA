using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    class Element
    {
        public int ID;

        public bool WeightChange = false;
        public bool DensityChange = false;
        public bool MeltingPointChange = false;
        public bool BoilingPointChange = false;

        public string Name;
        public string Symbol;
        public float AtomicWeight;
        
        public int Period;
        public int Group;
        public int Category;

        public string PeriodName;
        public string GroupName;
        public string CategoryName;

        public int Protons;
        public int Neutrons;
        public int Electrons;

        public string Description;
        public Color Color;

        public float Hardness;
        public float Density;
        public float MeltingPoint;
        public float BoilingPoint;
        public float Bounce;
        public float Friction;
        public float Fertility;
        
        public void GetGroupingNames()
        {
            #region Period

            switch (Period)
            {
                case 1:
                    PeriodName = "Period 1";
                    break;

                case 2:
                    PeriodName = "Period 2";
                    break;

                case 3:
                    PeriodName = "Period 3";
                    break;

                case 4:
                    PeriodName = "Period 4";
                    break;

                case 5:
                    PeriodName = "Period 5";
                    break;

                case 6:
                    PeriodName = "Period 6";
                    break;

                case 7:
                    PeriodName = "Period 7";
                    break;

                case 8:
                    PeriodName = "Period 8";
                    break;

                case 9:
                    PeriodName = "Period 9";
                    break;

                case 10:
                    PeriodName = "Period 10";
                    break;

                case 11:
                    PeriodName = "Period Omega";
                    break;

                case 0:
                    PeriodName = "Void";
                    break;
            }

            #endregion

            #region Group

            switch (Group)
            {
                case 1:
                    GroupName = "Alkali Metals";
                    break;

                case 2:
                    GroupName = "Alkaline Earth Metals";
                    break;

                case 3:
                    GroupName = "Scandium Family";
                    break;

                case 4:
                    GroupName = "Titanium Family";
                    break;

                case 5:
                    GroupName = "Vanadium Family";
                    break;

                case 6:
                    GroupName = "Chromium Family";
                    break;

                case 7:
                    GroupName = "Manganese Family";
                    break;

                case 8:
                    GroupName = "Iron Family";
                    break;

                case 9:
                    GroupName = "Cobalt Family";
                    break;

                case 10:
                    GroupName = "Nickel Family";
                    break;

                case 11:
                    GroupName = "Coinage Metals";
                    break;

                case 12:
                    GroupName = "Volatile Metals";
                    break;

                case 13:
                    GroupName = "Triels";
                    break;

                case 14:
                    GroupName = "Tetrels";
                    break;

                case 15:
                    GroupName = "Pnictogens";
                    break;

                case 16:
                    GroupName = "Chalcogens";
                    break;

                case 17:
                    GroupName = "Halogens";
                    break;

                case 18:
                    GroupName = "Noble Gases";
                    break;

                case 0:
                    GroupName = "";
                    break;
            }

            #endregion

            #region Category

            switch (Category)
            {
                case 1:
                    CategoryName = "Alkali Metal";
                    break;

                case 2:
                    CategoryName = "Alkaline Earth Metal";
                    break;

                case 3:
                    CategoryName = "Superactinide";
                    break;

                case 4:
                    CategoryName = "Eka-​Superactinide";
                    break;

                case 5:
                    CategoryName = "Lanthanide";
                    break;

                case 6:
                    CategoryName = "Actinide";
                    break;

                case 7:
                    CategoryName = "Transition Metal";
                    break;

                case 8:
                    CategoryName = "Poor Metal";
                    break;

                case 9:
                    CategoryName = "Metalloid";
                    break;

                case 10:
                    CategoryName = "Polyatomic Nonmetal";
                    break;

                case 11:
                    CategoryName = "Diatomic Nonmetal";
                    break;

                case 12:
                    CategoryName = "Noble gas";
                    break;

                case 13:
                    CategoryName = "";
                    break;

                case 0:
                    CategoryName = "";
                    break;
            }

            #endregion
        }
    }

    class Compound
    {
        public int ID;
        public string Name;
        public string Formula;
        public string Description;
        public float AtomicWeight;
        public float Density;
        public float MeltingPoint;
        public float BoilingPoint;
        public float Hardness;
        public float Bounce;
        public float Friction;
        public float Fertility;

        public List<Texture2D> Textures;

        public List<Element> Elements;
        public List<int> ElementCounts;
    }

    static class ElementTable
    {
        static public List<Element> Elements;
        static public List<Compound> Compounds;
        static public int variants;
        static private Compound ActiveCompound;
        static private List<int> StoredCompoundElements;
        static private List<int> CompoundElements;
        static private string StoredFormula;
        static private string formula;
        static private List<List<int>> variantList;
        static private List<List<int>> randomCount;
        static private List<List<Compound>> RandomCompounds;
        static private Random random;
        static private Random propertyRandom;
        static private Random graphicsRandom;
        static private Random colorRandom;
        static private List<string> brackets;
        static private List<string> element;
        static private List<int> elementCount;
        static private string form;
        static private ContentManager content;
        static private int TextureCounter;
        static private bool LoadTextures;
        static private List<Texture2D> elementTextures;
        static private List<RenderTarget2D> renderTargets;
        static private SpriteBatch spriteBatch;
        static private GraphicsDevice gDevice;
             
        static public void Initialize(ContentManager Content, SpriteBatch spritebatch, GraphicsDevice graphicsDevice)
        {
            Elements = new List<Element>();
            Compounds = new List<Compound>();
            CompoundElements = new List<int>();

            random = new Random(WorldVariables.ElementSeed);
            graphicsRandom = new Random(WorldVariables.ElementSeed);
            colorRandom = new Random(WorldVariables.ElementSeed);
            propertyRandom = new Random(WorldVariables.ElementSeed);

            StoredCompoundElements = new List<int>();

            ActiveCompound = new Compound();

            RandomCompounds = new List<List<Compound>>();
            randomCount = new List<List<int>>();

            content = Content;
            spriteBatch = spritebatch;
            gDevice = graphicsDevice;

            renderTargets = new List<RenderTarget2D>();

            AddElements();            
        }

        static private void AddElements()
        {
            Color noColor = new Color(0, 0, 0, 0);

            AddElement("Element Zero", "0", 0f, 0f, 0f, 0f, 0, 0, 0, new Color(0,0,0,0), 0f, 0f, 0f, 0f, "");
            AddElement("Hydrogen", "H", 1.008f, 0.00008988f, 14.01f, 20.28f, 1, 1, 11, new Color(255, 255, 255, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Helium", "He", 4.002602f, 0.0001785f, 0.95f, 4.22f, 18, 1, 12, new Color(217, 255, 255, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Lithium", "Li", 6.94f, 0.534f, 453.69f, 1560f, 1, 2, 1, new Color(204, 128, 255, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Beryllium", "Be", 9.012182f, 1.85f, 1560f, 2742f, 2, 2, 2, new Color(194, 255, 0, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Boron", "B", 10.81f, 2.34f, 2349f, 4200f, 13, 2, 9, new Color(255, 181, 181, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Carbon", "C", 12.011f, 2.267f, 3800f, 4300f, 14, 2, 10, new Color(144, 144, 144, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Nitrogen", "N", 14.007f, 0.0012506f, 63.15f, 77.36f, 15, 2, 11, new Color(48, 80, 248, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Oxygen", "O", 15.999f, 0.001429f, 54.36f, 90.20f, 16, 2, 11, new Color(255, 13, 13, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Fluorine", "F", 18.9984032f, 0.001696f, 53.53f, 85.03f, 17, 2, 11, new Color(144, 224, 80, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Neon", "Ne", 20.1797f, 0.0008999f, 24.56f, 27.07f, 18, 2, 12, new Color(179, 227, 245, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Sodium", "Na", 22.98976928f, 0.971f, 370.87f, 1156f, 1, 3, 1, new Color(171, 92, 242, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Magnesium", "Mg", 24.305f, 1.738f, 923f, 1363f, 2, 3, 2, new Color(138, 255, 0, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Aluminium", "Al", 26.9815386f, 2.698f, 933.47f, 2792f, 13, 3, 8, new Color(191, 166, 166, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Silicon", "Si", 28.085f, 2.3296f, 1687f, 3538f, 14, 3, 9, new Color(240, 200, 160, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Phosphorus", "P", 30.973762f, 1.82f, 317.30f, 550f, 15, 3, 10, new Color(255, 128, 0, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Sulfur", "S", 32.06f, 2.067f, 388.36f, 717.87f, 16, 3, 10, new Color(255, 255, 48, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Chlorine", "Cl", 35.45f, 0.003214f, 171.6f, 239.11f, 17, 3, 11, new Color(31, 240, 31, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Argon", "Ar", 39.948f, 0.0017837f, 83.80f, 87.30f, 18, 3, 12, new Color(128, 209, 227, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Potassium", "K", 39.0983f, 0.862f, 336.53f, 1032f, 1, 4, 1, new Color(143, 64, 212, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Calcium", "Ca", 40.078f, 1.54f, 1115f, 1757f, 2, 4, 2, new Color(61, 255, 0, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Scandium", "Sc", 44.955912f, 2.989f, 1814f, 3109f, 3, 4, 7, new Color(230, 230, 230, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Titanium", "Ti", 47.867f, 4.54f, 1941f, 3560f, 4, 4, 7, new Color(191, 194, 199, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Vanadium", "V", 50.9415f, 6.11f, 2183f, 3680f, 5, 4, 7, new Color(166, 166, 171, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Chromium", "Cr", 51.9961f, 7.15f, 2180f, 2944f, 6, 4, 7, new Color(138, 153, 199, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Manganese", "Mn", 54.938045f, 7.44f, 1519f, 2334f, 7, 4, 7, new Color(0, 0, 0, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Iron", "Fe", 55.845f, 7.874f, 1811f, 3134f, 8, 4, 7, new Color(224, 102, 51, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Cobalt", "Co", 58.933195f, 8.86f, 1768f, 3200f, 9, 4, 7, new Color(240, 144, 160, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Nickel", "Ni", 58.6934f, 8.912f, 1728f, 3186f, 10, 4, 7, new Color(80, 208, 80, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Copper", "Cu", 63.546f, 8.96f, 1357.77f, 2835f, 11, 4, 7, new Color(200, 128, 51, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Zinc", "Zn", 65.38f, 7.134f, 692.88f, 1180f, 12, 4, 7, new Color(125, 128, 176, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Gallium", "Ga", 69.723f, 5.907f, 302.9146f, 2477f, 13, 4, 8, new Color(194, 143, 143, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Garmanium", "Ge", 72.630f, 5.323f, 1211.40f, 3106f, 14, 4, 9, new Color(102, 143, 143, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Arsenic", "As", 74.92160f, 5.776f, 1090f, 887f, 15, 4, 9, new Color(189, 128, 227, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Selenium", "Se", 78.96f, 4.809f, 453f, 958f, 16, 4, 10, new Color(255, 161, 0, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Bromine", "Br", 79.904f, 3.122f, 265.8f, 332f, 17, 4, 11, new Color(166, 41, 41, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Krypton", "Kr", 83.798f, 0.003733f, 115.79f, 119.93f, 18, 4, 12, new Color(92, 184, 209, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Rubidium", "Rb", 85.4678f, 1.532f, 312.46f, 961f, 1, 5, 1, new Color(112, 46, 176, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Strontium", "Sr", 87.62f, 2.64f, 1050f, 1655f, 2, 5, 2, new Color(0, 255, 0, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Yttrium", "Y", 88.90585f, 4.469f, 1799f, 3609f, 3, 5, 7, new Color(148, 255, 255, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Zirconium", "Zr", 91.224f, 6.506f, 2128f, 4682f, 4, 5, 7, new Color(148, 224, 224, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Niobium", "Nb", 92.90638f, 8.57f, 2750f, 5017f, 5, 5, 7, new Color(115, 194, 201, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Molybdenum", "Mo", 95.96f, 10.22f, 2896f, 4912f, 6, 5, 7, new Color(84, 181, 181, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Technetium", "Tc", 98f, 11.5f, 2430f, 4538f, 7, 5, 7, new Color(59, 158, 158, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Ruthenium", "Ru", 101.07f, 12.37f, 2607f, 4423f, 8, 5, 7, new Color(36, 143, 143, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Rhodium", "Rh", 102.90550f, 12.41f, 2237f, 3968f, 9, 5, 7, new Color(10, 125, 140, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Palladium", "Pd", 106.42f, 12.02f, 1828.05f, 3236f, 10, 5, 7, new Color(0, 105, 133, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Silver", "Ag", 107.8682f, 10.501f, 1234.93f, 2435f, 11, 5, 7, new Color(192, 192, 192, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Cadmium", "Cd", 112.411f, 8.69f, 594.22f, 1040f, 12, 5, 7, new Color(255, 217, 143, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Indium", "In", 114.818f, 7.31f, 429.75f, 2345f, 13, 5, 8, new Color(166, 117, 115, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Tin", "Sn", 118.710f, 7.287f, 505.08f, 2875f, 14, 5, 8, new Color(102, 128, 128, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Antimony", "Sb", 121.760f, 6.685f, 903.78f, 1860f, 15, 5, 9, new Color(158, 99, 181, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Tellurium", "Te", 127.60f, 6.232f, 722.66f, 1261f, 16, 5, 9, new Color(212, 122, 0, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Iodine", "I", 126.90447f, 4.93f, 386.85f, 457.4f, 17, 5, 11, new Color(148, 0, 148, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Xenon", "Xe", 131.293f, 0.005887f, 161.4f, 165.03f, 18, 5, 12, new Color(66, 158, 176, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Caesium", "Cs", 132.9054519f, 1.873f, 301.59f, 944f, 1, 6, 1, new Color(87, 23, 143, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Barium", "Ba", 137.327f, 3.594f, 1000f, 2170f, 2, 6, 2, new Color(0, 201, 0, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Lanthanum", "La", 138.90547f, 6.145f, 1193f, 3737f, 3, 6, 5, new Color(112, 212, 255, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Cerium", "Ce", 140.116f, 6.77f, 1068f, 3716f, 3, 6, 5, new Color(255, 255, 199, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Praseodymium", "Pr", 140.90765f, 6.773f, 1208f, 3793f, 3, 6, 5, new Color(217, 255, 199, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Neodymium", "Nd", 144.242f, 7.007f, 1297f, 3347f, 3, 6, 5, new Color(199, 255, 199, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Promethium", "Pm", 145f, 7.26f, 1315f, 3273f, 3, 6, 5, new Color(163, 255, 199, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Samarium", "Sm", 150.36f, 7.52f, 1345f, 2067f, 3, 6, 5, new Color(143, 255, 199, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Europium", "Eu", 151.964f, 5.243f, 1099f, 1802f, 3, 6, 5, new Color(97, 255, 199, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Gadolinium", "Gd", 157.25f, 7.895f, 1585f, 3546f, 3, 6, 5, new Color(69, 255, 199, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Terbium", "Tb", 158.92535f, 8.229f, 1629f, 3503f, 3, 6, 5, new Color(48, 255, 199, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Dysprosium", "Dy", 162.500f, 8.55f, 1680f, 2840f, 3, 6, 5, new Color(31, 255, 199, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Holmium", "Ho", 164.93032f, 8.795f, 1734f, 2993f, 3, 6, 5, new Color(0, 255, 156, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Erbium", "Er", 167.259f, 9.066f, 1802f, 3141f, 3, 6, 5, new Color(0, 230, 117, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Thulium", "Tm", 168.93421f, 9.321f, 1818f, 2223f, 3, 6, 5, new Color(0, 212, 82, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Ytterbium", "Yb", 173.054f, 6.965f, 1097f, 1469f, 3, 6, 5, new Color(0, 191, 56, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Lutetium", "Lu", 174.9668f, 9.84f, 1925f, 3675f, 3, 6, 5, new Color(0, 171, 36, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Hafnium", "Hf", 178.49f, 13.31f, 2506f, 4876f, 4, 6, 7, new Color(77, 194, 255, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Tantalum", "Ta", 180.94788f, 16.654f, 3290f, 5731f, 5, 6, 7, new Color(77, 166, 255, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Tungsten", "W", 183.84f, 19.25f, 3695f, 5828f, 6, 6, 7, new Color(33, 148, 214, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Rhenium", "Re", 186.207f, 21.02f, 3459f, 5869f, 7, 6, 7, new Color(38, 125, 171, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Osmium", "Os", 190.23f, 22.61f, 3306f, 5285f, 8, 6, 7, new Color(38, 102, 150, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Iridium", "Ir", 192.217f, 22.56f, 2719f, 4701f, 9, 6, 7, new Color(23, 84, 135, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Platinum", "Pt", 195.084f, 21.46f, 2041.4f, 4098f, 10, 6, 7, new Color(208, 208, 224, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Gold", "Au", 196.966569f, 19.282f, 1337.33f, 3129f, 11, 6, 7, new Color(255, 209, 35, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Mercury", "Hg", 200.592f, 13.5336f, 234.43f, 629.88f, 12, 6, 7, new Color(184, 184, 208, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Thallium", "Tl", 204.38f, 11.85f, 577f, 1746f, 13, 6, 8, new Color(166, 84, 77, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Lead", "Pb", 207.2f, 11.342f, 600.61f, 2022f, 14, 6, 8, new Color(87, 89, 97, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Bismuth", "Bi", 208.98040f, 9.807f, 544.7f, 1837f, 15, 6, 8, new Color(158, 79, 181, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Polonium", "Po", 209f, 9.32f, 527f, 1235f, 16, 6, 8, new Color(171, 92, 0, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Astatine", "At", 210f, 7f, 575f, 610f, 17, 6, 9, new Color(117, 79, 69, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Radon", "Rn", 222f, 0.00973f, 202f, 211.3f, 18, 6, 12, new Color(66, 130, 150, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Francium", "Fr", 223f, 1.87f, 300f, 950f, 1, 7, 1, new Color(66, 0, 102, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Radium", "Ra", 226f, 5.5f, 973f, 2010f, 2, 7, 2, new Color(0, 125, 0, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Actinium", "Ac", 227f, 10.07f, 1323f, 3471f, 3, 7, 6, new Color(112, 171, 250, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Thorium", "Th", 232.03806f, 11.72f, 2115f, 5061f, 3, 7, 6, new Color(0, 186, 255, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Protactinium", "Pa", 231.03588f, 15.37f, 1841f, 4300f, 3, 7, 6, new Color(0, 161, 255, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Uranium", "U", 238.02891f, 18.95f, 1405.3f, 4404f, 3, 7, 6, new Color(0, 143, 255, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Neptunium", "Np", 237f, 20.45f, 917f, 4273f, 3, 7, 6, new Color(0, 128, 255, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Plutonium", "Pu", 244f, 19.84f, 912.5f, 3501f, 3, 7, 6, new Color(0, 107, 255, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Americium", "Am", 243f, 13.69f, 1449f, 2880f, 3, 7, 6, new Color(84, 92, 242, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Curium", "Cm", 247f, 13.51f, 1613f, 3383f, 3, 7, 6, new Color(120, 92, 227, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Berkelium", "Bk", 247f, 14.79f, 1259f, 2900f, 3, 7, 6, new Color(138, 79, 227, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Californium", "Cf", 251f, 15.1f, 1173f, 1743f, 3, 7, 6, new Color(161, 54, 212, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Einsteinium", "Es", 252f, 8.84f, 1133f, 1269f, 3, 7, 6, new Color(179, 31, 212, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Fermium", "Fm", 257f, 0f, 1125f, 0f, 3, 7, 6, new Color(179, 31, 186, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Mendelevium", "Md", 258f, 0f, 1100f, 0f, 3, 7, 6, new Color(179, 13, 166, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Nobelium", "No", 259f, 0f, 1100f, 0f, 3, 7, 6, new Color(189, 13, 135, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Lawrencium", "Lr", 262f, 0f, 1900f, 0f, 3, 7, 6, new Color(199, 0, 102, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Rutherfordium", "Rf", 267f, 23.2f, 2400f, 5800f, 4, 7, 7, new Color(204, 0, 89, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Dubnium", "Db", 268f, 29.3f, 0f, 0f, 5, 7, 7, new Color(209, 0, 79, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Seaborgium", "Sg", 269f, 35.0f, 0f, 0f, 6, 7, 7, new Color(217, 0, 69, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Bohrium", "Bh", 270f, 37.1f, 0f, 0f, 7, 7, 7, new Color(224, 0, 56, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Hassium", "Hs", 269f, 40.7f, 0f, 0f, 8, 7, 7, new Color(230, 0, 46, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Meitnerium", "Mt", 278f, 37.4f, 0f, 0f, 9, 7, 7, new Color(235, 0, 38, 255), 0f, 0f, 0f, 0f, "");
            AddElement("Darmstadtium", "Ds", 281f, 34.8f, 0f, 0f, 10, 7, 7, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Roentgenium", "Rg", 281f, 28.7f, 0f, 0f, 11, 7, 7, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Copernicium", "Cn", 285f, 23.7f, 0f, 357f, 12, 7, 7, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Queenium", "Q", 286f, 16f, 700f, 1400f, 13, 7, 8, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Flerovium", "Fl", 289f, 14f, 340f, 420f, 14, 7, 8, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Easium", "Ey", 288f, 13.5f, 700f, 1400f, 15, 7, 8, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Livermorium", "Lv", 293f, 12.9f, 708.5f, 1085f, 16, 7, 8, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Peasium", "Ps", 294f, 7.2f, 673f, 823f, 17, 7, 9, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Potatium", "Ot", 294f, 5.0f, 258f, 263f, 18, 7, 12, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");

            AddElement("Alphanium", "A", 0f, 0f, 0f, 0f, 1, 8, 1, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Betanium", "Bt", 0f, 0f, 0f, 0f, 2, 8, 2, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Gammanium", "Gm", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Deltanium", "D", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Epsillium", "E", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Iotium", "Io", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Kappium", "Ka", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Lambdanium", "L", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Omicronium", "Om", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Rhonium", "Ph", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Sigmanium", "Ig", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Taunium", "T", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Upsilonium", "Up", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Omegium", "Z", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Graphium ", "Gr", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Rickrollium", "Rr", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Membranium", "Mb", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Porkium", "Pk", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Mintium", "Mi", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Explodium", "Ex", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Implodium", "Im", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Combustium", "Cb", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Tutorium", "Tt", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Finalbossium", "Fb", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Creditium", "Ct", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Romanium", "Ro", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Pacmanium", "Wk", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Isaacium", "Is", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Meatboinium", "Ma", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Diablium", "Di", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Medium", "Me", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Picardium", "Pc", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Rickmanium", "Rm", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Fonzium", "Eh", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Flarbon", "Fa", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Glarbon", "Gb", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Jarbon", "Jb", 0f, 0f, 0f, 0f, 3, 8, 3, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Bourbon", "Hi", 0f, 0f, 0f, 0f, 4, 8, 7, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Pootonium", "Pn", 0f, 0f, 0f, 0f, 5, 8, 7, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Tootonium", "To", 0f, 0f, 0f, 0f, 6, 8, 7, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Tweedium ", "Tw", 0f, 0f, 0f, 0f, 7, 8, 7, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Lexium", "Lx", 0f, 0f, 0f, 0f, 8, 8, 7, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Fozzium", "Fz", 0f, 0f, 0f, 0f, 9, 8, 7, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Tollium", "Lt", 0f, 0f, 0f, 0f, 10, 8, 7, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Lukium", "Lk", 0f, 0f, 0f, 0f, 11, 8, 7, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Luthorium", "Lh", 0f, 0f, 0f, 0f, 12, 8, 7, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Mepsteadium", "Mp", 0f, 0f, 0f, 0f, 1, 9, 1, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Drapnium", "Dr", 0f, 0f, 0f, 0f, 2, 9, 2, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Tommium", "Ch", 0f, 0f, 0f, 0f, 13, 9, 8, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Cabinium", "Yh", 0f, 0f, 0f, 0f, 14, 9, 8, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("O'Regium", "Or", 0f, 0f, 0f, 0f, 15, 9, 8, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Shartium", "Sh", 0f, 0f, 0f, 0f, 16, 9, 8, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Lassium", "Ls", 0f, 0f, 0f, 0f, 17, 9, 11, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Amyum", "Ay", 0f, 0f, 0f, 0f, 18, 9, 12, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Dylum", "Dl", 0f, 0f, 0f, 0f, 3, 10, 4, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Damium", "Dm", 0f, 0f, 0f, 0f, 3, 10, 4, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Elementium", "El", 0f, 0f, 0f, 0f, 3, 10, 4, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Woodium", "Wd", 0f, 0f, 0f, 0f, 3, 10, 4, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Gassium", "Gs", 0f, 0f, 0f, 0f, 3, 10, 4, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Metalium", "Ha", 0f, 0f, 0f, 0f, 3, 10, 4, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Rockium", "Rk", 0f, 0f, 0f, 0f, 3, 10, 4, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Stonium", "St", 0f, 0f, 0f, 0f, 3, 10, 4, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Liquidium", "Lq", 0f, 0f, 0f, 0f, 3, 10, 4, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Solidium", "Sl", 0f, 0f, 0f, 0f, 3, 10, 4, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Grampium", "Gp", 0f, 0f, 0f, 0f, 3, 10, 4, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Granium", "Gn", 0f, 0f, 0f, 0f, 3, 10, 4, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");

            AddElement("Element 185", "Tmp1", 0f, 0f, 0f, 0f, 1, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 186", "Tmp2", 0f, 0f, 0f, 0f, 2, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 187", "Tmp3", 0f, 0f, 0f, 0f, 3, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 188", "Tmp4", 0f, 0f, 0f, 0f, 4, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 189", "Tmp5", 0f, 0f, 0f, 0f, 5, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 190", "Tmp6", 0f, 0f, 0f, 0f, 6, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 191", "Tmp7", 0f, 0f, 0f, 0f, 7, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 192", "Tmp8", 0f, 0f, 0f, 0f, 8, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 193", "Tmp9", 0f, 0f, 0f, 0f, 9, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 194", "Tmp10", 0f, 0f, 0f, 0f, 10, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 195", "Tmp11", 0f, 0f, 0f, 0f, 11, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 196", "Tmp12", 0f, 0f, 0f, 0f, 12, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 197", "Tmp13", 0f, 0f, 0f, 0f, 13, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 198", "Tmp14", 0f, 0f, 0f, 0f, 14, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 199", "Tmp15", 0f, 0f, 0f, 0f, 15, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 200", "Tmp16", 0f, 0f, 0f, 0f, 16, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 201", "Tmp17", 0f, 0f, 0f, 0f, 17, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element 202", "Tmp18", 0f, 0f, 0f, 0f, 18, 11, 13, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
            AddElement("Element One", "1", 0f, 0f, 0f, 0f, 0, 0, 0, new Color(0, 0, 0, 0), 0f, 0f, 0f, 0f, "");
        }
        
        static private void AddElement(string name, string symbol, float atomicWeight, float density, float meltingPoint, float boilingPoint, int group, int period, int category, Color color, float bounce, float friction, float fertility, float hardness, string description)
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                if (Elements[i].Name == name || Elements[i].Symbol == symbol)
                {
                }
            }

            bool weightChange = false;
            bool densityChange = false;
            bool meltingPointChange = false;
            bool boilingPointChange = false;
            
            if (atomicWeight == 0f)
            {
                atomicWeight = Elements.Count * (random.Next(2000, 4000) / 1000f);
                weightChange = true;
            }

            if (density == 0f)
            {
                density = atomicWeight / (random.Next(8000, 12000) / 1000f);
                densityChange = true;
            }

            if (meltingPoint == 0f)
            {
                meltingPoint = Elements.Count * (random.Next(1000, 12000) / 1000f);
                meltingPointChange = true;
            }

            if (boilingPoint == 0f)
            {
                boilingPoint = Elements.Count * (random.Next(12000, 20000) / 1000f);
                boilingPointChange = true;

            }

            if (bounce == 0f)
            {
                bounce = (float)propertyRandom.NextDouble();
                bounce = (float)Math.Round(bounce, 3);
            }

            if (friction == 0f)
            {
                friction = (float)propertyRandom.NextDouble();
                friction = (float)Math.Round(friction, 3);
            }

            if (fertility == 0f)
            {
                fertility = ((float)propertyRandom.NextDouble() * 2f) - 1f;
                fertility = (float)Math.Round(fertility, 3);
            }

            if (hardness == 0f)
            {
                hardness = propertyRandom.Next(0, 10001) / 1000f;
                hardness = (float)Math.Round(hardness, 3);
            }
            
            Elements.Add(new Element());
            
            Elements[Elements.Count - 1].Name = name;
            Elements[Elements.Count - 1].Symbol = symbol;
            Elements[Elements.Count - 1].AtomicWeight = atomicWeight;
            Elements[Elements.Count - 1].Density = density;
            Elements[Elements.Count - 1].MeltingPoint = meltingPoint;
            Elements[Elements.Count - 1].BoilingPoint = boilingPoint;
            Elements[Elements.Count - 1].Description = description;

            Elements[Elements.Count - 1].WeightChange = weightChange;
            Elements[Elements.Count - 1].DensityChange = densityChange;
            Elements[Elements.Count - 1].MeltingPointChange = meltingPointChange;
            Elements[Elements.Count - 1].BoilingPointChange = boilingPointChange;

            Elements[Elements.Count - 1].Group = group;
            Elements[Elements.Count - 1].Period = period;
            Elements[Elements.Count - 1].Category = category;

            Elements[Elements.Count - 1].GetGroupingNames();

            Elements[Elements.Count - 1].Protons = Elements.Count - 1;
            Elements[Elements.Count - 1].Neutrons = (int)Math.Round(atomicWeight) - (Elements.Count - 1);
            Elements[Elements.Count - 1].Electrons = Elements.Count - 1;

            Elements[Elements.Count - 1].Bounce = bounce;
            Elements[Elements.Count - 1].Friction = friction;
            Elements[Elements.Count - 1].Fertility = fertility;
            Elements[Elements.Count - 1].Hardness = hardness;

            Elements[Elements.Count - 1].ID = Elements.Count - 1;

            if (color == new Color(0, 0, 0, 0))
                color = new Color(colorRandom.Next(40, 256), colorRandom.Next(40, 256), colorRandom.Next(40, 256), 255);

            Elements[Elements.Count - 1].Color = color;

            AddTexture();
        }

        static void AddTexture()
        {
            elementTextures = new List<Texture2D>();
            bool load = true;
            int i = 0;

            while (load)
            {
                string number = "" + i;

                if (i < 100)
                    number = 0 + number;

                if (i < 10)
                    number = 0 + number;

                try
                {
                    elementTextures.Add(content.Load<Texture2D>("Images//Elements//NoTexture//" + number));
                    i++;
                }
                catch (ContentLoadException e)
                {
                    load = false;
                }
            }
        }

        #region Compound Building

        static private void StartRandomCompound()
        {
            StoredCompoundElements.Clear();

            for (int i = 0; i < CompoundElements.Count; i++)
            {
                StoredCompoundElements.Add(CompoundElements[i]);
            }

            CompoundElements.Clear();
            RandomCompounds.Add(new List<Compound>());
            randomCount.Add(new List<int>());

            StoredFormula = formula;
            formula = null;
        }

        static private void AddRandomCompound(int Count)
        {
            int c = RandomCompounds.Count - 1;

            RandomCompounds[c].Add(new Compound());

            int d = RandomCompounds[c].Count - 1;

            randomCount[c].Add(Count);

            RandomCompounds[c][d].Elements = new List<Element>();
            RandomCompounds[c][d].Formula = formula;

            for (int i = 0; i < CompoundElements.Count; i++)
            {
                RandomCompounds[c][d].Elements.Add(Elements[CompoundElements[i]]);
            }

            CompoundElements.Clear();
            formula = null;
        }

        static private void EndRandomCompound()
        {
            formula = StoredFormula + "(RANDOM" + (RandomCompounds.Count - 1);
            
            CompoundElements.Clear();

            for (int i = 0; i < StoredCompoundElements.Count; i++)
            {
                CompoundElements.Add(StoredCompoundElements[i]);
            }
        }

        static private void StartCondensedCompound()
        {
            formula = formula + "(";
        }

        static private void EndCondensedCompound(int CondensedCount)
        {
            if (CondensedCount > 1)
            {
                string subscriptNum = SubscriptNumbers("" + CondensedCount);
                formula = formula + ")" + subscriptNum;
            }
            else
                formula = formula + ")";
        }

        static private void AddNewCompoundElement(int AtomicNumber, int Count, int CondensedCount)
        {
            for (int i = 0; i < CondensedCount; i++)
            {
                for (int o = 0; o < Count; o++)
                {
                    CompoundElements.Add(AtomicNumber);
                }
            }

            formula = formula + Elements[AtomicNumber].Symbol;

            if (Count > 1)
            {
                string subscriptNum = SubscriptNumbers("" + Count);
                formula = formula + subscriptNum;
            }
        }

        static private void AddNewCompoundElement(int AtomicNumber, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                CompoundElements.Add(AtomicNumber);
            }

            formula = formula + Elements[AtomicNumber].Symbol;

            if (Count > 1)
            {
                string subscriptNum = SubscriptNumbers("" + Count);
                formula = formula + subscriptNum;
            }
        }

        static private void AddNewCompoundElement(string Symbol, int Count, int CondensedCount)
        {
            int ElementID = 1;

            for (int i = 0; i < Elements.Count; i++)
            {
                if (Symbol == Elements[i].Symbol)
                {
                    ElementID = i;
                    break;
                }
            }

            for (int i = 0; i < CondensedCount; i++)
            {
                for (int o = 0; o < Count; o++)
                {
                    CompoundElements.Add(ElementID);
                }
            }

            formula = formula + Elements[ElementID].Symbol;

            if (Count > 1)
            {
                string subscriptNum = SubscriptNumbers("" + Count);
                formula = formula + subscriptNum;
            }
        }

        static private void AddNewCompoundElement(string Symbol, int Count)
        {
            int ElementID = 1;

            for (int i = 0; i < Elements.Count; i++)
            {
                if (Symbol == Elements[i].Symbol)
                {
                    ElementID = i;
                    break;
                }
            }

            for (int i = 0; i < Count; i++)
            {
                CompoundElements.Add(ElementID);
            }

            formula = formula + Elements[ElementID].Symbol;
            
            if (Count > 1)
            {
                string subscriptNum = SubscriptNumbers("" + Count);
                formula = formula + subscriptNum;
            }
        }

        static private List<List<T>> GenerateCombinations<T>(List<List<T>> collectionOfSeries)
        {
            List<List<T>> generatedCombinations =
                collectionOfSeries.Take(1)
                                  .FirstOrDefault()
                                  .Select(i => (new T[] { i }).ToList())
                                  .ToList();

            foreach (List<T> series in collectionOfSeries.Skip(1))
            {
                generatedCombinations =
                    generatedCombinations
                          .Join(series as List<T>,
                                combination => true,
                                i => true,
                                (combination, i) =>
                                {
                                    List<T> nextLevelCombination =
                                        new List<T>(combination);
                                    nextLevelCombination.Add(i);
                                    return nextLevelCombination;
                                }).ToList();

            }

            return generatedCombinations;
        }

        #endregion

        static private void CreateCompound(string name, string description, float meltingPoint, float boilingPoint, float density, float hardness, float bounce, float friction, float fertility, List<Texture2D> texture)
        {
            List<int> elements = CompoundElements;

            variants = 1;

            if (RandomCompounds.Count > 0)
            {
                for (int x = 0; x < RandomCompounds.Count; x++)
                {
                    variants *= RandomCompounds[x].Count;
                }

                variantList = new List<List<int>>();

                for (int x = 0; x < RandomCompounds.Count; x++)
                {
                    variantList.Add(new List<int>());

                    for (int y = 0; y < RandomCompounds[x].Count; y++)
                    {
                        variantList[x].Add(y);
                    }
                }

                variantList = GenerateCombinations<int>(variantList);

                for (int o = 0; o < variants; o++)
                {
                    ActiveCompound = new Compound();

                    ActiveCompound.Elements = new List<Element>();
                    ActiveCompound.ElementCounts = new List<int>();

                    string form = AddVariables(formula, o);

                    ActiveCompound.Formula = form;
                    ActiveCompound.Description = description;

                    if (density == 0f)
                    {
                        //Do Something;
                    }

                    float atomicWeight = 0f;

                    List<int> elementList = new List<int>();

                    for (int i = 0; i < elements.Count; i++)
                    {
                        elementList.Add(elements[i]);
                    }

                    for (int i = 0; i < RandomCompounds.Count; i++)
                    {
                        for (int u = 0; u < RandomCompounds[i][variantList[o][i]].Elements.Count; u++)
                        {
                            string find = RandomCompounds[i][variantList[o][i]].Elements[u].Name;

                            for (int p = 0; p < Elements.Count; p++)
                            {
                                if (Elements[p].Name == find)
                                {
                                    for (int q = 0; q < randomCount[i][variantList[o][i]]; q++)
                                    {
                                        elementList.Add(p);
                                    }

                                    break;
                                }
                            }

                        }
                    }

                    for (int i = 0; i < elementList.Count; i++)
                    {
                        bool match = false;

                        for (int s = 0; s < ActiveCompound.Elements.Count; s++)
                        {
                            if (Elements[elementList[i]].Name == ActiveCompound.Elements[s].Name)
                            {
                                match = true;
                                ActiveCompound.ElementCounts[s]++;
                            }
                        }

                        if (!match)
                        {
                            ActiveCompound.Elements.Add(Elements[elementList[i]]);
                            ActiveCompound.ElementCounts.Add(1);
                        }

                        atomicWeight += Elements[elementList[i]].AtomicWeight;
                    }

                    ActiveCompound.AtomicWeight = atomicWeight;

                    if (density == 0)
                    {
                        int c = 0;
                        float b = 0;

                        for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                            for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                            {
                                b += ActiveCompound.Elements[i].Density;
                                c++;
                            }

                        density = b / c;
                    }
                    ActiveCompound.Density = density;

                    if (meltingPoint == 0)
                    {
                        int c = 0;
                        float b = 0;

                        for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                            for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                            {
                                b += ActiveCompound.Elements[i].MeltingPoint;
                                c++;
                            }

                        meltingPoint = b / c;
                    }
                    ActiveCompound.MeltingPoint = meltingPoint;

                    if (boilingPoint == 0)
                    {
                        int c = 0;
                        float b = 0;

                        for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                            for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                            {
                                b += ActiveCompound.Elements[i].BoilingPoint;
                                c++;
                            }

                        boilingPoint = b / c;
                    }
                    ActiveCompound.BoilingPoint = boilingPoint;

                    if (bounce == 0)
                    {
                        int c = 0;
                        float b = 0;

                        for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                            for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                            {
                                b += ActiveCompound.Elements[i].Bounce;
                                c++;
                            }

                        bounce = b / c;
                    }
                    ActiveCompound.Bounce = bounce;

                    if (friction == 0)
                    {
                        int c = 0;
                        float b = 0;

                        for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                            for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                            {
                                b += ActiveCompound.Elements[i].Friction;
                                c++;
                            }

                        friction = b / c;
                    }
                    ActiveCompound.Friction = friction;

                    if (fertility == 0)
                    {
                        int c = 0;
                        float b = 0;

                        for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                            for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                            {
                                b += ActiveCompound.Elements[i].Fertility;
                                c++;
                            }

                        fertility = b / c;
                    }
                    ActiveCompound.Fertility = fertility;

                    if (hardness == 0)
                    {
                        int c = 0;
                        float b = 0;

                        for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                            for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                            {
                                b += ActiveCompound.Elements[i].Hardness;
                                c++;
                            }

                        hardness = b / c;
                    }
                    ActiveCompound.Hardness = hardness;

                    ActiveCompound.Name = name;

                    ActiveCompound.ID = Compounds.Count;

                    Compounds.Add(ActiveCompound);

                    if (texture == null)
                        DrawTexture();
                }

                CompoundElements.Clear();
                RandomCompounds = new List<List<Compound>>();
                randomCount = new List<List<int>>();
                formula = null;
            }
            else
            {
                ActiveCompound = new Compound();

                ActiveCompound.Elements = new List<Element>();
                ActiveCompound.ElementCounts = new List<int>();
                ActiveCompound.Formula = formula;
                ActiveCompound.Description = description;

                if (density == 0f)
                {
                    //Do Something;
                }

                float atomicWeight = 0f;

                for (int i = 0; i < elements.Count; i++)
                {
                    bool match = false;

                    for (int s = 0; s < ActiveCompound.Elements.Count; s++)
                    {
                        if (Elements[elements[i]].Name == ActiveCompound.Elements[s].Name)
                        {
                            match = true;
                            ActiveCompound.ElementCounts[s]++;
                        }
                    }

                    if (!match)
                    {
                        ActiveCompound.Elements.Add(Elements[elements[i]]);
                        ActiveCompound.ElementCounts.Add(1);
                    }

                    atomicWeight += Elements[elements[i]].AtomicWeight;
                }

                ActiveCompound.AtomicWeight = atomicWeight;

                if (density == 0)
                {
                    int c = 0;
                    float b = 0;

                    for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                        for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                        {
                            b += ActiveCompound.Elements[i].Density;
                            c++;
                        }

                    density = b / c;
                }
                ActiveCompound.Density = density;

                if (meltingPoint == 0)
                {
                    int c = 0;
                    float b = 0;

                    for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                        for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                        {
                            b += ActiveCompound.Elements[i].MeltingPoint;
                            c++;
                        }

                    meltingPoint = b / c;
                }
                ActiveCompound.MeltingPoint = meltingPoint;

                if (boilingPoint == 0)
                {
                    int c = 0;
                    float b = 0;

                    for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                        for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                        {
                            b += ActiveCompound.Elements[i].BoilingPoint;
                            c++;
                        }

                    boilingPoint = b / c;
                }
                ActiveCompound.BoilingPoint = boilingPoint;

                if (bounce == 0)
                {
                    int c = 0;
                    float b = 0;

                    for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                        for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                        {
                            b += ActiveCompound.Elements[i].Bounce;
                            c++;
                        }

                    bounce = b / c;
                }
                ActiveCompound.Bounce = bounce;

                if (friction == 0)
                {
                    int c = 0;
                    float b = 0;

                    for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                        for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                        {
                            b += ActiveCompound.Elements[i].Friction;
                            c++;
                        }

                    friction = b / c;
                }
                ActiveCompound.Friction = friction;

                if (fertility == 0)
                {
                    int c = 0;
                    float b = 0;

                    for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                        for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                        {
                            b += ActiveCompound.Elements[i].Fertility;
                            c++;
                        }

                    fertility = b / c;
                }
                ActiveCompound.Fertility = fertility;

                if (hardness == 0)
                {
                    int c = 0;
                    float b = 0;

                    for (int i = 0; i < ActiveCompound.Elements.Count; i++)
                        for (int u = 0; u < ActiveCompound.ElementCounts[i]; u++)
                        {
                            b += ActiveCompound.Elements[i].Hardness;
                            c++;
                        }

                    hardness = b / c;
                }
                ActiveCompound.Hardness = hardness;

                ActiveCompound.Name = name;

                ActiveCompound.ID = Compounds.Count;

                Compounds.Add(ActiveCompound);

                if (texture == null)
                    DrawTexture();

                CompoundElements.Clear();
                RandomCompounds = new List<List<Compound>>();
                randomCount = new List<List<int>>();
                formula = null;
            }
        }

        static public string SubscriptNumbers(string Input)
        {
            string input = Input;

            input = input.Replace('0', '₀');
            input = input.Replace('1', '₁');
            input = input.Replace('2', '₂');
            input = input.Replace('3', '₃');
            input = input.Replace('4', '₄');
            input = input.Replace('5', '₅');
            input = input.Replace('6', '₆');
            input = input.Replace('7', '₇');
            input = input.Replace('8', '₈');
            input = input.Replace('9', '₉');

            return input;
        }

        static private string AddVariables(string Input, int permutation)
        {
            string input = Input;

            for (int i = 0; i < RandomCompounds.Count; i++)
            {
                int x = randomCount[i][variantList[permutation][i]];
                string num = "";
                    
                if (x > 1)
                    num = SubscriptNumbers("" + x);

                input = input.Replace("RANDOM" + i, RandomCompounds[i][variantList[permutation][i]].Formula + ")" + num);
            }

            return input;
        }

        static public void GenerateCompound(string Formula, string Name, float MeltingPoint, float BoilingPoint, float Hardness, float density, float bounce, float friction, float fertility, List<Texture2D> texture, string Description)
        {            
            //string[] randoms = formula.Split(',');

            form = Formula;

            brackets = new List<string>();
            element = new List<string>();
            elementCount = new List<int>();

            RandomNumber();

            SearchFirst();
            SearchForBrackets();

            if (brackets.Count > 0)
                SearchVariedTypes();

            bool s = true;

            while (s)
            {
                int t = element.Count;

                SearchFirst();
                if (brackets.Count > 0)
                    SearchVariedTypes();

                if (t == element.Count)
                    s = false;
            }

            FormCompound(Name, MeltingPoint, BoilingPoint, Hardness, density, bounce, friction, fertility, texture, Description);
        }

        #region Generate From Formula

        static private void RandomNumber()
        {
            bool search = true;

            string left = "";
            string right = "";

            while (search)
            {
                if (form.Contains('-'))
                {
                    bool search2 = true;

                    while (search2)
                    {
                        if (form.IndexOf('-') - 1 >= 0)
                        {
                            if (char.IsNumber(form.ElementAt(form.IndexOf('-') - 1)))
                            {
                                left = form.ElementAt(form.IndexOf('-') - 1) + left;
                                form = form.Remove(form.IndexOf('-') - 1, 1);
                            }
                            else
                                search2 = false;
                        }
                        else
                            search2 = false;
                    }

                    search2 = true;

                    while (search2)
                    {
                        if (form.IndexOf('-') + 1 < form.Length)
                        {
                            if (char.IsNumber(form.ElementAt(form.IndexOf('-') + 1)))
                            {
                                right = right + form.ElementAt(form.IndexOf('-') + 1);
                                form = form.Remove(form.IndexOf('-') + 1, 1);
                            }
                            else
                                search2 = false;
                        }
                        else
                            search2 = false;
                    }

                    form = form.Insert(form.IndexOf('-'), "" + random.Next(int.Parse(left), int.Parse(right)));
                    form = form.Remove(form.IndexOf('-'), 1);
                }
                else search = false;


            }
        }

        static private void SearchVariedTypes()
        {
            if (brackets[0].Contains(','))
            {
                element.Add("RANDOM");
                elementCount.Add(0);

                brackets[0] = brackets[0].Remove(0, 1);
                form = form.Remove(0, 1);

                bool searching = true;

                while (searching)
                {
                    if (brackets[0].ElementAt(0) == ')')
                    {
                        element.Add("RANDOMBREAK");
                        elementCount.Add(0);
                        brackets[0] = brackets[0].Remove(0, 1);
                        element.Add("REND");
                        if (brackets[0] != "")
                            elementCount.Add(int.Parse(brackets[0]));
                        else
                            elementCount.Add(1);
                        brackets.RemoveAt(0);
                        searching = false;
                    }
                    else
                    {

                        if (brackets[0].ElementAt(0) == ',')
                        {
                            element.Add("RANDOMBREAK");
                            elementCount.Add(0);

                            brackets[0] = brackets[0].Remove(0, 1);
                        }

                        string first = SearchFirstElement(brackets[0]);

                        if (first != null)
                        {
                            brackets[0] = brackets[0].Remove(0, first.Length);
                            element.Add(first);

                            bool GetNumbers = true;
                            string symbolCount = null;

                            while (GetNumbers)
                            {
                                if (char.IsNumber(brackets[0].ElementAt(0)))
                                {
                                    symbolCount += brackets[0].ElementAt(0);
                                    brackets[0] = brackets[0].Remove(0, 1);
                                }
                                else
                                    GetNumbers = false;
                            }

                            if (symbolCount != null)
                                elementCount.Add(int.Parse(symbolCount));
                            else
                                elementCount.Add(1);
                        }
                        else
                            searching = false;
                    }
                }
            }
            else
            {
                element.Add("CONDENSED");
                elementCount.Add(0);

                brackets[0] = brackets[0].Remove(0, 1);
                form = form.Remove(0, 1);

                bool searching = true;

                while (searching)
                {
                    if (brackets[0].ElementAt(0) == ')')
                    {
                        brackets[0] = brackets[0].Remove(0, 1);
                        element.Add("CEND");
                        if (brackets[0] != "")
                            elementCount.Add(int.Parse(brackets[0]));
                        else
                            elementCount.Add(1);
                        brackets.RemoveAt(0);
                        searching = false;
                    }
                    else
                    {
                        string first = SearchFirstElement(brackets[0]);

                        if (first != null)
                        {
                            brackets[0] = brackets[0].Remove(0, first.Length);
                            element.Add(first);

                            bool GetNumbers = true;
                            string symbolCount = null;

                            while (GetNumbers)
                            {
                                if (char.IsNumber(brackets[0].ElementAt(0)))
                                {
                                    symbolCount += brackets[0].ElementAt(0);
                                    brackets[0] = brackets[0].Remove(0, 1);
                                }
                                else
                                    GetNumbers = false;
                            }

                            if (symbolCount != null)
                                elementCount.Add(int.Parse(symbolCount));
                            else
                                elementCount.Add(1);
                        }
                        else
                            searching = false;
                    }
                }
            }            
        }

        static private void SearchFirst()
        {
            bool searching = true;

            while (searching)
            {
                string first = SearchFirstElement(form);

                if (first != null)
                {
                    form = form.Remove(0, first.Length);
                    element.Add(first);

                    bool GetNumbers = true;
                    string symbolCount = null;

                    while (GetNumbers)
                    {
                        if (form != "")
                        {
                            if (char.IsNumber(form.ElementAt(0)))
                            {
                                symbolCount += form.ElementAt(0);
                                form = form.Remove(0, 1);
                            }
                            else
                                GetNumbers = false;
                        }
                        else
                            GetNumbers = false;
                    }

                    if (symbolCount != null)
                        elementCount.Add(int.Parse(symbolCount));
                    else
                        elementCount.Add(1);
                }
                else
                    searching = false;
            }
        }
        
        static private void SearchForBrackets()
        {
            int count = form.Count(f => f == '(');

            for (int i = 0; i < count; i++)
            {
                int startPos = form.IndexOf("(");
                int length = form.IndexOf(")") - startPos + 1;

                bool search = true;
                int runs = 0;

                while (search)
                {
                    runs++;

                    if (Number(form, ")", runs))
                        length++;
                    else
                        search = false;
                }

                brackets.Add(form.Substring(startPos, length));

                if (brackets[brackets.Count - 1].Contains(','))
                    form = form.Replace(brackets[brackets.Count - 1], "?");
                else
                    form = form.Replace(brackets[brackets.Count - 1], "!");
            }
        }
        
        static private string SearchFirstElement(string form)
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                if (Elements[i].Symbol.Length <= form.Length)
                {
                    if (form.Substring(0, Elements[i].Symbol.Length) == Elements[i].Symbol)
                    {
                        int t = form.IndexOf(Elements[i].Symbol) + (Elements[i].Symbol.Length);
                        
                        if (t < form.Length)
                        {
                            if (!char.IsLower(form.ElementAt(t)))
                            {
                                return Elements[i].Symbol;
                            }
                        }
                        else if (Elements[i].Symbol == form)
                            return Elements[i].Symbol;
                    }
                }
            }

            return null;
        }

        static private bool Number(string form, string search, int i)
        {
            if (form.IndexOf(search) + i < form.Length)
            {
                if (form.ElementAt(form.IndexOf(search) + i + (search.Length - 1)) == '1')
                    return true;
                else if (form.ElementAt(form.IndexOf(search) + i + (search.Length - 1)) == '2')
                    return true;
                else if (form.ElementAt(form.IndexOf(search) + i + (search.Length - 1)) == '3')
                    return true;
                else if (form.ElementAt(form.IndexOf(search) + i + (search.Length - 1)) == '4')
                    return true;
                else if (form.ElementAt(form.IndexOf(search) + i + (search.Length - 1)) == '5')
                    return true;
                else if (form.ElementAt(form.IndexOf(search) + i + (search.Length - 1)) == '6')
                    return true;
                else if (form.ElementAt(form.IndexOf(search) + i + (search.Length - 1)) == '7')
                    return true;
                else if (form.ElementAt(form.IndexOf(search) + i + (search.Length - 1)) == '8')
                    return true;
                else if (form.ElementAt(form.IndexOf(search) + i + (search.Length - 1)) == '9')
                    return true;
                else if (form.ElementAt(form.IndexOf(search) + i + (search.Length - 1)) == '0')
                    return true;
            }

            return false;
        }
        
        static private void FormCompound(string Name, float MeltingPoint, float BoilingPoint, float hardness, float density, float bounce, float friction, float fertility, List<Texture2D> texture, string Description)
        {
            bool Random = false;
            bool Condensed = false;

            int condCount = 0;
            int randCount = 0;

            for (int i = 0; i < element.Count; i++)
            {
                if (element[i] == "RANDOM")
                    Random = true;
                else if (element[i] == "CONDENSED")
                    Condensed = true;
                
                if (!Random && !Condensed)
                {
                    AddNewCompoundElement(element[i], elementCount[i]);
                }
                else if (Condensed)
                {
                    if (element[i] == "CONDENSED")
                    {
                        StartCondensedCompound();
                        
                        int o = i;

                        while (condCount == 0)
                        {
                            if (element[o] != "CEND")
                                o++;
                            else                           
                                condCount = elementCount[o];
                        }
                    }
                    else if (element[i] == "CEND")
                    {
                        condCount = 0;
                        Condensed = false;
                        EndCondensedCompound(elementCount[i]);
                    }
                    else
                        AddNewCompoundElement(element[i], elementCount[i], condCount);
                }
                else if (Random)
                {
                    if (element[i] == "RANDOM")
                    {
                        StartRandomCompound();

                        int o = i;

                        while (randCount == 0)
                        {
                            if (element[o] != "REND")
                                o++;
                            else
                                randCount = elementCount[o];
                        }
                    }
                    else if (element[i] == "RANDOMBREAK")
                    {
                        AddRandomCompound(randCount);
                    }
                    else if (element[i] == "REND")
                    {
                        randCount = 0;
                        Random = false;
                        EndRandomCompound();
                    }
                    else
                        AddNewCompoundElement(element[i], elementCount[i]);
                }                
            }

            CreateCompound(Name, Description, MeltingPoint, BoilingPoint, density, hardness, bounce, friction, fertility, texture);
        }

        static private void DrawTexture()
        {
            Compounds[Compounds.Count - 1].Textures = new List<Texture2D>();

            for (int n = 0; n < 4; n++)
            {
                renderTargets.Add(new RenderTarget2D(gDevice, WorldVariables.TileSize * 2, WorldVariables.TileSize * 2));

                gDevice.SetRenderTarget(renderTargets[renderTargets.Count - 1]);
                gDevice.Clear(Compounds[Compounds.Count - 1].Elements[0].Color);

                spriteBatch = new SpriteBatch(gDevice);

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

                int test = 0;

                for (int i = 0; i < Compounds[Compounds.Count - 1].ElementCounts.Count; i++)
                    for (int o = 0; o < Compounds[Compounds.Count - 1].ElementCounts[i]; o++)
                        test++;

                for (int i = 0; i < Compounds[Compounds.Count - 1].ElementCounts.Count; i++)
                    for (int o = 0; o < Compounds[Compounds.Count - 1].ElementCounts[i]; o++)
                    {
                        int count = graphicsRandom.Next(0, elementTextures.Count);
                        float rotation = 0f;
                        int alpha = 100;
                        Vector2 pos;
                        float size;

                        int rot = graphicsRandom.Next(0, 4);

                        switch (rot)
                        {
                            case 0:
                                rotation = 0f;
                                break;

                            case 1:
                                rotation = MathHelper.Pi / 2f;
                                break;

                            case 2:
                                rotation = (float)MathHelper.Pi;
                                break;

                            case 3:
                                rotation = (MathHelper.Pi / 2f) + (float)MathHelper.Pi;
                                break;
                        }

                        pos = new Vector2(WorldVariables.TileSize, WorldVariables.TileSize);
                        size = 1f;

                        Color color = Compounds[Compounds.Count - 1].Elements[i].Color;
                        color.A = (byte)alpha;


                        if (i == 0 && o == 0)
                            color.A = 255;

                        spriteBatch.Draw(
                            elementTextures[count],
                            pos,
                            null,
                            color,
                            rotation,
                            new Vector2(WorldVariables.TileSize, WorldVariables.TileSize),
                            size,
                            SpriteEffects.None,
                            1f);
                    }

                spriteBatch.End();

                gDevice.SetRenderTarget(null);

                Color[] texData = new Color[renderTargets[renderTargets.Count - 1].Height * renderTargets[renderTargets.Count - 1].Width];
                renderTargets[renderTargets.Count - 1].GetData<Color>(texData);

                for (int x = 0; x < texData.Length; x++)
                    texData[x].A = 255;

                renderTargets[renderTargets.Count - 1].SetData<Color>(texData);

                Compounds[Compounds.Count - 1].Textures.Add(renderTargets[renderTargets.Count - 1]);
            }
        }

        #endregion

        static public Element GetElement(string Symbol)
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                if (Symbol == Elements[i].Symbol)
                    return Elements[i];
            }

            return null;
        }

        static public List<int> BuildTable(int Total)
        {
            List<int> ProbabilityList = new List<int>();
            
            int total = 0;
            int dest = Total;
            int pass = 0;

            bool calculating = true;

            while (calculating)
            {
                for (int i = 0; i < Elements.Count; i++)
                {
                    int prob = (Elements.Count + 1) - i;
                    int final;

                    if (total < dest)
                        final = random.Next(0, ((dest - total) / (i + 1)) + 1);
                    else final = 0;

                    total += final;

                    if (pass == 0)
                        ProbabilityList.Add(final);
                    else
                        ProbabilityList[i] += final;
                }

                if (total >= dest)
                    calculating = false;

                pass++;
            }

            return ProbabilityList;
        }
    }
}
