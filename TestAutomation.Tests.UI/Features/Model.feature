@Model
Feature: Model
	Validate features in Model page

Background: Login as admin user
	Given I have registered a new Admin user
	And I am logged in as Admin user

Scenario: Vote with a comment
	Given I am on Model Details page
	When I vote for a model with comment
	Then I should see the thanks message
	And I should see the total vote count is increased
	And I should see the new comment is added

Scenario: Vote without a comment
	Given I am on Model Details page
	And I haven't voted for a model yet 
	When I vote for a model without comment
	Then I should see the thanks message
	And I should see the total vote count is increased
	And I should see the new comment is not added