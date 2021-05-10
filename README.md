## Repository for Westpac Practical Assignment - Senior Automation QE (David Kim)


<br /><br />
## Overview
This test automation project has been created for site [BuggyCars](https://buggy.justtestit.org/).\
The goal is to automate the top 5 critical features that were identified during exploratory testing.
- Register User
- Login/Logout
- Votes in Model Details page
- Profile
- Navigation bar
They were prioritiesd based on the follwing factors.
- that has highs risks and importance
- that has has major bugs but frequently used

<br /><br />
## Project Overview
This is a .NET Framework 4.8 solution which consists of 3 different projects.

Project | Description
------------ | -------------
TestAutomation.Framework.Common | Contains models, extensions, configs and utilities to be used for a number of tests including UI and API tests
TestAutomation.Framework.UI | Contains models, extensions, configs and utilities to be used to support UI tests
TestAutomation.Tests.UI | Contains models, pages, tests and configs that are bound to business logic 


<br /><br />
## Tools/frameworks used
- C# (.NET Framework 8)
- Specflow
- Selenium
- ExtentReport

<br /><br />
## Pre-requisites
- **Chrome (Version 90.0.4430.93)**
- **Visual Studio Community 2019 with .NET desktop development and Framework 4.8 Development tools**
<img width="700" alt="1  Pre-requisites - VisualStudio .NET desktop development" src="https://user-images.githubusercontent.com/7684205/117662480-b6625900-b1f3-11eb-9334-9293e43abbae.png">

- **Specflow for Visual Studio 2019 (Version 2019.0.83.55623)**
<img width="700" alt="2  Pre-requisites - Specflow extension for Visual Studio 2019" src="https://user-images.githubusercontent.com/7684205/117662592-d7c34500-b1f3-11eb-99d9-f5e1d541df3e.png">



<br /><br />
## UI tests
### Test status

Tests | Pass | Fail |Total
------------ | -------------| ------------- | -------------
UI tests | 20 | 2| 22

There are 2 failing tests due to existing bugs.
1. Incorrect field name is included in the validation error message for Last Name field in Register page. It is denoted as First Name in the error message.
2. Brand link on top left doesn't work in Make page.

<br /><br />
## Instructions for UI test execution
1. Download solution from [GitHub repository](https://github.com/dkay719/Westpac.TestAutomation.DavidKim)
2. Open it with Visual Studio 2019 Community
3. Build solution
4. Open Test Explorer from Test menu in Visual Studio
<img width="700" src="https://user-images.githubusercontent.com/7684205/117666614-283ca180-b1f8-11eb-84d4-65f83d05580b.png">
5. Select the test suite and Click run button (If test suite is not displayed, then restart VisualStudio. Clean solution & repeat from step 2)
<img width="700" src="https://user-images.githubusercontent.com/7684205/117666712-42767f80-b1f8-11eb-9a87-8635223da7d1.png">


<br /><br />
## Test results
1. HTML test result file should be generated in the project folder /TestAutomation.Tests.UI/TestResults
<img width="700" src="https://user-images.githubusercontent.com/7684205/117669480-098bda00-b1fb-11eb-8c33-84369c9f1754.png">
2. Open the html result file to see the test results. You can click a feature title in the left pane to see the scenarios in the right pane. 
Clicking a scenario will open steps. Every scenario will have screenshot attached to its last step. It can be opened by clicking base64-img.
<img width="700" src="https://user-images.githubusercontent.com/7684205/117670071-ac445880-b1fb-11eb-900d-8faec8488ba8.png">

 
<br /><br />
## Testing strategy
Dynamic and heuristic testing approach was used as exploratory testing suits the best in this situation where execution and evaluation of tests can be done at the same time based on the following facts:
1. Lack of requirements of the application
2. The application has already been completed
3. Purpose of the testing is to explore the application and find critical bugs


<br /><br />
## Bug reports

Field Name | Value
:--: | :---
**Bug Id** | 1
**Title** | Brand link in Make page does not redirect user to Home page.
**Description** | Brand link has in Make page has incorrect href value of '/broken'
**Steps to reproduce** | 1. Navigate to Make page by clicking Popular Make in Homepage<br /> 2. Click Brand link on top left
**Expected result**|User should be redirected to Home page.
**Actual result**|User stays on the same page.
**Screenshot**|<img width="700" src="https://user-images.githubusercontent.com/7684205/117673430-e8c58380-b1fe-11eb-9338-db281d533c28.png">

<br /><br />
Field Name | Value
:--: | :---
**Bug Id** | 2
**Title** | Navigation to a specfic model page breaks the application
**Description** | Exception is thrown in console when navigate to the model page of. <br/>Browser refresh on another page is required as a temporary fix.
**Steps to reproduce** | 1. Navigate to Overall page <br/> 2. Select the following model (Brand: **Lancia** Model: **Stratos**)
**Expected result**|Model details should be displayed.
**Actual result**|Model Details page is empty. <br/> Console error is displayed.<br/>Every other page has become empty when navigated.
**Screenshot**|<img width="700" src="https://user-images.githubusercontent.com/7684205/117675480-b61c8a80-b200-11eb-87c1-88dffb136af3.png"><br/><img width="700" src="https://user-images.githubusercontent.com/7684205/117675536-c3397980-b200-11eb-9dfe-89067e429761.png">

## Minor bugs
1. Saved user credential is populated in Register page
2. Very long comment breaks the width of the page
3. Navigation between pages after clicking Votes filter shuffles items in Make page
4. Clicking image in Model Details page navigates to home
5. Pagination text field doesn't work for the first page and last page (they are 1 and 5 for now).
6. Twitter link broken in Overall page
7. Table filters are not consistent as per table below<br/>

Page | Filter Bugs
------------ | -------------
Make page | Model doesn't work at all<br/>Rank doesn't work at all<br/>Vote filter works incorrectly
Overall page | Make filter only supports order by ascending only<br/>Model filter only supports order by ascending only<br/>Rank filter only supports order by ascending only but it is in an incorrect order as the Rank values are treated as text values instead of numbers.<br/>Votes filter only supports order by descending only<br/>Model filter only supports order by ascending only

