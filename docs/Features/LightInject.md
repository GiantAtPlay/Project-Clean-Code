# Light Inject
```
Search for the tag [LI] to help find related areas of the project
```
Because the default logging built into .net Core has its limitations I have chosen to use Light inject for managing IoC.
This may not be required in simpler applications where Hangfire/property injection are not required.

### Setup .Net Core application
```
https://github.com/seesharper/LightInject.Microsoft.Hosting
```
1. Install nuget packages
   
    ![alt text](../Resources/LightInjectNugetPackages.PNG)
2. Configure Light Inject & Composition Root in program.cs
3. Manage IoC in Application.IoC/CompositionRoot.cs