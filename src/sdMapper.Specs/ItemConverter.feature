Feature: Converting Thin item
	In order to convert an item to an object based on a Map
	I want all the mapped properties to be populated from the item's fields


Background: 
	Given the following items
	|Name	|Title	|Body	|NumberOfViews	|
	|Item1	|Blue whale	|A book about the whales in the pacific	|500|
	|Item2	|The dark forest	| A book about the woods	|100|
	Given the following map
	|Property | Field |
	|Title	| Title|
	|Body	| Body |
	|Views	| NumberOfViews	|
	Given the following conventions
	|Prorerty	| Field	|
	|Name	| Name |

Scenario: Automatically setting name property if it exists
	Given I have item with name Item1
	When I Convert the item
	Then the resulting book entity's Title property is set to "Blue whale"
	And Body set to "A book about the whales in the pacific"
	And View set to "500"

Scenario: Setting string propreties
    Given I have item with name Item1
	When I Convert the item
	Then the resulting book entity's Title proprety is "Blue whale"
