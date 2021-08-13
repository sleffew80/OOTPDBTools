#region File Description
//---------------------------------------------------------------------------
//
// File: FileNames.cs
// Author: Steven Leffew
// Copyright: (C) 2021
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
    public static class FileNames
    {
        #region Members
        // Comma separated value(*.csv) file names
        private static string allstarFullFileName = "AllstarFull.csv";
        private static string awardsManagersFileName = "AwardsManagers.csv";
        private static string awardsPlayersFileName = "AwardsPlayers.csv";
        private static string battingFileName = "Batting.csv";
        private static string battingNormalizedFileName = "Batting-Normalized.csv";
        private static string battingPostSeasonFileName = "BattingPost.csv";
        private static string battingSplitsFileName = "BattingSplits.csv";
        private static string endOfSeasonRostersFileName = "EOSRosters.csv";
        private static string fieldingFileName = "Fielding.csv";
        private static string fieldingNormalizedFileName = "Fielding-Normalized.csv";
        private static string fieldingOutFieldFileName = "FieldingOF.csv";
        private static string fieldingOutFieldNormalizedFileName = "FieldingOF-Normalized.csv";
        private static string fieldingPostSeasonFileName = "FieldingPost.csv";
        private static string hallOfFameFileName = "HallOfFame.csv";
        private static string lineupsFileName = "Lineups.csv";
        private static string managersFileName = "Managers.csv";
        private static string masterFileName = "Master.csv";
        private static string milbBattingFileName = "MiLBBatting.csv";
        private static string milbFieldingFileName = "MiLBFielding.csv";
        private static string milbLeaguesFileName = "MiLBLeagues.csv";
        private static string milbMasterFileName = "MiLBMaster.csv";
        private static string milbPitchingFileName = "MiLBPitching.csv";
        private static string milbTeamsFileName = "MiLBTeams.csv";
        private static string openingDayRostersFileName = "ODRosters.csv";
        private static string pitchingFileName = "Pitching.csv";
        private static string pitchingNormalizedFileName = "Pitching-Normalized.csv";
        private static string pitchingPostSeasonFileName = "PitchingPost.csv";
        private static string recordsSingleGameFileName = "RecordsSingleGame.csv";
        private static string seriesPostSeasonFileName = "SeriesPost.csv";
        private static string teamFranchisesFileName = "TeamFranchises.csv";
        private static string teamsFileName = "Teams.csv";
        private static string transactionsFileName = "Transactions.csv";
        private static string uniformNumberFileName = "UniNumbers.csv";

        // Comma separated value(*.csv) file name arrays for database files
        private static String[] historicalDatabaseAllCsvFileNames = { masterFileName, battingFileName, battingNormalizedFileName,
                                                pitchingFileName, pitchingNormalizedFileName, fieldingFileName,
                                                fieldingNormalizedFileName, fieldingOutFieldFileName, fieldingOutFieldNormalizedFileName,
                                                allstarFullFileName, awardsPlayersFileName, hallOfFameFileName,
                                                teamsFileName, teamFranchisesFileName, seriesPostSeasonFileName,
                                                uniformNumberFileName, managersFileName, awardsManagersFileName, battingPostSeasonFileName,
                                                pitchingPostSeasonFileName, fieldingPostSeasonFileName, null, recordsSingleGameFileName,
                                                battingSplitsFileName, openingDayRostersFileName, endOfSeasonRostersFileName};
        private static String[] historicalMinorDatabaseAllCsvFileNames = { milbMasterFileName, milbBattingFileName, null,
                                                milbPitchingFileName, null, milbFieldingFileName,
                                                null, null, null,
                                                null, null, null,
                                                milbTeamsFileName, null, null,
                                                null, null, null, null,
                                                null, null, milbLeaguesFileName};
        private static String[] externalAllCsvFileNames = { masterFileName, teamsFileName, milbMasterFileName, milbTeamsFileName,
                                                milbLeaguesFileName, seriesPostSeasonFileName, uniformNumberFileName,
                                                openingDayRostersFileName, endOfSeasonRostersFileName };

        // OOTP Database(*.odb) file names
        private static string historicalDatabaseFileName = "historical_database.odb";
        private static string historicalMinorDatabaseFileName = "historical_minor_database.odb";
        private static string historicalLineupsDatabaseFileName = "historical_lineups.odb";
        private static string historicalTransactionsDatabaseFileName = "historical_transactions.odb";
        private static string statsDatabaseFileName = "stats.odb";
        #endregion

        #region Accessors
        public static string LineupsFileName => lineupsFileName;
        public static string TransactionsFileName => transactionsFileName;
        public static string HistoricalDatabaseFileName => historicalDatabaseFileName;
        public static string HistoricalMinorDatabaseFileName => historicalMinorDatabaseFileName;
        public static string HistoricalLineupsDatabaseFileName => historicalLineupsDatabaseFileName;
        public static string HistoricalTransactionsDatabaseFileName => historicalTransactionsDatabaseFileName;
        public static string StatsDatabaseFileName => statsDatabaseFileName;

        /// <summary>
        /// All comma separated value(*.csv) file names that exist outside of an OOTP database file.
        /// </summary>
        /// <remarks>Some files may exist both in an OOTP database file and outside as an over-riding, editable comma separated file.</remarks>
        /// <returns>An array of all comma separated value(*.csv) file names that exist outside of an OOTP database file.</returns>
        public static String[] ExternalAllCsvFileNames => externalAllCsvFileNames;

        /// <summary>
        /// All comma separated value(*.csv) file names used for OOTP Baseball.
        /// </summary>
        /// <returns>An array of all comma separated value(*.csv) file names used in OOTP Baseball.</returns>
        public static String[] AllCsvFileNames
        {
            get { return HistoricalDatabaseAllCsvFileNames(false).Concat(HistoricalMinorDatabaseAllCsvFileNames(false)).ToArray().Concat(ExternalAllCsvFileNames).ToArray().Concat(new String[] { lineupsFileName, transactionsFileName }).ToArray(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// All comma separated value(*.csv) file names required for OOTP's historical database.
        /// </summary>
        /// <param name="includeUnknownFileNames">Include file names in the format of "_unknown[#]" for unused sections of the database.</param>
        /// <returns>An array of all comma separated value(*.csv) file names required for the OOTP historical database.</returns>
        public static String[] HistoricalDatabaseAllCsvFileNames(bool includeUnknownFileNames)
        {
            if (includeUnknownFileNames == true)
            {
                String[] csvFileNamesWithUnknowns = historicalDatabaseAllCsvFileNames;
                for (int i = 0; i < csvFileNamesWithUnknowns.Length; i++)
                {
                    if (csvFileNamesWithUnknowns[i] == null)
                    {
                        csvFileNamesWithUnknowns[i] = "_unknown" + (i + 1).ToString() + ".csv";
                    }
                }
                return csvFileNamesWithUnknowns;
            }
            else
            {
                return historicalDatabaseAllCsvFileNames;
            }
        }

        /// <summary>
        /// All comma separated value(*.csv) file names required for OOTP's historical minor database.
        /// </summary>
        /// <param name="includeUnknownFileNames">Include file names in the format of "_unknown[#]" for unused sections of the database.</param>
        /// <returns>An array of all comma separated value(*.csv) file names required for the OOTP historical minor database.</returns>
        public static String[] HistoricalMinorDatabaseAllCsvFileNames(bool includeUnknownFileNames)
        {
            if (includeUnknownFileNames == true)
            {
                String[] csvFileNamesWithUnknowns = historicalMinorDatabaseAllCsvFileNames;
                for (int i = 0; i < csvFileNamesWithUnknowns.Length; i++)
                {
                    if (csvFileNamesWithUnknowns[i] == null)
                    {
                        csvFileNamesWithUnknowns[i] = "_unknownMinor" + (i + 1).ToString() + ".csv";
                    }
                }
                return csvFileNamesWithUnknowns;
            }
            else
            {
                return historicalMinorDatabaseAllCsvFileNames;
            }
        }
        #endregion
    }
}
