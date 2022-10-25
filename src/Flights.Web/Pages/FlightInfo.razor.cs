using Flights.Web.Store.Flights.Actions;
using Microsoft.AspNetCore.Components;

namespace Flights.Web.Pages;

public partial class FlightInfo
{
    private string searchQuery = string.Empty;
    
    public string SearchQuery
    {
        get
        {
            return searchQuery;
        }
        set
        {
            searchQuery = value;
            Dispatcher.Dispatch(new SearchFlightAction(searchQuery, Destination));
        }
    }

    [Inject]
    public IDispatcher Dispatcher { get; set; }

    private void Refresh()
    {
        Dispatcher.Dispatch(new LoadFlightAction(Destination));
        searchQuery = string.Empty;
    }
}
