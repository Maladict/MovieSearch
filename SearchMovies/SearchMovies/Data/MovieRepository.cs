﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SearchMovies.Data
{
    class MovieRepository : IMovieRepository
    {
        private string _url;
        private HttpClient _client;

        public MovieRepository()
        {
            _client = new HttpClient();
            _url = "http://www.omdbapi.com/?apikey=3818f875&";
        }

        public async Task<SeasonSearch> GetSeason(string imdbId, int season)
        {
            var requestString = _url + "i=" + imdbId + "&season=" + season;
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            var json = await _client.GetStringAsync(requestString);

            var searchResult = JsonConvert.DeserializeObject<SeasonSearch>(json);

            return searchResult;
        }

        public async Task<SeriesEpisode> GetEpisode(string imdbId)
        {
            var requestString = _url + "i=" + imdbId + "&plot=full";
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            var json = await _client.GetStringAsync(requestString);

            var searchResult = JsonConvert.DeserializeObject<SeriesEpisode>(json);

            return searchResult;
        }
    }
}