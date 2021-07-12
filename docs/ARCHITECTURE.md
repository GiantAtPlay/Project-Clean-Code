# Solution architecture
The solution shares many of the principles described by the [onion architecture](https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/). 
This is often referred to as the [clean architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).
The application is built using the concept of having many layers (like an onion) each layer with its own 
responsibilities. One of the fundamental ideas behind this architecture is that you can remove/replace outer 
layers without affecting any layer that sits below it. Below if have outlined the different layers this solution
implements along with a guide on what each is responsible for. The architecture also informally draws some
ideas from DDD and managing your domain rules and objects centrally in the application. These objects form
the basis for the entire application.

## Layers defined 
```
(Inside) Layer 0 -> .... Layer 5 (Outside)
```
### Layer 0: Domain
**Projects:** Application.Domain

The very center of the onion. This layer houses solution level models, interfaces and rules. Only 
add things here that are not application specific. This project has ZERO dependencies on any other project.
### Layer 1: Core (Business/Application)
**Projects:** Application.Core

Business logic and application specific logic belongs here. This is where most logic for the application belongs. 
It is advised to break up the files by feature, either using folders in smaller applications or separate projects 
if deemed necessary. 
### Layer 3: Persistence
**Projects:** Application.Data, Application.FileSystem

The layer responsible for saving and retrieving data from storage. This can include, for example,  databases, 
file systems or external API's.
### Layer 3: Presentation
**Projects:** Application.WWW, Application.API

Entry points for the application. This may contain just a single web application or many applications such as 
console apps, api's as well. **Each of them should contain very little, ideally zero, business logic.** These 
applications should be light weight and be responsible for making requests to the rest of the application and 
outputting the results.
### Layer 4: Infrastructure
**Projects:** Application.Hangfire, Application.Email, Application.AntiVirus

Implementations of application services. Each of these features will generally implement an interface/contract 
from the domain layer. Less often the contract may come from the core layer. This layer is responsible for 
supporting or carrying out application functions. **No business logic should be written here.**
### Layer 5: Umbrella
**Projects:** Application.IoC

The only project that does not strictly conform to the typical onion architecture. The project is responsible 
for a single duty, wiring up dependency injection centrally for the entire application. It will contain references 
too every, non presentation, project. Presentation projects will reference this project to obtain the configuration for IoC.
## Project dependency rules
Projects cannot reference any other project located in an outer layer in the architecture than its self.

The domain project, as it is the inner most project should have no project dependencies.

The exception to the first two rules is entry points of the application can reference the outer most layer Application.IoC. 
This should be done only to setup Inversion of Control/Dependency injection for the application.

Typically an entry point to the application would be a website or API or less commonly a console application.

Test projects can be considered part of the infrastructure layer and sit on the outside of the application. They should not be referenced by any application project.

## Project structure
```
root
|   README.MD
|   Application.sln
|
|---docs
|       README.MD
|       README.MD
|
|---src
|   |
|   |---Core
|   |       Application.Domain
|   |       Application.Core
|   |
|   |---Infrastructure
|   |       Application.DatabaseUpdate
|   |       Application.Email
|   |       Application.Hangfire
|   |       Application.IoC
|   |
|   |---Persistence
|   |       Application.Data
|   |       Application.FileSystem
|   |
|   |---Presentation
|           Application.API
|           Application.WWW
|
|---ui
|   |---src
|   |       styles
|   |       vue
|   |       js
|   |
|   |---static
|           images
|           fonts
|
|---tests
|       Application.Domain.UnitTests
|       Application.Core.UnitTests
|       Application.Data.IntegrationTests
|
```