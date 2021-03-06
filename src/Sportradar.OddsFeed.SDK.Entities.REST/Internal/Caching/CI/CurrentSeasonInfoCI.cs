﻿/*
* Copyright (C) Sportradar AG. See LICENSE for full license governing this code
*/
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using Sportradar.OddsFeed.SDK.Entities.REST.Internal.Caching.Events;
using Sportradar.OddsFeed.SDK.Entities.REST.Internal.DTO;
using Sportradar.OddsFeed.SDK.Messages;

namespace Sportradar.OddsFeed.SDK.Entities.REST.Internal.Caching.CI
{
    /// <summary>
    /// Cache item for current season info
    /// </summary>
    /// <seealso cref="CacheItem" />
    public class CurrentSeasonInfoCI : CacheItem
    {
        /// <summary>
        /// Gets a <see cref="string"/> representation of the current season year
        /// </summary>
        public string Year { get; private set; }

        /// <summary>
        /// Gets the start date of the season represented by the current instance
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Gets the end date of the season represented by the current instance
        /// </summary>
        /// <value>The end date.</value>
        public DateTime EndDate { get; private set; }

        /// <summary>
        /// Gets the <see cref="SeasonCoverageCI"/> instance containing information about coverage available for the season associated with the current instance
        /// </summary>
        /// <returns>The <see cref="SeasonCoverageCI"/> instance containing information about coverage available for the season associated with the current instance</returns>
        public SeasonCoverageCI SeasonCoverage { get; private set; }

        /// <summary>
        /// Gets the <see cref="IEnumerable{GroupCI}"/> specifying groups of tournament associated with the current instance
        /// </summary>
        /// <returns>The <see cref="IEnumerable{GroupCI}"/> specifying groups of tournament associated with the current instance</returns>
        public IEnumerable<GroupCI> Groups { get; private set; }

        /// <summary>
        /// Gets the <see cref="RoundCI"/> specifying the current round of the tournament associated with the current instance
        /// </summary>
        /// <returns>The <see cref="RoundCI"/> specifying the current round of the tournament associated with the current instance</returns>
        public RoundCI CurrentRound { get; private set; }

        /// <summary>
        /// Gets the list of competitors
        /// </summary>
        /// <value>The list of competitors</value>
        public IEnumerable<CompetitorCI> Competitors { get; private set; }

        /// <summary>
        /// Gets the list of all <see cref="CompetitionCI"/> that belongs to the season schedule
        /// </summary>
        /// <returns>The list of all <see cref="CompetitionCI"/> that belongs to the season schedule</returns>
        public IEnumerable<URN> Schedule { get; private set; }

        private readonly IDataRouterManager _dataRouterManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentSeasonInfoCI"/> class
        /// </summary>
        /// <param name="dto">The dto</param>
        /// <param name="culture">The culture</param>
        /// <param name="dataRouterManager">The <see cref="IDataRouterManager"/> used to fetch missing data</param>
        public CurrentSeasonInfoCI(CurrentSeasonInfoDTO dto, CultureInfo culture, IDataRouterManager dataRouterManager)
            : base(dto.Id, dto.Name, culture)
        {
            Contract.Requires(dto != null);

            _dataRouterManager = dataRouterManager;
            Year = dto.Year;
            StartDate = dto.StartDate;
            EndDate = dto.EndDate;

            SeasonCoverage = dto.SeasonCoverage == null ? null : new SeasonCoverageCI(dto.SeasonCoverage);
            Groups = dto.Groups == null ? null : dto.Groups.Select(s => new GroupCI(s, culture, _dataRouterManager));
            CurrentRound = dto.CurrentRound == null ? null : new RoundCI(dto.CurrentRound, culture);
            Competitors = dto.Competitors == null ? null : dto.Competitors.Select(s => new CompetitorCI(s, culture, _dataRouterManager));
            Schedule = dto.Schedule == null ? null : dto.Schedule.Select(s => s.Id);
        }

        /// <summary>
        /// Merges the specified dto
        /// </summary>
        /// <param name="dto">The dto</param>
        /// <param name="culture">The culture</param>
        public void Merge(CurrentSeasonInfoDTO dto, CultureInfo culture)
        {
            Contract.Requires(dto != null);

            base.Merge(dto, culture);

            Year = dto.Year;
            StartDate = dto.StartDate;
            EndDate = dto.EndDate;

            if (dto.SeasonCoverage != null)
            {
                SeasonCoverage = new SeasonCoverageCI(dto.SeasonCoverage);
            }
            if (dto.Groups != null)
            {
                Groups = dto.Groups.Select(s => new GroupCI(s, culture, _dataRouterManager));
            }
            if (dto.CurrentRound != null)
            {
                CurrentRound = new RoundCI(dto.CurrentRound, culture);
            }
            if (dto.Competitors != null)
            {
                Competitors = dto.Competitors.Select(s => new CompetitorCI(s, culture, _dataRouterManager));
            }
            if (dto.Schedule != null)
            {
                Schedule = dto.Schedule.Select(s => s.Id);
            }
        }
    }
}
