# InfloUserManager

This is an application to manage all the Users in a company.
The front end is a Blazor WASM application with MudBlazor for functionality and the back end is an ASP.Net Core WebAPI.

# Setting Up

You will need to publish the InfloUserManagerDatabase project in order to run this application, setting up your own server and connection details. The database should be called 'InfloUsers'

# Use

The application will run as-is. The User table has not been seeded in order for the assessor of this application to use the Create User functionality. Add two or more users with different statuses to be able to
use the sorting functionality (each column header on the Users index page is clickable to allow sorting) and use the filtering dropdown list to change the view to All / Active / Inactive as desired.

# Tests

The tests are in two different categories: Controller Tests and Handler Tests.

Additionally, Scalar has been implemented to enable testing of the API.

# ToDo

Pull the statuses in the Index page from the database rather than hard coding them.

Refactor the WebAPI project to use the CQRS pattern - and do the same with the tests.

Add a Department table and foreign key this into the User table. Add Interfaces, Handlers, Controllers and UI as necessary.

Ditto an Address table.

Add the functionality to be able to assign a 'Manager' to each user.

Etc, etc...!

# Other Resources

See my ManufacturerManagerWebAPI repo for another (slightly more involved) example of my WebAPI applications.

See my ManufacturerManager application for GitHub Actions, Playwright tests and more.

# And Finally...

This solution uses a SQL Database project as that was the fastest way to be able to create the database. I am happy to expand on why I have
done it this way - I have a couple of reasons.

I didn't have time to create Playwright tests for tis application, nor to set up GitHub Actions (but please see 'Other Resources' for details of where I have done this).

If there are any aspects of this that you would like to discuss I can be reached on *07790 209152*.

Regards

Julian Aburrow
