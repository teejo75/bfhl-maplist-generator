using System;
using System.Collections.Generic;

namespace BFHLMapListGenerator
{
    [Flags]
    public enum GameTypes : long
    {
        /// <summary>
        /// A list of game types that a map can belong to.
        /// This is used as a flag during map list population.
        /// </summary>
        NONE = 0,

        /*
        TurfWarLarge0 // Conquest Large
        TurfWarSmall0 // Conquest Small
        Hostage0 // Rescue
        Hit0 // Crosshair
        Heist0 // Heist
        TeamDeathMatch0 // Team Deathmatch
        Hotwire0 // Hotwire
        BloodMoney0 // Blood Money
        */

        TURFWARLARGE0 = 2,
        TURFWARSMALL0 = 4,
        HOSTAGE0 = 8,
        HIT0 = 16,
        TEAMDEATHMATCH0 = 32,
        HEIST0 = 64,
        HOTWIRE0 = 128,
        BLOODMONEY0 = 256,
        CASHGRAB0 = 512,
        SQUADHEIST0 = 1024,
        //AIRSUPERIORITY0 = 512, // Up to 24 players
        //CAPTURETHEFLAG0 = 1024, // Up to 32 players
        //CARRIERASSAULTLARGE0 = 2048, // Up to 64 players (Guessing here)
        //CARRIERASSAULTSMALL0 = 4096, // Up to 32 players (Guess)
        //CHAINLINK0 = 8192, // 32 Players (Guess)

        // Compound enums
        BASE = TURFWARLARGE0 | TURFWARSMALL0 | HOSTAGE0 | HIT0 | TEAMDEATHMATCH0 | HEIST0 | BLOODMONEY0,
        BASEPLUS = BASE | HOTWIRE0,
        EXPANSION1 = BASE | CASHGRAB0,
        EXPANSION2 = BASE | SQUADHEIST0

        //EXPANSION1 = BASE | AIRSUPERIORITY0,
        //EXPANSION2 = BASE | CAPTURETHEFLAG0,
        //EXPANSION3 = BASE | CARRIERASSAULTLARGE0 | CARRIERASSAULTSMALL0,
        //EXPANSION4 = BASE | CAPTURETHEFLAG0 | CHAINLINK0,
        //EXPANSION5 = EXPANSION2, // Stupid, but should be less confusing if someone else reads the code.
    }

    public class BFHLMap : ICloneable
    {
        /// <summary>
        /// A datastructure describing a BFHL Map
        /// </summary>
        public string InternalName;

        public string FriendlyName;

        // public string XPackName; // Expansion Pack Name
        public GameTypes GameTypeList = GameTypes.NONE;

        public int XPack;
        public BFHLGameType IntendedGameType = new BFHLGameType();

        // Default constructor
        public BFHLMap()
        {
        }

        public BFHLMap(string iname, string fname, int xp, GameTypes gts)
        {
            InternalName = iname;
            FriendlyName = fname;
            XPack = xp;
            GameTypeList = gts;
        }

        /// <summary>
        /// Clones the object to prevent the Program.BFHLMaps reference from being updated
        /// when generating map lists for each game type.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            BFHLMap b = new BFHLMap();
            b.InternalName = this.InternalName;
            b.FriendlyName = this.FriendlyName;
            b.GameTypeList = this.GameTypeList;
            b.IntendedGameType = this.IntendedGameType;
            return b;
        }
    }

    public class BFHLGameType
    {
        /// <summary>
        /// A data structure describing a BFHL Game Type
        /// </summary>
        public string InternalName; // Internal game type name

        public string FriendlyName;
        public int NumPlayers = -1; // Number of players supported by game type - THIS PROPERTY IS NOT USED RIGHT NOW.
        public GameTypes GameType = GameTypes.NONE;

        // Default constructor
        public BFHLGameType()
        {
        }

        public BFHLGameType(string iname, string fname, int nump, GameTypes gt)
        {
            InternalName = iname;
            FriendlyName = fname;
            NumPlayers = nump;
            GameType = gt;
        }
    }

    public class BFHLMapListPerGameType
    {
        /// <summary>
        /// This class describes a map list per game type pattern.
        /// It is populated from the pattern listbox lbGameTypesPattern on MainForm.
        /// </summary>
        ///
        /// This class is instantiated via the MainForm.GenerateList() function.
        public GameTypes GameTypeEnum = GameTypes.NONE; // Gametype of the pattern entry

        public List<BFHLMap> Maps = new List<BFHLMap>();
        public decimal Rounds = -1;
        public List<BFHLMap> UsedMaps = new List<BFHLMap>();
        public int UsedMapsCurrentIndex = 0;

        // Default constructor
        public BFHLMapListPerGameType()
        {
        }

        public BFHLMapListPerGameType(GameTypes gt, decimal r)
        {
            GameTypeEnum = gt;
            Rounds = r;
        }
    }
}