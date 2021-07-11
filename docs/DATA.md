# Dealing with data
```
Search for the tag [DATA] to help find related areas of the project
```

**The idea of this document and project is to provide ideas and examples of how this can be done. NOT to be a set of rules that 
need to be followed on every project, as always pick the best way to manage your data based on the size, scope of the project 
and team preferences.**

The most important concept when dealing with data is that the persistence layer sits on the outside of the onion architecture.

The second important concern is picking the correct tool for the task instead of feeling you must only use a single library/tool
for data management. You may choose to use Entity Framework for Create, Update, Delete (CUD) operations, whilst using Dapper as
the ORM of choice for querying the database. You may also need an entirely different tool for a particular query due to complexity
or the nature of what is required. Its important to note that it is often better to keep things simple until you have a particular 
need to introduce extra complexity.

This project follows these naming conventions:
- Classes named Repository are read only query handlers that retrieve data from the data store.
- Classes named Manager are concerned with Create, Update, Delete (CUD) operations.
- Classes name Service sit in between caller and the Manager/Repository.

This project follows these high level rules:
- Repositories/Managers are only ever concerned with one top level domain model.
- Services may interface with multiple data stores through repositories/managers.
- Services are not always required.
- Repositories/Managers can be extended with specific queries related to their domain model.
- Data project is separated by top level domain model, rather than class type.
- The Entity framework database context used is a database first approach as migrations are handled by DbUp.
- Entity Framework Managers use "Attach" to avoid doing a database lookup prior to updating the datastore.


