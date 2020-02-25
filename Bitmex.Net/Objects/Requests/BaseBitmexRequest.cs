﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace   Bitmex.Net.Client.Objects.Requests
{
    /// <summary>
    /// Base object for filtering results
    /// </summary>
    public class BitmexRequestWithFilter
    {        
        /// <summary>
        /// Instrument symbol. Send a bare series (e.g. XBT) to get data for the nearest expiring contract in that series.
        ///You can also send a timeframe, e.g.XBT:quarterly.Timeframes are nearest, daily, weekly, monthly, quarterly, biquarterly, and perpetual.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        /// <summary>        
        /// Generic table filter. Send JSON key/value pairs, such as {"key": "value"}. You can key on individual fields, and do more advanced querying on timestamps. See the <see href="https://www.bitmex.com/app/restAPI#-4">Timestamp Docs </see>for more details.
        /// </summary>
        [JsonProperty("filter")]
        public string Filter => JsonConvert.SerializeObject(Filters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value));
        [JsonIgnoreAttribute]
        private Dictionary<string, object> Filters { get; set; } = new Dictionary<string, object>();
        /// <summary>
        /// Array of column names to fetch. If omitted, will return all columns.
        ///Note that this method will always return item keys, even when not specified, so you may receive more columns that you expect.
        /// </summary>
        [JsonProperty("columns")]
        public HashSet<string> Columns { get; set; }
        /// <summary>
        /// Number of results to fetch.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; set; } = 100;
        /// <summary>
        ///  Starting point for results.
        /// </summary>
        [JsonProperty("start")]
        public int? Start { get; set; }

        /// <summary>
        /// If true, will sort results newest first.
        /// </summary>
        [JsonProperty("reverse")]
        public bool Reverse { get; set; } = false;

        /// <summary>
        /// Starting date filter for results.
        /// </summary>
        [JsonProperty("startTime")]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// Ending date filter for results.
        /// </summary>
        [JsonProperty("endTime")]
        public DateTime? EndTime { get; set; }

        public void AddFilter(string key, string value)
        {
            if (!Filters.ContainsKey(key))
            {
                Filters.Add(key, value);
            }
        }
        public void AddColumnToGetInRequest(string columnName)
        {
            Columns.Add(columnName);
        }

    }
}