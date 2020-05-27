using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpTalk;
using System.Speech.Synthesis;
using SkeletonEngine;

namespace DevStomp
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        string talk = "";
        List<Voice> voices;

        bool start = false;
        int startint = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
                {
                    PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8
                };

            Content.RootDirectory = "Content";
            this.IsMouseVisible = false;

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;

            InactiveSleepTime = new TimeSpan(0);
            
            Window.Title = "Poot Lords";
        }

        protected override void Initialize()
        {
            EngineLoader.InitializeEngine(Window.Handle, graphics, GraphicsDevice);



            SheetTest.Initialize(GraphicsDevice);












            ElementTable.Initialize(Content, spriteBatch, GraphicsDevice);
            DrawRectangle3.Initialize(GraphicsDevice);
            Measurements.Initialize();




            if (Directory.Exists(Serializer.SaveLocation))
                Directory.Delete(Serializer.SaveLocation, true);

            InputManager.Initialize();
            //Projectiles.Initialize();
            //Particles.Initialize();

            voices = new List<Voice>();



            base.Initialize();
        }

        protected override void LoadContent()
        {
            StaticInfoBox.LoadContent(Content);
            StaticTests.LoadTextures(Content);
            //SM.Initialize(Content, GraphicsDevice);
            SpineLoader.LoadContent(GraphicsDevice);
            WorldManager.Initialize(GraphicsDevice);
            FontManager.LoadContent(Content);

            //LifeManager.Initialize();
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            if (InputManager.KBButtonPressed(true, new KeyboardInput(Keys.Home)))
            {
                SheetTest.Initialize(GraphicsDevice);
            }

            DebugOptions.DebugDisplay.Clear();
            //Console..Clear();
            EngineLoader.Update(gameTime, Window.ClientBounds, IsActive);



            InputManager.Update();

            


            DebugOptions.Update();

            //IsFixedTimeStep = false;

            //Mouse.SetPosition(50, 50);

            SpeechSynthesizer test2 = new SpeechSynthesizer();
            //if (InputManager.KB.IsKeyDown(Keys.RightShift) && InputManager.pKB.IsKeyUp(Keys.RightShift))
            //{
            //    ////test.Voice.Gender = VoiceGender.Female;
            //    //test2.SelectVoiceByHints(VoiceGender.NotSet);
            //    //test2.SelectVoice("Microsoft Hazel Desktop");


            //    ////test.Voice.Name
            //    //test2.SpeakAsync("Basic speech is rather easy to accomplish. Simply type a desired sentence and click play. If a word doesn't sound right, try misspelling the word in a way that DECtalk would have to say it right.");
            //    ////test.Speak("Nah mayte");
            //}


            //if (InputManager.GP[0].






            //if (InputManager.KB.IsKeyDown(Keys.RightShift) && InputManager.pKB.IsKeyUp(Keys.RightShift))
            //{
            //    start = false;
            //    startint = 0;

            //    for (int i = 0; i < voices.Count; i++)
            //    {
            //        voices[i].Stop();
            //    }

            //    voices.Clear();
            //}


            //VoiceTest();




            //LifeManager.Update();
            //Projectiles.Update();
            //Particles.Update();

            //if (InputManager.M.RightButton == ButtonState.Pressed && InputManager.pM.RightButton == ButtonState.Released)
            //{
            //    if (InputManager.MouseControl)
            //        InputManager.MouseControl = false;
            //    else
            //        InputManager.MouseControl = true;
            //}

            //if (CameraManager.SelectedCamera != -1)
            //{
            //    float r = CameraManager.Cams[CameraManager.SelectedCamera].R;
            //    float z = CameraManager.Cams[CameraManager.SelectedCamera].Z;
            //    float cos = (float)Math.Cos(r);
            //    float sin = (float)Math.Sin(r);

            //    if (InputManager.GP[0].IsButtonDown(Buttons.LeftShoulder))
            //        CameraManager.Cams[0].SetRotation(CameraManager.Cams[0].R - (float)GlobalVariables.FrameTime);

            //    if (InputManager.GP[0].IsButtonDown(Buttons.RightShoulder))
            //        CameraManager.Cams[0].SetRotation(CameraManager.Cams[0].R + (float)GlobalVariables.FrameTime);


            //    if (InputManager.MouseControl)
            //    {

            //if (CameraManager.Cams[0].Z < 0.1f)
            //    CameraManager.Cams[0].SetZoom(0.1f);
            //else if (CameraManager.Cams[0].Z > 2f)
            //    CameraManager.Cams[0].SetZoom(2f);

            //if (CameraManager.Cams[0].ZD < 0.1f)
            //    CameraManager.Cams[0].ZD = 0.1f;
            //else if (CameraManager.Cams[0].ZD > 2f)
            //    CameraManager.Cams[0].ZD = 2f;

            //        Vector2 p = new Vector2(InputManager.pM.X - InputManager.M.X, InputManager.pM.Y - InputManager.M.Y);

            //        float x = p.X;
            //        float y = p.Y;

            //        Vector2 Butts;
            //        Butts.X = (x * cos) - (-y * sin);
            //        Butts.Y = (y * cos) - (x * sin);

            //        if (InputManager.M.LeftButton == ButtonState.Pressed)
            //            CameraManager.Cams[CameraManager.SelectedCamera].MovePosition(Butts / CameraManager.Cams[CameraManager.SelectedCamera].Z, 10);
            //    }
            //    else
            //    {
            //        //CameraManager.Cams[CameraManager.SelectedCamera].SetPosition(LifeManager.CameraPos, 20);
            //    }
            //}
            
            //Console..WriteLine(CameraManager.Cams[0].Position);

            WorldManager.Update(GraphicsDevice);

            List<TestImage> test = new List<TestImage>();

            //if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.Insert))
            //{
            //    //byte[] r = new byte[1024 * 1024];
            //    //byte[] g = new byte[1024 * 1024];
            //    //byte[] b = new byte[1024 * 1024];
            //    Random rand = new Random();

            //    byte[] r = new byte[2048 * 2048];
            //    byte[] g = new byte[2048 * 2048];
            //    byte[] b = new byte[2048 * 2048];

            //    rand.NextBytes(r);
            //    rand.NextBytes(g);
            //    rand.NextBytes(b);
            //    test.Add(new TestImage(r, g, b));
            //    r = new byte[2048 * 2048];
            //    g = new byte[2048 * 2048];
            //    b = new byte[2048 * 2048];
            //    rand.NextBytes(r);
            //    rand.NextBytes(g);
            //    rand.NextBytes(b);
            //    test.Add(new TestImage(r, g, b));
            //    r = new byte[2048 * 2048];
            //    g = new byte[2048 * 2048];
            //    b = new byte[2048 * 2048];
            //    rand.NextBytes(r);
            //    rand.NextBytes(g);
            //    rand.NextBytes(b);
            //    test.Add(new TestImage(r, g, b));
            //    r = new byte[2048 * 2048];
            //    g = new byte[2048 * 2048];
            //    b = new byte[2048 * 2048];
            //    rand.NextBytes(r);
            //    rand.NextBytes(g);
            //    rand.NextBytes(b);
            //    test.Add(new TestImage(r, g, b));
            //    r = new byte[2048 * 2048];
            //    g = new byte[2048 * 2048];
            //    b = new byte[2048 * 2048];
            //    rand.NextBytes(r);
            //    rand.NextBytes(g);
            //    rand.NextBytes(b);
            //    test.Add(new TestImage(r, g, b));
            //    //test.Add(new TestImage(r, g, b));
            //    //test.Add(new TestImage(r, g, b));
            //    //test.Add(new TestImage(r, g, b));
            //    //test.Add(new TestImage(r, g, b));

            //    //test.Add(new TestImage(r, g, b));
            //    //test.Add(new TestImage(r, g, b));
            //    //test.Add(new TestImage(r, g, b));
            //    //test.Add(new TestImage(r, g, b));
            //    //test.Add(new TestImage(r, g, b));

            //    SaveFile.Save(test, "", "Save");
            //}


            //if (InputManager.KBButtonPressed(true, Microsoft.Xna.Framework.Input.Keys.Home))
            //{
            //    List<TestImage> test67 = (List<TestImage>)SaveFile.Load("Save");
            //}


            base.Update(gameTime);
        }

        private void VoiceTest()
        {

            if (InputManager.KB.IsKeyDown(Keys.RightControl) && InputManager.pKB.IsKeyUp(Keys.RightControl))
            {
                start = true;
                startint = 0;

                for (int i = 0; i < voices.Count; i++)
                {
                    voices[i].Stop();
                }

                voices.Clear();

                for (int i = 0; i < new Random().Next(0, 2); i++)
                {
                    voices.Add(new Voice());
                    voices[voices.Count - 1].GenerateRandomVoice();
                }


                Song song = new Song();
                song.BPM = 120;
                song.AddNote("C3", 1f);
                song.AddNote("C3", 1f);
                song.AddNote("G3", 1f);
                song.AddNote("G3", 1f);
                song.AddNote("A3", 1f);
                song.AddNote("A3", 1f);
                song.AddNote("G3", 2f);

                song.AddNote("F3", 1f);
                song.AddNote("F3", 1f);
                song.AddNote("E3", 1f);
                song.AddNote("E3", 1f);
                song.AddNote("D3", 1f);
                song.AddNote("D3", 1f);
                song.AddNote("C3", 2f);

                song.AddNote("G3", 1f);
                song.AddNote("G3", 1f);
                song.AddNote("F3", 1f);
                song.AddNote("F3", 1f);
                song.AddNote("E3", 1f);
                song.AddNote("E3", 1f);
                song.AddNote("D3", 2f);

                song.AddNote("G3", 1f);
                song.AddNote("G3", 1f);
                song.AddNote("F3", 1f);
                song.AddNote("F3", 1f);
                song.AddNote("E3", 1f);
                song.AddNote("E3", 1f);
                song.AddNote("D3", 2f);

                song.AddNote("C3", 1f);
                song.AddNote("C3", 1f);
                song.AddNote("G3", 1f);
                song.AddNote("G3", 1f);
                song.AddNote("A3", 1f);
                song.AddNote("A3", 1f);
                song.AddNote("G3", 2f);

                song.AddNote("F3", 1f);
                song.AddNote("F3", 1f);
                song.AddNote("E3", 1f);
                song.AddNote("E3", 1f);
                song.AddNote("D3", 1f);
                song.AddNote("D3", 1f);
                song.AddNote("C3", 2f);

                talk = song.ReturnVocalSong();
            }

            if (start)
            {
                talk = "[spuh<300,19>kiy<300,19>skeh<300,18>riy<300,18>skeh<300,11>lleh<175,14>tih<200,11>ns][seh<300,11>nd][shih<100,19>ver<500,19>sdaw<300,18>nyur<300,18>spay<300,11>n][:comma 50][shriy<300,19>kiy<300,19>ng][skow<300,18>swih<300,18>ll][shah<300,11>kyur<300,14>sow<300,11>ll][siy<300,14>llyur<300,16>duh<300,13>mtuh<300,14>nay<300,11>t]";
                //talk = "[hxae<370,1>hxae<360,2>hxae<350,3>hxae<340,4>hxae<330,5>hxae<320,6>hxae<310,7>hxae<300,8>hxae<290,9>hxae<280,10>hxae<270,11>hxae<260,12>hxae<250,13>hxae<240,14>hxae<230,15>hxae<220,16>hxae<210,17>hxae<200,18>hxae<190,19>hxae<180,20>hxae<170,21>hxae<160,22>hxae<150,23>hxae<140,24>hxae<130,25>hxae<120,26>hxae<110,27>hxae<100,28>hxae<90,29>hxae<80,30>hxae<70,31>hxae<60,32>hxae<50,33>hxae<40,34>hxae<30,35>hxae<20,36>hxae<10,37>]";
                //talk = "[llao<1600,25>][llao<350,20>][llao<300,18>][llao<1600,20>][llao<350,13>][llao<300,15>][llao<1200,17>][llao<1200,20>][llao<600,17>][llao<1400,15>][llao<130,20>][llao<130,22>][llao<130,23>][llao<130,24>][llao<1600,25>][llao<350,20>][llao<300,18>][llao<1600,20>][llao<350,13>][llao<300,15>][llao<1200,17>][llao<1200,17>][llao<600,15>][llao<1000,13>][llao<130,13>][llao<130,17>][llao<130,20>][llao<130,25>][llao<350,24>][llao<130,24>][llao<130,20>][llao<350,22>][llao<130,22>][llao<130,18>][llao<620,20>][llao<130,8>][llao<130,12>][llao<130,15>][llao<130,18>][llao<1600,17>][llao<130,13>][llao<130,17>][llao<130,20>][llao<130,25>][llao<350,24>][llao<130,24>][llao<130,20>][llao<350,22>][llao<130,22>][llao<130,18>][llao<620,20>][llao<130,20>][llao<130,22>][llao<130,23>][llao<130,24>][llao<1600,25>][llao<200,29>][llao<200,27>][llao<200,25>][llao<200,24>][llao<200,22>][llao<300,20>][llao<130,17>][llao<130,18>][llao<1200,20>][llao<350,13>][llao<300,15>][llao<1200,17>][llao<1200,17>][llao<600,15>][llao<1000,13>]";
                //talk = "[_<1,20>]hey[_<400,12>]i[_<1,15>]just[_<1,20>]met[_<1,15>]yewwww[_<800,15>]and[_<1,12>]this[_<1,15>]is[_<1,24>]cray[_<1,20>]zee[_<1,20>]but[_<1,24>]here's[_<1,25>]my[_<1,24>]num[_<1,20>]ber[_<800,20>]so[_<1,24>]call[_<1,22>]me mey[_<1,20>]be[_<1,20>]and[_<200,12>]all[_<1,15>]the[_<1,20>]other[_<1,15>]boys[_<800,12>]try[_<1,15>]to[_<1,24>]chayyys[_<1,20>]me[_<1,20>]but[_<1,24>]here's[_<1,25>]my[_<1,24>]num[_<1,20>]ber[_<800,20>]so[_<1,24>]call[_<1,22>]me mey[_<1,20>]be";

                //talk = "Basic speech is rather easy to accomplish. Simply type a desired sentence and click play. If a word doesn't sound right, try misspelling the word in a way that DECtalk would have to say it right.";

                //talk = "We Are the Borg, You Will be Assimilated, Resistance is Futile.";

                //talk = "whirz mah dortor";
                talk = "[nae<99,20>nae<99,20>nae<99,19>nae<99,19>nae<99,18>nae<99,18>nae<99,19>nae<99,19>nae<99,20>nae<99,20>nae<99,19>nae<99,19>nae<99,18>nae<99,18>nae<99,19>nae<99,19>bae<140,25>ttmae<600,25>nn]";
                //talk = "[sow<150,28>ay<150,28>kkray<300,37>ssuh<300,37>mmttay<300,33>mms<50>wweh<150,33>nnay<150,33>mm<50>llay<150,28>ihnn<150,28>]" +
                //        "[ih<150,28>nnbeh<150,27>eh<300,25>dd<50>guh<150,26>sstuw<150,26>geh<150,37>ttow<300,35>ttwah<150,33>tts<50>ih<150,35>]" +
                //        "[mah<300,37>hxae<300,33>dd<50>ae<150,33>dd<50>ay<300,33>mm<10>ffiy<150,33>llih<150,35>nnpeh<150,28>kkuw<300,26>llyur<300,25>]" +
                //        "[sow<150,28>ay<150,28>wwey<300,37>kkih<150,37>nnth<20>uh<150,37>mor<200,33>nnihn<200,33>ae<200,33>nndday<200,33>ssteh<300,28>pp]" +
                //        "[aw<300,28>ttssah<150,26>ay<150,25>ddae<150,25>nday<150,25>ttey<150,26>kkuh<150,26>ddiy<150,37>ppbr<20>eh<300,35>thae<300,33>]" +
                //        "[ndgeh<300,35>ttrih<300,37>llhxay<300,33>ae<150,33>nnay<150,33>say<300,33>_<300>ae<150,33>tteh<150,33>ttaw<150,33>ppuh<150,33>]" +
                //        "[ffmaa<200,33>lluh<300,33>nngswah<300,35>ssgow<150,35>ih<300,37>ngaw<300,33>nn]" +
                //        "[ae<150,28>nnday<300,30>ssae<300,28>_<300>hxeh<750,37>ey<200,33>eh<750,33>ey<200,28>eh<750,28>ey<400,26>ey<400,25>]" +
                //        "[hxeh<750,37>ey<200,33>eh<750,33>ey<200,26>eh<750,26>_<750>ay<150,35>sseh<200,37>hxeh<300,33>eh<300,30>]" +
                //        "[wwah<300,35>ttsgow<300,35>ih<400,37>nnaw<300,33>nn<90>]";

                //talk = "[dah<600,20>][dah<600,20>][dah<600,20>][dah<500,16>][dah<130,23>][dah<600,20>][dah<500,16>][dah<130,23>][dah<600,20>][dah<600,27>][dah<600,27>][dah<600,27>][dah<500,28>][dah<130,23>][dah<600,19>][dah<500,15>][dah<130,23>][dah<600,20>][dah<600,32>][dah<600,20>][dah<600,32>][dah<600,31>][dah<100,30>][dah<100,29>][dah<100,28>][dah<300,29>][dah<150,18>][dah<600,28>][dah<600,27>][dah<100,26>][dah<100,25>][dah<100,24>][dah<100,26>][dah<150,15>][dah<600,20>][dah<600,16>][dah<150,23>][dah<600,20>][dah<600,20>][dah<150,23>][dah<600,27>][dah<600,32>][dah<600,20>][dah<600,32>][dah<600,31>][dah<100,30>][dah<100,29>][dah<100,28>][dah<300,29>][dah<150,18>][dah<600,28>][dah<600,27>][dah<100,26>][dah<100,25>][dah<100,24>][dah<100,26>][dah<150,15>][dah<600,20>][dah<600,16>][dah<150,23>][dah<600,20>][dah<600,16>][dah<150,23>][dah<600,20>]";
                //talk = "[dah<300,30>][dah<60,30>][dah<200,25>][dah<1000,30>][dah<200,23>][dah<400,25>][dah<700,18>]";
                //talk = "[_<1,13>]we're[_<1,18>]whalers[_<1,17]on[_<1,18>]the[_<1,20>]moon[_<400,13>]we[_<1,20>]carry[_<1,18>]a[_<1,20>]har[_<1,22>]poon[_<1,22>]but there[_<1,23>]aint no[_<1,15>]whales[_<1,23>]so we[_<1,22>]tell tall[_<1,18>]tales and[_<1,20>]sing our[_<1,18>]whale[_<1,17>]ing[_<1,18>]tune";


                //talk = "[:phone arpa on][:nk][r<64,14>ow<558,13>_<10,0>r<64,14>ow<558,13>_<10,0>r<64,14>ow<408,13>_<10,0>yx<64,15>rr<138,15>b<83,17>ow<367,17>t<57,17>_<120,0>jh<53,17>eh<181,17>n<32,17>tx<32,17>l<65,15>iy<198,15>d<70,17>aw<261,17>n<71,17>n<58,18>ax<97,18>s<83,20>t<51,20>r<57,20>iy<462,20>m<83,20>ix<25,20>_<0,100>][:np][m<76,26>er<169,25>ih<155,25>l<36,25>iy<160,25>m<76,21>er<169,20>ih<155,20>l<36,20>iy<160,20>m<76,18>er<169,17>ih<155,17>l<36,17>iy<160,17>m<76,14>er<169,13>ih<155,13>l<36,13>iy<160,13>_<100,0>l<76,20>ay<329,20>f<68,20>ih<122,18>z<56,18>b<88,17>ah<162,17>t<70,17>_<70,0>ax<225,15>d<64,13>r<38,13>iy<332,13>m<83,13>ix<25,13>_<0,100>]";


                for (int i = 0; i < voices.Count; i++)
                {
                    //if (new Random().Next(0, 10) == 0)
                    if (startint == i * 5)
                    {
                        voices[i].Speak(talk, true);
                    }
                }

                startint++;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            EngineLoader.Draw(spriteBatch);

            GraphicsDevice.Clear(new Color(15, 15, 15));
            CameraManager.Update();

            //CameraManager.Cams[0].viewport.Width = GlobalVariables.WindowWidth / 2;
            //CameraManager.Cams[0].viewport.Height = GlobalVariables.WindowHeight;

            //CameraManager.Cams[1].viewport.X = GlobalVariables.WindowWidth / 2;
            //CameraManager.Cams[1].viewport.Width = GlobalVariables.WindowWidth / 2;
            //CameraManager.Cams[1].viewport.Height = GlobalVariables.WindowHeight;
            //CameraManager.Cams[1].Z = 0.01f;

            //CameraManager.Cams[0].viewport.Width = (int)GlobalVariables.WindowSize.X;
            //CameraManager.Cams[0].viewport.Height = (int)GlobalVariables.WindowSize.Y;

            //CameraManager.Cams[1].viewport.X = 0;
            //CameraManager.Cams[1].viewport.Width = 200;
            //CameraManager.Cams[1].viewport.Height = 200;
            //CameraManager.Cams[1].viewport.Width = (int)GlobalVariables.WindowSize.X;
            //CameraManager.Cams[1].viewport.Height = (int)GlobalVariables.WindowSize.Y;
            //CameraManager.Cams[1].Z = 0.0003f;

            //CameraManager.Cams[1].SetPosition(CameraManager.Cams[0].P);
            //CameraManager.Cams[1].P         = CameraManager.Cams[0].P;
            //CameraManager.Cams[1].PositionX = CameraManager.Cams[0].PositionX;
            //CameraManager.Cams[1].PositionY = CameraManager.Cams[0].PositionY;
            //CameraManager.Cams[1].R = CameraManager.Cams[0].R;



            //CameraManager.Cams[1].viewport.Height = (int)(GlobalVariables.WindowSize.Y * 1.7f);
            //CameraManager.Cams[1].Update();
            //CameraManager.Cams[1].viewport.Height = (int)GlobalVariables.WindowSize.Y;

            Camera cam;
            Viewport view;

            IsFixedTimeStep = false;
            IsMouseVisible = false;
            graphics.SynchronizeWithVerticalRetrace = false;

            for (int i = 0; i < CameraManager.Cams.Count; i++)
                for (int o = -1; o < CameraManager.Cams[i].subCameras.Cams.Count; o++)
                {
                    if (o == -1)
                        cam = CameraManager.Cams[i];
                    else
                        cam = CameraManager.Cams[i].subCameras.Cams[o];

                    view = new Viewport(cam.viewport);

                    //int xAdd = (int)(((GraphicsManager.WindowResolution.X / 2f) - (GraphicsManager.GameResolution.X / 2f)) * GraphicsManager.ScreenScale.Y);
                    //int yAdd = (int)(((GraphicsManager.WindowResolution.Y / 2f) - (GraphicsManager.GameResolution.Y / 2f)) * GraphicsManager.ScreenScale.X);

                    //if (xAdd > 0)
                    //    xAdd = 0;
                    //if (yAdd > 0)
                    //    yAdd = 0;


                    //view.X = (int)(view.X * GraphicsManager.ScreenScale.X);
                    //view.Y = (int)(view.Y * GraphicsManager.ScreenScale.Y);
                    //view.Width =   (int)(view.Width * GraphicsManager.ScreenScale.X);
                    //view.Height =  (int)(view.Height * GraphicsManager.ScreenScale.Y);

                    view = GraphicsManager.ScaledViewport(view);

                    if (view.X + view.Width <= graphics.PreferredBackBufferWidth && view.Y + view.Height <= graphics.PreferredBackBufferHeight && view.X >= 0 && view.Y >= 0)
                    {
                        GraphicsDevice.Viewport = view;

                        spriteBatch.Begin();

                        Color color1 = GlowColor;
                        ColorManager.HSB hsb = ColorManager.RGB2HSB(color1);
                        hsb.B *= 0.1f;

                        Color color2 = ColorManager.HSB2RGB(hsb);

                        hsb = ColorManager.RGB2HSB(color1);
                        hsb.B = 1f;
                        hsb.S *= 0.75f;
                        color1 = ColorManager.HSB2RGB(hsb);

                        Color blend = Color.White;

                        blend.R = (byte)UsefulMethods.FindBetween(DayTime, 1f, 0f, color1.R, color2.R, false);
                        blend.G = (byte)UsefulMethods.FindBetween(DayTime, 1f, 0f, color1.G, color2.G, false);
                        blend.B = (byte)UsefulMethods.FindBetween(DayTime, 1f, 0f, color1.B, color2.B, false);



                        //float tint1 = UsefulMethods.FindBetween(DayTime, 1f, 0f, 0.3f, 0.1f, false);
                        //float tint2 = UsefulMethods.FindBetween(DayTime, 1f, 0f, 0.6f, 0.1f, false);
                        //float tint3 = UsefulMethods.FindBetween(DayTime, 1f, 0f, 0.9f, 0.4f, false);

                        spriteBatch.Draw(StaticTests.Marker, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), blend);
                        spriteBatch.Draw(SheetTest.GetTexture("TestImages", 5), new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Black);
                        spriteBatch.End();

                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cam.Transform * GraphicsManager.ScreenMatrix);



                        cam.DrawBack(spriteBatch);
                        WorldManager.Draw(spriteBatch, cam);

                        DrawPlanet();

                        cam.DrawFront(spriteBatch);
                        DrawRectangle3.Draw(GraphicsDevice, cam);




                        //Particles.Draw(spriteBatch);
                        //Projectiles.Draw(spriteBatch);
                        //LifeManager.DrawBack(spriteBatch);
                        //LifeManager.Draw(spriteBatch, i);
                        //LifeManager.DrawFront(spriteBatch);

                        spriteBatch.End();
                    }
                }

            //GraphicsDevice.Viewport = CameraManager.Cams[0].subCameras.Cams[0].viewport;

            //spriteBatch.Begin();
            //spriteBatch.Draw(StaticTests.Marker, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Red);
            //spriteBatch.End();

            //if (InputManager.KBButtonPressed(true, Keys.Insert))


            view = new Viewport(0, 0, (int)GraphicsManager.GameResolution.X, (int)GraphicsManager.GameResolution.Y);
            view = GraphicsManager.ScaledViewport(view);



            if (view.X + view.Width <= graphics.PreferredBackBufferWidth && view.Y + view.Height <= graphics.PreferredBackBufferHeight && view.X >= 0 && view.Y >= 0)
                GraphicsDevice.Viewport = view;









            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, GraphicsManager.ScreenMatrix);

            Color CursorColor = Color.White;

            DebugOptions.DebugDisplay.Add("" + GraphicsManager.ScreenScale);

            spriteBatch.Draw(StaticTests.TestCursor, InputManager.MousePosition, CursorColor);
            //LifeManager.DrawUI(spriteBatch);
            DebugOptions.Draw(spriteBatch);

            //spriteBatch.Draw(SM.PlanetBody.S(0), Vector2.Zero, Color.White);

            //if (CameraManager.SelectedCamera != -1)
            //    spriteBatch.Draw(StaticTests.Marker, new Rectangle(CameraManager.Cams[CameraManager.SelectedCamera].viewport.X, CameraManager.Cams[CameraManager.SelectedCamera].viewport.Y, CameraManager.Cams[CameraManager.SelectedCamera].viewport.Width, CameraManager.Cams[CameraManager.SelectedCamera].viewport.Height), Color.Red * 0.1f);


            //DrawPlanet();


            spriteBatch.End();

            //CameraManager.Cams[0].subCameras.Positions[0] = new Vector2(0.1f, 0.1f);
            //CameraManager.Cams[0].subCameras.Scales[0] = new Vector2(0.13f, 0.2f);


            #region Old Mouse Stuff

            //Vector2 MousePosition = new Vector2(InputManager.M.X - (GlobalVariables.WindowWidth / 2), InputManager.M.Y - (GlobalVariables.WindowHeight / 2));     

            //float WorldAngle = -CameraManager.Cams[0].R;
            ////WorldAngle -= (float)Math.PI / 2f;

            //float Distance = Vector2.Distance(Vector2.Zero, MousePosition);
            //float MouseAngle = (float)Math.Atan2(MousePosition.X, -MousePosition.Y) + WorldAngle;
            //MousePosition = new Vector2((float)Math.Sin(MouseAngle), -(float)Math.Cos(MouseAngle)) * Distance;

            //MousePosition = CameraManager.Cams[0].P + (MousePosition / CameraManager.Cams[0].Z);


            //Vector2 PreviousMousePosition = new Vector2(InputManager.pM.X - (GlobalVariables.WindowWidth / 2), InputManager.pM.Y - (GlobalVariables.WindowHeight / 2));
            //PreviousMousePosition = CameraManager.Cams[0].P + (PreviousMousePosition / CameraManager.Cams[0].Z);
            //PreviousMousePosition = MousePosition;

            //CollisionData VertexPoint = null;
            ////VertexPoint = WorldManager.worlds.CheckTile(MousePosition, PreviousMousePosition);

            #endregion

            base.Draw(gameTime);
        }

        int BodyID = 0;
        int CloudsID = 0;
        int GlowID = 0;
        int OverlayID = 0;
        int ShadowID = 0;

        Color BodyColor = Color.White;
        Color CloudsColor = Color.White;
        Color GlowColor = Color.White;
        Color OverlayColor = Color.White;
        Color ShadowColor = Color.White;

        float BodyRotation = 0;
        float CloudsRotation = 0;
        float GlowRotation = 0;
        float OverlayRotation = 0;
        float ShadowRotation = 0;

        private void DrawPlanet()
        {
            string directory = "Galaxy-Planet-";
            Random rand = new Random();

            if (InputManager.KBButtonPressed(true, new KeyboardInput(Keys.Enter)))
            {
                BodyID = rand.Next(0, SheetTest.GetTextureCount(directory + "Body"));
                CloudsID = rand.Next(0, SheetTest.GetTextureCount(directory + "Clouds"));
                GlowID = rand.Next(0, SheetTest.GetTextureCount(directory + "Glow"));
                OverlayID = rand.Next(0, SheetTest.GetTextureCount(directory + "Overlay"));
                ShadowID = rand.Next(0, SheetTest.GetTextureCount(directory + "Shadows"));

                BodyColor = ColorManager.RandomColor();
                CloudsColor = ColorManager.RandomColor();
                GlowColor = ColorManager.RandomColor();
                OverlayColor = ColorManager.RandomColor();
                ShadowColor = ColorManager.RandomColor();

                BodyRotation = (float)rand.NextDouble() * ((float)Math.PI * 2f);
                CloudsRotation = (float)rand.NextDouble() * ((float)Math.PI * 2f);
                GlowRotation = (float)rand.NextDouble() * ((float)Math.PI * 2f);
                OverlayRotation = (float)rand.NextDouble() * ((float)Math.PI * 2f);
                ShadowRotation = (float)rand.NextDouble() * ((float)Math.PI * 2f);
            }

            float speed = 0.15f;

            BodyRotation += speed * GlobalVariables.WorldTime;
            CloudsRotation += speed * GlobalVariables.WorldTime;
            GlowRotation += speed * GlobalVariables.WorldTime;
            OverlayRotation += speed * GlobalVariables.WorldTime;
            ShadowRotation += speed * GlobalVariables.WorldTime;

            if (BodyRotation > (float)Math.PI * 2f)
                BodyRotation -= (float)Math.PI * 2f;
            if (CloudsRotation > (float)Math.PI * 2f)
                CloudsRotation -= (float)Math.PI * 2f;
            if (GlowRotation > (float)Math.PI * 2f)
                GlowRotation -= (float)Math.PI * 2f;
            if (OverlayRotation > (float)Math.PI * 2f)
                OverlayRotation -= (float)Math.PI * 2f;
            if (ShadowRotation > (float)Math.PI * 2f)
                ShadowRotation -= (float)Math.PI * 2f;


            //ShadowRotation = 3.12f;

            DayTime = UsefulMethods.FindBetween(Math.Abs(ShadowRotation - MathHelper.Pi), MathHelper.Pi * 0.75f, 0f, 1f, 0f, false);
            DebugOptions.DebugDisplay.Add("" + DayTime);

            for (int i = 0; i < CameraManager.Cams.Count; i++)
            {
                CameraManager.Cams[i].Para[3].DrawAlpha = 1 - DayTime;
                CameraManager.Cams[i].Para[4].DrawAlpha = 1 - DayTime;
            }

            if (InputManager.KBButtonPressed(false, new KeyboardInput(Keys.D1)))
            {
                if (InputManager.KBButtonPressed(true, new KeyboardInput(Keys.Left)))
                {
                    BodyID++;
                    if (BodyID >= SheetTest.GetTextureCount(directory + "Body"))
                        BodyID = 0;

                }
                else if (InputManager.KBButtonPressed(true, new KeyboardInput(Keys.Right)))
                {
                    BodyID--;
                    if (BodyID < 0)
                        BodyID = SheetTest.GetTextureCount(directory + "Body") - 1;
                }
            }

            Vector2 pos = new Vector2(0, 0);

            spriteBatch.Draw(SheetTest.GetTexture(directory + "Body", BodyID), pos, null, BodyColor, BodyRotation, SheetTest.GetTextureOrigin(directory + "Body", BodyID), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(SheetTest.GetTexture(directory + "Overlay", OverlayID), pos, null, OverlayColor, OverlayRotation, SheetTest.GetTextureOrigin(directory + "Overlay", OverlayID), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(SheetTest.GetTexture(directory + "Clouds", CloudsID), pos, null, CloudsColor * 0.8f, CloudsRotation, SheetTest.GetTextureOrigin(directory + "Clouds", CloudsID), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(SheetTest.GetTexture(directory + "Glow", GlowID), pos, null, GlowColor, GlowRotation, SheetTest.GetTextureOrigin(directory + "Glow", GlowID), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(SheetTest.GetTexture(directory + "Shadows", ShadowID), pos, null, ShadowColor, ShadowRotation, SheetTest.GetTextureOrigin(directory + "Shadows", ShadowID), 1f, SpriteEffects.None, 0f);
        }

        float DayTime;
    }
}
