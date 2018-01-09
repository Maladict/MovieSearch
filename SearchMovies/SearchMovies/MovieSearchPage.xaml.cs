﻿using System;
using System.Collections.Generic;
using Microsoft.AppCenter.Analytics;
using SearchMovies.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchMovies
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieSearchPage : SearchPageBase
	{
		public MovieSearchPage ()
		{
		    this.InitializeComponent();
            Analytics.TrackEvent("Entered Movie Search");

		    SearchInput.Completed += SearchButtonOnPressed;
		    SearchButton.Pressed += SearchButtonOnPressed;
		    ResultsListView.ItemTapped += ResultSelected;
		}

	    private async void ResultSelected(object sender, ItemTappedEventArgs e)
	    {
	        var searchItem = (MovieViewModel)e.Item;
	        var movieId = searchItem.ImdbId;

	        Analytics.TrackEvent("ResultSelected", new Dictionary<string, string>() { { "ImdbId", movieId } });

            var details = await Repository.GetMovieDetails(movieId);

	        await Navigation.PushAsync(new MovieDetailPage(details));
        }

	    private async void SearchButtonOnPressed(object sender, EventArgs eventArgs)
	    {
	        if (IsConnected)
	        {
	            AIndicator.IsRunning = true;
	            var searchResult = await Repository.Search("movie", SearchInput.Text, null);

	            Analytics.TrackEvent("Search button pressed.", new Dictionary<string, string>()
	            {
	                { "SearchWord", SearchInput.Text },
	                { "SearchResponse", searchResult.Response}
	            });

                var movies = new List<MovieViewModel>();

	            foreach (var search in searchResult.Search)
	            {
	                var viewModel = new MovieViewModel()
	                {
	                    Title = search.Title,
                        Year = search.Year,
                        HasWatched = await HasWatched(search.imdbID),
                        BackgroundColorProperty = await HasWatched(search.imdbID) ? "DarkSeaGreen" : "White",
                        ImdbId = search.imdbID
	                };
                    movies.Add(viewModel);
	            }

	            ResultsListView.ItemsSource = movies;
	            ResultNumber.Text = "Results: " + searchResult.totalResults;
	            AIndicator.IsRunning = false;
            }
	    }

        public override void UpdateElements()
	    {
	        if (IsConnected)
	        {
	            this.Title = "Movie Search";
	            SearchButton.IsEnabled = true;

	        }
            else if (!IsConnected)
	        {
	            this.Title = "Movie Search (no connection)";
	            SearchButton.IsEnabled = false;
	        }
	    }
	}
}