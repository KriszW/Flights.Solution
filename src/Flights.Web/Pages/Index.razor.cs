using Flights.Domain.Enumerations;
using Flights.Web.Store.Flights;
using Flights.Web.Store.Flights.Actions;
using Microsoft.AspNetCore.Components;

namespace Flights.Web.Pages;

public partial class Index
{
    private int selectedTabIndex;

    public int SelectedTabIndex
    {
        get => selectedTabIndex;
        set
        {
            selectedTabIndex = value;
            Dispatcher.Dispatch(new LoadFlightAction((DestinationAirports)selectedTabIndex));
        }
    }

    [Inject]
    public IState<FlightsState> State { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Dispatcher.Dispatch(new LoadFlightAction((DestinationAirports)selectedTabIndex));
    }
}

