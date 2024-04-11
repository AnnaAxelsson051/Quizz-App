# Ultimate Quizz App

## UI

![User interface](Quizz-app-user-interface.png)

## Description

This is a full-stack quiz application built with Blazor and ASP.NET Core where users can register, log in and create or answer quizzes as well as share them with others. Quizzes can be created with a variable number of questions and each question can, along with text content, include image or video content and have multiple-choice or free-text answer format. Quiz-creators can set time limits for answering questions so that quiz-takers scores are effected based on how fast questions are answered. It is also possible for quiz-creators to see who has answered their quizzes and their respective scores. 


## Technologies used

 - ASP.NET Core
 - C#
 - Blazor 
 - JWT (Authorization)
 - Entity Framework Core
 - SQL 
 - Bootstrap

## Architecture

The app is abstracted into Controller, IService and Service-layers which compartmentalizes responsibilities and enhances maintainability of code. Data is transmitted from the server to the Razor client via shared view models, which makes communication effective and prevents exposure of data. Client side logic is implemented using Blazor and the app has a user-friendly interface that offers intuitive navigation. The database schema is implemented using Entity framework core with migrations and code first.

