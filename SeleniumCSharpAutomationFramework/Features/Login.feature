Feature: Login Functionality

	Scenario Outline: Login validation
		Given user is on the login page
		When user logs in with username "<username>" and password "<password>"
		Then login should be "<result>"

	Examples:
	| username        | password       | result   |
	| standard_user   | secret_sauce   | success  |
	| locked_out_user | secret_sauce   | failure  |
	| fail_user	      | secret_sauce   | success  |
