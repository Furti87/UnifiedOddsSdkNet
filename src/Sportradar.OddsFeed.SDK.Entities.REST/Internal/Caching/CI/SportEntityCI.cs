﻿/*
* Copyright (C) Sportradar AG. See LICENSE for full license governing this code
*/
using System.Diagnostics.Contracts;
using Sportradar.OddsFeed.SDK.Entities.REST.Internal.DTO;
using Sportradar.OddsFeed.SDK.Messages;

namespace Sportradar.OddsFeed.SDK.Entities.REST.Internal.Caching.CI
{
    /// <summary>
    /// A representation of the sport entity used by the cache
    /// </summary>
    public class SportEntityCI
    {
        /// <summary>
        /// Gets the id of the represented sport entity
        /// </summary>
        public URN Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SportEntityCI"/> class
        /// </summary>
        /// <param name="dto">A <see cref="SportEntityDTO"/> containing information about the sport entity</param>
        internal SportEntityCI(SportEntityDTO dto)
        {
            Contract.Requires(dto != null);

            Id = dto.Id;
        }

        /// <summary>Determines whether the specified object is equal to the current object</summary>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false</returns>
        /// <param name="obj">The object to compare with the current object</param>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            var other = obj as SportEntityCI;
            if (other == null)
            {
                return false;
            }
            return Id == other.Id;
        }

        /// <summary>Serves as the default hash function</summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
