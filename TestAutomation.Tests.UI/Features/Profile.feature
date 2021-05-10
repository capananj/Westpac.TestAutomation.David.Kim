@Profile
Feature: User Profile
	Validate user profile feature

Background: New Admin User is created
	Given I have registered a new Admin user
	And I am logged in as Admin user
	

Scenario Outline: Error messages should be displayed for empty first name or last name
	Given I am on Profile page
	When I remove <Field Name> value in Profile page
	Then I should see an error message <Error Message>
	And Save button should be Disabled

	Examples:
		| Field Name | Error Message          |
		| First Name | First Name is required |
		| Last Name  | Last Name is required  |

Scenario: Successful profile update
	Given I am on Home page
	And I am on Profile page
	When I update user profile
	Then I should see the Profile Update successful message
	And I should see that the user details are updated