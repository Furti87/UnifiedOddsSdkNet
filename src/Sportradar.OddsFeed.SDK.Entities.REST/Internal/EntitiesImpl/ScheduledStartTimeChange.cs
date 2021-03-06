﻿/*
* Copyright (C) Sportradar AG. See LICENSE for full license governing this code
*/
using System;
using System.Diagnostics.Contracts;
using Sportradar.OddsFeed.SDK.Entities.REST.Internal.DTO;

namespace Sportradar.OddsFeed.SDK.Entities.REST.Internal.EntitiesImpl
{
    internal class ScheduledStartTimeChange : IScheduledStartTimeChange
    {
        /// <summary>
        /// Gets the old time
        /// </summary>
        /// <value>The old time</value>
        public DateTime OldTime { get; }

        /// <summary>
        /// Gets the new time
        /// </summary>
        /// <value>The new time</value>
        public DateTime NewTime { get; }

        /// <summary>
        /// Gets the changed at
        /// </summary>
        /// <value>The changed at</value>
        public DateTime ChangedAt { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledStartTimeChangeDTO"/> class
        /// </summary>
        /// <param name="dto">The time change</param>
        public ScheduledStartTimeChange(ScheduledStartTimeChangeDTO dto)
        {
            Contract.Requires(dto != null);

            OldTime = dto.OldTime;
            NewTime = dto.NewTime;
            ChangedAt = dto.ChangedAt;
        }
    }
}
