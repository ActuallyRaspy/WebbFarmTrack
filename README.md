# FarmTrack
To run the program you need to have a local databased named "FarmTrackDB"
To create it follow these steps:
1. Open the command line with admin rights.
2. Paste in this: sqllocaldb create "FarmTrackDB"

To access the server using something like SSMS, use these settings:
Server type: Database engine
Server name: (localDb)\FarmTrackDB
Authentication: Windows Authentication
  
To remove the server when you're done checking out our project, follow these steps:
1. Open the command line with admin rights.
2. Paste this: sqllocaldb delete "FarmTrackDB"
