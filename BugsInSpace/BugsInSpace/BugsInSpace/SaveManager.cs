using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

namespace BugsInSpace
{
    public class SaveManager
    {
        public List<string> SaveFiles;
        StorageDevice device;
        public string containerName = "SaveData";
        public string fileName;
        public bool LoadSave;

        public string playerName;
        public float playerAcceleration;
        public float playerBulletSpeed;
        public int playerShip;
        public int playerMaxBullets;
        public int playerDamage;
        public float playerFireRate;
        public int playerCredits;
        public int playerLives;
        public int playerMaxHealth;
        public int playerMaxEnergy;
        public int playerRedValue;
        public int playerBlueValue;
        public int playerGreenValue;

        public int iplayerAmmo;
        public int iplayerBulletSpeed;
        public int iplayerDamage;
        public int iplayerElectricProjectile;
        public int iplayerEnergy;
        public int iplayerEnergyProjectile;
        public int iplayerExplosiveProjectile;
        public int iplayerFireProjectile;
        public int iplayerFireRate;
        public int iplayerHealingSpecial;
        public int iplayerHealth;
        public int iplayerHealthProjectile;
        public int iplayerLaserProjectile;
        public int iplayerLaserSpecial;
        public int iplayerMoneySpecial;
        public int iplayerMovementSpeed;
        public int iplayerPoisonProjectile;
        public int iplayerShieldSpecial;
        public int iplayerSlowProjectile;
        public int iplayerTimeStopSpecial;

        public bool bplayerAutoFire;
        public bool bplayerTripleShot;
        public bool bplayerExtraLife1;
        public bool bplayerExtraLife2;
        public bool bplayerExtraLife3;
        public bool bplayerExtraLife4;
        public bool bplayerQuadShot;
        public bool bplayerQuintupleShot;
        public bool bplayerDoubleShot;

        public int playerLevel;
        public int playerDeathCount;
        public int playerTimePlayedHours;
        public int playerTimePlayedMinutes;
        public int playerTimePlayedSeconds;
        public int playerCreditsCollected;
        public int playerCreditsSpent;
        public int playerWeaponsCollected;
        public int playerPercentageComplete;
        public int playerBulletsFired;
        public int playerAccuracy;
        public int playerEnemiesKilled;
        public int playerEnemiesHit;
        public int playerMiniGamesPassed;
        public int playerUpgradesPurchased;
        public int playerPowerUpsCollected;
        public int playerLevelsCompleted;

        public bool playerAchievement1;
        public bool playerAchievement2;
        public bool playerAchievement3;
        public bool playerAchievement4;
        public bool playerAchievement5;
        public bool playerAchievement6;
        public bool playerAchievement7;
        public bool playerAchievement8;
        public bool playerAchievement9;
        public bool playerAchievement10;
        public bool playerAchievement11;
        public bool playerAchievement12;
        public bool playerAchievement13;
        public bool playerAchievement14;
        public bool playerAchievement15;
        public bool playerAchievement16;
        public bool playerAchievement17;
        public bool playerAchievement18;
        public bool playerAchievement19;
        public bool playerAchievement20;
        public bool playerAchievement21;
        public bool playerAchievement22;
        public bool playerAchievement23;
        public bool playerAchievement24;
        public bool playerAchievement25;
        public bool playerAchievement26;
        public bool playerAchievement27;
        public bool playerAchievement28;
        public bool playerAchievement29;
        public bool playerAchievement30;
        public bool playerAchievement31;
        public bool playerAchievement32;
        public bool playerAchievement33;
        public bool playerAchievement34;
        public bool playerAchievement35;
        public bool playerAchievement36;
        public bool playerAchievement37;
        public bool playerAchievement38;
        public bool playerAchievement39;
        public bool playerAchievement40;
        public bool playerAchievement41;
        public bool playerAchievement42;
        public bool playerAchievement43;
        public bool playerAchievement44;
        public bool playerAchievement45;
        public bool playerAchievement46;
        public bool playerAchievement47;
        public bool playerAchievement48;
        public bool playerAchievement49;
        public bool playerAchievement50;
        public int playerAchievementCount;
        public int playerSelectedWeapon1;
        public int playerSelectedWeapon2;
        public int playerSelectedWeapon3;
        public int playerSelectedWeapon4;
        public int playerSelectedWeapon5;
        public int playerSelectedSpecial;
        public int playerShipsUnlocked;
        public int playerXP;
        public int playerLevelNumber;

        public struct SaveGame
        {
            public string PlayerName;
            public float PlayerAcceleration;
            public float PlayerBulletSpeed;
            public int PlayerShip;
            public int PlayerMaxBullets;
            public int PlayerDamage;
            public float PlayerFireRate;
            public int PlayerCredits;
            public int PlayerLives;
            public int PlayerMaxHealth;
            public int PlayerMaxEnergy;
            public int PlayerRedValue;
            public int PlayerBlueValue;
            public int PlayerGreenValue;

            public int iPlayerAmmo;
            public int iPlayerBulletSpeed;
            public int iPlayerDamage;
            public int iPlayerElectricProjectile;
            public int iPlayerEnergy;
            public int iPlayerEnergyProjectile;
            public int iPlayerExplosiveProjectile;
            public int iPlayerFireProjectile;
            public int iPlayerFireRate;
            public int iPlayerHealingSpecial;
            public int iPlayerHealth;
            public int iPlayerHealthProjectile;
            public int iPlayerLaserProjectile;
            public int iPlayerLaserSpecial;
            public int iPlayerMoneySpecial;
            public int iPlayerMovementSpeed;
            public int iPlayerPoisonProjectile;
            public int iPlayerShieldSpecial;
            public int iPlayerSlowProjectile;
            public int iPlayerTimeStopSpecial;

            public bool bPlayerAutoFire;
            public bool bPlayerTripleShot;
            public bool bPlayerExtraLife1;
            public bool bPlayerExtraLife2;
            public bool bPlayerExtraLife3;
            public bool bPlayerExtraLife4;
            public bool bPlayerQuadShot;
            public bool bPlayerQuintupleShot;
            public bool bPlayerDoubleShot;

            public int PlayerLevel;
            public int PlayerDeathCount;
            public int PlayerTimePlayedHours;
            public int PlayerTimePlayedMinutes;
            public int PlayerTimePlayedSeconds;
            public int PlayerCreditsCollected;
            public int PlayerCreditsSpent;
            public int PlayerWeaponsCollected;
            public int PlayerPercentageComplete;
            public int PlayerBulletsFired;
            public int PlayerAccuracy;
            public int PlayerEnemiesKilled;
            public int PlayerEnemiesHit;
            public int PlayerMiniGamesPassed;
            public int PlayerUpgradesPurchased;
            public int PlayerPowerUpsCollected;
            public int PlayerLevelsCompleted;

            public bool PlayerAchievement1;
            public bool PlayerAchievement2;
            public bool PlayerAchievement3;
            public bool PlayerAchievement4;
            public bool PlayerAchievement5;
            public bool PlayerAchievement6;
            public bool PlayerAchievement7;
            public bool PlayerAchievement8;
            public bool PlayerAchievement9;
            public bool PlayerAchievement10;
            public bool PlayerAchievement11;
            public bool PlayerAchievement12;
            public bool PlayerAchievement13;
            public bool PlayerAchievement14;
            public bool PlayerAchievement15;
            public bool PlayerAchievement16;
            public bool PlayerAchievement17;
            public bool PlayerAchievement18;
            public bool PlayerAchievement19;
            public bool PlayerAchievement20;
            public bool PlayerAchievement21;
            public bool PlayerAchievement22;
            public bool PlayerAchievement23;
            public bool PlayerAchievement24;
            public bool PlayerAchievement25;
            public bool PlayerAchievement26;
            public bool PlayerAchievement27;
            public bool PlayerAchievement28;
            public bool PlayerAchievement29;
            public bool PlayerAchievement30;
            public bool PlayerAchievement31;
            public bool PlayerAchievement32;
            public bool PlayerAchievement33;
            public bool PlayerAchievement34;
            public bool PlayerAchievement35;
            public bool PlayerAchievement36;
            public bool PlayerAchievement37;
            public bool PlayerAchievement38;
            public bool PlayerAchievement39;
            public bool PlayerAchievement40;
            public bool PlayerAchievement41;
            public bool PlayerAchievement42;
            public bool PlayerAchievement43;
            public bool PlayerAchievement44;
            public bool PlayerAchievement45;
            public bool PlayerAchievement46;
            public bool PlayerAchievement47;
            public bool PlayerAchievement48;
            public bool PlayerAchievement49;
            public bool PlayerAchievement50;
            public int PlayerAchievementCount;
            public int PlayerSelectedWeapon1;
            public int PlayerSelectedWeapon2;
            public int PlayerSelectedWeapon3;
            public int PlayerSelectedWeapon4;
            public int PlayerSelectedWeapon5;
            public int PlayerSelectedSpecial;
            public int PlayerShipsUnlocked;
            public int PlayerXP;
            public int PlayerLevelNumber;
        }

        public void Initialize()
        {
            SaveFiles = new List<string>();
        }

        public void InitiateSave()
        {
            fileName = playerName + ".sav";
            //if (!Guide.IsVisible)
            {
                device = null;
                StorageDevice.BeginShowSelector(PlayerIndex.One, this.SaveToDevice, null);
            }
        }

        void SaveToDevice(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            if (device != null && device.IsConnected)
            {
                SaveGame SaveData = new SaveGame()
                {
                    PlayerName = playerName,
                    PlayerAcceleration = playerAcceleration,
                    PlayerBulletSpeed = playerBulletSpeed,
                    PlayerShip = playerShip,
                    PlayerMaxBullets = playerMaxBullets,
                    PlayerDamage = playerDamage,
                    PlayerRedValue = playerRedValue,
                    PlayerBlueValue = playerBlueValue,
                    PlayerGreenValue = playerGreenValue,
                    PlayerFireRate = playerFireRate,
                    PlayerCredits = playerCredits,
                    PlayerLives = playerLives,
                    PlayerMaxHealth = playerMaxHealth,
                    PlayerMaxEnergy = playerMaxEnergy,

                    iPlayerAmmo = iplayerAmmo,
                    bPlayerAutoFire = bplayerAutoFire,
                    iPlayerBulletSpeed = iplayerBulletSpeed,
                    iPlayerDamage = iplayerDamage,
                    iPlayerElectricProjectile = iplayerElectricProjectile,
                    iPlayerEnergy = iplayerEnergy,
                    iPlayerEnergyProjectile = iplayerEnergyProjectile,
                    iPlayerExplosiveProjectile = iplayerExplosiveProjectile,
                    iPlayerFireProjectile = iplayerFireProjectile,
                    iPlayerFireRate = iplayerFireRate,
                    iPlayerHealingSpecial = iplayerHealingSpecial,
                    iPlayerHealth = iplayerHealth,
                    iPlayerHealthProjectile = iplayerHealthProjectile,
                    iPlayerLaserProjectile = iplayerLaserProjectile,
                    iPlayerLaserSpecial = iplayerLaserSpecial,
                    iPlayerMoneySpecial = iplayerMoneySpecial,
                    iPlayerMovementSpeed = iplayerMovementSpeed,
                    iPlayerPoisonProjectile = iplayerPoisonProjectile,
                    iPlayerShieldSpecial = iplayerShieldSpecial,
                    iPlayerSlowProjectile = iplayerSlowProjectile,
                    iPlayerTimeStopSpecial = iplayerTimeStopSpecial,

                    bPlayerQuadShot = bplayerQuadShot,
                    bPlayerQuintupleShot = bplayerQuintupleShot,
                    bPlayerTripleShot = bplayerTripleShot,
                    bPlayerDoubleShot = bplayerDoubleShot,
                    bPlayerExtraLife1 = bplayerExtraLife1,
                    bPlayerExtraLife2 = bplayerExtraLife2,
                    bPlayerExtraLife3 = bplayerExtraLife3,
                    bPlayerExtraLife4 = bplayerExtraLife4,

                    PlayerLevel = playerLevel,
                    PlayerDeathCount = playerDeathCount,
                    PlayerTimePlayedHours = playerTimePlayedHours,
                    PlayerTimePlayedMinutes = playerTimePlayedMinutes,
                    PlayerTimePlayedSeconds = playerTimePlayedSeconds,
                    PlayerCreditsCollected = playerCreditsCollected,
                    PlayerCreditsSpent = playerCreditsSpent,
                    PlayerWeaponsCollected = playerWeaponsCollected,
                    PlayerPercentageComplete = playerPercentageComplete,
                    PlayerBulletsFired = playerBulletsFired,
                    PlayerAccuracy = playerAccuracy,
                    PlayerEnemiesKilled = playerEnemiesKilled,
                    PlayerEnemiesHit = playerEnemiesHit,
                    PlayerMiniGamesPassed = playerMiniGamesPassed,
                    PlayerUpgradesPurchased = playerUpgradesPurchased,
                    PlayerPowerUpsCollected = playerPowerUpsCollected,
                    PlayerLevelsCompleted = playerLevelsCompleted,

                    PlayerAchievement1 = playerAchievement1,
                    PlayerAchievement2 = playerAchievement2,
                    PlayerAchievement3 = playerAchievement3,
                    PlayerAchievement4 = playerAchievement4,
                    PlayerAchievement5 = playerAchievement5,
                    PlayerAchievement6 = playerAchievement6,
                    PlayerAchievement7 = playerAchievement7,
                    PlayerAchievement8 = playerAchievement8,
                    PlayerAchievement9 = playerAchievement9,
                    PlayerAchievement10 = playerAchievement10,
                    PlayerAchievement11 = playerAchievement11,
                    PlayerAchievement12 = playerAchievement12,
                    PlayerAchievement13 = playerAchievement13,
                    PlayerAchievement14 = playerAchievement14,
                    PlayerAchievement15 = playerAchievement15,
                    PlayerAchievement16 = playerAchievement16,
                    PlayerAchievement17 = playerAchievement17,
                    PlayerAchievement18 = playerAchievement18,
                    PlayerAchievement19 = playerAchievement19,
                    PlayerAchievement20 = playerAchievement20,
                    PlayerAchievement21 = playerAchievement21,
                    PlayerAchievement22 = playerAchievement22,
                    PlayerAchievement23 = playerAchievement23,
                    PlayerAchievement24 = playerAchievement24,
                    PlayerAchievement25 = playerAchievement25,
                    PlayerAchievement26 = playerAchievement26,
                    PlayerAchievement27 = playerAchievement27,
                    PlayerAchievement28 = playerAchievement28,
                    PlayerAchievement29 = playerAchievement29,
                    PlayerAchievement30 = playerAchievement30,
                    PlayerAchievement31 = playerAchievement31,
                    PlayerAchievement32 = playerAchievement32,
                    PlayerAchievement33 = playerAchievement33,
                    PlayerAchievement34 = playerAchievement34,
                    PlayerAchievement35 = playerAchievement35,
                    PlayerAchievement36 = playerAchievement36,
                    PlayerAchievement37 = playerAchievement37,
                    PlayerAchievement38 = playerAchievement38,
                    PlayerAchievement39 = playerAchievement39,
                    PlayerAchievement40 = playerAchievement40,
                    PlayerAchievement41 = playerAchievement41,
                    PlayerAchievement42 = playerAchievement42,
                    PlayerAchievement43 = playerAchievement43,
                    PlayerAchievement44 = playerAchievement44,
                    PlayerAchievement45 = playerAchievement45,
                    PlayerAchievement46 = playerAchievement46,
                    PlayerAchievement47 = playerAchievement47,
                    PlayerAchievement48 = playerAchievement48,
                    PlayerAchievement49 = playerAchievement49,
                    PlayerAchievement50 = playerAchievement50,
                    PlayerAchievementCount = playerAchievementCount,
                    PlayerSelectedWeapon1 = playerSelectedWeapon1,
                    PlayerSelectedWeapon2 = playerSelectedWeapon2,
                    PlayerSelectedWeapon3 = playerSelectedWeapon3,
                    PlayerSelectedWeapon4 = playerSelectedWeapon4,
                    PlayerSelectedWeapon5 = playerSelectedWeapon5,
                    PlayerSelectedSpecial = playerSelectedSpecial,
                    PlayerShipsUnlocked = playerShipsUnlocked,
                    PlayerXP = playerXP,
                    PlayerLevelNumber = playerLevelNumber,
                };

                IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
                result.AsyncWaitHandle.WaitOne();
                StorageContainer container = device.EndOpenContainer(r);
                if (container.FileExists(fileName))
                    container.DeleteFile(fileName);
                Stream stream = container.CreateFile(fileName);
                XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
                serializer.Serialize(stream, SaveData);
                stream.Close();
                container.Dispose();
                result.AsyncWaitHandle.Close();
            }
        }

        public void InitiateLoad()
        {
            if (LoadSave)
                fileName = playerName + ".sav";
            //if (!Guide.IsVisible)
            {
                device = null;
                StorageDevice.BeginShowSelector(PlayerIndex.One, this.LoadFromDevice, null);
            }
        }
        string loadedString;
        void LoadFromDevice(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(r);
            result.AsyncWaitHandle.Close();

            string[] FileList = container.GetFileNames();
            SaveFiles.RemoveRange(0, SaveFiles.Count);
            foreach (string filename in FileList)
            {
                loadedString = filename.Remove(filename.Length - 4, 4);
                SaveFiles.Add(loadedString);
            }
            if (LoadSave)
                if (container.FileExists(fileName))
                {
                    Stream stream = container.OpenFile(fileName, FileMode.Open);
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveGame));
                    SaveGame SaveData = (SaveGame)serializer.Deserialize(stream);
                    stream.Close();
                    container.Dispose();
                    //Update the game based on the save game file


                    playerName = SaveData.PlayerName;
                    playerAcceleration = SaveData.PlayerAcceleration;
                    playerBulletSpeed = SaveData.PlayerBulletSpeed;
                    playerShip = SaveData.PlayerShip;
                    playerMaxBullets = SaveData.PlayerMaxBullets;
                    playerDamage = SaveData.PlayerDamage;
                    playerRedValue = SaveData.PlayerRedValue;
                    playerBlueValue = SaveData.PlayerBlueValue;
                    playerGreenValue = SaveData.PlayerGreenValue;
                    playerFireRate = SaveData.PlayerFireRate;
                    playerCredits = SaveData.PlayerCredits;
                    playerLives = SaveData.PlayerLives;
                    playerMaxHealth = SaveData.PlayerMaxHealth;
                    playerMaxEnergy = SaveData.PlayerMaxEnergy;

                    iplayerAmmo = SaveData.iPlayerAmmo;
                    bplayerAutoFire = SaveData.bPlayerAutoFire;
                    iplayerBulletSpeed = SaveData.iPlayerBulletSpeed;
                    iplayerDamage = SaveData.iPlayerDamage;
                    iplayerElectricProjectile = SaveData.iPlayerElectricProjectile;
                    iplayerEnergy = SaveData.iPlayerEnergy;
                    iplayerEnergyProjectile = SaveData.iPlayerEnergyProjectile;
                    iplayerExplosiveProjectile = SaveData.iPlayerExplosiveProjectile;
                    iplayerFireProjectile = SaveData.iPlayerFireProjectile;
                    iplayerFireRate = SaveData.iPlayerFireRate;
                    iplayerHealingSpecial = SaveData.iPlayerHealingSpecial;
                    iplayerHealth = SaveData.iPlayerHealth;
                    iplayerHealthProjectile = SaveData.iPlayerHealthProjectile;
                    iplayerLaserProjectile = SaveData.iPlayerLaserProjectile;
                    iplayerLaserSpecial = SaveData.iPlayerLaserSpecial;
                    iplayerMoneySpecial = SaveData.iPlayerMoneySpecial;
                    iplayerMovementSpeed = SaveData.iPlayerMovementSpeed;
                    iplayerPoisonProjectile = SaveData.iPlayerPoisonProjectile;
                    iplayerShieldSpecial = SaveData.iPlayerShieldSpecial;
                    iplayerSlowProjectile = SaveData.iPlayerSlowProjectile;
                    iplayerTimeStopSpecial = SaveData.iPlayerTimeStopSpecial;

                    bplayerQuadShot = SaveData.bPlayerQuadShot;
                    bplayerQuintupleShot = SaveData.bPlayerQuintupleShot;
                    bplayerTripleShot = SaveData.bPlayerTripleShot;
                    bplayerDoubleShot = SaveData.bPlayerDoubleShot;
                    bplayerExtraLife1 = SaveData.bPlayerExtraLife1;
                    bplayerExtraLife2 = SaveData.bPlayerExtraLife2;
                    bplayerExtraLife3 = SaveData.bPlayerExtraLife3;
                    bplayerExtraLife4 = SaveData.bPlayerExtraLife4;

                    playerLevel = SaveData.PlayerLevel;
                    playerDeathCount = SaveData.PlayerDeathCount;
                    playerTimePlayedHours = SaveData.PlayerTimePlayedHours;
                    playerTimePlayedMinutes = SaveData.PlayerTimePlayedMinutes;
                    playerTimePlayedSeconds = SaveData.PlayerTimePlayedSeconds;
                    playerCreditsCollected = SaveData.PlayerCreditsCollected;
                    playerCreditsSpent = SaveData.PlayerCreditsSpent;
                    playerWeaponsCollected = SaveData.PlayerWeaponsCollected;
                    playerPercentageComplete = SaveData.PlayerPercentageComplete;
                    playerBulletsFired = SaveData.PlayerBulletsFired;
                    playerAccuracy = SaveData.PlayerAccuracy;
                    playerEnemiesKilled = SaveData.PlayerEnemiesKilled;
                    playerEnemiesHit = SaveData.PlayerEnemiesHit;
                    playerMiniGamesPassed = SaveData.PlayerMiniGamesPassed;
                    playerUpgradesPurchased = SaveData.PlayerUpgradesPurchased;
                    playerPowerUpsCollected = SaveData.PlayerPowerUpsCollected;
                    playerLevelsCompleted = SaveData.PlayerLevelsCompleted;

                    playerAchievement1 = SaveData.PlayerAchievement1;
                    playerAchievement2 = SaveData.PlayerAchievement2;
                    playerAchievement3 = SaveData.PlayerAchievement3;
                    playerAchievement4 = SaveData.PlayerAchievement4;
                    playerAchievement5 = SaveData.PlayerAchievement5;
                    playerAchievement6 = SaveData.PlayerAchievement6;
                    playerAchievement7 = SaveData.PlayerAchievement7;
                    playerAchievement8 = SaveData.PlayerAchievement8;
                    playerAchievement9 = SaveData.PlayerAchievement9;
                    playerAchievement10 = SaveData.PlayerAchievement10;
                    playerAchievement11 = SaveData.PlayerAchievement11;
                    playerAchievement12 = SaveData.PlayerAchievement12;
                    playerAchievement13 = SaveData.PlayerAchievement13;
                    playerAchievement14 = SaveData.PlayerAchievement14;
                    playerAchievement15 = SaveData.PlayerAchievement15;
                    playerAchievement16 = SaveData.PlayerAchievement16;
                    playerAchievement17 = SaveData.PlayerAchievement17;
                    playerAchievement18 = SaveData.PlayerAchievement18;
                    playerAchievement19 = SaveData.PlayerAchievement19;
                    playerAchievement20 = SaveData.PlayerAchievement20;
                    playerAchievement21 = SaveData.PlayerAchievement21;
                    playerAchievement22 = SaveData.PlayerAchievement22;
                    playerAchievement23 = SaveData.PlayerAchievement23;
                    playerAchievement24 = SaveData.PlayerAchievement24;
                    playerAchievement25 = SaveData.PlayerAchievement25;
                    playerAchievement26 = SaveData.PlayerAchievement26;
                    playerAchievement27 = SaveData.PlayerAchievement27;
                    playerAchievement28 = SaveData.PlayerAchievement28;
                    playerAchievement29 = SaveData.PlayerAchievement29;
                    playerAchievement30 = SaveData.PlayerAchievement30;
                    playerAchievement31 = SaveData.PlayerAchievement31;
                    playerAchievement32 = SaveData.PlayerAchievement32;
                    playerAchievement33 = SaveData.PlayerAchievement33;
                    playerAchievement34 = SaveData.PlayerAchievement34;
                    playerAchievement35 = SaveData.PlayerAchievement35;
                    playerAchievement36 = SaveData.PlayerAchievement36;
                    playerAchievement37 = SaveData.PlayerAchievement37;
                    playerAchievement38 = SaveData.PlayerAchievement38;
                    playerAchievement39 = SaveData.PlayerAchievement39;
                    playerAchievement40 = SaveData.PlayerAchievement40;
                    playerAchievement41 = SaveData.PlayerAchievement41;
                    playerAchievement42 = SaveData.PlayerAchievement42;
                    playerAchievement43 = SaveData.PlayerAchievement43;
                    playerAchievement44 = SaveData.PlayerAchievement44;
                    playerAchievement45 = SaveData.PlayerAchievement45;
                    playerAchievement46 = SaveData.PlayerAchievement46;
                    playerAchievement47 = SaveData.PlayerAchievement47;
                    playerAchievement48 = SaveData.PlayerAchievement48;
                    playerAchievement49 = SaveData.PlayerAchievement49;
                    playerAchievement50 = SaveData.PlayerAchievement50;
                    playerAchievementCount = SaveData.PlayerAchievementCount;
                    playerSelectedWeapon1 = SaveData.PlayerSelectedWeapon1;
                    playerSelectedWeapon2 = SaveData.PlayerSelectedWeapon2;
                    playerSelectedWeapon3 = SaveData.PlayerSelectedWeapon3;
                    playerSelectedWeapon4 = SaveData.PlayerSelectedWeapon4;
                    playerSelectedWeapon5 = SaveData.PlayerSelectedWeapon5;
                    playerSelectedSpecial = SaveData.PlayerSelectedSpecial;
                    playerShipsUnlocked = SaveData.PlayerShipsUnlocked;
                    playerXP = SaveData.PlayerXP;
                    playerLevelNumber = SaveData.PlayerLevelNumber;
                }

            LoadSave = false;
        }
    }
}
