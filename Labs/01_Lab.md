# Lab: Designing for Speed

> Recommended Lab Time: 60 minutes

## Setup

This lab will use the "Whisky API".  The base version of this repository can be downloaded from https://github.com/1kevgriff/Whisky.API.

## Overview of the Whisky API
The Whisky API is a basic implementation of an API that'll provide information and ratings for a variety of Whiskys.

> Note: Hold all your judgement of this code.  It's a demo!

The Whisky API exposes several endpoints:

**GET /whisky** - get all the whisky in the database

**GET /whisky/:id** - get information / ratings for a particular whisky by it's ID
  
**GET /whisky/regions** - get all the regions where we have whisky.

**POST /whisky** - create a new whisky in the database.

**POST /whisky/:id/ratings** - add a rating to a whisky.

### Set up your environment

* Pull the Git repo from https://github.com/1kevgriff/Whisky.API
* Go into the folder `/01_Start`
* Run the command `dotnet restore`
* Run the command `dotnet run`

### Mailhog

This demo assumes you have access to a SMTP server.  Odds are you don't - and we really don't want you to send real emails.

Mailhog is an open-source SMTP server that captures all SMTP traffic for you in one easy to use interface.

You can quickly set up Mailhog using Docker:

```dotnetcli
docker run --rm --name mailhog -p 1025:1025 -p 8025:8025 -d mailhog/mailhog
```

SMTP Port: 1025
Web Portal:  http://localhost:8025

## Your Job:  Notification System Revamp

There are some... problems with Whisky API.  Let's skip past the obvious issue that the database is stored using .csv files and a whole directory full of .json files.  

There is a huge problem (sometimes) with adding new Whisky or ratings.  We have a huge database of people that want notifications for these changes!  

That shouldn't be a problem - except we email all the folks at the moment a new whisky is adding OR if a rating is added.  This severely impacts the response time of the API because all the emails need to be sent before the API can return.  

It's not important that emails are sent immediately.  

Your first task is to update our notification process to send these emails asynchronously.  Use some of the concepts we've talked about in this workshop.
