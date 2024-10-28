# Smokeball Project

## How to run the Frontend

* Ensure you have Node installed (Version 20.18.0)
* Ensure you have Yarn installed (Version 1.22.22)

Using your terminal of choice, navigate to the smokeball-ui folder and run the following commands:

`yarn install`

`yarn run build`

`yarn run preview`

The terminal will display the local address to go to in your browser to view the frontend

## How to run the Backend

* Ensure you have the .NET CLI installed
* Using your cmdline of choice, navigate to the SmokeballAPI folder and run dotnet run


## Improvements

If I had more time to work on this project, I would have added the following:

* Proper frontend validation. Currently all field validation is done on the backend.
* Better Css styling/fixes.
* Better frontend state management using react redux or similar.
* Search result caching. The slowest part of this application is waiting for the search results to come back from google/bing. This experience could be improved by introducing memory caching, so that we only need to pull data from the search engines once per hour or day.
