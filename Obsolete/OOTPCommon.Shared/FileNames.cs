#region File Description
//---------------------------------------------------------------------------
//
// File: FileNames.cs
// Author: Steven Leffew
// Copyright: (C) 2021-2024
// Description: OOTP Database related file names.
//
//---------------------------------------------------------------------------
#endregion

#region License Info
//---------------------------------------------------------------------------
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//---------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
#endregion

namespace OOTPCommon
{
    /// <summary>
    /// Contains all common OOTP Database related file names.
    /// </summary>
    [Obsolete("This class has been deprecated. Use OOTPDatabaseConverter.Core.FileNames instead.")]
    public class FileNames
    {
        #region Members
        // Comma separated value(*.csv) file names
        private const string allstarFullFileName = "AllstarFull.csv";
        private const string awardsManagersFileName = "AwardsManagers.csv";
        private const string awardsPlayersFileName = "AwardsPlayers.csv";
        private const string battingFileName = "Batting.csv";
        private const string battingNormalizedFileName = "Batting2.csv";
        private const string battingPostSeasonFileName = "BattingPost.csv";
        private const string battingSplitsFileName = "BattingSplits.csv";
        private const string endOfSeasonRostersFileName = "EOSRosters.csv";
        private const string fieldingFileName = "Fielding.csv";
        private const string fieldingNormalizedFileName = "Fielding2.csv";
        private const string fieldingOutFieldFileName = "FieldingOF.csv";
        private const string fieldingOutFieldNormalizedFileName = "FieldingOF2.csv";
        private const string fieldingPostSeasonFileName = "FieldingPost.csv";
        private const string hallOfFameFileName = "HallOfFame.csv";
        private const string lineupsFileName = "Lineups.csv";
        private const string managersFileName = "Managers.csv";
        private const string masterFileName = "Master.csv";
        private const string milbBattingFileName = "MiLBBatting.csv";
        private const string milbFieldingFileName = "MiLBFielding.csv";
        private const string milbLeaguesFileName = "MiLBLeagues.csv";
        private const string milbMasterFileName = "MiLBMaster.csv";
        private const string milbPitchingFileName = "MiLBPitching.csv";
        private const string milbTeamsFileName = "MiLBTeams.csv";
        private const string openingDayRostersFileName = "ODRosters.csv";
        private const string pitchingFileName = "Pitching.csv";
        private const string pitchingNormalizedFileName = "Pitching2.csv";
        private const string pitchingPostSeasonFileName = "PitchingPost.csv";
        private const string recordsSingleGameFileName = "RecordsSingleGame.csv";
        private const string seriesPostSeasonFileName = "SeriesPost.csv";
        private const string teamFranchisesFileName = "TeamFranchises.csv";
        private const string teamsFileName = "Teams.csv";
        private const string transactionsFileName = "Transactions.csv";
        private const string uniformNumberFileName = "UniNumbers.csv";
        private const string worldSeriesRostersFileName = "WSRosters.csv";

        private const string pitchingSplitsFileName = "PitchingSplits.csv";
        private const string fieldingRatingsFileName = "FieldingRatings.csv";
        private const string pitchingRatingsFileName = "PitchingRatings.csv";
        private const string negroLeagueBattingFileName = "NegroLeagueBatting.csv";
        private const string negroLeaguePitchingFileName = "NegroLeaguePitching.csv";
        private const string negroLeagueFieldingFileName = "NegroLeagueFielding.csv";

        private String[] historicalDatabaseAllCsvFileNames;
        private String[] historicalMinorDatabaseAllCsvFileNames;

        // Comma separated value(*.csv) file name arrays for database files
        private String[] historicalDatabaseAllCsvFileNamesV26 = { masterFileName, battingFileName, battingNormalizedFileName,
                                                pitchingFileName, pitchingNormalizedFileName, fieldingFileName,
                                                fieldingOutFieldFileName, allstarFullFileName, awardsPlayersFileName,
                                                hallOfFameFileName, teamsFileName, teamFranchisesFileName,
                                                seriesPostSeasonFileName, uniformNumberFileName, managersFileName,
                                                awardsManagersFileName, battingPostSeasonFileName, pitchingPostSeasonFileName,
                                                fieldingPostSeasonFileName, null, recordsSingleGameFileName, battingSplitsFileName,
                                                openingDayRostersFileName, endOfSeasonRostersFileName, pitchingSplitsFileName,
                                                fieldingRatingsFileName, pitchingRatingsFileName, 
                                                negroLeagueBattingFileName, negroLeaguePitchingFileName, negroLeagueFieldingFileName};

        private String[] historicalMinorDatabaseAllCsvFileNamesV26 = { milbMasterFileName, milbBattingFileName, null,
                                                milbPitchingFileName, null, milbFieldingFileName,
                                                null, null, null,
                                                null, milbTeamsFileName, null,
                                                null, null, null,
                                                null, null, null, null,
                                                milbLeaguesFileName};

        private String[] historicalDatabaseAllCsvFileNamesV25 = { masterFileName, battingFileName, battingNormalizedFileName,
                                                pitchingFileName, pitchingNormalizedFileName, fieldingFileName,
                                                fieldingOutFieldFileName, allstarFullFileName, awardsPlayersFileName,
                                                hallOfFameFileName, teamsFileName, teamFranchisesFileName,
                                                seriesPostSeasonFileName, uniformNumberFileName, managersFileName,
                                                awardsManagersFileName, battingPostSeasonFileName, pitchingPostSeasonFileName,
                                                fieldingPostSeasonFileName, null, recordsSingleGameFileName, battingSplitsFileName,
                                                openingDayRostersFileName, endOfSeasonRostersFileName, pitchingSplitsFileName, 
                                                fieldingRatingsFileName, pitchingRatingsFileName};

        private String[] historicalMinorDatabaseAllCsvFileNamesV25 = { milbMasterFileName, milbBattingFileName, null,
                                                milbPitchingFileName, null, milbFieldingFileName,
                                                null, null, null,
                                                null, milbTeamsFileName, null,
                                                null, null, null,
                                                null, null, null, null,
                                                milbLeaguesFileName};

        private String[] historicalDatabaseAllCsvFileNamesV22 = { masterFileName, battingFileName, battingNormalizedFileName,
                                                pitchingFileName, pitchingNormalizedFileName, fieldingFileName,
                                                fieldingOutFieldFileName, allstarFullFileName, awardsPlayersFileName,
                                                hallOfFameFileName, teamsFileName, teamFranchisesFileName,
                                                seriesPostSeasonFileName, uniformNumberFileName, managersFileName,
                                                awardsManagersFileName, battingPostSeasonFileName, pitchingPostSeasonFileName, 
                                                fieldingPostSeasonFileName, null, recordsSingleGameFileName, battingSplitsFileName, 
                                                openingDayRostersFileName, endOfSeasonRostersFileName, null, null};

        private String[] historicalMinorDatabaseAllCsvFileNamesV22 = { milbMasterFileName, milbBattingFileName, null,
                                                milbPitchingFileName, null, milbFieldingFileName,
                                                null, null, null,
                                                null, milbTeamsFileName, null,
                                                null, null, null,
                                                null, null, null, null,
                                                milbLeaguesFileName, null, null, null,
                                                null, null, null};

        private String[] historicalDatabaseAllCsvFileNamesV19 = { masterFileName, battingFileName, battingNormalizedFileName,
                                                pitchingFileName, pitchingNormalizedFileName, fieldingFileName,
                                                fieldingNormalizedFileName, fieldingOutFieldFileName, fieldingOutFieldNormalizedFileName,
                                                allstarFullFileName, awardsPlayersFileName, hallOfFameFileName,
                                                teamsFileName, teamFranchisesFileName, seriesPostSeasonFileName,
                                                uniformNumberFileName, managersFileName, awardsManagersFileName, battingPostSeasonFileName,
                                                pitchingPostSeasonFileName, fieldingPostSeasonFileName, null, recordsSingleGameFileName,
                                                battingSplitsFileName, openingDayRostersFileName, endOfSeasonRostersFileName};

        private String[] historicalMinorDatabaseAllCsvFileNamesV19 = { milbMasterFileName, milbBattingFileName, null,
                                                milbPitchingFileName, null, milbFieldingFileName,
                                                null, null, null,
                                                null, null, null,
                                                milbTeamsFileName, null, null,
                                                null, null, null, null,
                                                null, null, milbLeaguesFileName};

        private String[] historicalDatabaseAllCsvFileNamesV17 = { masterFileName, battingFileName, battingNormalizedFileName,
                                                pitchingFileName, pitchingNormalizedFileName, fieldingFileName,
                                                fieldingNormalizedFileName, fieldingOutFieldFileName, fieldingOutFieldNormalizedFileName,
                                                allstarFullFileName, awardsPlayersFileName, hallOfFameFileName,
                                                teamsFileName, teamFranchisesFileName, seriesPostSeasonFileName,
                                                uniformNumberFileName, managersFileName, awardsManagersFileName, battingPostSeasonFileName,
                                                pitchingPostSeasonFileName, worldSeriesRostersFileName};

        private String[] historicalMinorDatabaseAllCsvFileNamesV17 = { milbMasterFileName, milbBattingFileName, null,
                                                milbPitchingFileName, null, milbFieldingFileName,
                                                null, null, null,
                                                null, null, null,
                                                milbTeamsFileName, null, null,
                                                null, null, null, null,
                                                null, null, milbLeaguesFileName};

        private String[] externalAllCsvFileNames = { masterFileName, teamsFileName, milbMasterFileName, milbTeamsFileName,
                                                milbLeaguesFileName, seriesPostSeasonFileName, uniformNumberFileName,
                                                openingDayRostersFileName, endOfSeasonRostersFileName };

        // OOTP Database(*.odb) file names
        private const string historicalDatabaseFileName = "historical_database.odb";
        private const string historicalMinorDatabaseFileName = "historical_minor_database.odb";
        private const string historicalLineupsDatabaseFileName = "historical_lineups.odb";
        private const string historicalTransactionsDatabaseFileName = "historical_transactions.odb";
        private const string statsDatabaseFileName = "stats.odb";
        #endregion

        #region Helpers
        private void InitializeUnknownDatabaseCsvFileNames(String[] databaseCsvFileNames, bool isMinorDB)
        {
            if (databaseCsvFileNames.Length > 0)
            {
                for (int i = 0; i < databaseCsvFileNames.Length; i++)
                {
                    if (databaseCsvFileNames[i] == null)
                    {
                        if (isMinorDB)
                            databaseCsvFileNames[i] = "MiLB_Unknown_" + (i + 1).ToString() + ".csv";
                        else
                            databaseCsvFileNames[i] = "Unknown_" + (i + 1).ToString() + ".csv";
                    }
                }
            }
        }
        #endregion

        #region Accessors
        public string[] HistoricalDatabaseAllCsvFileNames => historicalDatabaseAllCsvFileNames;
        public string[] HistoricalMinorDatabaseAllCsvFileNames => historicalMinorDatabaseAllCsvFileNames;
        public string LineupsFileName => lineupsFileName;
        public string TransactionsFileName => transactionsFileName;
        public string HistoricalDatabaseFileName => historicalDatabaseFileName;
        public string HistoricalMinorDatabaseFileName => historicalMinorDatabaseFileName;
        public string HistoricalLineupsDatabaseFileName => historicalLineupsDatabaseFileName;
        public string HistoricalTransactionsDatabaseFileName => historicalTransactionsDatabaseFileName;
        public string StatsDatabaseFileName => statsDatabaseFileName;

        /// <summary>
        /// All comma separated value(*.csv) file names that exist outside of an OOTP database file.
        /// </summary>
        /// <remarks>Some files may exist both in an OOTP database file and outside as an over-riding, editable comma separated file.</remarks>
        /// <returns>An array of all comma separated value(*.csv) file names that exist outside of an OOTP database file.</returns>
        public String[] ExternalAllCsvFileNames => externalAllCsvFileNames;

        /// <summary>
        /// All comma separated value(*.csv) file names used for OOTP Baseball.
        /// </summary>
        /// <returns>An array of all comma separated value(*.csv) file names used in OOTP Baseball.</returns>
        public String[] AllCsvFileNames
        {
            get { return HistoricalDatabaseAllCsvFileNames.Concat(HistoricalMinorDatabaseAllCsvFileNames).ToArray().Concat(ExternalAllCsvFileNames).ToArray().Concat(new String[] { lineupsFileName, transactionsFileName }).ToArray(); }
        }
        #endregion

        #region Initialization
        public FileNames(int historicTables, int historicMinorTables)
        {
            historicalDatabaseAllCsvFileNames = new String[historicTables];
            historicalMinorDatabaseAllCsvFileNames = new String[historicMinorTables];

            InitializeUnknownDatabaseCsvFileNames(historicalDatabaseAllCsvFileNames, false);
            InitializeUnknownDatabaseCsvFileNames(historicalMinorDatabaseAllCsvFileNames, true);
        }

        public FileNames(OdbVersion odbVersion)
        {
            switch(odbVersion)
            {
                case OdbVersion.ODB_17:
                    historicalDatabaseAllCsvFileNames = historicalDatabaseAllCsvFileNamesV17;
                    historicalMinorDatabaseAllCsvFileNames = historicalMinorDatabaseAllCsvFileNamesV17;
                    break;
                case OdbVersion.ODB_19:
                    historicalDatabaseAllCsvFileNames = historicalDatabaseAllCsvFileNamesV19;
                    historicalMinorDatabaseAllCsvFileNames = historicalMinorDatabaseAllCsvFileNamesV19;
                    break;
                case OdbVersion.ODB_22:
                    historicalDatabaseAllCsvFileNames = historicalDatabaseAllCsvFileNamesV22;
                    historicalMinorDatabaseAllCsvFileNames = historicalMinorDatabaseAllCsvFileNamesV22;
                    break;
                case OdbVersion.ODB_25:
                    historicalDatabaseAllCsvFileNames = historicalDatabaseAllCsvFileNamesV25;
                    historicalMinorDatabaseAllCsvFileNames = historicalMinorDatabaseAllCsvFileNamesV25;
                    break;
                case OdbVersion.ODB_26:
                    historicalDatabaseAllCsvFileNames = historicalDatabaseAllCsvFileNamesV26;
                    historicalMinorDatabaseAllCsvFileNames = historicalMinorDatabaseAllCsvFileNamesV26;
                    break;
            }       
        }

        public FileNames(String[] historicCsvFileNames, String[] historicMinorCsvFileNames)
        {
            historicalDatabaseAllCsvFileNames = historicCsvFileNames;
            historicalMinorDatabaseAllCsvFileNames = historicMinorCsvFileNames;
        }
        #endregion
    }
}
