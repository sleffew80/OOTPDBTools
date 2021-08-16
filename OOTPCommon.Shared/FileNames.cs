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
    public class FileNames
    {
        #region Members
        // Comma separated value(*.csv) file names
        private const string allstarFullFileName = "AllstarFull.csv";
        private const string awardsManagersFileName = "AwardsManagers.csv";
        private const string awardsPlayersFileName = "AwardsPlayers.csv";
        private const string battingFileName = "Batting.csv";
        private const string battingNormalizedFileName = "Batting-Normalized.csv";
        private const string battingPostSeasonFileName = "BattingPost.csv";
        private const string battingSplitsFileName = "BattingSplits.csv";
        private const string endOfSeasonRostersFileName = "EOSRosters.csv";
        private const string fieldingFileName = "Fielding.csv";
        private const string fieldingNormalizedFileName = "Fielding-Normalized.csv";
        private const string fieldingOutFieldFileName = "FieldingOF.csv";
        private const string fieldingOutFieldNormalizedFileName = "FieldingOF-Normalized.csv";
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
        private const string pitchingNormalizedFileName = "Pitching-Normalized.csv";
        private const string pitchingPostSeasonFileName = "PitchingPost.csv";
        private const string recordsSingleGameFileName = "RecordsSingleGame.csv";
        private const string seriesPostSeasonFileName = "SeriesPost.csv";
        private const string teamFranchisesFileName = "TeamFranchises.csv";
        private const string teamsFileName = "Teams.csv";
        private const string transactionsFileName = "Transactions.csv";
        private const string uniformNumberFileName = "UniNumbers.csv";

        // Comma separated value(*.csv) file name arrays for database files
        private String[] historicalDatabaseAllCsvFileNames = { masterFileName, battingFileName, battingNormalizedFileName,
                                                pitchingFileName, pitchingNormalizedFileName, fieldingFileName,
                                                fieldingNormalizedFileName, fieldingOutFieldFileName, fieldingOutFieldNormalizedFileName,
                                                allstarFullFileName, awardsPlayersFileName, hallOfFameFileName,
                                                teamsFileName, teamFranchisesFileName, seriesPostSeasonFileName,
                                                uniformNumberFileName, managersFileName, awardsManagersFileName, battingPostSeasonFileName,
                                                pitchingPostSeasonFileName, fieldingPostSeasonFileName, null, recordsSingleGameFileName,
                                                battingSplitsFileName, openingDayRostersFileName, endOfSeasonRostersFileName};
        private String[] historicalMinorDatabaseAllCsvFileNames = { milbMasterFileName, milbBattingFileName, null,
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

        #region Accessors
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
            get { return HistoricalDatabaseAllCsvFileNames(false).Concat(HistoricalMinorDatabaseAllCsvFileNames(false)).ToArray().Concat(ExternalAllCsvFileNames).ToArray().Concat(new String[] { lineupsFileName, transactionsFileName }).ToArray(); }
        }
        #endregion

        public FileNames()
        { }

        #region Methods
        /// <summary>
        /// All comma separated value(*.csv) file names required for OOTP's historical database.
        /// </summary>
        /// <param name="includeUnknownFileNames">Include file names in the format of "_unknown[#]" for unused sections of the database.</param>
        /// <returns>An array of all comma separated value(*.csv) file names required for the OOTP historical database.</returns>
        public String[] HistoricalDatabaseAllCsvFileNames(bool includeUnknownFileNames)
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
        public String[] HistoricalMinorDatabaseAllCsvFileNames(bool includeUnknownFileNames)
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
