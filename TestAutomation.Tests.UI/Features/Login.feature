@Login
Feature: Login
	Validate login feature

Background:
	Given I am on Home page

Scenario Outline: Error message should be displayed for a empty field
	When I login with Username <Username> and Password <Password>
	Then I should see a validation error message for <Error message field> field

	Examples:
		| Username  | Password  | Error message field |
		| abcdefghi |           | Password            |
		|           | 123456789 | Username            |
		|           |           | Username            |

Scenario Outline: Error message should be displayed for invalid credential
	When I login with Username <Username> and Password <Password>
	Then I should see an error message <Error Message>

	Examples:
		| Username  | Password  | Error Message             |
		| abcdefghi | 123456789 | Invalid username/password |

Scenario: Succesful login with valid user credential
	When I login as Admin user
	Then I should be able to see a greeting message on Page

Scenario: Successful logout
	Given I am logged in as Admin user
	When I click logout link
	Then I should be loggeed out