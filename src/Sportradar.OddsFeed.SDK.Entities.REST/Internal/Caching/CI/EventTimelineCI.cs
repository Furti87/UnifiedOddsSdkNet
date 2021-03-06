﻿/*
* Copyright (C) Sportradar AG. See LICENSE for full license governing this code
*/
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using Sportradar.OddsFeed.SDK.Entities.REST.Enums;
using Sportradar.OddsFeed.SDK.Entities.REST.Internal.DTO;

namespace Sportradar.OddsFeed.SDK.Entities.REST.Internal.Caching.CI
{
    /// <summary>
    /// Defines a cache item for event timeline (aka MatchTimeline)
    /// </summary>
    public class EventTimelineCI
    {
        private List<TimelineEventCI> _timeline;
        private bool _isFinalized;
        private readonly List<CultureInfo> _fetchedCultures;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventTimelineCI"/> class.
        /// </summary>
        /// <param name="dto">The events.</param>
        /// <param name="culture"></param>
        public EventTimelineCI(MatchTimelineDTO dto, CultureInfo culture)
        {
            _fetchedCultures = new List<CultureInfo>();
            Merge(dto, culture);
        }

        /// <summary>
        /// Gets the list of timeline events
        /// </summary>
        /// <returns>The list of <see cref="TimelineEventCI"/></returns>
        public IEnumerable<TimelineEventCI> Timeline => _timeline;

        /// <summary>
        /// Gets a value indicating whether this instance is finalized
        /// </summary>
        /// <value><c>true</c> if this instance is finalized; otherwise, <c>false</c>.</value>
        public bool IsFinalized => _isFinalized;

        /// <summary>
        /// Gets the list already fetched cultures
        /// </summary>
        /// <value>The list already fetched cultures</value>
        public IEnumerable<CultureInfo> FetchedCultureInfos => _fetchedCultures;

        /// <summary>
        /// Merges the specified dto
        /// </summary>
        /// <param name="dto">The dto</param>
        /// <param name="culture">The culture</param>
        public void Merge(MatchTimelineDTO dto, CultureInfo culture)
        {
            Contract.Requires(dto != null);

            if (dto.BasicEvents != null && dto.BasicEvents.Any())
            {
                if (_timeline == null)
                {
                    _timeline = dto.BasicEvents.Select(s => new TimelineEventCI(s, culture)).ToList();
                }
                else
                {
                    foreach (var basicEvent in dto.BasicEvents)
                    {
                        var timelineEvent = _timeline.FirstOrDefault(s => s.Id == basicEvent.Id);
                        if (timelineEvent != null && timelineEvent.Id == basicEvent.Id)
                        {
                            timelineEvent.Merge(basicEvent, culture);
                        }
                        else
                        {
                            _timeline.Add(new TimelineEventCI(basicEvent, culture));
                        }
                    }
                }
            }

            if (!_isFinalized && dto.SportEventStatus != null)
            {
                if (dto.SportEventStatus.Status == EventStatus.Closed
                 || dto.SportEventStatus.Status == EventStatus.Ended)
                {
                    _isFinalized = true;
                }
            }

            _fetchedCultures.Add(culture);
        }
    }
}
