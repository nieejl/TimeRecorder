# TimeRecorder 
This application is a simple time recording tool, that saves the data locally, and has the possibility of backing it up on a server.

The vision is to have this application be a part of a larger application, that performs performance measurements, for software development projects, supporting multiple users collaborating.

The goal is to have a range of tools for development, eg. kanban board, burndown charts, story point estimation, and task distribution.
In the future, I hope to integrate with some video conference software - Zoom could be a valid candidate.
## Frameworks
The applcation uses:
* WPF
* Entity Framework Core
* ASP.NET Core

For tests:

* XUnit,
* Moq
## Components
The GUI is made in WPF, using the MVVM pattern.
GUI -> VM -> Controller

The local data-storage is implemented with Entity Framework and SQLite.

The server consists of a WEB API made with ASP.NET Core. It connects to an Entity Framework database, currently implemented in SQLite.

## Todo (short term)
* Buttons for swapping between Online and offline data-store. (temporary, till synchronisation is implemented)
* Refactor Time-recording features out of the viewmodel.
* Refactor WPF view-components into UserControls.
* Create OnWindowMovedHandler forcing location of Detail Window to 
* Retrieve any non-finished timer on start.
* Hide EndDate/Time field from detail-view when timer is still running
* Implement synchronisation of data.
* Improve the searchbox for Projects.
* Unit Testing:
    * What's missing in repositories (eg. mock savechanges),
    * Server-repository
    * Try out AutoFixture
    * 

## Todo Long term
* Expand database, so application works for more than a single user.
* Seperate as much of the view models as possible, to allow for easier implementation of view on other platforms.
* Implement web front end.
* Fix opening and closing of detail view on picking different 