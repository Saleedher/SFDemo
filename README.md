# SFDemo
Service Fabric With OWIN implementing a File Upload API
Creating an Azure Service Fabric Stateless Service with File Upload API by using OWIN
--------------------------------------------------------------------------------------

Developers who are working with IIS Hosted Web APIs are very much addicted to the legacy way of using HttpContext. But when we design something related to Microservice or implementing Targeted deployment we should stay away from IIS hosting and instead, we have to adopt OWIN Self-hosting. No more HttpContext or HttpContext.Current !! Yes, you don't require even an IIS express instance to deploy or run an API. Interesting right??

Let's discuss something about Microservice in detail - Azure Service Fabric is a distributed systems platform that makes it easy to package, deploy, and manage scalable and reliable microservices and containers. Service Fabric also addresses the significant challenges in developing and managing cloud-native applications. I would recommend everyone to go to the link https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-overview to get a better understanding of Azure Service fabrics

Following are the challenges with Traditional IIS Strategy to implement Microservice
 Manually creating new sites when new API’s are introduced.
Maintaining multiple instances of these websites one each for each project
Poor in handling failover scenario.
Need multiple deployments for replications of the same package.
Dependency on Infrastructure
We can see that we will be addressing each of these above-mentioned challenges by introducing Microsoft Azure Service Fabric SDK and Visual Studio 2017 with Azure Services enabled in it.

Let's follow a step by step approach

We need to have Visual Studio 2017 Community / Developer edition. You must open VS in Administrator Mode to work with Service Fabric Project.
We must have installed Service Fabric SDK3.0 or above in our machine to have the Service Fabric Local Cluster available. Also, we need to have Window Powershell Tools available

Open New Project (Ctrl+Shift+N) - Create a new Service Fabric Project. I will get a list of Project Templates as shown in the image
I named it as "HelloWorldStateless". When I click OK there will be 2 projects available in the solution explorer. One is a Publisher Project & another is a Stateless Service
We don't do any Coding activities in the Publisher projects as it is mentioned to be used for configuring the publish & deployment activities of the microservice.
