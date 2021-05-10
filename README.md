# Repository for Westpac Practical Assignment - Senior Automation QE


## Overview
This test automation project has been created for site [BuggyCars](https://buggy.justtestit.org/).
The purpose was to automate the following top 5 critical features that were identified during exploratory testing.
- Register User
- Login/Logout
- Voting in model page
- Profile
- Navigation bar

They were prioritiesd based on the follwing factors.
1. that has highs risks and importance
2. that has has major bugs but frequently used

## Project Overview
This is a .NET Framework 4.8 solution which consists of 3 different projects for test automation.

Project | Description
------------ | -------------
TestAutomation.Framework.Common | Contains models, extensions, configs and utilities to be used for a number of tests including UI and API tests
TestAutomation.Framework.UI | Contains models, extensions, configs and utilities to be used to support UI tests
TestAutomation.Tests.UI | Contains models, pages, tests and configs that are bound to business logic 

### Pre-requites
- Visual Studio 2019 Community
- .NET Framework 4.8 Runtime
- Specflow for Visual Studio 2019 (Version 2019.0.83.55623)
- Chrome (Version 90.0.4430.93)
- Nuget packages
