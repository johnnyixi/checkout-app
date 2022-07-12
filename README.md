# checkout-app

Introduction:

This app is a simple "proof of concept"" Checkout app designed to work with .Net 6 and EF6. 
It is in no way a production ready app as it misses a lot of "must have"" functionalities (tracing, retry, authentication, monitoring. distributed and concurrent approach, etc.).


## Startup:

The app can use both an InMemory DB provider or it can work with an actual SQL Server (mainly (localdb)\\MSSQLLocalDB).

### I. For an easy startup, the InMemory option is enabled by default.

### II. If you want to use the MSSQLLocalDB:

1. In appsettings.json change the value of CheckoutDBContextOptions.UseInMemoryDb to false

2. Make sure you have MSSQLLocalDB installed
https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16

3. Make sure that MSSQLLocalDB is started

4. Open the Package Manager Console in Visual Studio

5. Select the CheckoutApp.DataAccess project

6. Run Update-Database


## Observations:
- Due to the lack of time, not all unit tests have been added
- Idempotency is added just to reflect that it is an option, and not exactly on the best method.
- A lot more validations should be added in the Business layer
- As stated in the introduction, some of the functionalities that I would add are:
	- tracing (Request/Response logging)
	- authentication (based on JWT)
	- service monitoring (healthchecks)
	- use a distributed and concurrent approach (Kafka)
- And others