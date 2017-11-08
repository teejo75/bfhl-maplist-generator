using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BFHLMapListGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Instantiate the class for setting persistence
        private AppSettings appsettings = new AppSettings();

        private int OutH;
        private int OutW;

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Specify the settings to save
            Dictionary<string, bool> tempdict = new Dictionary<string, bool>();
            appsettings.InfiniteChecked = cbRepeatAll.Checked;
            appsettings.RepeatsValue = nmRepeats.Value;
            appsettings.RepeatsEnabled = nmRepeats.Enabled;
            appsettings.RoundsValue = nmRounds.Value;
            appsettings.lvmapsCheckedItems = new ArrayList(lvMaps.CheckedItems);
            appsettings.lvmapsItems = new ArrayList(lvMaps.Items);
            appsettings.PatternItems = new ArrayList(lbGameTypesPattern.Items);
            foreach (ToolStripMenuItem item in mapToolStripMenuItem.DropDownItems)
            {
                tempdict.Add(item.Name, item.Checked);
            }
            appsettings.dropDownItems = tempdict;
            appsettings.OutputSizeH = OutH;
            appsettings.OutputSizeW = OutW;
            // Save to file
            appsettings.SaveAppSettings();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Form startup code
            // Set the form title to the product name (Set in Application properties)
            this.Text = Application.ProductName;
            // Initialize form controls
            // Populate Map Selection menu
            foreach (string xpItem in Program.BFHLXPack)
            {
                mapToolStripMenuItem.DropDownItems.Add(xpItem);
            }
            foreach (ToolStripMenuItem mItem in mapToolStripMenuItem.DropDownItems)
            {
                mItem.Checked = true;
                mItem.CheckOnClick = false;
                mItem.Name = mItem.Text;
                mItem.Click += new EventHandler(mItem_ChangeSelection);
            }
            // Load game types into the game types control
            foreach (BFHLGameType gameType in Program.BFHLGameTypes)
            {
                lbGameTypes.Items.Add(gameType.FriendlyName);
            }
            // Add the columns to the lvMaps control (Not visible at runtime)
            lvMaps.Columns.Add("Map", lvMaps.Width - 25, HorizontalAlignment.Left);
            // Populate the lvMaps control from the class created and populated in Program.cs
            // This causes the listview to not update, prevents flashing when loading data.
            // Not really needed since we're not loading tons of data here.
            lvMaps.BeginUpdate();
            // Load stored settings, if any
            if (this.appsettings.LoadAppSettings())
            {
                this.lvMaps.Items.AddRange((ListViewItem[])appsettings.lvmapsItems.ToArray(typeof(ListViewItem)));
                Dictionary<string, bool> tempdict = new Dictionary<string, bool>();
                tempdict = appsettings.dropDownItems;
                foreach (var entry in tempdict)
                {
                    ((ToolStripMenuItem)mapToolStripMenuItem.DropDownItems[entry.Key]).Checked = entry.Value;
                }
                this.lbGameTypesPattern.Items.AddRange(appsettings.PatternItems.ToArray());
                this.nmRepeats.Value = appsettings.RepeatsValue;
                this.nmRepeats.Enabled = appsettings.RepeatsEnabled;
                this.nmRounds.Value = appsettings.RoundsValue;
                this.cbRepeatAll.Checked = appsettings.InfiniteChecked;
                this.cbDontRepeat.Checked = appsettings.DontRepeatChecked;
                OutH = appsettings.OutputSizeH;
                OutW = appsettings.OutputSizeW;
            }
            else
            {
                // No settings found, set defaults.
                foreach (BFHLMap gameMap in Program.BFHLMaps)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = gameMap.FriendlyName;
                    item.Checked = true;
                    lvMaps.Items.Add(item);
                }
                cbRepeatAll.Checked = true;
            }
            // Let the listview refresh
            lvMaps.EndUpdate();
        }

        // Changes map selection based on what was clicked.
        private void mItem_ChangeSelection(object sender, EventArgs e)
        {
            var mItem = sender as ToolStripMenuItem;
            // Expansion Pack index = mapToolStripMenuItem.DropDownItems.IndexOf(mitem)
            foreach (ListViewItem item in lvMaps.Items)
            {
                int mIndex = Program.BFHLMaps.FindIndex(mp => mp.FriendlyName.Equals(item.Text));
                if (mItem.Text == Program.BFHLXPack[Program.BFHLMaps[mIndex].XPack])
                {
                    if (item.Checked)
                    {
                        item.Checked = false;
                    }
                    else item.Checked = true;
                }
            }
            if (mItem.Checked)
            {
                mItem.Checked = false;
            }
            else mItem.Checked = true;
        }

        #region MENU_PROCESSING_CODE

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Set all items checked
            for (int i = 0; i < lvMaps.Items.Count; i++)
            {
                lvMaps.Items[i].Checked = true;
            }

            foreach (ToolStripMenuItem mItem in mapToolStripMenuItem.DropDownItems)
            {
                mItem.Checked = true;
            }
        }

        private void clearSelectionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Clear all checked items
            for (int i = 0; i < lvMaps.Items.Count; i++)
            {
                lvMaps.Items[i].Checked = false;
            }
            foreach (ToolStripMenuItem mItem in mapToolStripMenuItem.DropDownItems)
            {
                mItem.Checked = false;
            }
        }

        private void maplisttxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateList(true);
        }

        private void cleanMapListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateList(false);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }

        #endregion MENU_PROCESSING_CODE

        #region BUTTON_PROCESSING_CODE

        private void btnClear_Click(object sender, EventArgs e)
        {
            lbGameTypesPattern.Items.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            lbGameTypesPattern.Items.Add(Program.BFHLGameTypes[lbGameTypes.SelectedIndex].FriendlyName + "," + nmRounds.Value.ToString());
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lbGameTypesPattern.Items.Remove(lbGameTypesPattern.SelectedItem);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lbGameTypesPattern.SelectedItems.Count > 0)
            {
                var selectedIndex = lbGameTypesPattern.SelectedIndex;
                if (selectedIndex > 0)
                {
                    var ItemToMoveUp = lbGameTypesPattern.Items[selectedIndex];
                    lbGameTypesPattern.Items.RemoveAt(selectedIndex);
                    lbGameTypesPattern.Items.Insert(selectedIndex - 1, ItemToMoveUp);
                    lbGameTypesPattern.SelectedIndex = selectedIndex - 1;
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lbGameTypesPattern.SelectedItems.Count > 0)
            {
                var selectedIndex = lbGameTypesPattern.SelectedIndex;
                if (selectedIndex + 1 < lbGameTypesPattern.Items.Count)
                {
                    var itemToMoveDown = lbGameTypesPattern.Items[selectedIndex];
                    lbGameTypesPattern.Items.RemoveAt(selectedIndex);
                    lbGameTypesPattern.Items.Insert(selectedIndex + 1, itemToMoveDown);
                    lbGameTypesPattern.SelectedIndex = selectedIndex + 1;
                }
            }
        }

        #endregion BUTTON_PROCESSING_CODE

        private void GenerateList(bool Comments)
        {
            int totalMapCount = 0;
            string outputTxt = "";
            Random _rnd = new Random();
            if (lbGameTypesPattern.Items.Count > 0)
            {
                #region LISTBOX_PROCESSING

                List<BFHLMapListPerGameType> BFHLMapList = new List<BFHLMapListPerGameType>();
                // Process the patterns
                foreach (string item in lbGameTypesPattern.Items)
                {
                    // Split the contents of the pattern box into the friendly name and the rounds
                    string[] itemSplit = item.Split(',');
                    // Get the Internal game type name
                    int index = Program.BFHLGameTypes.FindIndex(gt => gt.FriendlyName.Equals(itemSplit[0]));
                    BFHLMapList.Add(new BFHLMapListPerGameType(Program.BFHLGameTypes[index].GameType, Convert.ToDecimal(itemSplit[1])));
                }

                #endregion LISTBOX_PROCESSING

                #region GAMETYPE_PROCESSING

                // This segment goes through each BFHLMapListPerGameType entry and generates a map list for it.
                foreach (BFHLMapListPerGameType mlpgt in BFHLMapList)
                {
                    foreach (ListViewItem item in lvMaps.CheckedItems)
                    {
                        int mIndex = Program.BFHLMaps.FindIndex(mp => mp.FriendlyName.Equals(item.Text));
                        switch (mlpgt.GameTypeEnum)
                        {
                            case GameTypes.TURFWARLARGE0:
                                if ((Program.BFHLMaps[mIndex].GameTypeList & GameTypes.TURFWARLARGE0) == GameTypes.TURFWARLARGE0)
                                {
                                    mlpgt.Maps.Add((BFHLMap)Program.BFHLMaps[mIndex].Clone());
                                    mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.BFHLGameTypes[Program.BFHLGameTypes.FindIndex(id => id.InternalName.Equals("TurfWarLarge0"))];
                                }
                                break;

                            case GameTypes.TURFWARSMALL0:
                                if ((Program.BFHLMaps[mIndex].GameTypeList & GameTypes.TURFWARSMALL0) == GameTypes.TURFWARSMALL0)
                                {
                                    mlpgt.Maps.Add((BFHLMap)Program.BFHLMaps[mIndex].Clone());
                                    mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.BFHLGameTypes[Program.BFHLGameTypes.FindIndex(id => id.InternalName.Equals("TurfWarSmall0"))];
                                }
                                break;

                            case GameTypes.HOSTAGE0:
                                if ((Program.BFHLMaps[mIndex].GameTypeList & GameTypes.HOSTAGE0) == GameTypes.HOSTAGE0)
                                {
                                    mlpgt.Maps.Add((BFHLMap)Program.BFHLMaps[mIndex].Clone());
                                    mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.BFHLGameTypes[Program.BFHLGameTypes.FindIndex(id => id.InternalName.Equals("Hostage0"))];
                                }
                                break;

                            case GameTypes.HIT0:
                                if ((Program.BFHLMaps[mIndex].GameTypeList & GameTypes.HIT0) == GameTypes.HIT0)
                                {
                                    mlpgt.Maps.Add((BFHLMap)Program.BFHLMaps[mIndex].Clone());
                                    mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.BFHLGameTypes[Program.BFHLGameTypes.FindIndex(id => id.InternalName.Equals("Hit0"))];
                                }
                                break;

                            case GameTypes.TEAMDEATHMATCH0:
                                if ((Program.BFHLMaps[mIndex].GameTypeList & GameTypes.TEAMDEATHMATCH0) == GameTypes.TEAMDEATHMATCH0)
                                {
                                    mlpgt.Maps.Add((BFHLMap)Program.BFHLMaps[mIndex].Clone());
                                    mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.BFHLGameTypes[Program.BFHLGameTypes.FindIndex(id => id.InternalName.Equals("TeamDeathMatch0"))];
                                }
                                break;

                            case GameTypes.HEIST0:
                                if ((Program.BFHLMaps[mIndex].GameTypeList & GameTypes.HEIST0) == GameTypes.HEIST0)
                                {
                                    mlpgt.Maps.Add((BFHLMap)Program.BFHLMaps[mIndex].Clone());
                                    mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.BFHLGameTypes[Program.BFHLGameTypes.FindIndex(id => id.InternalName.Equals("Heist0"))];
                                }
                                break;

                            case GameTypes.HOTWIRE0:
                                if ((Program.BFHLMaps[mIndex].GameTypeList & GameTypes.HOTWIRE0) == GameTypes.HOTWIRE0)
                                {
                                    mlpgt.Maps.Add((BFHLMap)Program.BFHLMaps[mIndex].Clone());
                                    mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.BFHLGameTypes[Program.BFHLGameTypes.FindIndex(id => id.InternalName.Equals("Hotwire0"))];
                                }
                                break;

                            case GameTypes.BLOODMONEY0:
                                if ((Program.BFHLMaps[mIndex].GameTypeList & GameTypes.BLOODMONEY0) == GameTypes.BLOODMONEY0)
                                {
                                    mlpgt.Maps.Add((BFHLMap)Program.BFHLMaps[mIndex].Clone());
                                    mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.BFHLGameTypes[Program.BFHLGameTypes.FindIndex(id => id.InternalName.Equals("BloodMoney0"))];
                                }
                                break;

                            case GameTypes.CASHGRAB0:
                                if ((Program.BFHLMaps[mIndex].GameTypeList & GameTypes.CASHGRAB0) == GameTypes.CASHGRAB0)
                                {
                                    mlpgt.Maps.Add((BFHLMap)Program.BFHLMaps[mIndex].Clone());
                                    mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.BFHLGameTypes[Program.BFHLGameTypes.FindIndex(id => id.InternalName.Equals("CashGrab0"))];
                                }
                                break;

                            case GameTypes.SQUADHEIST0:
                                if ((Program.BFHLMaps[mIndex].GameTypeList & GameTypes.SQUADHEIST0) == GameTypes.SQUADHEIST0)
                                {
                                    mlpgt.Maps.Add((BFHLMap)Program.BFHLMaps[mIndex].Clone());
                                    mlpgt.Maps[mlpgt.Maps.Count - 1].IntendedGameType = Program.BFHLGameTypes[Program.BFHLGameTypes.FindIndex(id => id.InternalName.Equals("SquadHeist0"))];
                                }
                                break;


                            default:
                                // This should never be executed, but you never know when I could stuff something up somewhere.
                                MessageBox.Show("ERROR: Invalid gametype passed while trying to populate map list.\r\n\r\nIn other words, there is a bug.\t>.<");
                                Application.Exit();
                                break;
                        }
                    }
                    totalMapCount += mlpgt.Maps.Count;
                }

                #endregion GAMETYPE_PROCESSING

                #region GENERATE_OUTPUT

                if (cbRepeatAll.Checked)
                {
                    BFHLMap previousMap = new BFHLMap();
                    BFHLMap previousPreviousMap = new BFHLMap();
                    while (totalMapCount > 0)
                    {
                        foreach (BFHLMapListPerGameType mapListPgt in BFHLMapList)
                        // This should get executed once per game type, repeating while there are still maps.
                        {
                            // TotalMapCount loop is almost done, but we still have maps left, so fix TotalMapCount
                            if (totalMapCount < mapListPgt.Maps.Count)
                            {
                                totalMapCount = mapListPgt.Maps.Count;
                            }
                            BFHLMap selectedMap = new BFHLMap();
                            // string gt = "";
                            // We've run out of maps for this game type, start picking from UsedMaps list.
                            if (mapListPgt.Maps.Count == 0 && mapListPgt.UsedMaps.Count > 0)
                            {
                                if (cbDontRepeat.Checked) { continue; }
                                if (mapListPgt.UsedMapsCurrentIndex >= mapListPgt.UsedMaps.Count) // Reset counter to 0 if necessary
                                {
                                    mapListPgt.UsedMapsCurrentIndex = 0;
                                }
                                selectedMap = mapListPgt.UsedMaps[mapListPgt.UsedMapsCurrentIndex];
                                mapListPgt.UsedMapsCurrentIndex++;
                            }
                            else
                            {
                                if (mapListPgt.Maps.Count > 0)
                                {
                                    selectedMap = Program.SelectMap(mapListPgt.Maps, _rnd.Next(0, mapListPgt.Maps.Count)); // Randomly select a map from the list
                                }
                                else continue; // We don't have maps for this game type, so continue.
                            }
                            while ((selectedMap.InternalName == previousMap.InternalName) | (selectedMap.InternalName == previousPreviousMap.InternalName)) // Ensure that two of the same maps don't occur in a row
                            {
                                // Either we don't have any maps left or there is one map left so we cannot resolve the duplicate issue, so force picking from usedmaps.
                                if ((mapListPgt.Maps.Count == 0) | (mapListPgt.Maps.Count == 1))
                                {
                                    if (mapListPgt.UsedMapsCurrentIndex >= mapListPgt.UsedMaps.Count) // Reset counter to 0 if necessary to start the loop over.
                                    {
                                        mapListPgt.UsedMapsCurrentIndex = 0;
                                    }
                                    selectedMap = mapListPgt.UsedMaps[mapListPgt.UsedMapsCurrentIndex];
                                    mapListPgt.UsedMapsCurrentIndex++;
                                }
                                else
                                {
                                    selectedMap = Program.SelectMap(mapListPgt.Maps, _rnd.Next(0, mapListPgt.Maps.Count));
                                }
                            }
                            if (Comments)
                            {
                                outputTxt += selectedMap.InternalName + " " + selectedMap.IntendedGameType.InternalName + " " + mapListPgt.Rounds.ToString() + " # Map: " + selectedMap.FriendlyName + ", Game Type: " + selectedMap.IntendedGameType.FriendlyName + "\r\n";
                            }
                            else
                            {
                                outputTxt += selectedMap.InternalName + " " + selectedMap.IntendedGameType.InternalName + " " + mapListPgt.Rounds.ToString() + "\r\n";
                            }
                            if (mapListPgt.Maps.Count > 0) // Only run the below if we haven't run out of maps.
                            {
                                mapListPgt.UsedMaps.Add(selectedMap);
                                mapListPgt.Maps.Remove(selectedMap);
                            }
                            totalMapCount--;
                            previousPreviousMap = previousMap;
                            previousMap = selectedMap;
                        }
                    }
                }
                else
                {
                    BFHLMap previousMap = new BFHLMap();
                    // else loop the number of nmRepeats.Value
                    int Repeats = Convert.ToInt32(nmRepeats.Value);
                    while (Repeats > 0)
                    {
                        foreach (BFHLMapListPerGameType mapListPgt in BFHLMapList)
                        // This should get executed once per game type, repeating nmRepeats.Value times.
                        {
                            BFHLMap selectedMap = new BFHLMap();
                            //string gt = "";
                            if (mapListPgt.Maps.Count == 0) // We've run out of maps, start picking from UsedMaps list.
                            {
                                if (mapListPgt.UsedMapsCurrentIndex >= mapListPgt.UsedMaps.Count) // Reset counter to 0 if necessary
                                {
                                    mapListPgt.UsedMapsCurrentIndex = 0;
                                }
                                selectedMap = mapListPgt.UsedMaps[mapListPgt.UsedMapsCurrentIndex];
                                mapListPgt.UsedMapsCurrentIndex++;
                            }
                            else
                            {
                                selectedMap = Program.SelectMap(mapListPgt.Maps, _rnd.Next(0, mapListPgt.Maps.Count)); // Randomly select a map from the list
                            }
                            while (selectedMap.InternalName == previousMap.InternalName) // Ensure that two of the same maps don't occur in a row
                            {
                                // Either we don't have any maps left or there is one map left so we cannot resolve the duplicate issue, so force picking from usedmaps.
                                if ((mapListPgt.Maps.Count == 0) | (mapListPgt.Maps.Count == 1))
                                {
                                    if (mapListPgt.UsedMapsCurrentIndex >= mapListPgt.UsedMaps.Count) // Reset counter to 0 if necessary to start the loop over.
                                    {
                                        mapListPgt.UsedMapsCurrentIndex = 0;
                                    }
                                    selectedMap = mapListPgt.UsedMaps[mapListPgt.UsedMapsCurrentIndex];
                                    mapListPgt.UsedMapsCurrentIndex++;
                                }
                                else
                                {
                                    selectedMap = Program.SelectMap(mapListPgt.Maps, _rnd.Next(0, mapListPgt.Maps.Count));
                                }
                            }
                            outputTxt += selectedMap.InternalName + " " + selectedMap.IntendedGameType.InternalName + " " + mapListPgt.Rounds.ToString() + " # Map: " + selectedMap.FriendlyName + ", Game Type: " + selectedMap.IntendedGameType.FriendlyName + "\r\n";
                            if (mapListPgt.Maps.Count > 0) // Only run the below if we haven't run out of maps.
                            {
                                mapListPgt.UsedMaps.Add(selectedMap);
                                mapListPgt.Maps.Remove(selectedMap);
                            }
                            previousMap = selectedMap;
                        }
                        Repeats--;
                    }
                }

                #endregion GENERATE_OUTPUT

                // ---
                // Instancing the output form
                Output outputform = new Output();
                outputform.txOutput.Text = outputTxt;
                if ((outputform.Size.Height <= OutH) | (outputform.Size.Width <= OutW))
                {
                    outputform.Size = new System.Drawing.Size(OutW, OutH);
                }
                outputform.ShowDialog();
                // After Show
                if (outputform.Size.Width != OutW)
                {
                    OutW = outputform.Size.Width;
                }
                if (outputform.Size.Height != OutH)
                {
                    OutH = outputform.Size.Height;
                }
                outputform.Dispose();
            }
            else MessageBox.Show("ERROR: The pattern list is empty");
        }

        #region DOUBLECLICK_EVENTS

        private void lbGameTypes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Add the game type class and number of rounds to the pattern list
            lbGameTypesPattern.Items.Add(Program.BFHLGameTypes[lbGameTypes.SelectedIndex].FriendlyName + "," + nmRounds.Value.ToString());
        }

        private void lbGameTypesPattern_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Removes a pattern from the pattern list
            lbGameTypesPattern.Items.Remove(lbGameTypesPattern.SelectedItem);
        }

        #endregion DOUBLECLICK_EVENTS

        #region EVENTS

        private void cbRepeatAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRepeatAll.Checked)
            {
                nmRepeats.Enabled = false;
                cbDontRepeat.Enabled = true;
            }
            else
            {
                nmRepeats.Enabled = true;
                cbDontRepeat.Enabled = false;
                cbDontRepeat.Checked = false;
            }
        }

        #endregion EVENTS
    }
}