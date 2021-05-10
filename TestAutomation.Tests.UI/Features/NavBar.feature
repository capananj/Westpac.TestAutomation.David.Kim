@NavBar
Feature: NavBar
	Verify that NavBar works as expected from different pages

Scenario: User is redirected to Home page when brand link is clicked from Register page
	Given I am not logged in
	And I am on Register page
	When I click brand link
	Then I should be redirected to Home page

Scenario Outline: User is redirected to Home page when brand link is clicked
	Given I have registered a new Admin user
	And I am logged in as Admin user
	And I am on <Page Name> page
	When I click brand link
	Then I should be redirected to Home page

	Examples:
		| Page Name     |
		| Home          |
		| Profile       |
		| Overall       |
		| Model Details |
		| Make          |