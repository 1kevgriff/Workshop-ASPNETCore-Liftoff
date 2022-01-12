# Lab: Health Checks

> Recommended lab time: 30 minutes

As has been discussed in the workshop, Health Checks are a major feature of ASP.NET Core that allows you to quickly get an "at-a-glance" view of how your current applications is running.

## Setup

In the Whisky.API repository, open the "06_HealthChecks" solution.  

This project contains a basic implementation of Health Checks for our Azure Queues.

> Azurite (VS 2022 - As ADMIN): C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\Extensions\Microsoft\Azure Storage Emulator\azurite.exe
> 
> Azurite Docker: docker run --rm --name azurite -p 10000:10000 -p 10001:10001 -p 10002:10002 -d -v c:/azurite:/data mcr.microsoft.com/azure-storage/azurite


## Your Job

Take a few minutes and work on implementing a health check or two that does the following:

* Checks to see if the whisky.csv file exists
* Checks tos ee if the notifications.json file exists

## Need help?

Reference the "06_PostLab_HealthChecks" solution for the finished version of this lab.
