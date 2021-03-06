﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SearchMovies.Data.ViewModels
{
    public class EpisodeViewModel
    {
        public string Number { get; set; }
        public string Title { get; set; }
        public string ImdbId { get; set; }
        public string Plot { get; set; }
        public bool PlotIsVisible { get; set; }
        public bool HasWatched { get; set; }
        public string BackgroundColorProperty { get; set; }
    }
}
