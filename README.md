# Bridge.AcademyDevices.BatteryMonitoring

# Preamble

* Solution is developed in Asp.Net Core framework.
* Swashbuckle Swagger is been used for API documentation.
* Current projects included are the Main Service API, Business Logic where core compuation should reside. Contract of Buisness Logic. Unittests of the same.
* Solution isn't complete FYI. I have followed TDD right from the begining, spent about 8 hours, did what i could do. I felt and was tempted to fallback to traditional way of approaching problem to completing it.It was extremely hard,and i Chose not to do it.
* I took it as a challenge to implement what i can, by following Kent Beck "Don't write a production code, without a failing test".
* Started with small increments and a bigger one came at the end.


# Future Scope

* Currently we have a json with full data.
* System should be in a position to handle the "Delta Data" which is generally preferrable. Instead of a full load, we might get a notification from device and
  compute average reading on the word go.
* We can implement Authentication and Auth policies for our API resource to consume.  
* A service layer to consume the various dependent services.
* Importantly we don't want to compute the already computed readings, so might have to store the Avgerage computed for a device with previously recorded readings.
* Assumption that we might not recieve the same reading which was already used for Average computation in future.
* Logging capabilities of API.
* Global Exception handling of API.

# Tests

* Only Two tests i could write, but i'm proud, i have written production code only so much for that tests.
