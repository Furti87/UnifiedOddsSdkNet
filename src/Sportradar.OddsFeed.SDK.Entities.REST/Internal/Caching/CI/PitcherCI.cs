﻿/*
* Copyright (C) Sportradar AG. See LICENSE for full license governing this code
*/
using System.Diagnostics.Contracts;
using System.Globalization;
using Sportradar.OddsFeed.SDK.Entities.REST.Enums;
using Sportradar.OddsFeed.SDK.Entities.REST.Internal.DTO;

namespace Sportradar.OddsFeed.SDK.Entities.REST.Internal.Caching.CI
{
    /// <summary>
    /// Provides information about pitcher (cache item)
    /// </summary>
    public class PitcherCI : SportEntityCI
    {
        /// <summary>
        /// Gets the name of the pitcher
        /// </summary>
        /// <value>The name of the pitcher</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the hand with which player pitches
        /// </summary>
        /// <value>The hand with which player pitches</value>
        public PlayerHand Hand { get; private set; }

        /// <summary>
        /// Gets the indicator if the competitor is Home or Away
        /// </summary>
        /// <value>The indicator if the competitor is Home or Away</value>
        public HomeAway Competitor { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PitcherCI"/> class
        /// </summary>
        /// <param name="pitcher">A <see cref="PitcherDTO"/> containing information about the pitcher</param>
        /// <param name="culture">A <see cref="CultureInfo"/> specifying the language of the pitcher</param>
        internal PitcherCI(PitcherDTO pitcher, CultureInfo culture)
            : base(pitcher)
        {
            Contract.Requires(pitcher != null);
            Contract.Requires(culture != null);

            Merge(pitcher, culture);
        }

        /// <summary>
        /// Merges the provided information about the current pitcher
        /// </summary>
        /// <param name="pitcher">A <see cref="PitcherDTO"/> containing pitcher info</param>
        /// <param name="culture">A <see cref="CultureInfo"/> specifying the language of the pitcher info</param>
        internal void Merge(PitcherDTO pitcher, CultureInfo culture)
        {
            Contract.Requires(pitcher != null);
            Contract.Requires(culture != null);

            Name = pitcher.Name;
            Hand = pitcher.Hand;
            Competitor = pitcher.Competitor;
        }
    }
}
