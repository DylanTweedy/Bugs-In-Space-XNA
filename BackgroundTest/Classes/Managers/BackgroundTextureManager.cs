using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BackgroundTest
{
    static class BackgroundTextureManager
    {
        static public List<Texture2D> starTexturesO = new List<Texture2D>();
        static public List<Texture2D> starTexturesB = new List<Texture2D>();
        static public List<Texture2D> starTexturesA = new List<Texture2D>();
        static public List<Texture2D> starTexturesF = new List<Texture2D>();
        static public List<Texture2D> starTexturesG = new List<Texture2D>();
        static public List<Texture2D> starTexturesK = new List<Texture2D>();
        static public List<Texture2D> starTexturesM = new List<Texture2D>();
        static public List<Texture2D> starTexturesL = new List<Texture2D>();
        static public List<Texture2D> starTexturesT = new List<Texture2D>();
        static public List<Texture2D> starTexturesY = new List<Texture2D>();
        static public List<Texture2D> starTexturesD = new List<Texture2D>();
        static public List<Texture2D> starTexturesWR = new List<Texture2D>();
        static public List<Texture2D> starTexturesProtostar = new List<Texture2D>();
        static public List<Texture2D> starTexturesPulsar = new List<Texture2D>();
        static public List<Texture2D> starTexturesNeutron = new List<Texture2D>();
        static public List<Texture2D> starTexturesQuark = new List<Texture2D>();
        static public List<Texture2D> starTexturesBoson = new List<Texture2D>();
        static public List<Texture2D> starTexturesBlackHole = new List<Texture2D>();
        static public List<Texture2D> starTexturesAncient = new List<Texture2D>();
        static public List<Texture2D> starTexturesAntiMatter = new List<Texture2D>();
        static public List<Texture2D> starTexturesDarkMatter = new List<Texture2D>();
        static public List<Texture2D> starTexturesFaction = new List<Texture2D>();
        static public List<Texture2D> starTexturesGhost = new List<Texture2D>();
        static public List<Texture2D> starTexturesGlorious = new List<Texture2D>();
        static public List<Texture2D> starTexturesRainbow = new List<Texture2D>();
        static public List<Texture2D> starTexturesRing = new List<Texture2D>();
        static public List<Texture2D> starTexturesSquare = new List<Texture2D>();
        static public List<Texture2D> starTexturesSulphur = new List<Texture2D>();
        static public List<Texture2D> starTexturesTranslocation = new List<Texture2D>();
        static public List<Texture2D> starTexturesWhiteHole = new List<Texture2D>();
        static public List<Texture2D> starTexturesQuantum = new List<Texture2D>();

        static public List<Texture2D> smallStarTexturesO = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesB = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesA = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesF = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesG = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesK = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesM = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesL = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesT = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesY = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesD = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesWR = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesProtostar = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesPulsar = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesNeutron = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesQuark = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesBoson = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesBlackHole = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesAncient = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesAntiMatter = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesDarkMatter = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesFaction = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesGhost = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesGlorious = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesRainbow = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesRing = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesSquare = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesSulphur = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesTranslocation = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesWhiteHole = new List<Texture2D>();
        static public List<Texture2D> smallStarTexturesQuantum = new List<Texture2D>();
        static public void LoadContent(ContentManager Content)
        {
            LoadStars(Content);
        }

        static void LoadStars(ContentManager Content)
        {
            starTexturesO.Add(Content.Load<Texture2D>("Images//Background//Stars//O//OStar01"));
            smallStarTexturesO.Add(Content.Load<Texture2D>("Images//Background//Stars//O//OSmallStar01"));

            starTexturesB.Add(Content.Load<Texture2D>("Images//Background//Stars//B//BStar01"));
            smallStarTexturesB.Add(Content.Load<Texture2D>("Images//Background//Stars//B//BSmallStar01"));

            starTexturesA.Add(Content.Load<Texture2D>("Images//Background//Stars//A//AStar01"));
            smallStarTexturesA.Add(Content.Load<Texture2D>("Images//Background//Stars//A//ASmallStar01"));

            starTexturesF.Add(Content.Load<Texture2D>("Images//Background//Stars//F//FStar01"));
            smallStarTexturesF.Add(Content.Load<Texture2D>("Images//Background//Stars//F//FSmallStar01"));

            starTexturesG.Add(Content.Load<Texture2D>("Images//Background//Stars//G//GStar01"));
            smallStarTexturesG.Add(Content.Load<Texture2D>("Images//Background//Stars//G//GSmallStar01"));

            starTexturesK.Add(Content.Load<Texture2D>("Images//Background//Stars//K//KStar01"));
            smallStarTexturesK.Add(Content.Load<Texture2D>("Images//Background//Stars//K//KSmallStar01"));

            starTexturesM.Add(Content.Load<Texture2D>("Images//Background//Stars//M//MStar01"));
            smallStarTexturesM.Add(Content.Load<Texture2D>("Images//Background//Stars//M//MSmallStar01"));

            starTexturesL.Add(Content.Load<Texture2D>("Images//Background//Stars//L//LStar01"));
            smallStarTexturesL.Add(Content.Load<Texture2D>("Images//Background//Stars//L//LSmallStar01"));

            starTexturesT.Add(Content.Load<Texture2D>("Images//Background//Stars//T//TStar01"));
            smallStarTexturesT.Add(Content.Load<Texture2D>("Images//Background//Stars//T//TSmallStar01"));

            starTexturesY.Add(Content.Load<Texture2D>("Images//Background//Stars//Y//YStar01"));
            smallStarTexturesY.Add(Content.Load<Texture2D>("Images//Background//Stars//Y//YSmallStar01"));

            starTexturesD.Add(Content.Load<Texture2D>("Images//Background//Stars//D//DStar01"));
            smallStarTexturesD.Add(Content.Load<Texture2D>("Images//Background//Stars//D//DSmallStar01"));

            starTexturesWR.Add(Content.Load<Texture2D>("Images//Background//Stars//WR//WRStar01"));
            smallStarTexturesWR.Add(Content.Load<Texture2D>("Images//Background//Stars//WR//WRSmallStar01"));

            starTexturesProtostar.Add(Content.Load<Texture2D>("Images//Background//Stars//Protostar//Protostar01"));
            smallStarTexturesProtostar.Add(Content.Load<Texture2D>("Images//Background//Stars//Protostar//ProtostarSmall01"));

            starTexturesPulsar.Add(Content.Load<Texture2D>("Images//Background//Stars//Pulsar//Pulsar01"));
            smallStarTexturesPulsar.Add(Content.Load<Texture2D>("Images//Background//Stars//Pulsar//PulsarSmall01"));

            starTexturesNeutron.Add(Content.Load<Texture2D>("Images//Background//Stars//Neutron//Neutron01"));
            smallStarTexturesNeutron.Add(Content.Load<Texture2D>("Images//Background//Stars//Neutron//NeutronSmall01"));

            starTexturesQuark.Add(Content.Load<Texture2D>("Images//Background//Stars//Quark//Quark01"));
            smallStarTexturesQuark.Add(Content.Load<Texture2D>("Images//Background//Stars//Quark//QuarkSmall01"));

            starTexturesBoson.Add(Content.Load<Texture2D>("Images//Background//Stars//Boson//Boson01"));
            smallStarTexturesBoson.Add(Content.Load<Texture2D>("Images//Background//Stars//Boson//BosonSmall01"));

            starTexturesBlackHole.Add(Content.Load<Texture2D>("Images//Background//Stars//BlackHole//BlackHole01"));
            smallStarTexturesBlackHole.Add(Content.Load<Texture2D>("Images//Background//Stars//BlackHole//BlackHoleSmall01"));

            starTexturesWhiteHole.Add(Content.Load<Texture2D>("Images//Background//Stars//WhiteHole//WhiteHole01"));
            smallStarTexturesWhiteHole.Add(Content.Load<Texture2D>("Images//Background//Stars//WhiteHole//WhiteHoleSmall01"));

            starTexturesSquare.Add(Content.Load<Texture2D>("Images//Background//Stars//Square//SquareStar01"));
            smallStarTexturesSquare.Add(Content.Load<Texture2D>("Images//Background//Stars//Square//SquareSmallStar01"));

            starTexturesRainbow.Add(Content.Load<Texture2D>("Images//Background//Stars//Rainbow//RainbowStar01"));
            smallStarTexturesRainbow.Add(Content.Load<Texture2D>("Images//Background//Stars//Rainbow//RainbowSmallStar01"));

            starTexturesRing.Add(Content.Load<Texture2D>("Images//Background//Stars//Ring//RingStar01"));
            smallStarTexturesRing.Add(Content.Load<Texture2D>("Images//Background//Stars//Ring//RingSmallStar01"));

            starTexturesDarkMatter.Add(Content.Load<Texture2D>("Images//Background//Stars//DarkMatter//DarkMatterStar01"));
            smallStarTexturesDarkMatter.Add(Content.Load<Texture2D>("Images//Background//Stars//DarkMatter//DarkMatterSmallStar01"));

            starTexturesAntiMatter.Add(Content.Load<Texture2D>("Images//Background//Stars//AntiMatter//AntiMatterStar01"));
            smallStarTexturesAntiMatter.Add(Content.Load<Texture2D>("Images//Background//Stars//AntiMatter//AntiMatterSmallStar01"));

            starTexturesFaction.Add(Content.Load<Texture2D>("Images//Background//Stars//Faction//FactionStar01"));
            smallStarTexturesFaction.Add(Content.Load<Texture2D>("Images//Background//Stars//Faction//FactionSmallStar01"));

            starTexturesSulphur.Add(Content.Load<Texture2D>("Images//Background//Stars//Sulphur//SulphurStar01"));
            smallStarTexturesSulphur.Add(Content.Load<Texture2D>("Images//Background//Stars//Sulphur//SulphurSmallStar01"));

            starTexturesGlorious.Add(Content.Load<Texture2D>("Images//Background//Stars//Glorious//GloriousStar01"));
            smallStarTexturesGlorious.Add(Content.Load<Texture2D>("Images//Background//Stars//Glorious//GloriousSmallStar01"));

            starTexturesQuantum.Add(Content.Load<Texture2D>("Images//Background//Stars//Quantum//QuantumStar01"));
            smallStarTexturesQuantum.Add(Content.Load<Texture2D>("Images//Background//Stars//Quantum//QuantumSmallStar01"));

            starTexturesTranslocation.Add(Content.Load<Texture2D>("Images//Background//Stars//Translocation//TranslocationStar01"));
            smallStarTexturesTranslocation.Add(Content.Load<Texture2D>("Images//Background//Stars//Translocation//TranslocationSmallStar01"));

            starTexturesAncient.Add(Content.Load<Texture2D>("Images//Background//Stars//Ancient//AncientStar01"));
            smallStarTexturesAncient.Add(Content.Load<Texture2D>("Images//Background//Stars//Ancient/AncientSmallStar01"));

            starTexturesGhost.Add(Content.Load<Texture2D>("Images//Background//Stars//Ghost//GhostStar01"));
            smallStarTexturesGhost.Add(Content.Load<Texture2D>("Images//Background//Stars//Ghost//GhostSmallStar01"));
        }

    }
}
