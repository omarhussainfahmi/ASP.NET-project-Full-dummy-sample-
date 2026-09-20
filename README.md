firstASPNET - ASP.NET Core MVC Solution
A layered ASP.NET Core MVC application for managing People, Countries, and Products with JWT authentication, custom filters, and comprehensive exception handling.

Solution Structure
Projects
CRUDTests - Unit tests for CRUD operations

Entities - Domain models (Person, Country, ApplicationDbContext)

Exceptions - Custom exceptions (InvalidPersonIdException)

firstASPNET - Main web application

Repositories - Data access layer (PersonRepository)

RepositoryContracts - Repository interfaces (ICountriesRepository, IPersonsRepository)

ServiceContracts - Service interfaces, enums, and request/response models

Services - Business logic layer

ServiceContracts1 - Additional service contracts and DTOs

Main Application (firstASPNET)
Controllers
CountriesService - Country operations

HomeController - Home pages

PersonsController - Person CRUD operations

ProductsController - Product operations

Filters
ActionFilters: PersonCreateAndEditPostActionFilter, PersonsListActionFilter, ResponseHeaderActionFilter

AuthorizationFilter: TokenAuthorizationFilter

ExceptionFilters: HandleExceptionFilter

ResourceFilters: FeatureDisabledResourceFilter

ResultFilters: PersonAlwaysRunResultFilter, PersonsListResultFilter

SkipFilter

Middleware
ExceptionHandlingMiddleware

Models
Book, ListModel, Person, PersonGridModel

ViewComponents
GridViewComponent

Views
Home: _ViewImports, _ViewStart, About, Contact, Index, Index1

Persons: Create, PersonsPDF

Products: Order, Search

Shared: _Layout, _ListPartialView, _MasterLayout, All

Components/Grid: Default.cshtml

wwwroot
Rotativa (wkhtmltopdf.exe)

StyleSheet.css

Configuration
launchSettings.json

appsettings.json, appsettings.Development.json

MyOwnConfig.json

Program.cs

Connected Services & Properties
Supporting Projects
ServiceContracts
Enums: GenderOptions

Interfaces: ICitiesService, ICitiesServiceBase, IPersonAddRequest, IPersonResponse

Services: CitiesService, CountriesService

ServiceContracts1
DTO: AuthenticationResponse, CountryAddRequest, CountryResponse, PersonAddRequest, PersonResponse, PersonUpdateRequest

ServiceContracts: ApplicationUser, IJwtService

Services: JwtService

Interfaces: ICountriesService

Services
_db.cs, CitiesService, CountriesService, JwtService, IPersonService, PersonService

Repositories
PersonRepository

RepositoryContracts
ICountriesRepository, IPersonsRepository

Features
ASP.NET Core MVC with Razor views

Layered architecture (Controllers → Services → Repositories)

JWT-based authentication

Custom action, authorization, exception, resource, result, and skip filters

Global exception handling middleware

PDF generation via Rotativa

Grid ViewComponent for reusable tables

Entity Framework Core with ApplicationDbContext

Unit testing project (CRUDTests)

Multiple configuration files for environment-specific settings
