Feature: Pau_Details
	In order to verify that the PAU files were loaded correctly
	As a verification
	I want to run tests on the Excel files and loaded table

@AcceptanceTest
Scenario: The loaded PAU table has the same number of total records as Excel files
	Given the total number of records is calculated for all files in a directory
	Given the total number of records queried in the database for table 'HSCRC_Insight.Fact.PAU_Details'
	Then the number of records in the database matches the number in the files

@AcceptanceTest
Scenario: The loaded PAU table has the same number of IP records as Excel files
	Given the total number of IP records is calculated for all files in a directory
	Given the total number of IP records queried in the database for table 'HSCRC_Insight.Fact.PAU_Details'
	Then the number of records in the database matches the number in the files

@AcceptanceTest
Scenario: The loaded PAU table has the same number of OP records as Excel files
	Given the total number of OP records is calculated for all files in a directory
	Given the total number of OP records queried in the database for table 'HSCRC_Insight.Fact.PAU_Details'
	Then the number of records in the database matches the number in the files