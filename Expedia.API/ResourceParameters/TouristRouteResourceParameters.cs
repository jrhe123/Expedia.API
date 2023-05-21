using System;
using System.Text.RegularExpressions;

namespace Expedia.API.ResourceParameters
{
	public class TouristRouteResourceParameters
	{
		public string? Keyword { get; set; }

		public string? Rating {
			get { return _rating; }
			set {
                _rating = value;

                RatingOperator = "";
                RatingValue = -1;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    Regex regex = new Regex(@"([A-Za-z0-9\-]+)(\d+)");
                    Match match = regex.Match(value);
                    if (match.Success)
                    {
                        RatingOperator = match.Groups[1].Value;
                        RatingValue = Int32.Parse(match.Groups[2].Value);
                    }
                }
			}
		}
        private string _rating;

        public string? RatingOperator { get; set; }
        public int? RatingValue { get; set; }
		
    }
}

