The following is work undertaken to learn C# through use of Mark Price's C#12 and .NET 8 Modern Cross-Platform Development Fundamentals.  
# Exercise 12
12.1 - Test your knowledge

## What was the name of Microsoft's first dynamic server-side-executed web page technology, and why is it still useful to know this history today?
Active Server Pages is the technologies name. It is still used in some legacy applications and is an important ancestor to modern tools such as Blazor.
Its ability to easily integrate with databases is one of the substantial and lasting contributions it made to modern web development.
## What are the names of two Microsoft web servers?
Internet Information Services and Kestrel. 
## What are some of the differences between a microservice and a nanoservice?
A nanoservice is a service that is utilized only for one specific task, whereas a microservice is a service used for a set of like tasks.
You can think of a nanoservice as an individual function, whereas a microservice might be thought of as more of a focused class library.
Additionally, unlike microservices, a nanoservice is usually inactive until called, rather than always on. This reduces resource consumption.
## What is Blazor?
Blazor is used to handle client side events and in place of JavaScript. It lets a C# developer continue using C# to build a user experience
in the browser. 
## What was the first version of ASP.NET Core that could not be hosted on .NET Framework?
ASP.NET Core 3.0 was the first version that could not be hosted on the older .NET Framework. All future versions would be hosted on .NET Core.
## What is a user agent?
A user agent is the client.
## What impact does the HTTP Request-response communcation model have on web developers?
It creates a strict delineation between the server and client. You need to implement your own ways of managing state, HTTP is stateless.
## Name and describe four components of a URL.

Scheme: http:/ Clear or https:/ Encrypted
Domain: MyDomain.com
Port Number: When in production, 80 for http: and 443 for https. These ports are inferred from the scheme. During development, other port numbers  can be used.

Also, a cool table of the dimensions of this project:

| Name    | Ports     | Description    |
| ------- | ------------ | ------- |
| *Northwind.Common* | **na** | `A class library for common types like interfaces, enums, classes, records and structs, used across multiple projects.` |



Path: relative path to resource 
Query string: A way to pass parameter values, for example, ?country=Germany&searchtext=shoes. Note the use of ? for query
Fragment: This is a reference to an element id on the page #toc
## What capabilities does Developer Tools give you? 
DevTools can allow you to edit the css and html of a web page in real time.
You can see the response and requests exchanged between you and the server
App panel to see storage and data usage
Debug in Javascript
## What are the three main client-side web development technologies and what do they do?
CSS - Style style style
HTML - Present the web page over HTTP
JavaScript - Handle client side interactions

