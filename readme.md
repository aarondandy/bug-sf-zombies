# Reproduction of a bug where single fire reminders rise from the dead to eat non-idempotent brains.

1. Open and build the .sln
2. Launch the application under a debugger in a cluster with multiple nodes
3. Wait for the application to be read
4. Switch the Output tab in Visual Studio to "Debug"
5. Run the DontForget.exe program to create a reminders
6. Observe that the reminder is created and handled one time
7. Optional: Wait for a period of time
8. Find the primary node for the actor that handleed the reminder (the only partition)
9. Deactivate or restart the primary node
10. Observe that the reminder is handled again
11. Go to step 7