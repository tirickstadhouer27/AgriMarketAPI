using System;

namespace AgriMarketAPI.Exceptions
{
    public class ListingNotFoundException : Exception
    {
        public ListingNotFoundException(int id) 
            : base($"Produce listing with ID {id} was not found.")
        {
        }
    }
}