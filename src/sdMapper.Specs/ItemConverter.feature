Feature: Converting Thin item
	In order to convert an item to an object based on a Map
	I want all the mapped properties to be populated from the item's fields


Background: 
	Given the following items
	|Name	|Title	|Body	|NumberOfViews	|Image|
	|Item1	|Blue whale	|A book about the whales in the pacific	|500||

Scenario: Automatically setting name property if it exists
	Given I have item with name Item1
	And I have BookMap without a Mapping for Name
	When I Convert the item
	Then the resulting book entity's Name property is set to Item1

Scenario: Setting string propreties
    Given I have item with name Item1
	And I have BookMap with Mapping for Title
	When I Convert the item
	Then the resulting book entity's Title proprety is "Blue whale"
