#### F# TDD
#####System that is untestable is detestable
    Unit tests help remove fear of making changes to existing code
#####Test-Induced Damage
    complexity due to excessive indirection
##### Loosely Couple code requires dependency injection
	- introduce a lot of interfaces
	- implement a lot of interfaces
##### Unit tests require a lot of mocking
	- lots of code
    - lines of code and defects correlated
#####Stand-ins for code
	Stubs
		- won't break tests
		- queries (returns data)
		- no side-effects
	Mocks
		- strong opinion on how system and test should interact
		- brittle unit tests
		- commands (no data returned)
		- side-effects

In F# if structures have same values then structures are equal, no reference equality checks

#####Isolation
	- well designed function has no implicit knowledge of outside world
	- everything it knows must be passed to it

#####Encapsulation 
	- internal state of object not implicitly known to outside world

#####Few Mocks
#####Few interfaces
#####Function composition