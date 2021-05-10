@Register
Feature: Register
	Validate user registration feature

Background: User is already on Register page
	Given I am on Register page

Scenario: Error messages should be displayed for empty fields
	When I enter an Invalid user details in Register page
	And I remove the user details in Register page
	Then Register button should be Disabled
	And I should see an error message Login is required
	And I should see an error message First Name is required
	And I should see an error message Password is required
	And I should see an error message Passwords do not match
	And I should see an error message Last Name is required

Scenario: Register button becomes enbaled when all details are provided
	When I enter a Valid user details in Register page
	Then Register button should be Enabled

Scenario: User cannot be registered if Confirm Password is different
	When I enter a Valid user details in Register page
	And I enter an extra letter in Confirm Password field
	Then I should see an error message Passwords do not match
	And Register button should be Disabled

Scenario: Cancel link redirects user to home page
	When I enter a Valid user details in Register page
	And I click Cancel button
	Then I should be redirected to home page

Scenario: Successful user registration
	When I enter a Valid user details in Register page
	And I click Register button
	Then I should see the Registration successful message
	Then I should be able to login with newly created user credential