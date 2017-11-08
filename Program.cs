using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BFHLMapListGenerator
{
    public static class Program
    {
        #region PROGRAM_STARTUP_CODE

        /// <summary>
        /// Populates a list of class BFHLGameType with game type data
        /// </summary>
        ///
        private const int XPACK_VANILLA = 0, XPACK_1 = 1, XPACK_2 = 2; //, XPACK_3 = 3, XPACK_4 = 4, XPACK_5 = 5;

        public static string[] BFHLXPack = { "Vanilla", "Criminal Activity", "Robbery" }; //, "China Rising", "Second Assault", "Naval Strike", "Dragon's Teeth", "Final Stand" };

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

        public static List<BFHLGameType> BFHLGameTypes = new List<BFHLGameType>() {
            new BFHLGameType( "TurfWarLarge0", "Conquest Large", 64, GameTypes.TURFWARLARGE0),
            new BFHLGameType( "TurfWarSmall0", "Conquest Small", 32, GameTypes.TURFWARSMALL0),
            new BFHLGameType( "Hostage0", "Rescue", 32, GameTypes.HOSTAGE0),
            new BFHLGameType( "Hit0", "Crosshair", 32, GameTypes.HIT0),
            new BFHLGameType( "Heist0", "Heist", 32, GameTypes.HEIST0),
            new BFHLGameType( "TeamDeathMatch0", "Team Deathmatch", 32, GameTypes.TEAMDEATHMATCH0),
            new BFHLGameType( "Hotwire0", "Hotwire", 32, GameTypes.HOTWIRE0),
            new BFHLGameType( "BloodMoney0", "Blood Money", 32, GameTypes.BLOODMONEY0),
            new BFHLGameType( "CashGrab0", "Cash Grab", 32, GameTypes.CASHGRAB0),
            new BFHLGameType( "SquadHeist0", "Squad Heist", 16, GameTypes.SQUADHEIST0)
            //new BFHLGameType( "ConquestLarge0","Conquest Large",64, GameTypes.CONQUESTLARGE0 ),
            //new BFHLGameType( "ConquestSmall0","Conquest Small",32, GameTypes.CONQUESTSMALL0 ),
            //new BFHLGameType( "RushLarge0","Rush",32, GameTypes.RUSHLARGE0 ),
            //new BFHLGameType( "SquadDeathMatch0","Squad Deathmatch",16, GameTypes.SQUADDEATHMATCH0 ),
            //new BFHLGameType( "TeamDeathMatch0","Team Deathmatch",24, GameTypes.TEAMDEATHMATCH0 ),
            //new BFHLGameType( "Domination0","Domination",20,GameTypes.DOMINATION0 ),
            //new BFHLGameType( "AirSuperiority0", "Air Superiority",24,GameTypes.AIRSUPERIORITY0 ),
            //new BFHLGameType( "CaptureTheFlag0", "Capture The Flag",32,GameTypes.CAPTURETHEFLAG0 ),
            //new BFHLGameType( "Elimination0", "Defuse", 10,GameTypes.ELIMINATION0 ),
            //new BFHLGameType( "Obliteration", "Obliteration", 32, GameTypes.OBLITERATION),
            //new BFHLGameType( "CarrierAssaultLarge0", "Carrier Assault Large", 64, GameTypes.CARRIERASSAULTLARGE0),
            //new BFHLGameType( "CarrierAssaultSmall0", "Carrier Assault Small", 32, GameTypes.CARRIERASSAULTSMALL0),
            //new BFHLGameType( "Chainlink0", "Chain Link", 32, GameTypes.CHAINLINK0),
            };

        /// <summary>
        /// Populates a list of class BFHLMap with map data
        /// </summary>
        public static List<BFHLMap> BFHLMaps = new List<BFHLMap>()
        {
            // Constructor Internal Name, Friendly Name, Expansion Pack, Game Types enum
            new BFHLMap( "MP_Downtown","Downtown",XPACK_VANILLA,GameTypes.BASEPLUS),
            new BFHLMap( "MP_Bloodout","The Block",XPACK_VANILLA,GameTypes.BASE),
            new BFHLMap( "MP_Eastside","Derailed",XPACK_VANILLA,GameTypes.BASEPLUS),
            new BFHLMap( "MP_Desert05","Dust Bowl",XPACK_VANILLA,GameTypes.BASEPLUS),
            new BFHLMap( "MP_Bank","Bank Job",XPACK_VANILLA,GameTypes.BASE),
            new BFHLMap( "MP_Growhouse","Growhouse",XPACK_VANILLA,GameTypes.BASE),
            new BFHLMap( "MP_OffShore","Riptide",XPACK_VANILLA,GameTypes.BASEPLUS),
            new BFHLMap( "MP_Hills","Hollywood Heights",XPACK_VANILLA,GameTypes.BASE),
            new BFHLMap( "MP_Glades","Everglades",XPACK_VANILLA,GameTypes.BASEPLUS),
            new BFHLMap( "XP1_MallCops","Black Friday",XPACK_1,GameTypes.EXPANSION1),
            new BFHLMap( "XP1_Nights", "Code Blue",XPACK_1,GameTypes.EXPANSION1 | GameTypes.HOTWIRE0),
            new BFHLMap( "XP1_Projects", "The Beat",XPACK_1,GameTypes.EXPANSION1),
            new BFHLMap( "XP1_Sawmill", "Backwoods",XPACK_1,GameTypes.EXPANSION1),
            new BFHLMap( "xp2_cargoship", "The Docks", XPACK_2, GameTypes.EXPANSION2 | GameTypes.HOTWIRE0),
            new BFHLMap( "xp2_coastal", "Break Point", XPACK_2, GameTypes.EXPANSION2 | GameTypes.HOTWIRE0),
            new BFHLMap( "xp2_museum02", "Museum", XPACK_2, GameTypes.EXPANSION2),
            new BFHLMap( "xp2_precinct7", "Precinct 7", XPACK_2, GameTypes.EXPANSION2 | GameTypes.HOTWIRE0)


/*

							twl0	tws0	hstg0	hit0	hst0	tdm0	hw0	bm0	cg0
XP1_MallCops "Black Friday"	x		x		x		x		x		x			x	x
XP1_Nights "Code Blue"		x		x		x		x		x		x		x	x	x
XP1_Projects "The Beat"		x		x		x		x		x		x			x	x
XP1_Sawmill "Backwoods"		x		x		x		x		x		x			x	x

							twl0	tws0	hstg0	hit0	hst0	tdm0	hw0	bm0	cg0 sh0
xp2_cargoship "The Docks"	x		x		x		x		x		x		x	x	x	x
xp2_coastal "Break Point"	x		x		x		x		x		x		x	x	x	x
xp2_museum02 "Museum"		x		x		x		x		x		x			x	x	x
xp2_precinct7 "Precinct 7"	x		x		x		x		x		x		x	x	x	x

            */



        };

        public static BFHLMap SelectMap(List<BFHLMap> maps, int rndindex)
        {
            BFHLMap selectedMap = null;
            return selectedMap = maps[rndindex];
        }

        #endregion PROGRAM_STARTUP_CODE

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}