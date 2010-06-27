using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotFermion.Qiblah
{
    public class GeoErrors
    {
        // No errors occurred; the address was successfully parsed and its geocode has been returned.
        public const int G_GEO_SUCCESS = 200;

        // A directions request could not be successfully parsed. For example, the request may have been rejected if it contained more than the maximum number of waypoints allowed.
        public const int G_GEO_BAD_REQUEST = 400;
        
        // A geocoding or directions request could not be successfully processed, yet the exact reason for the failure is not known.
        public const int G_GEO_SERVER_ERROR = 500;
        
        // The HTTP q parameter was either missing or had no value. For geocoding requests, this means that an empty address was specified as input. For directions requests, this means that no query was specified in the input.
        public const int G_GEO_MISSING_QUERY = 601;
        
        // Synonym for G_GEO_MISSING_QUERY.
        public const int G_GEO_MISSING_ADDRESS = 601;
        
        // No corresponding geographic location could be found for the specified address. This may be due to the fact that the address is relatively new, or it may be incorrect.
        public const int G_GEO_UNKNOWN_ADDRESS = 602;
        
        // The geocode for the given address or the route for the given directions query cannot be returned due to legal or contractual reasons.
        public const int G_GEO_UNAVAILABLE_ADDRESS = 603;
        
        // The GDirections object could not compute directions between the points mentioned in the query. This is usually because there is no route available between the two points, or because we do not have data for routing in that region.
        public const int G_GEO_UNKNOWN_DIRECTIONS = 604;
        
        // The given key is either invalid or does not match the domain for which it was given.
        public const int G_GEO_BAD_KEY = 610;
        
        // The given key has gone over the requests limit in the 24 hour period or has submitted too many requests in too short a period of time. If you're sending multiple requests in parallel or in a tight loop, use a timer or pause in your code to make sure you don't send the requests too quickly.
        public const int G_GEO_TOO_MANY_QUERIES = 620;
    }
}
