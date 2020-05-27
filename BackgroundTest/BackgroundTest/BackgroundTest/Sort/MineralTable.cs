using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BackgroundTest
{
    class Mineral
    {
        public int ID;
        public string Name;
        public Compound Compound;
        public int CategoryID;
        public string Category;
        public int SubCategoryID;
        public string SubCategory;
        public string FullFormula;
    }

    static class MineralTable
    {
        static public List<Mineral> Minerals;
        static private Random random;

        static public void Initialize()
        {
            Minerals = new List<Mineral>();
            random = new Random(WorldVariables.ElementSeed);

            AddMinerals();
        }

        static private void AddMinerals()
        {
            AddNativeElements();

            AddMineral("Test1", "AuFeAu", 0f, 0f, 0f, 0f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Test2", "FeAuFe", 0f, 0f, 0f, 0f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Test3", "Pt2Fe15Au", 0f, 0f, 0f, 0f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Test4", "FeAuPt", 0f, 0f, 0f, 0f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Test5", "AuNi2Au", 0f, 0f, 0f, 0f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Test6", "FeAuNi", 0f, 0f, 0f, 0f, 1, 1, 0f, 0f, 0f, "");

            AddMineral("Apatite", "Ca5(PO4)3(F,Cl,OH)2", 0f, 0f, 0f, 0f, 0, 0, 0f, 0f, 0f, "");
            AddMineral("Dickite", "Al2Si2O5(OH)4", 0f, 0f, 0f, 0f, 0, 0, 0f, 0f, 0f, "");
            AddMineral("Cummingtonite", "(Mg,Fe)7Si8O22(OH)2", 0f, 0f, 0f, 0f, 0, 0, 0f, 0f, 0f, "");
            AddMineral("Heulandite", "(Ca,Na)2-3Al3(Al,Si)2Si13O36", 0f, 0f, 0f, 0f, 0, 0, 0f, 0f, 0f, "");
            AddMineral("Orthoclase", "KAlSi3O8", 0f, 0f, 0f, 0f, 0, 0, 0f, 0f, 0f, "");

            AddMineral("LexLuthium", "LxLh", 0f, 0f, 0f, 0f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Lexium", "Lx", 0f, 0f, 0f, 0f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Luthium", "Lh", 0f, 0f, 0f, 0f, 1, 1, 0f, 0f, 0f, "");


            AddMineral("Potato", "Ot", 0f, 0f, 0f, 0f, 1, 1, 0f, 0f, 0f, "");

            AddRandomMinerals();
        }

        static private void AddRandomMinerals()
        {
            for (int i = 0; i < 50; i++)
            {
                string formula = "";
                int chemicals = random.Next(2, 10);

                for (int o = 0; o < chemicals; o++)
                {
                    formula += ElementTable.Elements[random.Next(1, ElementTable.Elements.Count)].Symbol;
                    formula += random.Next(1, 15);
                }

                AddMineral("Test " + i, formula, 0f, 0f, 0f, 0f, 1, 1, 0f, 0f, 0f, "");
            }
        }

        static private void AddNativeElements()
        {
            //Copper-Cupalite Family
            AddMineral("Copper", "Cu", ElementTable.GetElement("Cu").Density, ElementTable.GetElement("Cu").MeltingPoint, ElementTable.GetElement("Cu").BoilingPoint, 3f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Lead", "Pb", ElementTable.GetElement("Pb").Density, ElementTable.GetElement("Pb").MeltingPoint, ElementTable.GetElement("Pb").BoilingPoint, 1.5f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Gold", "Au", ElementTable.GetElement("Au").Density, ElementTable.GetElement("Au").MeltingPoint, ElementTable.GetElement("Au").BoilingPoint, 2.5f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Silver", "Ag", ElementTable.GetElement("Ag").Density, ElementTable.GetElement("Ag").MeltingPoint, ElementTable.GetElement("Ag").BoilingPoint, 2.5f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Nickel", "Ni", ElementTable.GetElement("Ni").Density, ElementTable.GetElement("Ni").MeltingPoint, ElementTable.GetElement("Ni").BoilingPoint, 4f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Aluminium", "Al", ElementTable.GetElement("Al").Density, ElementTable.GetElement("Al").MeltingPoint, ElementTable.GetElement("Al").BoilingPoint, 2.75f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Auricupride", "Cu3Au", 0f, 0f, 0f, 3.5f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Tetra-Auricupride", "CuAu", 0f, 0f, 0f, 4.5f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Novodneprite", "AuPb3", 0f, 0f, 0f, 0f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Khatyrkite", "(Cu,Zn,Fe)Al2", 0f, 0f, 0f, random.Next(500, 601) / 100f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Anyuiite", "Au(Pb,Sb)2", 0f, 0f, 0f, 3.5f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Cupalite", "(Cu,Zn,Fe)Al", 0f, 0f, 0f, random.Next(400, 450) / 100f, 1, 1, 0f, 0f, 0f, "");
            AddMineral("Hunchunite", "(Au,Ag)2Pb", 0f, 0f, 0f, 3.5f, 1, 1, 0f, 0f, 0f, "");

            //Zinc-Brass Family
            AddMineral("Cadmium", "Cd", ElementTable.GetElement("Cd").Density, ElementTable.GetElement("Cd").MeltingPoint, ElementTable.GetElement("Cd").BoilingPoint, 2f, 1, 2, 0f, 0f, 0f, "");
            AddMineral("Zinc", "Zn", ElementTable.GetElement("Zn").Density, ElementTable.GetElement("Zn").MeltingPoint, ElementTable.GetElement("Zn").BoilingPoint, 2.5f, 1, 2, 0f, 0f, 0f, "");
            AddMineral("Titanium", "Ti", ElementTable.GetElement("Ti").Density, ElementTable.GetElement("Ti").MeltingPoint, ElementTable.GetElement("Ti").BoilingPoint, 6f, 1, 2, 0f, 0f, 0f, "");
            AddMineral("Rhenium", "Re", ElementTable.GetElement("Re").Density, ElementTable.GetElement("Re").MeltingPoint, ElementTable.GetElement("Re").BoilingPoint, 7f, 1, 2, 0f, 0f, 0f, "");
            AddMineral("Brass", "Cu3Zn2", 0f, 0f, 0f, random.Next(3, 4), 1, 2, 0f, 0f, 0f, "");
            AddMineral("Zhanghengite", "CuZn", 0f, 0f, 0f, 3.5f, 1, 2, 0f, 0f, 0f, "");
            AddMineral("Danbaite", "CuZn2", 7.36f, 0f, 0f, 4f, 1, 2, 0f, 0f, 0f, "");
            AddMineral("Tongxinite", "Cu2Zn", 0f, 0f, 0f, 3.5f, 1, 2, 0f, 0f, 0f, "");

            //Indium-Tin Family
            AddMineral("Indium", "In", ElementTable.GetElement("In").Density, ElementTable.GetElement("In").MeltingPoint, ElementTable.GetElement("In").BoilingPoint, 1.2f, 1, 3, 0f, 0f, 0f, "");
            AddMineral("Tin", "Sn", ElementTable.GetElement("Sn").Density, ElementTable.GetElement("Sn").MeltingPoint, ElementTable.GetElement("Sn").BoilingPoint, 1.5f, 1, 3, 0f, 0f, 0f, "");
            AddMineral("Yuanjiangite", "AuSn", 0f, 0f, 0f, random.Next(350, 400) / 100f, 1, 3, 0f, 0f, 0f, "");
            AddMineral("Sorosite", "Cu(Sn,Sb)", random.Next(760, 790) / 100f, 0f, 0f, random.Next(500, 550) / 100f, 1, 3, 0f, 0f, 0f, "");

            //Mercury-Amalgam Family
            AddMineral("Amalgam", "Ag2Hg3", random.Next(1375, 1410) / 100f, 0f, 0f, random.Next(300, 350) / 100f, 1, 4, 0f, 0f, 0f, "");
            AddMineral("Mercury", "Hg", ElementTable.GetElement("Hg").Density, ElementTable.GetElement("Hg").MeltingPoint, ElementTable.GetElement("Hg").BoilingPoint, 1.5f, 1, 4, 0f, 0f, 0f, "");
            AddMineral("Belendorffite", "Cu7Hg6", 13.2f, 0f, 0f, random.Next(250, 400) / 100f, 1, 4, 0f, 0f, 0f, "");
            AddMineral("Kolymite", "Cu7Hg6", 13f, 0f, 0f, 4f, 1, 4, 0f, 0f, 0f, "");
            AddMineral("Paraschachnerite", "Ag3Hg2", 12.98f, 0f, 0f, 4f, 1, 4, 0f, 0f, 0f, "");
            AddMineral("Schachnerite", "AgHg", 0f, 0f, 0f, 2.3f, 1, 4, 0f, 0f, 0f, "");
            AddMineral("Luanheite", "Ag3Hg", 12.5f, 0f, 0f, 2.5f, 1, 4, 0f, 0f, 0f, "");
            AddMineral("Eugenite", "Ag9Hg2", 10.75f, 0f, 0f, random.Next(250, 300) / 100f, 1, 4, 0f, 0f, 0f, "");
            AddMineral("Moschellandsbergite", "Ag2Hg3", 0f, 0f, 0f, 3.5f, 1, 4, 0f, 0f, 0f, "");
            AddMineral("Weishanite", "(Au,Ag)3Hg2", 0f, 0f, 0f, 2.5f, 1, 4, 0f, 0f, 0f, "");
            AddMineral("Goldamalgam", "(Au,Ag)Hg", 15.47f, 0f, 0f, 3f, 1, 4, 0f, 0f, 0f, "");
            AddMineral("Potarite", "PdHg", 14.88f, 0f, 0f, 3.5f, 1, 4, 0f, 0f, 0f, "");
            AddMineral("Leadamalgam", "HgPb2", 0f, 0f, 0f, 1.5f, 1, 4, 0f, 0f, 0f, "");

            //Iron-Chromium Family
            AddMineral("Kamacite", "(Fe,Ni)", 7.9f, 0f, 0f, 4f, 1, 5, 0f, 0f, 0f, "");
            AddMineral("Iron", "Fe", ElementTable.GetElement("Fe").Density, ElementTable.GetElement("Fe").MeltingPoint, ElementTable.GetElement("Fe").BoilingPoint, 4f, 1, 5, 0f, 0f, 0f, "");
            AddMineral("Chromium", "Cr", ElementTable.GetElement("Cr").Density, ElementTable.GetElement("Cr").MeltingPoint, ElementTable.GetElement("Cr").BoilingPoint, 8.5f, 1, 5, 0f, 0f, 0f, "");
            AddMineral("Antitaenite", "Fe3Ni", 0f, 0f, 0f, 4.5f, 1, 5, 0f, 0f, 0f, "");
            AddMineral("Taenite", "(Ni,Fe)", 0f, 0f, 0f, random.Next(500, 550) / 100f, 1, 5, 0f, 0f, 0f, "");
            AddMineral("Tetrataenite", "FeNi", 8.275f, 0f, 0f, 3.5f, 1, 5, 0f, 0f, 0f, "");
            AddMineral("Chromferide", "Fe3Cr1-6", 0f, 0f, 0f, 4f, 1, 5, 0f, 0f, 0f, "");
            AddMineral("Wairauite", "CoFe", 0f, 0f, 0f, 5f, 1, 5, 0f, 0f, 0f, "");
            AddMineral("Ferchromide", "Cr3Fe1-6", 0f, 0f, 0f, 6.5f, 1, 5, 0f, 0f, 0f, "");
            AddMineral("Awaruite", "Ni2-3Fe", 0f, 0f, 0f, random.Next(550, 600) / 100f, 1, 5, 0f, 0f, 0f, "");
            AddMineral("Jedwabite", "Fe7(Ta,Nb)3", 8.6f, 0f, 0f, 7f, 1, 5, 0f, 0f, 0f, "");

            //Platinum Group Elements
            AddMineral("Osmium", "Os", ElementTable.GetElement("Os").Density, ElementTable.GetElement("Os").MeltingPoint, ElementTable.GetElement("Os").BoilingPoint, 7f, 1, 6, 0f, 0f, 0f, "");
            AddMineral("Rutheniridosmine", "(Ir,Os,Ru)", 0f, 0f, 0f, random.Next(600, 700) / 100f, 1, 6, 0f, 0f, 0f, "");
            AddMineral("Ruthenium", "Ru", ElementTable.GetElement("Ru").Density, ElementTable.GetElement("Ru").MeltingPoint, ElementTable.GetElement("Ru").BoilingPoint, 6.5f, 1, 6, 0f, 0f, 0f, "");
            AddMineral("Palladium", "Pd", ElementTable.GetElement("Pd").Density, ElementTable.GetElement("Pd").MeltingPoint, ElementTable.GetElement("Pd").BoilingPoint, 4.75f, 1, 6, 0f, 0f, 0f, "");
            AddMineral("Iridium", "Ir", ElementTable.GetElement("Ir").Density, ElementTable.GetElement("Ir").MeltingPoint, ElementTable.GetElement("Ir").BoilingPoint, 6.5f, 1, 6, 0f, 0f, 0f, "");
            AddMineral("Rhodium", "Rh", ElementTable.GetElement("Rh").Density, ElementTable.GetElement("Rh").MeltingPoint, ElementTable.GetElement("Rh").BoilingPoint, 6f, 1, 6, 0f, 0f, 0f, "");
            AddMineral("Platinum", "Pt", ElementTable.GetElement("Pt").Density, ElementTable.GetElement("Pt").MeltingPoint, ElementTable.GetElement("Pt").BoilingPoint, random.Next(400, 450) / 100f, 1, 6, 0f, 0f, 0f, "");

            //Platinum Group Alloys
            AddMineral("Garutiite", "(Ni,Fe,Ir)", 11.33f, 0f, 0f, 0f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Hexaferrum", "(Fe,Os,Ru,Ir)", 0f, 0f, 0f, random.Next(600, 700) / 100f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Atokite", "(Pd,Pt)3Sn", 14.9f, 0f, 0f, 5f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Zvyagintsevite", "Pd3Pb", 13.32f, 0f, 0f, 4.5f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Rustenburgite", "(Pt,Pd)3Sn", 0f, 0f, 0f, 5f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Taimyrite", "(Pd,Cu,Pt)3Sn", 15.6f, 0f, 0f, 5f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Tatyanaite", "(Pt,Pd,Cu)9Cu3Sn4", 0f, 0f, 0f, random.Next(350, 400) / 100f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Paolovite", "Pd2Sn", 11.32f, 0f, 0f, 5f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Plumbopalladinite", "Pd3Pb2", 12.4f, 0f, 0f, 5f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Stannopalladinite", "(Pd,Cu)3Sn2", 10.2f, 0f, 0f, 5f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Cabriite", "Pd2SnCu", 11.1f, 0f, 0f, random.Next(400, 450) / 100f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Chengdeite", "Ir3Fe", 19.3f, 0f, 0f, 5f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Isoferroplatinum", "(Pt,Pd)3(Fe,Cu)", 16.5f, 0f, 0f, 5f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Ferronickelplatinum", "Pt2FeNi", 15.39f, 0f, 0f, 5f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Tetraferroplatinum", "PtFe", 14.3f, 0f, 0f, random.Next(400, 500) / 100f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Tulameenite", "Pt2FeCu", 14.9f, 0f, 0f, 5f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Hongshiite", "PtCu", 15.63f, 0f, 0f, 5f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Skaergaardite", "PdCu", 0f, 0f, 0f, random.Next(400, 500) / 100f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Yixunite", "Pt3In", random.Next(1821, 1832) / 100f, 0f, 0f, 6f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Damiaoite", "PtIn2", 10.95f, 0f, 0f, 5f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Niggliite", "PtSn", 13.44f, 0f, 0f, 3f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Bortnikovite", "Pd4Cu3Zn", 0f, 0f, 0f, random.Next(450, 500) / 100f, 1, 7, 0f, 0f, 0f, "");
            AddMineral("Nielsenite", "PdCu3", 0f, 0f, 0f, 0f, 1, 7, 0f, 0f, 0f, "");

            //Carbides
            AddMineral("Cohenite", "(Fe,Ni,Co)3C", 0f, 0f, 0f, random.Next(550, 600) / 100f, 1, 8, 0f, 0f, 0f, "");
            AddMineral("Isovite", "(Cr,Fe)23C6", 0f, 0f, 0f, 8f, 1, 8, 0f, 0f, 0f, "");
            AddMineral("Haxonite", "(Fe,Ni)23C6", 0f, 0f, 0f, random.Next(550, 600) / 100f, 1, 8, 0f, 0f, 0f, "");
            AddMineral("Tongbaite", "Cr3C2", 0f, 0f, 0f, 9.6f, 1, 8, 0f, 0f, 0f, "");
            AddMineral("Khamrabaevite", "(Ti,V,Fe)C", 4.93f, 3430f, 5090f, random.Next(900, 950) / 100f, 1, 8, 0f, 0f, 0f, "");
            AddMineral("Niobocarbide", "(Nb,Ta)C", 10.25f, 0f, 0f, 8f, 1, 8, 0f, 0f, 0f, "");
            AddMineral("Tantalcarbide", "TaC", 14.5f, 0f, 0f, random.Next(600, 700) / 100f, 1, 8, 0f, 0f, 0f, "");
            AddMineral("Qusongite", "WC", 0f, 0f, 0f, 9.5f, 1, 8, 0f, 0f, 0f, "");
            AddMineral("Yarlongite", "(Cr4Fe4Ni)C4", 0f, 0f, 0f, random.Next(550, 600) / 100f, 1, 8, 0f, 0f, 0f, "");

            //Silicides
            AddMineral("Zangboite", "TiFeSi2", 5.09f, 0f, 0f, 5.5f, 1, 9, 0f, 0f, 0f, "");
            AddMineral("Mavlyanovite", "Mn5Si3", 0f, 0f, 0f, 7f, 1, 9, 0f, 0f, 0f, "");
            AddMineral("Suessite", "Fe3Si", 7.09f, 0f, 0f, 5.75f, 1, 9, 0f, 0f, 0f, "");
            AddMineral("Perryite", "(Ni,Fe)8(Si,P)3", 0f, 0f, 0f, 2f, 1, 9, 0f, 0f, 0f, "");
            AddMineral("Fersilicite", "FeSi", 5.95f, 0f, 0f, 6.5f, 1, 9, 0f, 0f, 0f, "");
            AddMineral("Ferdisilicite", "FeSi2", 5.05f, 0f, 0f, 6.5f, 1, 9, 0f, 0f, 0f, "");
            AddMineral("Luobusaite", "FeSi2", 0f, 0f, 0f, 7f, 1, 9, 0f, 0f, 0f, "");
            AddMineral("Gupeiite", "Fe3Si", 7.15f, 0f, 0f, 5f, 1, 9, 0f, 0f, 0f, "");
            AddMineral("Hapkeite", "Fe2Si", 0f, 0f, 0f, 0f, 1, 9, 0f, 0f, 0f, "");
            AddMineral("Xifengite", "Fe5Si3", 0f, 0f, 0f, 5f, 1, 9, 0f, 0f, 0f, "");

            //Nitrides
            AddMineral("Roaldite", "(Fe,Ni)4N", 0f, 0f, 0f, random.Next(550, 650) / 100f, 1, 10, 0f, 0f, 0f, "");
            AddMineral("Siderazot", "Fe5N2", 3.147f, 0f, 0f, 4f, 1, 10, 0f, 0f, 0f, "");
            AddMineral("Carlsbergite", "CrN", 0f, 0f, 0f, 7f, 1, 10, 0f, 0f, 0f, "");
            AddMineral("Osbornite", "TiN", 0f, 0f, 0f, 7f, 1, 10, 0f, 0f, 0f, "");

            //Phosphides
            AddMineral("Schreibersite", "(Fe,Ni)3P", 0f, 0f, 0f, random.Next(650, 700) / 100f, 1, 11, 0f, 0f, 0f, "");
            AddMineral("Nickelphosphide", "(Ni,Fe)3P", 0f, 0f, 0f, random.Next(650, 700) / 100f, 1, 11, 0f, 0f, 0f, "");
            AddMineral("Barringerite", "(Fe,Ni)2P", 6.92f, 0f, 0f, 7f, 1, 11, 0f, 0f, 0f, "");
            AddMineral("Monipite", "MoNiP", 0f, 0f, 0f, 0f, 1, 11, 0f, 0f, 0f, "");
            AddMineral("Allabogdanite", "(Fe,Ni)2P", 0f, 0f, 0f, random.Next(500, 600) / 100f, 1, 11, 0f, 0f, 0f, "");
            AddMineral("Florenskyite", "(Fe,Ni)TiP", 0f, 0f, 0f, 0f, 1, 11, 0f, 0f, 0f, "");
            AddMineral("Andreyivanovite", "FeCrP", 0f, 0f, 0f, 0f, 1, 11, 0f, 0f, 0f, "");
            AddMineral("Melliniite", "(Ni,Fe)4P", 0f, 0f, 0f, random.Next(800, 850) / 100f, 1, 11, 0f, 0f, 0f, "");

            //Arsenic Group
            AddMineral("Bismuth", "Bi", ElementTable.GetElement("Bi").Density, ElementTable.GetElement("Bi").MeltingPoint, ElementTable.GetElement("Bi").BoilingPoint, 2.25f, 1, 12, 0f, 0f, 0f, "");
            AddMineral("Antimony", "Sb", ElementTable.GetElement("Sb").Density, ElementTable.GetElement("Sb").MeltingPoint, ElementTable.GetElement("Sb").BoilingPoint, 3f, 1, 12, 0f, 0f, 0f, "");
            AddMineral("Arsenic", "As", ElementTable.GetElement("As").Density, ElementTable.GetElement("As").MeltingPoint, ElementTable.GetElement("As").BoilingPoint, 3.5f, 1, 12, 0f, 0f, 0f, "");
            AddMineral("Stibarsen", "AsSb", 0f, 0f, 0f, random.Next(300, 400) / 100f, 1, 12, 0f, 0f, 0f, "");
            AddMineral("Arsenolamprite", "As", random.Next(530, 558) / 100f, 0f, 0f, 2f, 1, 12, 0f, 0f, 0f, "");
            AddMineral("Pararsenolamprite", "As", random.Next(588, 601) / 100f, 0f, 0f, random.Next(200, 250) / 100f, 1, 12, 0f, 0f, 0f, "");
            AddMineral("Paradocrasite", "Sb2(Sb,As)2", 6.52f, 0f, 0f, 3.5f, 1, 12, 0f, 0f, 0f, "");

            //Carbon-Silicon Family
            AddMineral("Graphite", "C", random.Next(209, 223) / 100f, 0f, 0f, random.Next(100, 200) / 100f, 1, 13, 0f, 0f, 0f, "");
            AddMineral("Chaoite", "C", 0f, 0f, 0f, random.Next(100, 200) / 100f, 1, 13, 0f, 0f, 0f, "");
            AddMineral("Fullerite", "C60", random.Next(190, 200) / 100f, 0f, 0f, 3.5f, 1, 13, 0f, 0f, 0f, "");
            AddMineral("Diamond", "C", random.Next(350, 354) / 100f, 0f, 0f, 10f, 1, 13, 0f, 0f, 0f, "");
            AddMineral("Lonsdaleite", "C", 0f, 0f, 0f, random.Next(700, 800) / 100f, 1, 13, 0f, 0f, 0f, "");
            AddMineral("Silicon", "Si", ElementTable.GetElement("Si").Density, ElementTable.GetElement("Si").MeltingPoint, ElementTable.GetElement("Si").BoilingPoint, 7f, 1, 13, 0f, 0f, 0f, "");

            //Sulfur-Selenium-Iodine
            AddMineral("Sulfur", "S", ElementTable.GetElement("S").Density, ElementTable.GetElement("S").MeltingPoint, ElementTable.GetElement("S").BoilingPoint, 2f, 1, 14, 0f, 0f, 0f, "");
            AddMineral("Rosickýite", "S", 0f, 0f, 0f, random.Next(200, 300) / 100f, 1, 14, 0f, 0f, 0f, "");
            AddMineral("Tellurium", "Te", ElementTable.GetElement("Te").Density, ElementTable.GetElement("Te").MeltingPoint, ElementTable.GetElement("Te").BoilingPoint, 2.25f, 1, 14, 0f, 0f, 0f, "");
            AddMineral("Selenium", "Se", ElementTable.GetElement("Se").Density, ElementTable.GetElement("Se").MeltingPoint, ElementTable.GetElement("Se").BoilingPoint, 2f, 1, 14, 0f, 0f, 0f, "");

            //Nonmetallic Carbides
            AddMineral("Moissanite", "SiC", 0f, 3003.15f, 0f, 9.5f, 1, 15, 0f, 0f, 0f, "");

            //Nonmetallic Nitrides
            AddMineral("Nierite", "N4Si3", 3.2f, 0f, 0f, 0f, 1, 16, 0f, 0f, 0f, "");
            AddMineral("Sinoite", "Si2N2O", random.Next(280, 286) / 100f, 0f, 0f, 0f, 1, 16, 0f, 0f, 0f, "");

            //Unknown
            AddMineral("Hexamolybdenum", "(Mo,Ru,Fe,Ir,Os)", 0f, 0f, 0f, 0f, 1, 17, 0f, 0f, 0f, "");
            AddMineral("Tantalum", "Ta", ElementTable.GetElement("Ta").Density, ElementTable.GetElement("Ta").MeltingPoint, ElementTable.GetElement("Ta").BoilingPoint, 6.5f, 1, 17, 0f, 0f, 0f, "");
            AddMineral("Brownleeite", "MnSi", 0f, 0f, 0f, 0f, 1, 17, 0f, 0f, 0f, "");
        }

        static private void AddTectosilicates()
        {
            //Quartz Varieties


            //Feldspar Family


            //Feldspathoid


            //Zeolites


            //AddMineral("", "", 0f, 0f, 0f, 1, 1, "");
        }

        static private void AddMineral(string name, string formula, float density, float meltingPoint, float boilingPoint, float hardness, int category, int subcategory, float bounce, float friction, float fertility, string description)
        {
            List<Texture2D> textures = SearchTextures();

            ElementTable.GenerateCompound(formula, name, meltingPoint, boilingPoint, hardness, density, bounce, friction, fertility, textures, description);
            
            for (int i = ElementTable.variants; i > 0; i--)
            {
                Minerals.Add(new Mineral());

                Minerals[Minerals.Count - 1].Name = name;
                Minerals[Minerals.Count - 1].ID = Minerals.Count - 1;
                Minerals[Minerals.Count - 1].Compound = new Compound();
                Minerals[Minerals.Count - 1].CategoryID = category;
                Minerals[Minerals.Count - 1].SubCategoryID = subcategory;
                Minerals[Minerals.Count - 1].FullFormula = ElementTable.SubscriptNumbers(formula);
                SetCategoryID(category, subcategory);

                Minerals[Minerals.Count - 1].Compound = ElementTable.Compounds[ElementTable.Compounds.Count - i];
            }
        }

        static private List<Texture2D> SearchTextures()
        {

            return null;
        }

        static private void SetCategoryID(int Category, int SubCategory)
        {
            switch (Category)
            {
                case 0:
                    Minerals[Minerals.Count - 1].Category = null;
                    break;

                case 1:
                    Minerals[Minerals.Count - 1].Category = "Native Element";
                    break;
            }

            switch (SubCategory)
            {
                case 0:
                    Minerals[Minerals.Count - 1].SubCategory = null;
                    break;

                case 1:
                    Minerals[Minerals.Count - 1].SubCategory = "Copper-Cupalite Family";
                    break;

                case 2:
                    Minerals[Minerals.Count - 1].SubCategory = "Zinc-Brass Family";
                    break;

                case 3:
                    Minerals[Minerals.Count - 1].SubCategory = "Indium-Tin Family";
                    break;

                case 4:
                    Minerals[Minerals.Count - 1].SubCategory = "Mercury-Amalgam Family";
                    break;

                case 5:
                    Minerals[Minerals.Count - 1].SubCategory = "Iron-Chromium Family";
                    break;

                case 6:
                    Minerals[Minerals.Count - 1].SubCategory = "Platinum Group Elements";
                    break;
                    
                case 7:
                    Minerals[Minerals.Count - 1].SubCategory = "Platinum Group Alloys";
                    break;

                case 8:
                    Minerals[Minerals.Count - 1].SubCategory = "Carbides";
                    break;

                case 9:
                    Minerals[Minerals.Count - 1].SubCategory = "Silicides";
                    break;

                case 10:
                    Minerals[Minerals.Count - 1].SubCategory = "Nitrides";
                    break;

                case 11:
                    Minerals[Minerals.Count - 1].SubCategory = "Phosphides";
                    break;

                case 12:
                    Minerals[Minerals.Count - 1].SubCategory = "Arsenic Group";
                    break;

                case 13:
                    Minerals[Minerals.Count - 1].SubCategory = "Carbon-Silicon Family";
                    break;

                case 14:
                    Minerals[Minerals.Count - 1].SubCategory = "Sulfur-Selenium-Iodine";
                    break;

                case 15:
                    Minerals[Minerals.Count - 1].SubCategory = "Nonmetallic Carbides";
                    break;

                case 16:
                    Minerals[Minerals.Count - 1].SubCategory = "Nonmetallic Nitrides";
                    break;

                case 17:
                    Minerals[Minerals.Count - 1].SubCategory = "Unknown";
                    break;
            }
        }

        static private void AddCompounds()
        {

        }

    }
}
