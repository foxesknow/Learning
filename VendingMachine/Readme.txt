The vending machine solutions presented here use a greedy algorithm approach
to finding the correct change for an item. There is an implementation of a 
regular greedy algorithm, which as shown in the code and tests will fail under
certain scenerios even when there is the correct change in the machine.

There is also a recursive backtracking implementation of the greedy algorithm
which has worse performance but will find a valid solution if there is a correct
change combination in the machine.

To demonstrate the machines working I have created an MSTest project which shows the
machines running under various scenerios, in addition to having tests for the various
classes that make up the project.