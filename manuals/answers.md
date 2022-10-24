## Requirement

- Do you want the see the flights aggregated into one table with the airport name at the end of each column, or you want to see it one-by-one, or both?
This is up to you. Whichever makes more sense to you.
- What do you mean by live? Does it have to be real-time? Or is it enough to just update it when the user clicks a button or something like that?
Live is... You need to implement a backend service so it's not enough just to query the airport data every time in the frontend. But equally consider implications of the backend doing it all the time. Maybe some form of a database and time to live on the records...
- What you want to see on the frontend page? Is it enough to just print out the same data as the two example sites do (AIRLINE, FLIGHTNUMBER, DUE, FROM, STATUS) or something else?
Similar information to what the original webpages display. How pretty... That's up to you.
- What does the "local display purposes" exactly mean?
Not for serving to to third parties via an API. Only to be displayed in the test application.

## Implementation

- Does the frontend can be a Desktop APP or only a Web?
Web
- Is it okay to do it in Blazor or any preferred technologies?
You can use blazor or JavaScript or typescript 
- Does the  https://www.heathrow.com/arrivals and https://www.london-luton.co.uk/flights sites has an API that i have to use or any documentation that i can use to get the data, or a service i can use for that?
This is part of the task to find out but no third party flight APIs.
- You want me to store the data or just query it everytime (like a gateway)?
Up to you but you'll need to explain the reasoning for your choice.
- What version of dotnet should I use? (i would prefer 6.0)
No preference whichever version you like.

## Testing
- What type of tests are required if any? (Frontend tests, unit tests, end-to-end tests, integration tests, mutation tets, load tests...)
Up to you. Keep in mind this is a test task and you should not spend a week writing tests. Choose whatever you feel comfortable with and then explain why and how...

## Deployment
- Could I Use a docker?


Requirement

What do you want to see on the frontend page? Is it enough to just print out the same data as the two example sites do (AIRLINE, FLIGHTNUMBER, DUE, FROM, STATUS) or something else?





Implementation
Does the frontend can be a Desktop APP or only a Web?
Web
Is it okay to do it in Blazor or any preferred technologies?


Does the  https://www.heathrow.com/arrivals and https://www.london-luton.co.uk/flights sites have an API that i have to use or any documentation that i can use to get the data, or a service i can use for that?

Do you want me to store the data or just query it everytime (like a gateway)?

What version of dotnet should I use? (i would prefer 6.0)


Testing
What type of tests are required if any? (Frontend tests, unit tests, end-to-end tests, integration tests, mutation tets, load tests...)
